using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Validation.Implementation;

public delegate IEnumerable<ValidationFailure> ValidatorExecutor<T, TProperty>(
    T value, RuleDelegate<TProperty> rule, string? message = null);

public sealed class ValidationRuleBuilder<T, TProperty>(
    Action<Func<T, IEnumerable<ValidationFailure>>> registerRule,
    ValidatorExecutor<T, TProperty> executor)
{
    /// <summary>
    /// Adds a validation rule to the builder.
    /// </summary>
    /// <param name="rule">Rule delegate to execute during validation.</param>
    /// <param name="message">Optional message used when the rule fails.</param>
    /// <returns>The current <see cref="ValidationRuleBuilder{T, TProperty}"/> instance.</returns>
    internal ValidationRuleBuilder<T, TProperty> Add(RuleDelegate<TProperty> rule, string? message = null)
    {
        registerRule(value => executor(value, rule, message));
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
                executor(value, property => compose(property, rule), message));

        return new ValidationRuleBuilder<T, TNextProperty>(registerRule, newExecutor);
    }
}