using System.Numerics;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class FloatingPointValidationTargetExtensions
{
    extension<T, TFloating>(ValidationTarget<T, TFloating> target)
        where TFloating : struct, IFloatingPoint<TFloating>, IMinMaxValue<TFloating>
    {
        /// <summary>
        /// Validates that the number is at least the specified minimum, using an optional tolerance.
        /// </summary>
        /// <param name="min">The minimum value the number must be.</param>
        /// <param name="tolerance">Optional tolerance used for comparisons.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> AtLeast(TFloating min, TFloating? tolerance = null, string? message = null) =>
            target.Apply(FloatingPointRules.AtLeast(min, tolerance), message);

        /// <summary>
        /// Validates that the number is at most the specified maximum, using an optional tolerance.
        /// </summary>
        /// <param name="max">The maximum value the number must be.</param>
        /// <param name="tolerance">Optional tolerance used for comparisons.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> AtMost(TFloating max, TFloating? tolerance = null, string? message = null) =>
            target.Apply(FloatingPointRules.AtMost(max, tolerance), message);

        /// <summary>
        /// Validates that the number is between the specified minimum and maximum, using an optional tolerance.
        /// </summary>
        /// <param name="min">The minimum value the number must be.</param>
        /// <param name="max">The maximum value the number must be.</param>
        /// <param name="tolerance">Optional tolerance used for comparisons.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> Between(TFloating min, TFloating max, TFloating? tolerance = null, string? message = null) =>
            target.Apply(FloatingPointRules.Between(min, max, tolerance), message);

        /// <summary>
        /// Validates that the number is a multiple of the specified factor, using an optional tolerance.
        /// </summary>
        /// <param name="factor">The factor the number must be a multiple of.</param>
        /// <param name="tolerance">Optional tolerance used for comparisons.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> MultipleOf(TFloating factor, TFloating? tolerance = null, string? message = null) =>
            target.Apply(FloatingPointRules.MultipleOf(factor, tolerance), message);

        /// <summary>
        /// Validates that the number is finite.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> Finite(string? message = null) =>
            target.Apply(FloatingPointRules.Finite<TFloating>(), message);
    }
}
