using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Ensure.Extensions;

public static class DateOnlyExtensions
{
    extension<T>(EnsureTarget<T, DateOnly> target)
    {
        /// <summary>
        /// Ensures that the date is on or after the specified minimum.
        /// </summary>
        /// <param name="min">The minimum allowed date.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> OnOrAfter(DateOnly min, string? message = null) =>
            target.Apply(DateRules.OnOrAfter(min, message));

        /// <summary>
        /// Ensures that the date is on or before the specified maximum.
        /// </summary>
        /// <param name="max">The maximum allowed date.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> OnOrBefore(DateOnly max, string? message = null) =>
            target.Apply(DateRules.OnOrBefore(max, message));

        /// <summary>
        /// Ensures that the date is between the specified minimum and maximum.
        /// </summary>
        /// <param name="min">The minimum allowed date.</param>
        /// <param name="max">The maximum allowed date.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> MustBeBetween(DateOnly min, DateOnly max, string? message = null) =>
            target.Apply(DateRules.MustBeBetween(min, max, message));
    }
}