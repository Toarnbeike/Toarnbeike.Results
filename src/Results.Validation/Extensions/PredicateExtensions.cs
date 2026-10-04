using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class PredicateExtensions
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
            builder.Add(PredicateRules.Must(predicate, message));

        /// <summary>
        /// Validates that the property does not satisfy the provided predicate.
        /// </summary>
        /// <param name="predicate">The predicate that must not be satisfied by the property.</param>
        /// <param name="message">The failure message to use if the predicate is satisfied.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TProperty> MustNot(Func<TProperty, bool> predicate, string message) =>
            builder.Add(PredicateRules.MustNot(predicate, message));
    }

    extension<T, TProperty>(ValidationTarget<T, TProperty> target)
    {
        /// <summary>
        /// Validates that the property satisfies the provided predicate.
        /// </summary>
        /// <param name="predicate">The predicate to evaluate against the property.</param>
        /// <param name="message">The failure message to use if the predicate is not satisfied.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> Must(Func<TProperty, bool> predicate, string message) =>
            target.Apply(PredicateRules.Must(predicate, message));

        /// <summary>
        /// Validates that the property does not satisfy the provided predicate.
        /// </summary>
        /// <param name="predicate">The predicate that must not be satisfied by the property.</param>
        /// <param name="message">The failure message to use if the predicate is satisfied.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> MustNot(Func<TProperty, bool> predicate, string message) =>
            target.Apply(PredicateRules.MustNot(predicate, message));
    }
}