using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Rules.Extensions;

/// <summary>
/// Provides fluent validation operations on generic objects on an <see cref="RuleTarget{T, TProperty}"/>.
/// </summary>
/// <remarks>Supports evaluating whether a property satisfies or does not satisfy a given predicate.</remarks>
public static class ObjectExtensions
{
    /// <typeparam name="T">The type associated with the rule target and validation result.</typeparam>
    /// <typeparam name="TProperty">The type of the property being evaluated by the rule.</typeparam>
    /// <param name="target">The rule target for <typeparamref name="TProperty"/> values.</param>
    extension<T, TProperty>(RuleTarget<T, TProperty> target)
    {
        /// <summary>
        /// Verifies that the property is equal to the specified expected value.
        /// </summary>
        /// <param name="expected">The value to compare against the target.</param>
        /// <param name="message">An optional message to include in the result.</param>
        /// <returns>A result representing the outcome of the equality check.</returns>
        public Result<T> EqualTo(TProperty expected, string? message = null) =>
            target.Apply(ObjectRules.EqualTo(expected), message);


        /// <summary>
        /// Verifies that the property is not equal to the specified expected value.
        /// </summary>
        /// <param name="expected">The value to compare against the target.</param>
        /// <param name="message">An optional message to include in the result.</param>
        /// <returns>A result representing the outcome of the inequality check.</returns>
        public Result<T> NotEqualTo(TProperty expected, string? message = null) =>
            target.Apply(ObjectRules.NotEqualTo(expected), message);

        /// <summary>
        /// Verifies that the property satisfies the provided predicate.
        /// </summary>
        /// <param name="predicate">The predicate to evaluate against the property.</param>
        /// <param name="message">A failure message describing why the predicate must be satisfied.</param>
        /// <returns>A result representing whether the property satisfies the provided predicate.</returns>
        public Result<T> Must(Func<TProperty, bool> predicate, string message) =>
            target.Apply(ObjectRules.Satisfies(predicate), message);

        /// <summary>
        /// Verifies that the property does not satisfy the provided predicate.
        /// </summary>
        /// <param name="predicate">The predicate that must not be satisfied by the property.</param>
        /// <param name="message">A failure message describing why the predicate must not be satisfied.</param>
        /// <returns>A result representing whether the property does not satisfy the provided predicate.</returns>
        public Result<T> MustNot(Func<TProperty, bool> predicate, string message) =>
            target.Apply(ObjectRules.NotSatisfies(predicate), message);

        /// <summary>
        /// Asynchronously verifies that the property satisfies the provided predicate.
        /// </summary>
        /// <param name="predicate">The asynchronous predicate to evaluate against the property.</param>
        /// <param name="message">A failure message describing why the predicate must be satisfied.</param>
        /// <returns>A task representing whether the property satisfies the provided predicate.</returns>
        public Task<Result<T>> MustAsync(Func<TProperty, Task<bool>> predicate, string message) =>
            target.ApplyAsync(ObjectRules.SatisfiesAsync(predicate), message);

        /// <summary>
        /// Asynchronously verifies that the property does not satisfy the provided predicate.
        /// </summary>
        /// <param name="predicate">The asynchronous predicate that must not be satisfied by the property.</param>
        /// <param name="message">A failure message describing why the predicate must not be satisfied.</param>
        /// <returns>A task representing whether the property does not satisfy the provided predicate.</returns>
        public Task<Result<T>> MustNotAsync(Func<TProperty, Task<bool>> predicate, string message) =>
            target.ApplyAsync(ObjectRules.NotSatisfiesAsync(predicate), message);
    }
}