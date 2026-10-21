using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class TimeSpanValidationTargetExtensions
{
    extension<T>(ValidationTarget<T, TimeSpan> target)
    {
        /// <summary>
        /// Validates that the TimeSpan is at least the specified minimum.
        /// </summary>
        /// <param name="min">The minimum allowed TimeSpan.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> AtLeast(TimeSpan min, string? message = null) =>
            target.Apply(TimeSpanRules.AtLeast(min), message);

        /// <summary>
        /// Validates that the TimeSpan is at most the specified maximum.
        /// </summary>
        /// <param name="max">The maximum allowed TimeSpan.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> AtMost(TimeSpan max, string? message = null) =>
            target.Apply(TimeSpanRules.AtMost(max), message);

        /// <summary>
        /// Validates that the TimeSpan is between the specified minimum and maximum.
        /// </summary>
        /// <param name="min">The minimum allowed TimeSpan.</param>
        /// <param name="max">The maximum allowed TimeSpan.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> Between(TimeSpan min, TimeSpan max, string? message = null) =>
            target.Apply(TimeSpanRules.Between(min, max), message);

        /// <summary>
        /// Validates that the TimeSpan is within the specified tolerance of the expected value.
        /// </summary>
        /// <param name="expected">The expected TimeSpan value.</param>
        /// <param name="tolerance">The tolerance allowed around the expected value.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> Around(TimeSpan expected, TimeSpan tolerance, string? message = null) =>
            target.Apply(TimeSpanRules.Around(expected, tolerance), message);
    }
}