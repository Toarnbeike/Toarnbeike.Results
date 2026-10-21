using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class DateTimeValidationTargetExtensions
{
    extension<T>(ValidationTarget<T, DateTime> target)
    {
        /// <summary>
        /// Validates that the date/time is on or after the specified minimum.
        /// </summary>
        /// <param name="min">The minimum allowed date/time.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> OnOrAfter(DateTime min, string? message = null) =>
            target.Apply(DateRules.OnOrAfter(min), message);

        /// <summary>
        /// Validates that the date/time is on or before the specified maximum.
        /// </summary>
        /// <param name="max">The maximum allowed date/time.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> OnOrBefore(DateTime max, string? message = null) =>
            target.Apply(DateRules.OnOrBefore(max), message);

        /// <summary>
        /// Validates that the date/time is between the specified minimum and maximum.
        /// </summary>
        /// <param name="min">The minimum allowed date/time.</param>
        /// <param name="max">The maximum allowed date/time.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> Between(DateTime min, DateTime max, string? message = null) =>
            target.Apply(DateRules.Between(min, max), message);
    }
}