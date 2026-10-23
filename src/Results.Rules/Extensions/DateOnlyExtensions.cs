using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Rules.Extensions;

/// <summary>
/// Provides fluent validation operations for <see cref="DateOnly"/> values on an <see cref="RuleTarget{T, DateOnly}"/>.
/// </summary>
/// <remarks>Supports minimum, maximum, and range comparisons.</remarks>
public static class DateOnlyExtensions
{
    /// <typeparam name="T">The type associated with the rule target and validation result.</typeparam>
    /// <param name="target">The rule target for <see cref="DateOnly"/> values.</param>
    extension<T>(RuleTarget<T, DateOnly> target)
    {
        /// <summary>
        /// Verifies that the date is on or after the specified minimum.
        /// </summary>
        /// <param name="min">The minimum allowed date.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the target date is on or after the specified minimum.</returns>
        public Result<T> OnOrAfter(DateOnly min, string? message = null) =>
            target.Apply(DateRules.OnOrAfter(min), message);

        /// <summary>
        /// Verifies that the date is on or before the specified maximum.
        /// </summary>
        /// <param name="max">The maximum allowed date.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the target date is on or before the specified maximum.</returns>
        public Result<T> OnOrBefore(DateOnly max, string? message = null) =>
            target.Apply(DateRules.OnOrBefore(max), message);

        /// <summary>
        /// Verifies that the date is between the specified minimum and maximum.
        /// </summary>
        /// <param name="min">The minimum allowed date.</param>
        /// <param name="max">The maximum allowed date.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the target date is between the specified minimum and maximum.</returns>
        public Result<T> Between(DateOnly min, DateOnly max, string? message = null) =>
            target.Apply(DateRules.Between(min, max), message);
    }
}