using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Rules.Extensions;

/// <summary>
/// Provides fluent validation operations for collections of items on an <see cref="RuleTarget{T, IEnumerable{TItem}}"/>.
/// </summary>
/// <remarks>Supports minimum, maximum, and range comparisons.</remarks>
public static class CollectionExtensions
{
    /// <typeparam name="T">The type associated with the rule target and validation result.</typeparam>
    /// <typeparam name="TItem">The type of the items in the collection being evaluated by the rule.</typeparam>
    /// <param name="target">The rule target for <see cref="IEnumerable{TItem}"/> values.</param>
    extension<T, TItem>(RuleTarget<T, IEnumerable<TItem>> target)
    {
        /// <summary>
        /// Verifies that the target collection contains the specified item.
        /// </summary>
        /// <param name="expected">The item that must be present in the target collection.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the target collection contains the expected item.</returns>
        public Result<T> Contains(TItem expected, string? message = null) =>
            target.Apply(CollectionRules.Contains(expected), message);

        /// <summary>
        /// Verifies that the target collection does not contains the specified item.
        /// </summary>
        /// <param name="expected">The item that must not be present in the target collection.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the target collection contains the expected item.</returns>
        public Result<T> NotContains(TItem expected, string? message = null) =>
            target.Apply(CollectionRules.NotContains(expected), message);

        /// <summary>
        /// Verifies that the target collection is not empty.
        /// </summary>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the target collection is not empty.</returns>
        public Result<T> NotEmpty(string? message = null) =>
            target.Apply(CollectionRules.HasAtLeast<TItem>(1), message);

        /// <summary>
        /// Verifies that the target collection is empty.
        /// </summary>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the target collection is empty.</returns>
        public Result<T> Empty(string? message = null) =>
            target.Apply(CollectionRules.HasExactly<TItem>(0), message);

        /// <summary>
        /// Verifies that the collection has at least the specified number of elements.
        /// </summary>
        /// <param name="min">The minimum number of elements required.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the target collection has at least the specified number of elements.</returns>
        public Result<T> HasAtLeast(int min, string? message = null) =>
            target.Apply(CollectionRules.HasAtLeast<TItem>(min), message);

        /// <summary>
        /// Verifies that the collection has at most the specified number of elements.
        /// </summary>
        /// <param name="max">The maximum number of elements allowed.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the target collection has at most the specified number of elements.</returns>
        public Result<T> HasAtMost(int max, string? message = null) =>
            target.Apply(CollectionRules.HasAtMost<TItem>(max), message);

        /// <summary>
        /// Verifies that the collection has between the specified minimum and maximum number of elements.
        /// </summary>
        /// <param name="min">The minimum number of elements required.</param>
        /// <param name="max">The maximum number of elements allowed.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the target collection has between the specified minimum and maximum number of elements.</returns>
        public Result<T> HasBetween(int min, int max, string? message = null) =>
            target.Apply(CollectionRules.HasBetween<TItem>(min, max), message);

        /// <summary>
        /// Verifies that the collection has exactly the specified number of elements.
        /// </summary>
        /// <param name="expected">The exact number of elements expected.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the target collection has exactly the specified number of elements.</returns>
        public Result<T> HasExactly(int expected, string? message = null) =>
            target.Apply(CollectionRules.HasExactly<TItem>(expected), message);
    }
}