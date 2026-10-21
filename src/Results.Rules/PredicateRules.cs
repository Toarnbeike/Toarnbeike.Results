using Toarnbeike.Results.Rules.Evaluations.Predicates;

namespace Toarnbeike.Results.Rules;

public static class PredicateRules
{
    /// <summary>
    /// Verifies that the property value satisfies the specified predicate.
    /// </summary>
    /// <typeparam name="TProperty">The type of the value to evaluate.</typeparam>
    /// <param name="predicate">The predicate to evaluate the value against.</param>
    /// <returns>A <see cref="RuleDelegate{TProperty}"/> that evaluates the predicate.</returns>
    public static RuleDelegate<TProperty> Satisfies<TProperty>(Func<TProperty, bool> predicate) => value =>
        {
            ArgumentNullException.ThrowIfNull(predicate);
            return RuleResult.Create("Predicate.Satisfies", value, new SatisfiesEvaluator<TProperty>(predicate));
        };
    
    /// <summary>
    /// Verifies that the property value does not satisfy the specified predicate.\\\\
    /// </summary>
    /// <typeparam name="TProperty">The type of the value to evaluate.</typeparam>
    /// <param name="predicate">The predicate to evaluate the value against.</param>
    /// <returns>A <see cref="RuleDelegate{TProperty}"/> that evaluates the predicate.</returns>
    public static RuleDelegate<TProperty> NotSatisfies<TProperty>(Func<TProperty, bool> predicate) => value =>
        {
            ArgumentNullException.ThrowIfNull(predicate);
            return RuleResult.Create("Predicate.NotSatisfies", value, new NotSatisfiesEvaluator<TProperty>(predicate));
        };
    
    /// <summary>
    /// Verifies that the property value satisfies the specified asynchronous predicate.
    /// </summary>
    /// <typeparam name="TProperty">The type of the value to evaluate.</typeparam>
    /// <param name="predicate">The asynchronous predicate to evaluate the value against.</param>
    /// <returns>A <see cref="AsyncRuleDelegate{TProperty}"/> that evaluates the asynchronous predicate.</returns>
    public static AsyncRuleDelegate<TProperty> SatisfiesAsync<TProperty>(Func<TProperty, Task<bool>> predicate) => value =>
        {
            ArgumentNullException.ThrowIfNull(predicate);
            return RuleResult.CreateAsync("Predicate.Satisfies", value, new SatisfiesAsyncEvaluator<TProperty>(predicate));
        };
    
    /// <summary>
    /// Verifies that the property value does not satisfy the specified asynchronous predicate.
    /// </summary>
    /// <typeparam name="TProperty">The type of the value to evaluate.</typeparam>
    /// <param name="predicate">The asynchronous predicate to evaluate the value against.</param>
    /// <returns>A <see cref="AsyncRuleDelegate{TProperty}"/> that evaluates the asynchronous predicate.</returns>
    public static AsyncRuleDelegate<TProperty> NotSatisfiesAsync<TProperty>(Func<TProperty, Task<bool>> predicate) => value =>
        {
            ArgumentNullException.ThrowIfNull(predicate);
            return RuleResult.CreateAsync("Predicate.NotSatisfies", value, new NotSatisfiesAsyncEvaluator<TProperty>(predicate));
        };
}
