using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Rules;
using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Validation.Implementation;

public delegate Result ValidationExecutor<T, out TProperty>(T value, RuleDelegate<TProperty> rule, string? message = null);

public sealed class ValidationTarget<T, TProperty>(Result<T> result, ValidationExecutor<T, TProperty> executor)
    : IRuleTarget<T, TProperty>
{
    /// <summary>
    /// Applies the specified rule to the property of the target.
    /// </summary>
    /// <param name="rule">The rule to apply.</param>
    /// <param name="message">An optional message.</param>
    /// <returns>The result of applying the rule.</returns>
    public Result<T> Apply(RuleDelegate<TProperty> rule, string? message = null) =>
        result.BindTap(value => executor(value, rule, message));

    /// <summary>
    /// Creates a new validation target that composes the current property validation pipeline with validation rules for
    /// a next property type.
    /// </summary>
    /// <typeparam name="TNextProperty">The type of the next property validated by the composed target.</typeparam>
    /// <param name="compose">A function that combines the current property value and a next-property rule delegate into a rule result.</param>
    /// <returns>A <see cref="ValidationTarget{T, TNextProperty}"/> that uses the composed executor to run validation for the
    /// next property type.</returns>
    public IRuleTarget<T, TNextProperty> Compose<TNextProperty>(
        Func<TProperty, RuleDelegate<TNextProperty>, RuleResult> compose)
    {
        var newExecutor = new ValidationExecutor<T, TNextProperty>(
            (value, rule, message) =>
                    executor(value, property => compose(property, rule), message));

        return new ValidationTarget<T, TNextProperty>(result, newExecutor);
    }
}