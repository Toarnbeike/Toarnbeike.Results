using System.Numerics;
using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Rules;

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
            builder.Add(NumericRules.GreaterThan(min, message));

        /// <summary>
        /// Validates that the numeric value is less than the specified maximum.
        /// </summary>
        /// <param name="max">The exclusive maximum value.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TNumeric> LessThan(TNumeric max, string? message = null) =>
            builder.Add(NumericRules.LessThan(max, message));

        /// <summary>
        /// Validates that the numeric value is positive (greater than zero).
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TNumeric> Positive(string? message = null) =>
            builder.Add(NumericRules.Positive<TNumeric>(message));
    }

    extension<T, TNumeric>(ValidationTarget<T, TNumeric> target)
        where TNumeric : struct, INumber<TNumeric>
    {
        /// <summary>
        /// Validates that the numeric value is greater than the specified minimum.
        /// </summary>
        /// <param name="min">The exclusive minimum value.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> GreaterThan(TNumeric min, string? message = null) =>
            target.Apply(NumericRules.GreaterThan(min, message));

        /// <summary>
        /// Validates that the numeric value is less than the specified maximum.
        /// </summary>
        /// <param name="max">The exclusive maximum value.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> LessThan(TNumeric max, string? message = null) =>
            target.Apply(NumericRules.LessThan(max, message));

        /// <summary>
        /// Validates that the numeric value is positive (greater than zero).
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> Positive(string? message = null) =>
            target.Apply(NumericRules.Positive<TNumeric>(message));
    }
}
