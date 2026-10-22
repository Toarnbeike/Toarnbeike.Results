using System.Numerics;
using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Rules.Extensions;

/// <summary>
/// Provides fluent validation operations for integer values on an <see cref="IRuleTarget{T, TInteger}"/>.
/// </summary>
/// <remarks>Supports range and multiple of validations.</remarks>
public static class IntegerExtensions
{
    /// <typeparam name="T">The type associated with the rule target and validation result.</typeparam>
    /// <typeparam name="TInteger">The type of the integer value being evaluated by the rule.</typeparam>
    /// <param name="target">The rule target for <typeparamref name="TInteger"/> values.</param>
    extension<T, TInteger>(IRuleTarget<T, TInteger> target)
        where TInteger : struct, IBinaryInteger<TInteger>
    {
        /// <summary>
        /// Verifies that the integer is at least the specified minimum.
        /// </summary>
        /// <param name="min">The minimum allowed value.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the integer is at least the specified minimum.</returns>
        public Result<T> AtLeast(TInteger min, string? message = null) =>
            target.Apply(NumberRules.AtLeast(min), message);

        /// <summary>
        /// Verifies that the integer is at most the specified maximum.
        /// </summary>
        /// <param name="max">The maximum allowed value.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the integer is at most the specified maximum.</returns>
        public Result<T> AtMost(TInteger max, string? message = null) =>
            target.Apply(NumberRules.AtMost(max), message);

        /// <summary>
        /// Verifies that the integer is between the specified minimum and maximum.
        /// </summary>
        /// <param name="min">The minimum allowed value.</param>
        /// <param name="max">The maximum allowed value.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the integer is between the specified minimum and maximum.</returns>
        public Result<T> Between(TInteger min, TInteger max, string? message = null) =>
            target.Apply(NumberRules.Between(min, max), message);

        /// <summary>
        /// Verifies that the integer is a multiple of the specified factor.
        /// </summary>
        /// <param name="factor">The factor the value must be a multiple of.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the integer is a multiple of the specified factor.</returns>
        public Result<T> MultipleOf(TInteger factor, string? message = null) =>
            target.Apply(NumberRules.MultipleOf(factor), message);
    }
}