using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class ObjectExtensions
{
    extension<T, TProperty>(ValidationRuleBuilder<T, TProperty> builder)
    {
        /// <summary>
        /// Validates that the property is equal to the specified expected value.
        /// </summary>
        /// <param name="expected">The value to compare against the target.</param>
        /// <param name="message">An optional message to include in the result.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TProperty> EqualTo(TProperty expected, string? message = null) =>
            builder.Add(ObjectRules.EqualTo(expected), message);

        /// <summary>
        /// Validates that the property is not equal to the specified expected value.
        /// </summary>
        /// <param name="expected">The value to compare against the target.</param>
        /// <param name="message">An optional message to include in the result.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TProperty> NotEqualTo(TProperty expected, string? message = null) =>
            builder.Add(ObjectRules.NotEqualTo(expected), message);

        /// <summary>
        /// Validates that the property satisfies the provided predicate.
        /// </summary>
        /// <param name="predicate">The predicate to evaluate against the property.</param>
        /// <param name="message">The failure message to use if the predicate is not satisfied.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TProperty> Must(Func<TProperty, bool> predicate, string message) =>
            builder.Add(ObjectRules.Satisfies(predicate), message);

        /// <summary>
        /// Validates that the property does not satisfy the provided predicate.
        /// </summary>
        /// <param name="predicate">The predicate that must not be satisfied by the property.</param>
        /// <param name="message">The failure message to use if the predicate is satisfied.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TProperty> MustNot(Func<TProperty, bool> predicate, string message) =>
            builder.Add(ObjectRules.NotSatisfies(predicate), message);
    }
}