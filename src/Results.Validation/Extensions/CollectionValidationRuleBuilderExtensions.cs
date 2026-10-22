using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class CollectionValidationRuleBuilderExtensions
{
    extension<T, TItem>(ValidationRuleBuilder<T, IEnumerable<TItem>> builder)
    {
        /// <summary>
        /// Validates that the collection contains the specified item.
        /// </summary>
        /// <param name="expected">The item that must be present in the collection.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        public ValidationRuleBuilder<T, IEnumerable<TItem>> Contains(TItem expected, string? message = null) =>
            builder.Add(CollectionRules.Contains(expected), message);

        /// <summary>
        /// Validates that the collection does not contain the specified item.
        /// </summary>
        /// <param name="expected">The item that must not be present in the collection.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        public ValidationRuleBuilder<T, IEnumerable<TItem>> NotContains(TItem expected, string? message = null) =>
            builder.Add(CollectionRules.NotContains(expected), message);

        /// <summary>
        /// Validates that the collection is not empty.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, IEnumerable<TItem>> NotEmpty(string? message = null) =>
            builder.Add(CollectionRules.HasAtLeast<TItem>(1), message);

        /// <summary>
        /// Validates that the collection is empty.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, IEnumerable<TItem>> Empty(string? message = null) =>
            builder.Add(CollectionRules.HasExactly<TItem>(0), message);

        /// <summary>
        /// Validates that the collection has at least the specified number of elements.
        /// </summary>
        /// <param name="min">The minimum number of elements required.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, IEnumerable<TItem>> HasAtLeast(int min, string? message = null) =>
            builder.Add(CollectionRules.HasAtLeast<TItem>(min), message);

        /// <summary>
        /// Validates that the collection has at most the specified number of elements.
        /// </summary>
        /// <param name="max">The maximum number of elements allowed.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, IEnumerable<TItem>> HasAtMost(int max, string? message = null) =>
            builder.Add(CollectionRules.HasAtMost<TItem>(max), message);

        /// <summary>
        /// Validates that the collection has between the specified minimum and maximum number of elements.
        /// </summary>
        /// <param name="min">The minimum number of elements required.</param>
        /// <param name="max">The maximum number of elements allowed.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, IEnumerable<TItem>> HasBetween(int min, int max, string? message = null) =>
            builder.Add(CollectionRules.HasBetween<TItem>(min, max), message);

        /// <summary>
        /// Validates that the collection has exactly the specified number of elements.
        /// </summary>
        /// <param name="expected">The exact number of elements expected.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, IEnumerable<TItem>> HasExactly(int expected, string? message = null) =>
            builder.Add(CollectionRules.HasExactly<TItem>(expected), message);
    }
}