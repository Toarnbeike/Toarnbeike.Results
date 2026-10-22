using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Rules.Extensions;

/// <summary>
/// Provides fluent validation operations for <see cref="TimeSpan"/> values on an <see cref="IRuleTarget{T, TimeSpan}"/>.
/// </summary>
/// <remarks>Supports minimum, maximum, range, and tolerance-based comparisons.</remarks>
public static class TimeSpanExtensions
{
    /// <typeparam name="T">The type associated with the rule target and validation result.</typeparam>
    /// <param name="target">The rule target for <see cref="TimeSpan"/> values.</param>
    extension<T>(IRuleTarget<T, TimeSpan> target)
    {
        /// <summary>
        /// Verifies that the TimeSpan is at least the specified minimum.
        /// </summary>
        /// <param name="min">The minimum allowed TimeSpan.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the TimeSpan is at least the specified minimum.</returns>
        public Result<T> AtLeast(TimeSpan min, string? message = null) =>
            target.Apply(TimeSpanRules.AtLeast(min), message);

        /// <summary>
        /// Verifies that the TimeSpan is at most the specified maximum.
        /// </summary>
        /// <param name="max">The maximum allowed TimeSpan.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the TimeSpan is at most the specified maximum.</returns>
        public Result<T> AtMost(TimeSpan max, string? message = null) =>
            target.Apply(TimeSpanRules.AtMost(max), message);

        /// <summary>
        /// Verifies that the TimeSpan is between the specified minimum and maximum.
        /// </summary>
        /// <param name="min">The minimum allowed TimeSpan.</param>
        /// <param name="max">The maximum allowed TimeSpan.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the TimeSpan is between the specified minimum and maximum.</returns>
        public Result<T> Between(TimeSpan min, TimeSpan max, string? message = null) =>
            target.Apply(TimeSpanRules.Between(min, max), message);

        /// <summary>
        /// Verifies that the TimeSpan is within the specified tolerance of the expected value.
        /// </summary>
        /// <param name="expected">The expected TimeSpan value.</param>
        /// <param name="tolerance">The tolerance allowed around the expected value.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the TimeSpan is within the specified tolerance of the expected value.</returns>
        public Result<T> Around(TimeSpan expected, TimeSpan tolerance, string? message = null) =>
            target.Apply(TimeSpanRules.Around(expected, tolerance), message);
    }
}