using System.Runtime.CompilerServices;

namespace Toarnbeike.Results.Ensure.PrimitiveExtensions;

public static class DateTimeOffsetExtensions
{
    extension(DateTimeOffset date)
    {
        /// <summary>
        /// Ensure that the provided date is after the provided threshold.
        /// </summary>
        /// <param name="other">The date to compare against.</param>
        /// <param name="message">Optional: failure message specific for this date.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming date.</param>
        /// <returns>Result containing either the incoming date or a <see cref="GuardFailure"/></returns>

        public Result<DateTimeOffset> After(DateTimeOffset other, string? message = null,
            [CallerArgumentExpression(nameof(date))] string? expr = null)
        {
            var result = Guards.After(date, other);
            return result.IsValid ? date : new GuardFailure(result, nameof(After), expr, date, message);
        }

        /// <summary>
        /// Ensure that the provided date is before the provided threshold.
        /// </summary>
        /// <param name="other">The date to compare against.</param>
        /// <param name="message">Optional: failure message specific for this date.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming date.</param>
        /// <returns>Result containing either the incoming date or a <see cref="GuardFailure"/></returns>
        public Result<DateTimeOffset> Before(DateTimeOffset other, string? message = null,
            [CallerArgumentExpression(nameof(date))] string? expr = null)
        {
            var result = Guards.Before(date, other);
            return result.IsValid ? date : new GuardFailure(result, nameof(Before), expr, date, message);
        }
    }
}