using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Rules;

public delegate Result RuleExecutor<T, out TProperty>(T value, RuleDelegate<TProperty> rule, string? message = null);
public delegate Task<Result> AsyncRuleExecutor<T, out TProperty>(T value, AsyncRuleDelegate<TProperty> rule, string? message = null);

/// <summary>
/// Represents a target for applying rules to a property and producing a result for the owning type.
/// </summary>
/// <remarks>Supports property-focused rule evaluation while preserving the root object type in the result.</remarks>
/// <typeparam name="T">The type of the object associated with the rule target.</typeparam>
/// <typeparam name="TProperty">The type of the property value evaluated by rules.</typeparam>
public sealed class RuleTarget<T, TProperty>(
    Result<T> result,
    RuleExecutor<T, TProperty> executor,
    AsyncRuleExecutor<T, TProperty> asyncExecutor)
{
    /// <summary>
    /// Applies a rule and returns the resulting <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="rule">Rule delegate to execute.</param>
    /// <param name="message">Optional message to include in the result.</param>
    /// <returns>A <c>Result&lt;T&gt;</c> representing the outcome of applying the rule.</returns>
    public Result<T> Apply(RuleDelegate<TProperty> rule, string? message = null) =>
        result.BindTap(value => executor(value, rule, message));

    /// <summary>
    /// Applies an asynchronous rule and returns the resulting <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="rule">Asynchronous rule delegate to execute.</param>
    /// <param name="message">Optional message to include in the result.</param>
    /// <returns>A task representing the outcome of applying the asynchronous rule.</returns>
    public Task<Result<T>> ApplyAsync(AsyncRuleDelegate<TProperty> rule, string? message = null)
    {
        ArgumentNullException.ThrowIfNull(rule);
        return result.BindTapAsync(value => asyncExecutor(value, rule, message));
    }

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
    public RuleTarget<T, TNextProperty> Compose<TNextProperty>(
        Func<TProperty, RuleDelegate<TNextProperty>, RuleResult> compose,
        Func<TProperty, AsyncRuleDelegate<TNextProperty>, Task<RuleResult>> asyncCompose)
    {
        ArgumentNullException.ThrowIfNull(compose);
        ArgumentNullException.ThrowIfNull(asyncCompose);

        var newExecutor = new RuleExecutor<T, TNextProperty>(
            (value, rule, message) =>
                executor(value, property => compose(property, rule), message));

        async Task<Result> newAsyncExecutor(T value, AsyncRuleDelegate<TNextProperty> rule, string? message = null) =>
                await asyncExecutor(value, property => asyncCompose(property, rule), message).ConfigureAwait(false);

        return new RuleTarget<T, TNextProperty>(result, newExecutor, newAsyncExecutor);
    }
}
