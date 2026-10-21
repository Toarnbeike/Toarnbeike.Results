using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class PredicateValidationRuleBuilderExtensions
{
    extension<T, TProperty>(ValidationRuleBuilder<T, TProperty> builder)
    {
        /// <summary>
        /// Validates that the property satisfies the provided predicate.
        /// </summary>
        /// <param name="predicate">The predicate to evaluate against the property.</param>
        /// <param name="message">The failure message to use if the predicate is not satisfied.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TProperty> Must(Func<TProperty, bool> predicate, string message) =>
            builder.Add(PredicateRules.Satisfies(predicate), message);

        /// <summary>
        /// Validates that the property does not satisfy the provided predicate.
        /// </summary>
        /// <param name="predicate">The predicate that must not be satisfied by the property.</param>
        /// <param name="message">The failure message to use if the predicate is satisfied.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TProperty> MustNot(Func<TProperty, bool> predicate, string message) =>
            builder.Add(PredicateRules.NotSatisfies(predicate), message);
    }
}