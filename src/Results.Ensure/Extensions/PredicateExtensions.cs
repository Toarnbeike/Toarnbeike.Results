using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Ensure.Extensions;

public static class PredicateExtensions
{
    extension<T, TProperty>(EnsureTarget<T, TProperty> target)
    {
        /// <summary>
        /// Ensures that the property satisfies the provided predicate.
        /// </summary>
        /// <param name="predicate">The predicate to evaluate against the property.</param>
        /// <param name="message">The failure message to use if the predicate is not satisfied.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> Must(Func<TProperty, bool> predicate, string message) =>
            target.Apply(PredicateRules.Must(predicate, message));

        /// <summary>
        /// Ensures that the property does not satisfy the provided predicate.
        /// </summary>
        /// <param name="predicate">The predicate that must not be satisfied by the property.</param>
        /// <param name="message">The failure message to use if the predicate is satisfied.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> MustNot(Func<TProperty, bool> predicate, string message) =>
            target.Apply(PredicateRules.MustNot(predicate, message));
    }
}