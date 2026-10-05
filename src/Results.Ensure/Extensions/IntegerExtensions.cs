using System.Numerics;
using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Ensure.Extensions;

public static class IntegerExtensions
{
    extension<T, TInteger>(EnsureTarget<T, TInteger> target)
        where TInteger : struct, IBinaryInteger<TInteger>
    {
        /// <summary>
        /// Ensures that the integer is at least the specified minimum.
        /// </summary>
        /// <param name="min">The minimum allowed value.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> AtLeast(TInteger min, string? message = null) =>
            target.Apply(IntegerRules.AtLeast(min, message));

        /// <summary>
        /// Ensures that the integer is at most the specified maximum.
        /// </summary>
        /// <param name="max">The maximum allowed value.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> AtMost(TInteger max, string? message = null) =>
            target.Apply(IntegerRules.AtMost(max, message));
        /// <summary>
        /// Ensures that the integer is between the specified minimum and maximum.
        /// </summary>
        /// <param name="min">The minimum allowed value.</param>
        /// <param name="max">The maximum allowed value.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> Between(TInteger min, TInteger max, string? message = null) =>
            target.Apply(IntegerRules.Between(min, max, message));

        /// <summary>
        /// Ensures that the integer is a multiple of the specified factor.
        /// </summary>
        /// <param name="factor">The factor the value must be a multiple of.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> MultipleOf(TInteger factor, string? message = null) =>
            target.Apply(IntegerRules.MultipleOf(factor, message));
    }
}