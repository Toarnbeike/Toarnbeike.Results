using System.Numerics;
using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class IntegerValidationTargetExtensions
{
    extension<T, TInteger>(ValidationTarget<T, TInteger> target)
        where TInteger : struct, IBinaryInteger<TInteger>
    {
        /// <summary>
        /// Validates that the integer is at least the specified minimum.
        /// </summary>
        /// <param name="min">The minimum allowed value.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> AtLeast(TInteger min, string? message = null) =>
            target.Apply(NumberRules.AtLeast(min), message);

        /// <summary>
        /// Validates that the integer is at most the specified maximum.
        /// </summary>
        /// <param name="max">The maximum allowed value.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> AtMost(TInteger max, string? message = null) =>
            target.Apply(NumberRules.AtMost(max), message);
        /// <summary>
        /// Validates that the integer is between the specified minimum and maximum.
        /// </summary>
        /// <param name="min">The minimum allowed value.</param>
        /// <param name="max">The maximum allowed value.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> Between(TInteger min, TInteger max, string? message = null) =>
            target.Apply(NumberRules.Between(min, max), message);

        /// <summary>
        /// Validates that the integer is a multiple of the specified factor.
        /// </summary>
        /// <param name="factor">The factor the value must be a multiple of.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> MultipleOf(TInteger factor, string? message = null) =>
            target.Apply(NumberRules.MultipleOf(factor), message);
    }
}