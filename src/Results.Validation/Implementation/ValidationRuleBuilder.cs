using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Validation.Implementation;

public delegate IEnumerable<ValidationFailure> ValidatorExecutor<T, TProperty>(
    T value, RuleDelegate<TProperty> rule, string? message = null);

public sealed class ValidationRuleBuilder<T, TProperty>
{
    private readonly Action<Func<T, IEnumerable<ValidationFailure>>> _registerRule;
    private readonly ValidatorExecutor<T, TProperty> _executor;
    private readonly Func<T, TProperty>? _propertySelector;
    private readonly Action<IValidator<TProperty>>? _addValidator;

    public ValidationRuleBuilder(
        Action<Func<T, IEnumerable<ValidationFailure>>> registerRule,
        ValidatorExecutor<T, TProperty> executor)
        : this(registerRule, executor, null, null)
    {
    }

    internal ValidationRuleBuilder(
        Action<Func<T, IEnumerable<ValidationFailure>>> registerRule,
        ValidatorExecutor<T, TProperty> executor,
        Func<T, TProperty>? propertySelector,
        string? propertyName,
        Action<IValidator<TProperty>>? addValidator = null)
    {
        _registerRule = registerRule;
        _executor = executor;
        _propertySelector = propertySelector;
        PropertyName = propertyName;
        _addValidator = addValidator;
    }

    internal string? PropertyName { get; }

    internal TProperty Select(T value) =>
        _propertySelector is null
            ? throw new InvalidOperationException("This builder does not have a property selector.")
            : _propertySelector(value);

    internal void RegisterRule(Func<T, IEnumerable<ValidationFailure>> rule) => _registerRule(rule);

    internal void AddValidator(IValidator<TProperty> validator)
    {
        ArgumentNullException.ThrowIfNull(validator);
        (_addValidator ?? throw new InvalidOperationException("This builder does not support nested validators."))(validator);
    }

    internal ValidationRuleBuilder<T, TNextProperty> Create<TNextProperty>(
        ValidatorExecutor<T, TNextProperty> executor,
        Action<IValidator<TNextProperty>>? addValidator = null) =>
        new(_registerRule, executor, null, PropertyName, addValidator);

    /// <summary>
    /// Adds a validation rule to the builder.
    /// </summary>
    /// <param name="rule">Rule delegate to execute during validation.</param>
    /// <param name="message">Optional message used when the rule fails.</param>
    /// <returns>The current <see cref="ValidationRuleBuilder{T, TProperty}"/> instance.</returns>
    internal ValidationRuleBuilder<T, TProperty> Add(RuleDelegate<TProperty> rule, string? message = null)
    {
        _registerRule(value => _executor(value, rule, message));
        return this;
    }

    /// <summary>
    /// Creates a validation rule builder for a subsequent property type by composing the current rule pipeline with the
    /// specified delegate.
    /// </summary>
    /// <remarks>Preserves the existing rule registration flow while adapting execution to the next property
    /// type.</remarks>
    /// <typeparam name="TNextProperty">The type of the next property targeted by the composed rule.</typeparam>
    /// <param name="compose">Combines the current property value and the next rule delegate into a rule result.</param>
    /// <returns>A <see cref="ValidationRuleBuilder{T, TNextProperty}"/> that uses the composed delegate during rule execution.</returns>
    internal ValidationRuleBuilder<T, TNextProperty> Compose<TNextProperty>(
        Func<TProperty, RuleDelegate<TNextProperty>, RuleResult> compose)
    {
        var newExecutor = new ValidatorExecutor<T, TNextProperty>(
            (value, rule, message) =>
                _executor(value, property => compose(property, rule), message));

        return Create(newExecutor);
    }
}