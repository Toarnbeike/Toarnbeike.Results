using System.Runtime.CompilerServices;
using Toarnbeike.Results.Ensure.Rules;

namespace Toarnbeike.Results.Ensure.Extensions;

public static class DateTimeExtensions
{
    extension(DateTime date)
    {
        /// <summary>
        /// Ensure that the provided date is on or after the provided threshold.
        /// </summary>
        /// <param name="other">The date to compare against.</param>
        /// <param name="message">Optional: failure message specific for this date.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming date.</param>
        /// <returns>Result containing either the incoming date or a <see cref="GuardFailure"/></returns>
        public Result<DateTime> OnOrAfter(DateTime other, string? message = null,
            [CallerArgumentExpression(nameof(date))] string? expr = null) =>
                Result.Ensure().That(date).OnOrAfter(other)
                .WithMessage(message).WithArgumentName(expr)
                .ToResult(date);

        /// <summary>
        /// Ensure that the provided date is on or before the provided threshold.
        /// </summary>
        /// <param name="other">The date to compare against.</param>
        /// <param name="message">Optional: failure message specific for this date.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming date.</param>
        /// <returns>Result containing either the incoming date or a <see cref="GuardFailure"/></returns>
        public Result<DateTime> OnOrBefore(DateTime other, string? message = null,
            [CallerArgumentExpression(nameof(date))] string? expr = null) =>
            Result.Ensure().That(date).OnOrBefore(other)
                .WithMessage(message).WithArgumentName(expr)
                .ToResult(date);

        /// <summary>
        /// Ensure that the provided date is between the provided start and end dates.
        /// </summary>
        /// <param name="start">The start date of the range.</param>
        /// <param name="end">The end date of the range.</param>
        /// <param name="message">Optional: failure message specific for this date.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming date.</param>
        /// <returns>Result containing either the incoming date or a <see cref="GuardFailure"/></returns>
        public Result<DateTime> Between(DateTime start, DateTime end, string? message = null,
            [CallerArgumentExpression(nameof(date))] string? expr = null) =>
            Result.Ensure().That(date).Between(start, end)
                .WithMessage(message).WithArgumentName(expr)
                .ToResult(date);

        /// <summary>
        /// Ensure that the provided date is in the future compared to the current date.
        /// </summary>
        /// <param name="timeProvider">Optional: the date time provider used.</param>
        /// <param name="message">Optional: failure message specific for this date.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming date.</param>
        /// <returns>Result containing either the incoming date or a <see cref="GuardFailure"/></returns>
        public Result<DateTime> Future(TimeProvider? timeProvider = null, string? message = null,
            [CallerArgumentExpression(nameof(date))] string? expr = null) =>
            Result.Ensure(timeProvider: timeProvider).That(date).Future()
                .WithMessage(message).WithArgumentName(expr)
                .ToResult(date);

        /// <summary>
        /// Ensure that the provided date is in the past compared to the current date.
        /// </summary>
        /// <param name="timeProvider">Optional: the date time provider used.</param>
        /// <param name="message">Optional: failure message specific for this date.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming date.</param>
        /// <returns>Result containing either the incoming date or a <see cref="GuardFailure"/></returns>
        public Result<DateTime> Past(TimeProvider? timeProvider = null, string? message = null,
            [CallerArgumentExpression(nameof(date))] string? expr = null) =>
            Result.Ensure(timeProvider: timeProvider).That(date).Past()
                .WithMessage(message).WithArgumentName(expr)
                .ToResult(date);

        /// <summary>
        /// Ensure that the provided date is in the future within the specified timespan compared to the current date.
        /// </summary>
        /// <param name="timespan">The timespan within which the date should be in the future.</param>
        /// <param name="timeProvider">Optional: the date time provider used.</param>
        /// <param name="message">Optional: failure message specific for this date.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming date.</param>
        /// <returns>Result containing either the incoming date or a <see cref="GuardFailure"/></returns>
        public Result<DateTime> WithinFuture(TimeSpan timespan, TimeProvider? timeProvider = null, string? message = null,
            [CallerArgumentExpression(nameof(date))] string? expr = null) =>
            Result.Ensure(timeProvider: timeProvider).That(date).WithinFuture(timespan)
                .WithMessage(message).WithArgumentName(expr)
                .ToResult(date);

        /// <summary>
        /// Ensure that the provided date is in the past within the specified timespan compared to the current date.
        /// </summary>
        /// <param name="timespan">The timespan within which the date should be in the past.</param>
        /// <param name="timeProvider">Optional: the date time provider used.</param>
        /// <param name="message">Optional: failure message specific for this date.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming date.</param>
        /// <returns>Result containing either the incoming date or a <see cref="GuardFailure"/></returns>
        public Result<DateTime> WithinPast(TimeSpan timespan, TimeProvider? timeProvider = null, string? message = null,
            [CallerArgumentExpression(nameof(date))] string? expr = null) =>
            Result.Ensure(timeProvider: timeProvider).That(date).WithinPast(timespan)
                .WithMessage(message).WithArgumentName(expr)
                .ToResult(date);

        /// <summary>
        /// Ensure that the provided date is within the specified timespan compared to the specified date.
        /// </summary>
        /// <param name="other">The date to compare against.</param>
        /// <param name="timespan">The timespan within which the date should be in the past.</param>
        /// <param name="message">Optional: failure message specific for this date.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming date.</param>
        /// <returns>Result containing either the incoming date or a <see cref="GuardFailure"/></returns>
        public Result<DateTime> Around(DateTime other, TimeSpan timespan, string? message = null,
            [CallerArgumentExpression(nameof(date))] string? expr = null) =>
            Result.Ensure().That(date).Around(other, timespan)
                .WithMessage(message).WithArgumentName(expr)
                .ToResult(date);

        /// <summary>
        /// Ensure that the provided date falls on the specified day of the week.
        /// </summary>
        /// <param name="dayOfWeek">The expected day of the week</param>
        /// <param name="message">Optional: failure message specific for this date.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming date.</param>
        /// <returns>Result containing either the incoming date or a <see cref="GuardFailure"/></returns>
        public Result<DateTime> OnDayOfWeek(DayOfWeek dayOfWeek, string? message = null,
            [CallerArgumentExpression(nameof(date))] string? expr = null) =>
            Result.Ensure().That(date).OnDayOfWeek(dayOfWeek)
                .WithMessage(message).WithArgumentName(expr)
                .ToResult(date);

        /// <summary>
        /// Ensure that the provided date falls on one of the specified days of the week.
        /// </summary>
        /// <param name="allowedDaysOfWeek">The allowed days of the week</param>
        /// <param name="message">Optional: failure message specific for this date.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming date.</param>
        /// <returns>Result containing either the incoming date or a <see cref="GuardFailure"/></returns>
        public Result<DateTime> OnDaysOfWeek(DayOfWeek[] allowedDaysOfWeek, string? message = null,
            [CallerArgumentExpression(nameof(date))] string? expr = null) =>
            Result.Ensure().That(date).OnDaysOfWeek(allowedDaysOfWeek)
                .WithMessage(message).WithArgumentName(expr)
                .ToResult(date);

        /// <summary>
        /// Ensure that the provided date falls on a weekday (monday through friday).
        /// </summary>
        /// <param name="message">Optional: failure message specific for this date.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming date.</param>
        /// <returns>Result containing either the incoming date or a <see cref="GuardFailure"/></returns>
        public Result<DateTime> OnWeekday(string? message = null,
            [CallerArgumentExpression(nameof(date))] string? expr = null) =>
            Result.Ensure().That(date).OnWeekday()
                .WithMessage(message).WithArgumentName(expr)
                .ToResult(date);

        /// <summary>
        /// Ensure that the provided date falls on a weekend (saturday or sunday).
        /// </summary>
        /// <param name="message">Optional: failure message specific for this date.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming date.</param>
        /// <returns>Result containing either the incoming date or a <see cref="GuardFailure"/></returns>
        public Result<DateTime> OnWeekend(string? message = null,
            [CallerArgumentExpression(nameof(date))] string? expr = null) =>
            Result.Ensure().That(date).OnWeekend()
                .WithMessage(message).WithArgumentName(expr)
                .ToResult(date);
    }
}