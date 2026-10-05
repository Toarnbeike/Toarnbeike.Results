using System.Numerics;
using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Ensure.Extensions;

public static class NumericExtensions
{
    extension<T, TNumeric>(EnsureTarget<T, TNumeric> target)
        where TNumeric : struct, INumber<TNumeric>
    {
        /// <summary>
        /// Ensures that the numeric value is greater than the specified minimum.
        /// </summary>
        /// <param name="min">The exclusive minimum value.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> GreaterThan(TNumeric min, string? message = null) =>
            target.Apply(NumericRules.GreaterThan(min, message));

        /// <summary>
        /// Ensures that the numeric value is less than the specified maximum.
        /// </summary>
        /// <param name="max">The exclusive maximum value.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> LessThan(TNumeric max, string? message = null) =>
            target.Apply(NumericRules.LessThan(max, message));

        /// <summary>
        /// Ensures that the numeric value is positive (greater than zero).
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> Positive(string? message = null) =>
            target.Apply(NumericRules.Positive<TNumeric>(message));
    }
}
