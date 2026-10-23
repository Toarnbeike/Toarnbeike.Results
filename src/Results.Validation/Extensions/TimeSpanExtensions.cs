using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class TimeSpanExtensions
{
    extension<T>(ValidationRuleBuilder<T, TimeSpan> builder)
    {
        /// <summary>
        /// Validates that the TimeSpan is at least the specified minimum.
        /// </summary>
        /// <param name="min">The minimum allowed TimeSpan.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TimeSpan> AtLeast(TimeSpan min, string? message = null) =>
            builder.Add(TimeSpanRules.AtLeast(min), message);

        /// <summary>
        /// Validates that the TimeSpan is at most the specified maximum.
        /// </summary>
        /// <param name="max">The maximum allowed TimeSpan.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TimeSpan> AtMost(TimeSpan max, string? message = null) =>
            builder.Add(TimeSpanRules.AtMost(max), message);

        /// <summary>
        /// Validates that the TimeSpan is between the specified minimum and maximum.
        /// </summary>
        /// <param name="min">The minimum allowed TimeSpan.</param>
        /// <param name="max">The maximum allowed TimeSpan.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TimeSpan> Between(TimeSpan min, TimeSpan max, string? message = null) =>
            builder.Add(TimeSpanRules.Between(min, max), message);

        /// <summary>
        /// Validates that the TimeSpan is within the specified tolerance of the expected value.
        /// </summary>
        /// <param name="expected">The expected TimeSpan value.</param>
        /// <param name="tolerance">The tolerance allowed around the expected value.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TimeSpan> Around(TimeSpan expected, TimeSpan tolerance, string? message = null) =>
            builder.Add(TimeSpanRules.Around(expected, tolerance), message);

        /// <summary>
        /// Validates that the TimeSpan is at least zero.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TimeSpan> AtLeastZero(string? message = null) =>
            builder.Add(TimeSpanRules.AtLeastZero(), message);
    }
}