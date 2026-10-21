using System.Numerics;
using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class FloatingPointValidationRuleBuilderExtensions
{
    extension<T, TFloating>(ValidationRuleBuilder<T, TFloating> builder)
        where TFloating : struct, IFloatingPoint<TFloating>, IMinMaxValue<TFloating>
    {
        /// <summary>
        /// Validates that the number is at least the specified minimum, using an optional tolerance.
        /// </summary>
        /// <param name="min">The minimum allowed value.</param>
        /// <param name="tolerance">Optional tolerance used for comparisons.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TFloating> AtLeast(TFloating min, TFloating? tolerance = null, string? message = null) =>
            builder.Add(FloatingPointRules.AtLeast(min, tolerance), message);

        /// <summary>
        /// Validates that the number is at most the specified maximum, using an optional tolerance.
        /// </summary>
        /// <param name="max">The maximum allowed value.</param>
        /// <param name="tolerance">Optional tolerance used for comparisons.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TFloating> AtMost(TFloating max, TFloating? tolerance = null, string? message = null) =>
            builder.Add(FloatingPointRules.AtMost(max, tolerance), message);

        /// <summary>
        /// Validates that the number is between the specified minimum and maximum, using an optional tolerance.
        /// </summary>
        /// <param name="min">The minimum allowed value.</param>
        /// <param name="max">The maximum allowed value.</param>
        /// <param name="tolerance">Optional tolerance used for comparisons.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TFloating> Between(TFloating min, TFloating max, TFloating? tolerance = null, string? message = null) =>
            builder.Add(FloatingPointRules.Between(min, max, tolerance), message);
        
        /// <summary>
        /// Validates that the number is a multiple of the specified factor, using an optional tolerance.
        /// </summary>
        /// <param name="factor">The factor the value must be a multiple of.</param>
        /// <param name="tolerance">Optional tolerance used for comparisons.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TFloating> MultipleOf(TFloating factor, TFloating? tolerance = null, string? message = null) =>
            builder.Add(FloatingPointRules.MultipleOf(factor, tolerance), message);

        /// <summary>
        /// Validates that the number is finite (not infinite or NaN).
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TFloating> Finite(string? message = null) =>
            builder.Add(FloatingPointRules.Finite<TFloating>(), message);
    }
}
