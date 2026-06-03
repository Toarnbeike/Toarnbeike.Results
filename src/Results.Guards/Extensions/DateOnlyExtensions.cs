using System.Globalization;
using System.Runtime.CompilerServices;
using Toarnbeike.Results.Guards.Rules;

namespace Toarnbeike.Results.Guards.Extensions;

public static class DateOnlyExtensions
{
    extension(DateOnly date)
    {
        /// <summary>
        /// Ensure that the provided date is on or after the provided threshold.
        /// </summary>
        /// <param name="other">The date to compare against.</param>
        /// <param name="message">Optional: failure message specific for this date.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming date.</param>
        /// <returns>Result containing either the incoming date or a <see cref="GuardFailure"/></returns>
        public Result<DateOnly> OnOrAfter(DateOnly other, string? message = null,
            [CallerArgumentExpression(nameof(date))] string? expr = null) =>
                Result.Ensure().That(date).OnOrAfter(other)
                .WithCustomMessage(message).WithCustomExpression(expr)
                .ToResult();

        /// <summary>
        /// Ensure that the provided date is on or before the provided threshold.
        /// </summary>
        /// <param name="other">The date to compare against.</param>
        /// <param name="message">Optional: failure message specific for this date.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming date.</param>
        /// <returns>Result containing either the incoming date or a <see cref="GuardFailure"/></returns>
        public Result<DateOnly> OnOrBefore(DateOnly other, string? message = null,
            [CallerArgumentExpression(nameof(date))] string? expr = null) =>
            Result.Ensure().That(date).OnOrBefore(other)
                .WithCustomMessage(message).WithCustomExpression(expr)
                .ToResult();

        /// <summary>
        /// Ensure that the provided date is between the provided start and end dates.
        /// </summary>
        /// <param name="start">The start date of the range.</param>
        /// <param name="end">The end date of the range.</param>
        /// <param name="message">Optional: failure message specific for this date.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming date.</param>
        /// <returns>Result containing either the incoming date or a <see cref="GuardFailure"/></returns>
        public Result<DateOnly> Between(DateOnly start, DateOnly end, string? message = null,
            [CallerArgumentExpression(nameof(date))] string? expr = null) =>
            Result.Ensure().That(date).Between(start, end)
                .WithCustomMessage(message).WithCustomExpression(expr)
                .ToResult();

        /// <summary>
        /// Ensure that the provided date is in the future compared to the current date.
        /// </summary>
        /// <param name="timeProvider">Optional: the date time provider used.</param>
        /// <param name="message">Optional: failure message specific for this date.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming date.</param>
        /// <returns>Result containing either the incoming date or a <see cref="GuardFailure"/></returns>
        public Result<DateOnly> Future(TimeProvider? timeProvider = null, string? message = null,
            [CallerArgumentExpression(nameof(date))] string? expr = null) =>
            Result.Ensure(timeProvider: timeProvider).That(date).Future()
                .WithCustomMessage(message).WithCustomExpression(expr)
                .ToResult();

        /// <summary>
        /// Ensure that the provided date is in the past compared to the current date.
        /// </summary>
        /// <param name="timeProvider">Optional: the date time provider used.</param>
        /// <param name="message">Optional: failure message specific for this date.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming date.</param>
        /// <returns>Result containing either the incoming date or a <see cref="GuardFailure"/></returns>
        public Result<DateOnly> Past(TimeProvider? timeProvider = null, string? message = null,
            [CallerArgumentExpression(nameof(date))] string? expr = null) =>
            Result.Ensure(timeProvider: timeProvider).That(date).Past()
                .WithCustomMessage(message).WithCustomExpression(expr)
                .ToResult();

        /// <summary>
        /// Ensure that the provided date is in the future within the specified days compared to the current date.
        /// </summary>
        /// <param name="days">The number of days within which the date should be in the past.</param>
        /// <param name="timeProvider">Optional: the date time provider used.</param>
        /// <param name="message">Optional: failure message specific for this date.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming date.</param>
        /// <returns>Result containing either the incoming date or a <see cref="GuardFailure"/></returns>
        public Result<DateOnly> WithinFuture(int days, TimeProvider? timeProvider = null, string? message = null,
            [CallerArgumentExpression(nameof(date))] string? expr = null) =>
            Result.Ensure(timeProvider: timeProvider).That(date).WithinFuture(days)
                .WithCustomMessage(message).WithCustomExpression(expr)
                .ToResult();

        /// <summary>
        /// Ensure that the provided date is in the past within the specified days compared to the current date.
        /// </summary>
        /// <param name="days">The number of days within which the date should be in the past.</param>
        /// <param name="timeProvider">Optional: the date time provider used.</param>
        /// <param name="message">Optional: failure message specific for this date.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming date.</param>
        /// <returns>Result containing either the incoming date or a <see cref="GuardFailure"/></returns>
        public Result<DateOnly> WithinPast(int days, TimeProvider? timeProvider = null, string? message = null,
            [CallerArgumentExpression(nameof(date))] string? expr = null) =>
            Result.Ensure(timeProvider: timeProvider).That(date).WithinPast(days)
                .WithCustomMessage(message).WithCustomExpression(expr)
                .ToResult();

        /// <summary>
        /// Ensure that the provided date falls on the specified day of the week.
        /// </summary>
        /// <param name="dayOfWeek">The expected day of the week</param>
        /// <param name="message">Optional: failure message specific for this date.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming date.</param>
        /// <returns>Result containing either the incoming date or a <see cref="GuardFailure"/></returns>
        public Result<DateOnly> OnDayOfWeek(DayOfWeek dayOfWeek, string? message = null,
            [CallerArgumentExpression(nameof(date))] string? expr = null) =>
            Result.Ensure().That(date).OnDayOfWeek(dayOfWeek)
                .WithCustomMessage(message).WithCustomExpression(expr)
                .ToResult();

        /// <summary>
        /// Ensure that the provided date falls on one of the specified days of the week.
        /// </summary>
        /// <param name="allowedDaysOfWeek">The allowed days of the week</param>
        /// <param name="message">Optional: failure message specific for this date.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming date.</param>
        /// <returns>Result containing either the incoming date or a <see cref="GuardFailure"/></returns>
        public Result<DateOnly> OnDaysOfWeek(DayOfWeek[] allowedDaysOfWeek, string? message = null,
            [CallerArgumentExpression(nameof(date))] string? expr = null) =>
            Result.Ensure().That(date).OnDaysOfWeek(allowedDaysOfWeek)
                .WithCustomMessage(message).WithCustomExpression(expr)
                .ToResult();

        /// <summary>
        /// Ensure that the provided date falls on a weekday (monday through friday).
        /// </summary>
        /// <param name="message">Optional: failure message specific for this date.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming date.</param>
        /// <returns>Result containing either the incoming date or a <see cref="GuardFailure"/></returns>
        public Result<DateOnly> OnWeekday(string? message = null,
            [CallerArgumentExpression(nameof(date))] string? expr = null) =>
            Result.Ensure().That(date).OnWeekday()
                .WithCustomMessage(message).WithCustomExpression(expr)
                .ToResult();

        /// <summary>
        /// Ensure that the provided date falls on a weekend (saturday or sunday).
        /// </summary>
        /// <param name="message">Optional: failure message specific for this date.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming date.</param>
        /// <returns>Result containing either the incoming date or a <see cref="GuardFailure"/></returns>
        public Result<DateOnly> OnWeekend(string? message = null,
            [CallerArgumentExpression(nameof(date))] string? expr = null) =>
            Result.Ensure().That(date).OnWeekend()
                .WithCustomMessage(message).WithCustomExpression(expr)
                .ToResult();
    }
}