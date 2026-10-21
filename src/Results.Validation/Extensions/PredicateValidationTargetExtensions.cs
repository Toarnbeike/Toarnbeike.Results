using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class PredicateValidationTargetExtensions
{
    extension<T, TProperty>(ValidationTarget<T, TProperty> target)
    {
        /// <summary>
        /// Validates that the property satisfies the provided predicate.
        /// </summary>
        /// <param name="predicate">The predicate to evaluate against the property.</param>
        /// <param name="message">The failure message to use if the predicate is not satisfied.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> Must(Func<TProperty, bool> predicate, string message) =>
            target.Apply(PredicateRules.Satisfies(predicate), message);

        /// <summary>
        /// Validates that the property does not satisfy the provided predicate.
        /// </summary>
        /// <param name="predicate">The predicate that must not be satisfied by the property.</param>
        /// <param name="message">The failure message to use if the predicate is satisfied.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> MustNot(Func<TProperty, bool> predicate, string message) =>
            target.Apply(PredicateRules.NotSatisfies(predicate), message);
    }
}