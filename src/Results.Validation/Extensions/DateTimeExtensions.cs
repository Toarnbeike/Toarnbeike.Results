using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class DateTimeExtensions
{
    extension<T>(ValidationRuleBuilder<T, DateTime> builder)
    {
        /// <summary>
        /// Validates that the date/time is on or after the specified minimum.
        /// </summary>
        /// <param name="min">The minimum allowed date/time.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, DateTime> OnOrAfter(DateTime min, string? message = null) =>
            builder.Add(DateRules.OnOrAfter(min), message);

        /// <summary>
        /// Validates that the date/time is on or before the specified maximum.
        /// </summary>
        /// <param name="max">The maximum allowed date/time.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, DateTime> OnOrBefore(DateTime max, string? message = null) =>
            builder.Add(DateRules.OnOrBefore(max), message);

        /// <summary>
        /// Validates that the date/time is between the specified minimum and maximum.
        /// </summary>
        /// <param name="min">The minimum allowed date/time.</param>
        /// <param name="max">The maximum allowed date/time.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, DateTime> Between(DateTime min, DateTime max, string? message = null) =>
            builder.Add(DateRules.Between(min, max), message);

        /// <summary>
        /// Verifies that the date is in the future (inclusive).
        /// </summary>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        /// <remarks>Uses the UtcNow provider configured in the <see cref="RuleContext"/> to get the current date.</remarks>
        public ValidationRuleBuilder<T, DateTime> Future(string? message = null) =>
            builder.Add(DateRules.Future<DateTime>(), message);

        /// <summary>
        /// Verifies that the date is in the past (inclusive).
        /// </summary>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        /// <remarks>Uses the UtcNow provider configured in the <see cref="RuleContext"/> to get the current date.</remarks>
        public ValidationRuleBuilder<T, DateTime> Past(string? message = null) =>
            builder.Add(DateRules.Past<DateTime>(), message);
    }
}