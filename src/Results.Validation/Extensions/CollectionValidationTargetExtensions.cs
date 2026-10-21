using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class CollectionValidationTargetExtensions
{
    extension<T, TItem>(ValidationTarget<T, IEnumerable<TItem>> target)
    {
        /// <summary>
        /// Validates that the collection contains the specified item.
        /// </summary>
        /// <param name="expected">The item expected to be in the collection.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> Contains(TItem expected, string? message = null) =>
            target.Apply(CollectionRules.Contains(expected), message);

        /// <summary>
        /// Validates that the collection does not contain the specified item.
        /// </summary>
        /// <param name="expected">The item expected to not be in the collection.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> NotContains(TItem expected, string? message = null) =>
            target.Apply(CollectionRules.NotContains(expected), message);

        /// <summary>
        /// Validates that the collection is not empty.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> NotEmpty(string? message = null) =>
            target.Apply(CollectionRules.HasAtLeast<TItem>(1),message);

        /// <summary>
        /// Validates that the collection is empty.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> Empty(string? message = null) =>
            target.Apply(CollectionRules.HasExactly<TItem>(0),message);

        /// <summary>
        /// Validates that the collection has at least the specified number of elements.
        /// </summary>
        /// <param name="min">The minimum number of elements required.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> HasAtLeast(int min, string? message = null) =>
            target.Apply(CollectionRules.HasAtLeast<TItem>(min), message);

        /// <summary>
        /// Validates that the collection has at most the specified number of elements.
        /// </summary>
        /// <param name="max">The maximum number of elements allowed.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> HasAtMost(int max, string? message = null) =>
            target.Apply(CollectionRules.HasAtMost<TItem>(max), message);

        /// <summary>
        /// Validates that the collection has between the specified minimum and maximum number of elements.
        /// </summary>
        /// <param name="min">The minimum number of elements required.</param>
        /// <param name="max">The maximum number of elements allowed.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> HasBetween(int min, int max, string? message = null) =>
            target.Apply(CollectionRules.HasBetween<TItem>(min, max), message);

        /// <summary>
        /// Validates that the collection has exactly the specified number of elements.
        /// </summary>
        /// <param name="expected">The exact number of elements expected.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> HasExactly(int expected, string? message = null) =>
            target.Apply(CollectionRules.HasExactly<TItem>(expected), message);
    }
}