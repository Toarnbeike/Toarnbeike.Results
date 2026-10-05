using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Ensure.Extensions;

public static class DateTimeExtensions
{
    extension<T>(EnsureTarget<T, DateTime> target)
    {
        /// <summary>
        /// Ensures that the date/time is on or after the specified minimum.
        /// </summary>
        /// <param name="min">The minimum allowed date/time.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> OnOrAfter(DateTime min, string? message = null) =>
            target.Apply(DateRules.OnOrAfter(min, message));

        /// <summary>
        /// Ensures that the date/time is on or before the specified maximum.
        /// </summary>
        /// <param name="max">The maximum allowed date/time.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> OnOrBefore(DateTime max, string? message = null) =>
            target.Apply(DateRules.OnOrBefore(max, message));

        /// <summary>
        /// Ensures that the date/time is between the specified minimum and maximum.
        /// </summary>
        /// <param name="min">The minimum allowed date/time.</param>
        /// <param name="max">The maximum allowed date/time.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> MustBeBetween(DateTime min, DateTime max, string? message = null) =>
            target.Apply(DateRules.MustBeBetween(min, max, message));
    }
}