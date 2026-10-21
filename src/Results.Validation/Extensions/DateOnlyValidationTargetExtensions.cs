using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class DateOnlyValidationTargetExtensions
{
    extension<T>(ValidationTarget<T, DateOnly> target)
    {
        /// <summary>
        /// Validates that the date is on or after the specified minimum.
        /// </summary>
        /// <param name="min">The minimum allowed date.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> OnOrAfter(DateOnly min, string? message = null) =>
            target.Apply(DateRules.OnOrAfter(min), message);

        /// <summary>
        /// Validates that the date is on or before the specified maximum.
        /// </summary>
        /// <param name="max">The maximum allowed date.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> OnOrBefore(DateOnly max, string? message = null) =>
            target.Apply(DateRules.OnOrBefore(max), message);

        /// <summary>
        /// Validates that the date is between the specified minimum and maximum.
        /// </summary>
        /// <param name="min">The minimum allowed date.</param>
        /// <param name="max">The maximum allowed date.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> Between(DateOnly min, DateOnly max, string? message = null) =>
            target.Apply(DateRules.Between(min, max), message);
    }
}