using System.Collections;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class CollectionExtensions
{
    extension<T, TEnumerable>(ValidationRuleBuilder<T, TEnumerable> builder)
        where TEnumerable : IEnumerable
    {
        /// <summary>
        /// Validates that the collection is not empty.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TEnumerable> NotEmpty(string? message = null) =>
            builder.Add(CollectionRules.NotEmpty<TEnumerable>(message));

        /// <summary>
        /// Validates that the collection is empty.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TEnumerable> Empty(string? message = null) =>
            builder.Add(CollectionRules.Empty<TEnumerable>(message));

        /// <summary>
        /// Validates that the collection has at least the specified number of elements.
        /// </summary>
        /// <param name="min">The minimum number of elements required.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TEnumerable> HasAtLeast(int min, string? message = null) =>
            builder.Add(CollectionRules.HasAtLeast<TEnumerable>(min, message));

        /// <summary>
        /// Validates that the collection has at most the specified number of elements.
        /// </summary>
        /// <param name="max">The maximum number of elements allowed.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TEnumerable> HasAtMost(int max, string? message = null) =>
            builder.Add(CollectionRules.HasAtMost<TEnumerable>(max, message));

        /// <summary>
        /// Validates that the collection has between the specified minimum and maximum number of elements.
        /// </summary>
        /// <param name="min">The minimum number of elements required.</param>
        /// <param name="max">The maximum number of elements allowed.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TEnumerable> HasBetween(int min, int max, string? message = null) =>
            builder.Add(CollectionRules.HasBetween<TEnumerable>(min, max, message));

        /// <summary>
        /// Validates that the collection has exactly the specified number of elements.
        /// </summary>
        /// <param name="expected">The exact number of elements expected.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TEnumerable> HasExactly(int expected, string? message = null) =>
            builder.Add(CollectionRules.HasExactly<TEnumerable>(expected, message));
    }

    extension<T, TEnumerable>(ValidationTarget<T, TEnumerable> target)
        where TEnumerable : IEnumerable
    {
        /// <summary>
        /// Validates that the collection is not empty.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> NotEmpty(string? message = null) =>
            target.Apply(CollectionRules.NotEmpty<TEnumerable>(message));

        /// <summary>
        /// Validates that the collection is empty.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> Empty(string? message = null) =>
            target.Apply(CollectionRules.Empty<TEnumerable>(message));

        /// <summary>
        /// Validates that the collection has at least the specified number of elements.
        /// </summary>
        /// <param name="min">The minimum number of elements required.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> HasAtLeast(int min, string? message = null) =>
            target.Apply(CollectionRules.HasAtLeast<TEnumerable>(min, message));

        /// <summary>
        /// Validates that the collection has at most the specified number of elements.
        /// </summary>
        /// <param name="max">The maximum number of elements allowed.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> HasAtMost(int max, string? message = null) =>
            target.Apply(CollectionRules.HasAtMost<TEnumerable>(max, message));

        /// <summary>
        /// Validates that the collection has between the specified minimum and maximum number of elements.
        /// </summary>
        /// <param name="min">The minimum number of elements required.</param>
        /// <param name="max">The maximum number of elements allowed.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> HasBetween(int min, int max, string? message = null) =>
            target.Apply(CollectionRules.HasBetween<TEnumerable>(min, max, message));

        /// <summary>
        /// Validates that the collection has exactly the specified number of elements.
        /// </summary>
        /// <param name="expected">The exact number of elements expected.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> HasExactly(int expected, string? message = null) =>
            target.Apply(CollectionRules.HasExactly<TEnumerable>(expected, message));
    }
}