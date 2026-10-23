using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class DateOnlyExtensions
{
    extension<T>(ValidationRuleBuilder<T, DateOnly> builder)
    {
        /// <summary>
        /// Validates that the date is on or after the specified minimum.
        /// </summary>
        /// <param name="min">The minimum allowed date.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, DateOnly> OnOrAfter(DateOnly min, string? message = null) =>
            builder.Add(DateRules.OnOrAfter(min), message);

        /// <summary>
        /// Validates that the date is on or before the specified maximum.
        /// </summary>
        /// <param name="max">The maximum allowed date.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, DateOnly> OnOrBefore(DateOnly max, string? message = null) =>
            builder.Add(DateRules.OnOrBefore(max), message);

        /// <summary>
        /// Validates that the date is between the specified minimum and maximum.
        /// </summary>
        /// <param name="min">The minimum allowed date.</param>
        /// <param name="max">The maximum allowed date.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, DateOnly> Between(DateOnly min, DateOnly max, string? message = null) =>
            builder.Add(DateRules.Between(min, max), message);

        /// <summary>
        /// Validates that the date is in the future (inclusive).
        /// </summary>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        /// <remarks>Uses the UtcNow provider configured in the <see cref="RuleContext"/> to get the current date.</remarks>
        public ValidationRuleBuilder<T, DateOnly> Future(string? message = null) =>
            builder.Add(DateRules.Future<DateOnly>(), message);

        /// <summary>
        /// Validates that the date is in the past (inclusive).
        /// </summary>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        /// <remarks>Uses the UtcNow provider configured in the <see cref="RuleContext"/> to get the current date.</remarks>
        public ValidationRuleBuilder<T, DateOnly> Past(string? message = null) =>
            builder.Add(DateRules.Past<DateOnly>(), message);

        /// <summary>
        /// Validates that the date is within the specified number of days in the future (inclusive).
        /// </summary>
        /// <param name="days">The number of days in the future the date must be within.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        /// <remarks>Uses the UtcNow provider configured in the <see cref="RuleContext"/> to get the current date.</remarks>
        public ValidationRuleBuilder<T, DateOnly> WithinFuture(int days, string? message = null) =>
            builder.Add(DateRules.WithinFuture(days), message);

        /// <summary>
        /// Validates that the date is within the specified number of days in the past (inclusive).
        /// </summary>
        /// <param name="days">The number of days in the past the date must be within.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        /// <remarks>Uses the UtcNow provider configured in the <see cref="RuleContext"/> to get the current date.</remarks>
        public ValidationRuleBuilder<T, DateOnly> WithinPast(int days, string? message = null) =>
            builder.Add(DateRules.WithinPast(days), message);
    }
}