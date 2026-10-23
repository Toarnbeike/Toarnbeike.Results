using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class DateTimeOffsetExtensions
{
    extension<T>(ValidationRuleBuilder<T, DateTimeOffset> builder)
    {
        /// <summary>
        /// Validates that the date/time is on or after the specified minimum.
        /// </summary>
        /// <param name="min">The minimum allowed date/time.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, DateTimeOffset> OnOrAfter(DateTimeOffset min, string? message = null) =>
            builder.Add(DateRules.OnOrAfter(min), message);

        /// <summary>
        /// Validates that the date/time is on or before the specified maximum.
        /// </summary>
        /// <param name="max">The maximum allowed date/time.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, DateTimeOffset> OnOrBefore(DateTimeOffset max, string? message = null) =>
            builder.Add(DateRules.OnOrBefore(max), message);

        /// <summary>
        /// Validates that the date/time is between the specified minimum and maximum.
        /// </summary>
        /// <param name="min">The minimum allowed date/time.</param>
        /// <param name="max">The maximum allowed date/time.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, DateTimeOffset> Between(DateTimeOffset min, DateTimeOffset max, string? message = null) =>
            builder.Add(DateRules.Between(min, max), message);

        /// <summary>
        /// Validates that the date is in the future (inclusive).
        /// </summary>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        /// <remarks>Uses the UtcNow provider configured in the <see cref="RuleContext"/> to get the current date.</remarks>
        public ValidationRuleBuilder<T, DateTimeOffset> Future(string? message = null) =>
            builder.Add(DateRules.Future<DateTimeOffset>(), message);

        /// <summary>
        /// Validates that the date is in the past (inclusive).
        /// </summary>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        /// <remarks>Uses the UtcNow provider configured in the <see cref="RuleContext"/> to get the current date.</remarks>
        public ValidationRuleBuilder<T, DateTimeOffset> Past(string? message = null) =>
            builder.Add(DateRules.Past<DateTimeOffset>(), message);

        /// <summary>
        /// Validates that the date is within the specified timespan in the future (inclusive).
        /// </summary>
        /// <param name="timespan">The timespan in the future the date must be within.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        /// <remarks>Uses the UtcNow provider configured in the <see cref="RuleContext"/> to get the current date.</remarks>
        public ValidationRuleBuilder<T, DateTimeOffset> WithinFuture(TimeSpan timespan, string? message = null) =>
            builder.Add(DateRules.WithinFuture(timespan), message);

        /// <summary>
        /// Validates that the date is within the specified timespan in the past (inclusive).
        /// </summary>
        /// <param name="timespan">The timespan in the past the date must be within.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        /// <remarks>Uses the UtcNow provider configured in the <see cref="RuleContext"/> to get the current date.</remarks>
        public ValidationRuleBuilder<T, DateTimeOffset> WithinPast(TimeSpan timespan, string? message = null) =>
            builder.Add(DateRules.WithinPast(timespan), message);
    }
}