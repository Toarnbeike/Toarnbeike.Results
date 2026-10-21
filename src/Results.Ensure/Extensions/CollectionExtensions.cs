using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Ensure.Extensions;

public static class CollectionExtensions
{
    extension<T, TItem>(EnsureTarget<T, IEnumerable<TItem>> target)
    {
        /// <summary>
        /// Ensures that the collection does contain the specified element.
        /// </summary>
        /// <param name="expected">The element that should not be present in the collection.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> DoesContain(TItem expected, string? message = null) =>
            target.Apply(CollectionRules.Contains(expected), message);

        /// <summary>
        /// Ensures that the collection does not contain the specified element.
        /// </summary>
        /// <param name="expected">The element that should not be present in the collection.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> DoesNotContain(TItem expected, string? message = null) =>
            target.Apply(CollectionRules.NotContains(expected), message);

        /// <summary>
        /// Ensures that the collection is not empty.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> NotEmpty(string? message = null) =>
            target.Apply(CollectionRules.HasAtLeast<TItem>(1), message);

        /// <summary>
        /// Ensures that the collection is empty.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> Empty(string? message = null) =>
            target.Apply(CollectionRules.HasExactly<TItem>(0), message);

        /// <summary>
        /// Ensures that the collection has at least the specified number of elements.
        /// </summary>
        /// <param name="min">The minimum number of elements required.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> HasAtLeast(int min, string? message = null) =>
            target.Apply(CollectionRules.HasAtLeast<TItem>(min), message);

        /// <summary>
        /// Ensures that the collection has at most the specified number of elements.
        /// </summary>
        /// <param name="max">The maximum number of elements allowed.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> HasAtMost(int max, string? message = null) =>
            target.Apply(CollectionRules.HasAtMost<TItem>(max), message);

        /// <summary>
        /// Ensures that the collection has between the specified minimum and maximum number of elements.
        /// </summary>
        /// <param name="min">The minimum number of elements required.</param>
        /// <param name="max">The maximum number of elements allowed.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> HasBetween(int min, int max, string? message = null) =>
            target.Apply(CollectionRules.HasBetween<TItem>(min, max), message);

        /// <summary>
        /// Ensures that the collection has exactly the specified number of elements.
        /// </summary>
        /// <param name="expected">The exact number of elements expected.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> HasExactly(int expected, string? message = null) =>
            target.Apply(CollectionRules.HasExactly<TItem>(expected), message);
    }
}