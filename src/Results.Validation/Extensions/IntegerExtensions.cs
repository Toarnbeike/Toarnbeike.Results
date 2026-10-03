using System.Numerics;
using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class IntegerExtensions
{
    extension<T, TInteger>(ValidationRuleBuilder<T, TInteger> builder)
    where TInteger : struct, IBinaryInteger<TInteger>
    {
        /// <summary>
        /// Validates that the integer is at least the specified minimum.
        /// </summary>
        /// <param name="min">The minimum allowed value.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TInteger> AtLeast(TInteger min, string? message = null) =>
            builder.Add(IntegerRules.AtLeast(min, message));
        /// <summary>
        /// Validates that the integer is at most the specified maximum.
        /// </summary>
        /// <param name="max">The maximum allowed value.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TInteger> AtMost(TInteger max, string? message = null) =>
            builder.Add(IntegerRules.AtMost(max, message));

        /// <summary>
        /// Validates that the integer is between the specified minimum and maximum.
        /// </summary>
        /// <param name="min">The minimum allowed value.</param>
        /// <param name="max">The maximum allowed value.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TInteger> Between(TInteger min, TInteger max, string? message = null) =>
            builder.Add(IntegerRules.Between(min, max, message));

        /// <summary>
        /// Validates that the integer is a multiple of the specified factor.
        /// </summary>
        /// <param name="factor">The factor the value must be a multiple of.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TInteger> MultipleOf(TInteger factor, string? message = null) =>
            builder.Add(IntegerRules.MultipleOf(factor, message));
    }

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
            target.Apply(IntegerRules.AtLeast(min, message));

        /// <summary>
        /// Validates that the integer is at most the specified maximum.
        /// </summary>
        /// <param name="max">The maximum allowed value.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> AtMost(TInteger max, string? message = null) =>
            target.Apply(IntegerRules.AtMost(max, message));
        /// <summary>
        /// Validates that the integer is between the specified minimum and maximum.
        /// </summary>
        /// <param name="min">The minimum allowed value.</param>
        /// <param name="max">The maximum allowed value.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> Between(TInteger min, TInteger max, string? message = null) =>
            target.Apply(IntegerRules.Between(min, max, message));

        /// <summary>
        /// Validates that the integer is a multiple of the specified factor.
        /// </summary>
        /// <param name="factor">The factor the value must be a multiple of.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> MultipleOf(TInteger factor, string? message = null) =>
            target.Apply(IntegerRules.MultipleOf(factor, message));
    }
}