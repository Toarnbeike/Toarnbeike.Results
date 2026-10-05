using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Ensure.Extensions;

public static class TimeSpanExtensions
{
    extension<T>(EnsureTarget<T, TimeSpan> target)
    {
        /// <summary>
        /// Ensures that the TimeSpan is at least the specified minimum.
        /// </summary>
        /// <param name="min">The minimum allowed TimeSpan.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> AtLeast(TimeSpan min, string? message = null) =>
            target.Apply(TimeSpanRules.AtLeast(min, message));

        /// <summary>
        /// Ensures that the TimeSpan is at most the specified maximum.
        /// </summary>
        /// <param name="max">The maximum allowed TimeSpan.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> AtMost(TimeSpan max, string? message = null) =>
            target.Apply(TimeSpanRules.AtMost(max, message));

        /// <summary>
        /// Ensures that the TimeSpan is between the specified minimum and maximum.
        /// </summary>
        /// <param name="min">The minimum allowed TimeSpan.</param>
        /// <param name="max">The maximum allowed TimeSpan.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> Between(TimeSpan min, TimeSpan max, string? message = null) =>
            target.Apply(TimeSpanRules.MustBeBetween(min, max, message));

        /// <summary>
        /// Ensures that the TimeSpan is within the specified tolerance of the expected value.
        /// </summary>
        /// <param name="expected">The expected TimeSpan value.</param>
        /// <param name="tolerance">The tolerance allowed around the expected value.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> Around(TimeSpan expected, TimeSpan tolerance, string? message = null) =>
            target.Apply(TimeSpanRules.Around(expected, tolerance, message));
    }
}