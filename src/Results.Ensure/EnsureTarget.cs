using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Ensure;

public delegate Result EnsureExecutor<T, out TProperty>(T value, RuleDelegate<TProperty> rule, string? message = null);

public sealed class EnsureTarget<T, TProperty>(Result<T> result, EnsureExecutor<T, TProperty> executor)
{
    /// <summary>
    /// Applies the specified rule to the property of the target.
    /// </summary>
    /// <param name="rule">The rule to apply.</param>
    /// <param name="message">An optional message.</param>
    /// <returns>The result of applying the rule.</returns>
    internal Result<T> Apply(RuleDelegate<TProperty> rule, string? message = null) =>
        result.BindTap(value => executor(value, rule, message));

    /// <summary>
    /// Composes a new EnsureTarget with a different property type by applying the provided compose function to the current property.
    /// </summary>
    /// <typeparam name="TNextProperty">The type of the next property.</typeparam>
    /// <param name="compose">A function to compose the current property with the next property.</param>
    /// <returns>A new EnsureTarget with the composed property type.</returns>
    internal EnsureTarget<T, TNextProperty> Compose<TNextProperty>(
        Func<TProperty, RuleDelegate<TNextProperty>, RuleResult> compose)
    {
        var newExecutor = new EnsureExecutor<T, TNextProperty>(
            (value, rule, message) =>
                executor(value, property => compose(property, rule), message));

        return new EnsureTarget<T, TNextProperty>(result, newExecutor);
    }
}