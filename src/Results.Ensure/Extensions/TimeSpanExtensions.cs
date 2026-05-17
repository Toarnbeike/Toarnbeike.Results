using System.Runtime.CompilerServices;
using Toarnbeike.Results.Ensure.Rules;

namespace Toarnbeike.Results.Ensure.Extensions;

public static class TimeSpanExtensions
{
    extension(TimeSpan timeSpan)
    {
        /// <summary>
        /// Ensure that the provided time span is longer than the provided threshold.
        /// </summary>
        /// <param name="other">The time span to compare against.</param>
        /// <param name="message">Optional: failure message specific for this time span.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming time span.</param>
        /// <returns>Result containing either the incoming time span or a <see cref="GuardFailure"/></returns>
        public Result<TimeSpan> AtLeast(TimeSpan other, string? message = null,
            [CallerArgumentExpression(nameof(timeSpan))] string? expr = null) =>
            Result.Ensure().That(timeSpan).AtLeast(other)
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(timeSpan);

        /// <summary>
        /// Ensure that the provided time span is shorter than the provided threshold.
        /// </summary>
        /// <param name="other">The time span to compare against.</param>
        /// <param name="message">Optional: failure message specific for this time span.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming time span.</param>
        /// <returns>Result containing either the incoming time span or a <see cref="GuardFailure"/></returns>
        public Result<TimeSpan> AtMost(TimeSpan other, string? message = null,
            [CallerArgumentExpression(nameof(timeSpan))] string? expr = null) =>
            Result.Ensure().That(timeSpan).AtMost(other)
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(timeSpan);

        /// <summary>
        /// Ensure that the provided time span is between the provided upper and lower threshold.
        /// </summary>
        /// <param name="min">The minimum duration of the time span.</param>
        /// <param name="max">The maximum duration of the time span.</param>
        /// <param name="message">Optional: failure message specific for this time span.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming time span.</param>
        /// <returns>Result containing either the incoming time span or a <see cref="GuardFailure"/></returns>
        public Result<TimeSpan> Between(TimeSpan min, TimeSpan max, string? message = null,
            [CallerArgumentExpression(nameof(timeSpan))] string? expr = null) =>
            Result.Ensure().That(timeSpan).Between(min, max)
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(timeSpan);

        /// <summary>
        /// Ensure that the provided time span is close to the specified timespan, which the specified tolerance.
        /// </summary>
        /// <param name="expected">The expected duration of the time span.</param>
        /// <param name="tolerance">The maximum tolerance for the time span.</param>
        /// <param name="message">Optional: failure message specific for this time span.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming time span.</param>
        /// <returns>Result containing either the incoming time span or a <see cref="GuardFailure"/></returns>
        public Result<TimeSpan> CloseTo(TimeSpan expected, TimeSpan tolerance, string? message = null,
            [CallerArgumentExpression(nameof(timeSpan))] string? expr = null) =>
            Result.Ensure().That(timeSpan).CloseTo(expected, tolerance)
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(timeSpan);

        /// <summary>
        /// Ensure that the provided time span is longer than zero.
        /// </summary>
        /// <param name="message">Optional: failure message specific for this time span.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming time span.</param>
        /// <returns>Result containing either the incoming time span or a <see cref="GuardFailure"/></returns>
        public Result<TimeSpan> AtLeastZero(string? message = null,
            [CallerArgumentExpression(nameof(timeSpan))] string? expr = null) =>
            Result.Ensure().That(timeSpan).AtLeastZero()
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(timeSpan);

        /// <summary>
        /// Ensure that the provided time span is shorter than zero.
        /// </summary>
        /// <param name="message">Optional: failure message specific for this time span.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming time span.</param>
        /// <returns>Result containing either the incoming time span or a <see cref="GuardFailure"/></returns>
        public Result<TimeSpan> AtMostZero(string? message = null,
            [CallerArgumentExpression(nameof(timeSpan))] string? expr = null) =>
            Result.Ensure().That(timeSpan).AtMostZero()
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(timeSpan);

        /// <summary>
        /// Ensure that the provided time span is not equal to zero.
        /// </summary>
        /// <param name="message">Optional: failure message specific for this time span.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming time span.</param>
        /// <returns>Result containing either the incoming time span or a <see cref="GuardFailure"/></returns>
        public Result<TimeSpan> NotZero(string? message = null,
            [CallerArgumentExpression(nameof(timeSpan))] string? expr = null) =>
            Result.Ensure().That(timeSpan).NotZero()
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(timeSpan);
    }
}