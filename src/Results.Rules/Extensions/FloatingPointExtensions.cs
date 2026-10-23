using System.Numerics;
using Toarnbeike.Results.Rules.Tolerance;
using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Rules.Extensions;

/// <summary>
/// Provides fluent validation operations for floating-point values on an <see cref="RuleTarget{T, TFloating}"/>.
/// </summary>
/// <remarks>Supports comparisons such as minimum, maximum, range, multiple, and finiteness checks, with optional tolerance.</remarks>
/// <remarks>If a tolerance is not specified, comparisons are performed using the configured <see cref="IToleranceProvider"/></remarks>
public static class FloatingPointExtensions
{
    /// <typeparam name="T">The type associated with the rule target and validation result.</typeparam>
    /// <typeparam name="TFloating">The type of the floating-point value being evaluated by the rule.</typeparam>
    /// <param name="target">The rule target for <typeparamref name="TFloating"/> values.</param>
    extension<T, TFloating>(RuleTarget<T, TFloating> target)
        where TFloating : struct, IFloatingPoint<TFloating>, IMinMaxValue<TFloating>
    {
        /// <summary>
        /// Verifies that the number is at least the specified minimum, using an optional tolerance.
        /// </summary>
        /// <param name="min">The minimum value the number must be.</param>
        /// <param name="tolerance">Optional tolerance used for comparisons.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the number is at least the specified minimum.</returns>
        public Result<T> AtLeast(TFloating min, TFloating? tolerance = null, string? message = null) =>
            target.Apply(FloatingPointRules.AtLeast(min, tolerance), message);

        /// <summary>
        /// Verifies that the number is at most the specified maximum, using an optional tolerance.
        /// </summary>
        /// <param name="max">The maximum value the number must be.</param>
        /// <param name="tolerance">Optional tolerance used for comparisons.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the number is at most the specified maximum.</returns>
        public Result<T> AtMost(TFloating max, TFloating? tolerance = null, string? message = null) =>
            target.Apply(FloatingPointRules.AtMost(max, tolerance), message);

        /// <summary>
        /// Verifies that the number is between the specified minimum and maximum, using an optional tolerance.
        /// </summary>
        /// <param name="min">The minimum value the number must be.</param>
        /// <param name="max">The maximum value the number must be.</param>
        /// <param name="tolerance">Optional tolerance used for comparisons.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the number is between the specified minimum and maximum.</returns>
        public Result<T> Between(TFloating min, TFloating max, TFloating? tolerance = null, string? message = null) =>
            target.Apply(FloatingPointRules.Between(min, max, tolerance), message);

        /// <summary>
        /// Verifies that the number is a multiple of the specified factor, using an optional tolerance.
        /// </summary>
        /// <param name="factor">The factor the number must be a multiple of.</param>
        /// <param name="tolerance">Optional tolerance used for comparisons.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the number is a multiple of the specified factor.</returns>
        public Result<T> MultipleOf(TFloating factor, TFloating? tolerance = null, string? message = null) =>
            target.Apply(FloatingPointRules.MultipleOf(factor, tolerance), message);

        /// <summary>
        /// Verifies that the number is finite.
        /// </summary>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the number is finite.</returns>
        public Result<T> Finite(string? message = null) =>
            target.Apply(FloatingPointRules.Finite<TFloating>(), message);
    }
}
