using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class DateOnlyValidationRuleBuilderExtensions
{
    extension<T>(ValidationRuleBuilder<T, DateOnly> builder)
    {
        /// <summary>
        /// Validates that the date is on or after the specified minimum.
        /// </summary>
        /// <param name="min">The minimum allowed date.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, DateOnly> OnOrAfter(DateOnly min, string? message = null) =>
            builder.Add(DateRules.OnOrAfter(min), message);

        /// <summary>
        /// Validates that the date is on or before the specified maximum.
        /// </summary>
        /// <param name="max">The maximum allowed date.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, DateOnly> OnOrBefore(DateOnly max, string? message = null) =>
            builder.Add(DateRules.OnOrBefore(max), message);

        /// <summary>
        /// Validates that the date is between the specified minimum and maximum.
        /// </summary>
        /// <param name="min">The minimum allowed date.</param>
        /// <param name="max">The maximum allowed date.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, DateOnly> Between(DateOnly min, DateOnly max, string? message = null) =>
            builder.Add(DateRules.Between(min, max), message);
    }
}