using Toarnbeike.Results.Rules.Evaluations.Objects;

namespace Toarnbeike.Results.Rules.TypeRules;

public static class ObjectRules
{
    /// <summary>
    /// Verifies that a property is equal to the specified expected property.
    /// </summary>
    /// <typeparam name="TProperty">The type of the value to evaluate.</typeparam>
    /// <param name="expected">The value to evaluate the value against.</param>
    /// <returns>A <see cref="RuleDelegate{TProperty}"/> that evaluates the predicate.</returns>
    public static RuleDelegate<TProperty> EqualTo<TProperty>(TProperty expected) => value =>
        RuleResult.Create("Object.EqualTo", value, new EqualityEvaluator<TProperty>(expected));

    /// <summary>
    /// Verifies that a property is unequal to the specified expected property.
    /// </summary>
    /// <typeparam name="TProperty">The type of the value to evaluate.</typeparam>
    /// <param name="expected">The value to evaluate the value against.</param>
    /// <returns>A <see cref="RuleDelegate{TProperty}"/> that evaluates the predicate.</returns>
    public static RuleDelegate<TProperty> NotEqualTo<TProperty>(TProperty expected) => value =>
        RuleResult.Create("Object.NotEqualTo", value, new NotEqualityEvaluator<TProperty>(expected));

    /// <summary>
    /// Verifies that the property value satisfies the specified predicate.
    /// </summary>
    /// <typeparam name="TProperty">The type of the value to evaluate.</typeparam>
    /// <param name="predicate">The predicate to evaluate the value against.</param>
    /// <returns>A <see cref="RuleDelegate{TProperty}"/> that evaluates the predicate.</returns>
    public static RuleDelegate<TProperty> Satisfies<TProperty>(Func<TProperty, bool> predicate) => value =>
        {
            ArgumentNullException.ThrowIfNull(predicate);
            return RuleResult.Create("Object.Satisfies", value, new SatisfiesEvaluator<TProperty>(predicate));
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
            return RuleResult.Create("Object.NotSatisfies", value, new NotSatisfiesEvaluator<TProperty>(predicate));
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
            return RuleResult.CreateAsync("Object.Satisfies", value, new SatisfiesAsyncEvaluator<TProperty>(predicate));
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
            return RuleResult.CreateAsync("Object.NotSatisfies", value, new NotSatisfiesAsyncEvaluator<TProperty>(predicate));
        };
}
