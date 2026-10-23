using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Rules.Extensions;

/// <summary>
/// Provides fluent validation operations for <see cref="DateTimeOffset"/> values on an <see cref="RuleTarget{T, DateTimeOffset}"/>.
/// </summary>
/// <remarks>Supports minimum, maximum, and range comparisons.</remarks>
public static class DateTimeOffsetExtensions
{
    /// <typeparam name="T">The type associated with the rule target and validation result.</typeparam>
    /// <param name="target">The rule target for <see cref="DateTimeOffset"/> values.</param>
    extension<T>(RuleTarget<T, DateTimeOffset> target)
    {
        /// <summary>
        /// Verifies that the date/time is on or after the specified minimum.
        /// </summary>
        /// <param name="min">The minimum allowed date/time.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the target date/time is on or after the specified minimum.</returns>
        public Result<T> OnOrAfter(DateTimeOffset min, string? message = null) =>
            target.Apply(DateRules.OnOrAfter(min), message);

        /// <summary>
        /// Verifies that the date/time is on or before the specified maximum.
        /// </summary>
        /// <param name="max">The maximum allowed date/time.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the target date/time is on or before the specified maximum.</returns>
        public Result<T> OnOrBefore(DateTimeOffset max, string? message = null) =>
            target.Apply(DateRules.OnOrBefore(max), message);

        /// <summary>
        /// Verifies that the date/time is between the specified minimum and maximum.
        /// </summary>
        /// <param name="min">The minimum allowed date/time.</param>
        /// <param name="max">The maximum allowed date/time.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the target date/time is between the specified minimum and maximum.</returns>
        public Result<T> Between(DateTimeOffset min, DateTimeOffset max, string? message = null) =>
            target.Apply(DateRules.Between(min, max), message);

        /// <summary>
        /// Verifies that the date is in the future (inclusive).
        /// </summary>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the target date on or after today.</returns>
        /// <remarks>Uses the UtcNow provider configured in the <see cref="RuleContext"/> to get the current date.</remarks>
        public Result<T> Future(string? message = null) =>
            target.Apply(DateRules.Future<DateTimeOffset>(), message);

        /// <summary>
        /// Verifies that the date is in the past (inclusive).
        /// </summary>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the target date on or before today.</returns>
        /// <remarks>Uses the UtcNow provider configured in the <see cref="RuleContext"/> to get the current date.</remarks>
        public Result<T> Past(string? message = null) =>
            target.Apply(DateRules.Past<DateTimeOffset>(), message);

        /// <summary>
        /// Verifies that the date is within the specified timespan in the future (inclusive).
        /// </summary>
        /// <param name="timespan">The timespan in the future the date must be within.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the target date is within the specified timespan in the future.</returns>
        /// <remarks>Uses the UtcNow provider configured in the <see cref="RuleContext"/> to get the current date.</remarks>
        public Result<T> WithinFuture(TimeSpan timespan, string? message = null) =>
            target.Apply(DateRules.WithinFuture(timespan), message);

        /// <summary>
        /// Verifies that the date is within the specified timespan in the past (inclusive).
        /// </summary>
        /// <param name="timespan">The timespan in the past the date must be within.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the target date is within the specified timespan in the past.</returns>
        /// <remarks>Uses the UtcNow provider configured in the <see cref="RuleContext"/> to get the current date.</remarks>
        public Result<T> WithinPast(TimeSpan timespan, string? message = null) =>
            target.Apply(DateRules.WithinPast(timespan), message);
    }
}