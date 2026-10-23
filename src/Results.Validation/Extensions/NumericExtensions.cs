using System.Numerics;
using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class NumericExtensions
{
    extension<T, TNumeric>(ValidationRuleBuilder<T, TNumeric> builder)
    where TNumeric : struct, INumber<TNumeric>
    {
        /// <summary>
        /// Validates that the numeric value is greater than the specified minimum.
        /// </summary>
        /// <param name="min">The exclusive minimum value.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TNumeric> GreaterThan(TNumeric min, string? message = null) =>
            builder.Add(NumberRules.GreaterThan(min), message);

        /// <summary>
        /// Validates that the numeric value is less than the specified maximum.
        /// </summary>
        /// <param name="max">The exclusive maximum value.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TNumeric> LessThan(TNumeric max, string? message = null) =>
            builder.Add(NumberRules.LessThan(max), message);

        /// <summary>
        /// Validates that the numeric value is positive (greater than zero).
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TNumeric> Positive(string? message = null) =>
            builder.Add(NumberRules.GreaterThan(TNumeric.Zero), message);
    }
}