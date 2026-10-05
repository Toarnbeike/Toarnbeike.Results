using System.Numerics;
using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Ensure.Extensions;

public static class FloatingPointExtensions
{
    extension<T, TFloating>(EnsureTarget<T, TFloating> target)
        where TFloating : struct, IFloatingPoint<TFloating>
    {
        /// <summary>
        /// Ensures that the number is at least the specified minimum, using an optional tolerance.
        /// </summary>
        /// <param name="min">The minimum value the number must be.</param>
        /// <param name="tolerance">Optional tolerance used for comparisons.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> AtLeast(TFloating min, TFloating? tolerance = null, string? message = null) =>
            target.Apply(FloatingPointRules.AtLeast(min, tolerance, message));

        /// <summary>
        /// Ensures that the number is at most the specified maximum, using an optional tolerance.
        /// </summary>
        /// <param name="max">The maximum value the number must be.</param>
        /// <param name="tolerance">Optional tolerance used for comparisons.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> AtMost(TFloating max, TFloating? tolerance = null, string? message = null) =>
            target.Apply(FloatingPointRules.AtMost(max, tolerance, message));

        /// <summary>
        /// Ensures that the number is between the specified minimum and maximum, using an optional tolerance.
        /// </summary>
        /// <param name="min">The minimum value the number must be.</param>
        /// <param name="max">The maximum value the number must be.</param>
        /// <param name="tolerance">Optional tolerance used for comparisons.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> Between(TFloating min, TFloating max, TFloating? tolerance = null, string? message = null) =>
            target.Apply(FloatingPointRules.Between(min, max, tolerance, message));

        /// <summary>
        /// Ensures that the number is a multiple of the specified factor, using an optional tolerance.
        /// </summary>
        /// <param name="factor">The factor the number must be a multiple of.</param>
        /// <param name="tolerance">Optional tolerance used for comparisons.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> MultipleOf(TFloating factor, TFloating? tolerance = null, string? message = null) =>
            target.Apply(FloatingPointRules.MultipleOf(factor, tolerance, message));

        /// <summary>
        /// Ensures that the number is finite.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> Finite(string? message = null) =>
            target.Apply(FloatingPointRules.Finite<TFloating>(message));
        
        /// <summary>
        /// Ensures that the number is not NaN.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> NotNaN(string? message = null) =>
            target.Apply(FloatingPointRules.NotNaN<TFloating>(message));
    }
}
