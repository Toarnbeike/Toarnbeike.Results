using System.Globalization;
using System.Linq.Expressions;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Rules.EvaluationData;
using Toarnbeike.Results.Rules.PredefinedRules.DateTimeRules;
using Toarnbeike.Results.Rules.PredefinedRules.StringRules;
using Toarnbeike.Results.Rules.Validators;

namespace Toarnbeike.Results.Rules;

/// <summary>
/// A rule node is fully configured rule, optionally enriched with a custom failure message and or a custom condition description. 
/// </summary>
public interface IRuleNode
{
    /// <summary>
    /// Weakly typed validation method for validating a value against the rule contained in this node.
    /// </summary>
    /// <param name="valueObject">The object to validate</param>
    /// <param name="validationContext">The used configured context for the validation.</param>
    /// <param name="messageContext">The context for the validation message.</param>
    /// <returns>A weakly typed result of the validation, either a success result with the next value or a failure result with a <see cref="ValidationFailure"/></returns>
    Result<object?> Validate(object? valueObject, ValidationContext validationContext, ValidationMessageContext messageContext);

    /// <summary>
    /// Get the condition message for this specific rule node, which describes the condition that must be met for the rule to pass.
    /// </summary>
    /// <param name="culture">The culture used for localization.</param>
    /// <returns>A string containing the condition message.</returns>
    string GetConditionMessage(CultureInfo culture);
}

/// <inheritdoc/>
public sealed record RuleNode<TInput, TOutput, TEvaluationData>(
    IRule<TInput, TOutput, TEvaluationData> Rule,
    Func<TInput, TEvaluationData, ValidationMessageContext, string>? CustomMessage,
    Func<string>? CustomCondition) : IRuleNode
    where TEvaluationData : IRuleEvaluationData
    where TOutput : notnull
{
    /// <inheritdoc/>
    public Result<object?> Validate(object? valueObject, ValidationContext validationContext, ValidationMessageContext messageContext)
    {
        return valueObject is TInput value
            ? Validate(value, validationContext, messageContext)
            : throw new ArgumentException($"Expected value of type {typeof(TInput).Name}, but got {valueObject?.GetType().Name ?? "null"}.");
    }

    /// <summary>
    /// Strongly typed implementation of the Validate method.
    /// </summary>
    /// <param name="value">The value to validate</param>
    /// <param name="validationContext">The used configured context for the validation.</param>
    /// <param name="messageContext">The context for the validation message.</param>
    /// <returns>A strongly typed result of the validation, either a success result with the next value or a failure result with a <see cref="ValidationFailure"/></returns>
    private Result<TOutput> Validate(TInput value, ValidationContext validationContext, ValidationMessageContext messageContext)
    {
        var ruleResult = Rule.Validate(value, validationContext);
        if (ruleResult.IsValid)
        {
            return Result.Success(ruleResult.Output);
        }

        var message =
            CustomMessage?.Invoke(value, ruleResult.EvaluationData, messageContext)
            ?? Rule.GetFailureMessage(value, ruleResult.EvaluationData, messageContext);
        return new ValidationFailure(messageContext.PropertyName, message);
    }

    /// <inheritdoc/>
    public string GetConditionMessage(CultureInfo culture)
    {
        return CustomCondition?.Invoke() ?? Rule.GetConditionMessage(culture);
    }
}

/// <summary>
/// Chain of rules applied on a member of the provided model
/// </summary>
/// <typeparam name="TModel">The type of the model that is validated.</typeparam>
public interface IRuleChain<in TModel>
{
    /// <summary>
    /// The name of the property that is validated by this rule chain.
    /// This is used for error reporting and message generation.
    /// </summary>
    string PropertyName { get; }

    /// <summary>
    /// Validate the entire chain, by providing the model to validate, the validation context and the message context.
    /// </summary>
    /// <param name="model">The model to validate. Implementations of this interface are responsible for extracting the relevant property from the model.</param>
    /// <param name="validationContext">The validation context.</param>
    /// <param name="culture">The culture for localization.</param>
    /// <returns>The validation result.</returns>
    Result Validate(TModel model, ValidationContext validationContext, CultureInfo culture);

    /// <summary>
    /// Add a rule to the chain. This is used during the building stage of the validation registration. The rule is added to the end of the chain.
    /// </summary>
    /// <param name="node">The rule node to add.</param>
    void AddRule(IRuleNode node);

    /// <summary>
    /// Get the condition message for the entire chain, which describes the conditions that must be met for all rules in the chain to pass.
    /// </summary>
    /// <param name="culture">The culture for localization.</param>
    /// <returns>The condition messages for the specified property.</returns>
    IEnumerable<string> GetConditionMessages(CultureInfo culture);
}

internal class PropertyRuleChain<TModel, TProperty>(Expression<Func<TModel, TProperty>> propertySelector, string propertyName) : IRuleChain<TModel>
{
    public string PropertyName { get; } = propertyName;
    private readonly List<IRuleNode> _ruleNodes = [];

    public Result Validate(TModel model, ValidationContext validationContext, CultureInfo culture)
    {
        object? currentValue = propertySelector.Compile().Invoke(model);
        var messageContext = new ValidationMessageContext(PropertyName, culture);

        foreach (var rule in _ruleNodes)
        {
            var result = rule.Validate(currentValue, validationContext, messageContext);

            // Check if valid and either return failure or update currentValue and continue loop.
            if (!result.TryGetValue(out currentValue))
            {
                return result;
            }
        }

        return Result.Success();
    }

    public void AddRule(IRuleNode node)
    {
        _ruleNodes.Add(node);
    }

    public IEnumerable<string> GetConditionMessages(CultureInfo culture)
    {
        return _ruleNodes.Select(node => node.GetConditionMessage(culture));
    }
}

internal class CollectionRuleChain<TModel, TElement>(Expression<Func<TModel, IEnumerable<TElement>>> collectionSelector, string propertyName) : IRuleChain<TModel>
{
    public string PropertyName { get; } = propertyName;

    private readonly List<IRuleNode> _ruleNodes = [];
    public Result Validate(TModel model, ValidationContext validationContext, CultureInfo culture)
    {
        var failureCollection = new List<ValidationFailure>();
        var collection = collectionSelector.Compile().Invoke(model).ToList();
        var messageContext = new ValidationMessageContext(PropertyName, culture);

        for (var index = 0; index < collection.Count; index++)
        {
            var result = ValidateElement(collection[index], validationContext, messageContext.WithIndex(index));
            if (result.TryGetFailure(out var failure))
            {
                if (failure is not ValidationFailure validationFailure)
                {
                    throw new InvalidOperationException("Unexpected failure type encountered.");
                }
                failureCollection.Add(validationFailure);
            }
            index++;
        }

        return failureCollection.Count == 0
            ? Result.Success()
            : new ValidationFailureSummary(failureCollection);
    }

    public void AddRule(IRuleNode node)
    {
        _ruleNodes.Add(node);
    }

    public IEnumerable<string> GetConditionMessages(CultureInfo culture)
    {
        return _ruleNodes.Select(node => node.GetConditionMessage(culture));
    }

    private Result ValidateElement(object? element, ValidationContext validationContext, ValidationMessageContext messageContext)
    {
        foreach (var rule in _ruleNodes)
        {
            var result = rule.Validate(element, validationContext, messageContext);
            if (!result.TryGetValue(out element))
            {
                return result;
            }
        }
        return Result.Success();
    }
}

public record PropertyRuleBuilder<TModel, TCurrent>(IRuleChain<TModel> Chain)
{
    public PropertyRuleBuilder<TModel, TNext> AddNode<TNext, TEvaluationData>(RuleNode<TCurrent, TNext, TEvaluationData> rule)
        where TEvaluationData : IRuleEvaluationData
        where TNext : notnull
    {
        Chain.AddRule(rule);
        return new PropertyRuleBuilder<TModel, TNext>(Chain);
    }
}

public record CollectionRuleBuilder<TModel, TElement>(IRuleChain<TModel> Chain)
{
    public CollectionRuleBuilder<TModel, TNext> AddNode<TNext, TEvaluationData>(RuleNode<TElement, TNext, TEvaluationData> rule)
        where TEvaluationData : IRuleEvaluationData
        where TNext : notnull
    {
        Chain.AddRule(rule);
        return new CollectionRuleBuilder<TModel, TNext>(Chain);
    }
}

public abstract class Validator<T> : IValidator<T>
{
    private readonly List<IRuleChain<T>> _registrations = [];

    private readonly ValidationContext _validationContext = ValidationContext.Default;
    private readonly CultureInfo _culture = CultureInfo.InvariantCulture;

    protected PropertyRuleBuilder<T, TProperty> That<TProperty>(
        Expression<Func<T, TProperty>> selector)
    {
        var propertyName = GetPropertyName(selector);
        return That(selector, propertyName);
    }

    protected PropertyRuleBuilder<T, TProperty> That<TProperty>(
        Expression<Func<T, TProperty>> selector, string propertyName)
    {
        var chain = new PropertyRuleChain<T, TProperty>(selector, propertyName);
        _registrations.Add(chain);
        return new PropertyRuleBuilder<T, TProperty>(chain);
    }

    protected CollectionRuleBuilder<T, TElement> ThatEach<TElement>(
        Expression<Func<T, IEnumerable<TElement>>> selector)
    {
        var propertyName = GetPropertyName(selector);
        return ThatEach(selector, propertyName);
    }

    protected CollectionRuleBuilder<T, TElement> ThatEach<TElement>(
        Expression<Func<T, IEnumerable<TElement>>> selector, string propertyName)
    {
        var chain = new CollectionRuleChain<T, TElement>(selector, propertyName);
        _registrations.Add(chain);
        return new CollectionRuleBuilder<T, TElement>(chain);
    }

    public Result<T> Validate(T value)
    {
        var failures = new List<ValidationFailure>();

        var results = _registrations.Select(chain => chain.Validate(value, _validationContext, _culture));
        foreach (var result  in results)
        {
            failures.AddRange(GetFailuresFromResult(result));
        }

        return failures.Count == 0
            ? value
            : new ValidationFailureSummary(failures);
    }

    public Dictionary<string, string[]> GetConditions(CultureInfo culture)
    {
        return _registrations.Select(reg => new KeyValuePair<string, string[]>(
                key: reg.PropertyName, 
                value: reg.GetConditionMessages(culture).ToArray()))
            .ToDictionary();
    }

    private static string GetPropertyName<TModel, TProperty>(Expression<Func<TModel, TProperty>> selector)
    {
        // todo: improve support for nested members, e.g. x => x.Address.Street
        if (selector.Body is MemberExpression member)
        {
            return member.Member.Name;
        }

        throw new ArgumentException("Selector must be a property access.", nameof(selector));
    }

    private static IEnumerable<ValidationFailure> GetFailuresFromResult(Result result)
    {
        result.TryGetFailure(out var failure);
        return failure switch
        {
            null => [],
            ValidationFailure validationFailure => [validationFailure],
            ValidationFailureSummary validationSummary => validationSummary.InnerFailures,
            _ => throw new InvalidOperationException("Unexpected failure type.")
        };
    }
}

public static class PropertyRuleBuilderExtensions
{
    public static PropertyRuleBuilder<TModel, string> MinLength<TModel>
    (this PropertyRuleBuilder<TModel, string> builder,
        int minimum,
        Func<string, EmptyRuleEvaluationData, ValidationMessageContext, string>? customMessage = null,
        Func<string>? customCondition = null)
    {
        builder.AddNode(new RuleNode<string, string, EmptyRuleEvaluationData>(
            Rule: new MinLengthRule(minimum),
            CustomMessage: customMessage,
            CustomCondition: customCondition));

        return builder;
    }

    public static PropertyRuleBuilder<TModel, string> NotEmpty<TModel>(
        this PropertyRuleBuilder<TModel, string?> builder,
        Func<string?, EmptyRuleEvaluationData, ValidationMessageContext, string>? customMessage = null,
        Func<string>? customCondition = null)
    {
        builder.AddNode(new RuleNode<string?, string, EmptyRuleEvaluationData>(
            Rule: new NotEmptyRule(),
            CustomMessage: customMessage,
            CustomCondition: customCondition));

        return new PropertyRuleBuilder<TModel, string>(builder.Chain);
    }

    public static PropertyRuleBuilder<TModel, DateTime> WithinPast<TModel>
    (this PropertyRuleBuilder<TModel, DateTime> builder,
        TimeSpan maxDuration,
        Func<DateTime, UtcNowEvaluationData, ValidationMessageContext, string>? customMessage = null,
        Func<string>? customCondition = null)
    {
        builder.AddNode(new RuleNode<DateTime,DateTime, UtcNowEvaluationData>(
            Rule: new WithinPastRule(maxDuration),
            CustomMessage: customMessage,
            CustomCondition: customCondition));

        return builder;
    }
}

public static class CollectionRuleBuilderExtensions
{
    public static CollectionRuleBuilder<TModel, string> MinLength<TModel>
    (this CollectionRuleBuilder<TModel, string> builder,
        int minimum,
        Func<string, EmptyRuleEvaluationData, ValidationMessageContext, string>? customMessage = null,
        Func<string>? customCondition = null)
    {
        builder.AddNode(new RuleNode<string, string, EmptyRuleEvaluationData>(
            Rule: new MinLengthRule(minimum),
            CustomMessage: customMessage,
            CustomCondition: customCondition));

        return builder;
    }

    public static CollectionRuleBuilder<TModel, string> NotEmpty<TModel>(
        this CollectionRuleBuilder<TModel, string?> builder,
        Func<string?, EmptyRuleEvaluationData, ValidationMessageContext, string>? customMessage = null,
        Func<string>? customCondition = null)
    {
        builder.AddNode(new RuleNode<string?, string, EmptyRuleEvaluationData>(
            Rule: new NotEmptyRule(),
            CustomMessage: customMessage,
            CustomCondition: customCondition));

        return new CollectionRuleBuilder<TModel, string>(builder.Chain);
    }
}

//internal interface IChainOwner
//{
//    ValidationChain Chain { get; }
//}

//internal sealed class PropertyRegistration<TModel, TProperty>(
//    Expression<Func<TModel, TProperty>> selector,
//    string propertyName)
//    : IValidationRegistration<TModel>, IChainOwner
//{
//    public Expression<Func<TModel, TProperty>> Selector { get; } = selector;

//    public string PropertyName { get; } = propertyName;

//    public ValidationChain Chain { get; } = new();


//    public IEnumerable<ValidationFailure> Validate(TModel instance, ValidationContext validationContext, CultureInfo culture)
//    {
//        var value = Selector.Compile().Invoke(instance);
//        var messageContext = new ValidationMessageContext(PropertyName, culture);

//        return Chain.Nodes.SelectMany(node => node.Validate(value!, validationContext, messageContext));
//    }

//    public KeyValuePair<string, string[]> GetConditions(CultureInfo culture)
//    {
//        return new KeyValuePair<string, string[]>(
//            PropertyName, Chain.Nodes.Select(node => node.GetConditionMessage(culture)).ToArray());
//    }
//}

//internal sealed class CollectionPropertyRegistration<TModel, TElement>(
//    Expression<Func<TModel, IEnumerable<TElement>>> selector,
//    string propertyName)
//    : IValidationRegistration<TModel>, IChainOwner
//{
//    public Expression<Func<TModel, IEnumerable<TElement>>> Selector { get; } = selector;
//    public string PropertyName { get; } = propertyName;
//    public ValidationChain Chain { get; } = new();

//    public IEnumerable<ValidationFailure> Validate(TModel instance, ValidationContext validationContext, CultureInfo culture)
//    {
//        var values = Selector.Compile().Invoke(instance).ToList();

//        for (var i = 0; i < values.Count; i++)
//        {
//            var propertyName = PropertyName + $"[{i}]";
//            var messageContext = new ValidationMessageContext(propertyName, culture);

//            foreach (var node in Chain.Nodes)
//            {
//                foreach (var failure in node.Validate(values[i]!, validationContext, messageContext))
//                {
//                    yield return failure;
//                }
//            }
//        }
//    }

//    public KeyValuePair<string, string[]> GetConditions(CultureInfo culture)
//    {
//        return new KeyValuePair<string, string[]>(
//            PropertyName, Chain.Nodes.Select(node => node.GetConditionMessage(culture)).ToArray());
//    }
//}

//internal sealed class ValidationChain
//{
//    private readonly List<IRuleNode> _nodes = [];

//    public IReadOnlyList<IRuleNode> Nodes => _nodes;

//    internal void Add(IRuleNode node)
//    {
//        _nodes.Add(node);
//    }
//}

//public interface IValidationRegistration<TInput>;

//internal record PropertyRuleRegistration<TInput, TOutput>
//    (string Property, IValidationNode<TInput, TOutput> ValidationNode) 
//    : IValidationRegistration<TOutput>;

//internal record PropertyValidatorRegistration<T>(string Property, IValidator<T> Validator) 
//    : IValidationRegistration<T>;

//internal interface IRuleRegistration<in TProperty>
//{
//    IEnumerable<ValidationFailure> Validate(
//        TProperty value,
//        string propertyName,
//        ValidationContext context,
//        ValidationMessageContext messageContext);

//    string GetConditionMessage(CultureInfo culture);
//}

//internal sealed class RuleRegistration<TProperty, TEvaluationData>(IRule<TProperty, TEvaluationData> rule) : IRuleRegistration<TProperty>
//    where TEvaluationData : IRuleEvaluationData
//{
//    public IEnumerable<ValidationFailure> Validate(TProperty value, string propertyName, ValidationContext context, ValidationMessageContext messageContext)
//    {
//        var ruleResult = rule.Validate(value, context);
//        if (!ruleResult.IsValid)
//        {
//            yield return new ValidationFailure(propertyName, rule.GetFailureMessage(value, ruleResult.EvaluationData, messageContext));
//        }
//    }

//    public string GetConditionMessage(CultureInfo culture) => rule.GetConditionMessage(culture);
//}

//internal sealed class RefiningRuleRegistration<TInput, TOutput, TEvaluationData>(IRefiningRule<TInput, TOutput, TEvaluationData> rule) : IRuleRegistration<TInput>
//    where TEvaluationData : IRuleEvaluationData
//{
//    public IEnumerable<ValidationFailure> Validate(TInput value, string propertyName, ValidationContext context, ValidationMessageContext messageContext)
//    {
//        var ruleResult = rule.Validate(value, context);
//        if (!ruleResult.IsValid)
//        {
//            yield return new ValidationFailure(propertyName, rule.GetFailureMessage(value, ruleResult.EvaluationData, messageContext));
//        }
//    }

//    public string GetConditionMessage(CultureInfo culture) => rule.GetConditionMessage(culture);
//}