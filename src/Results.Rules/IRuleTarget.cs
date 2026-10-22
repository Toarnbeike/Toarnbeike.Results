using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Rules;

/// <summary>
/// Represents a target for applying rules to a property and producing a result for the owning type.
/// </summary>
/// <remarks>Supports property-focused rule evaluation while preserving the root object type in the result.</remarks>
/// <typeparam name="T">The type of the object associated with the rule target.</typeparam>
/// <typeparam name="TProperty">The type of the property value evaluated by rules.</typeparam>
public interface IRuleTarget<T, TProperty>
{
    /// <summary>
    /// Applies a rule and returns the resulting <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="rule">Rule delegate to execute.</param>
    /// <param name="message">Optional message to include in the result.</param>
    /// <returns>A <c>Result&lt;T&gt;</c> representing the outcome of applying the rule.</returns>
    Result<T> Apply(RuleDelegate<TProperty> rule, string? message = null);

    /// <summary>
    /// Composes a rule pipeline step from the current property type to a next property type.
    /// </summary>
    /// <remarks>
    /// Enables chaining rules across different property types by bridging the current property value
    /// to the next rule delegate.
    /// </remarks>
    /// <typeparam name="TNextProperty">The type of the next property in the composed rule pipeline.</typeparam>
    /// <param name="compose">A function that receives the current property value and the next-property rule delegate, then returns the composed rule result.</param>
    /// <returns>A rule target for the composed next property type.</returns>
    IRuleTarget<T, TNextProperty> Compose<TNextProperty>
        (Func<TProperty, RuleDelegate<TNextProperty>, RuleResult> compose);
}
