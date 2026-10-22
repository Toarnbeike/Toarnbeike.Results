using System.Numerics;
using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Rules.Extensions;

/// <summary>
/// Provides fluent validation operations for numeric values on an <see cref="IRuleTarget{T, TNumber}"/>.
/// </summary>
/// <remarks>Supports absolute comparisons without tolerance.</remarks>
public static class NumericExtensions
{
    /// <typeparam name="T">The type associated with the rule target and validation result.</typeparam>
    /// <typeparam name="TNumber">The type of the numeric value being evaluated by the rule.</typeparam>
    /// <param name="target">The rule target for <typeparamref name="TNumber"/> values.</param>
    extension<T, TNumber>(IRuleTarget<T, TNumber> target)
        where TNumber : struct, INumber<TNumber>
    {
        /// <summary>
        /// Verifies that the numeric value is greater than the specified minimum.
        /// </summary>
        /// <param name="min">The exclusive minimum value.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the numeric value is greater than the specified minimum.</returns>
        public Result<T> GreaterThan(TNumber min, string? message = null) =>
            target.Apply(NumberRules.GreaterThan(min), message);

        /// <summary>
        /// Verifies that the numeric value is less than the specified maximum.
        /// </summary>
        /// <param name="max">The exclusive maximum value.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the numeric value is less than the specified maximum.</returns>
        public Result<T> LessThan(TNumber max, string? message = null) =>
            target.Apply(NumberRules.LessThan(max), message);

        /// <summary>
        /// Verifies that the numeric value is positive (greater than zero).
        /// </summary>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the numeric value is positive (greater than zero).</returns>
        public Result<T> Positive(string? message = null) =>
            target.Apply(NumberRules.GreaterThan(TNumber.Zero), message);
    }
}
