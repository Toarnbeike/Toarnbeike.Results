using System.Numerics;
using System.Runtime.CompilerServices;
using Toarnbeike.Results.Ensure.Rules;

namespace Toarnbeike.Results.Ensure.Extensions;

public static class FloatingPointExtensions
{
    // The extensions below are specific for floating point values
    // since these checkt the amount of decimal places of the value.
    extension<TFloatingPoint>(TFloatingPoint value) where TFloatingPoint : struct, IFloatingPointIeee754<TFloatingPoint>
    {
        /// <summary>
        /// Ensure that the provided value is strictly greater than the provided minimum.
        /// </summary>
        /// <param name="min">The comparison. The provided value must be strictly greater than this value.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TFloatingPoint> GreaterThan(TFloatingPoint min, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).GreaterThan(min)
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value is greater than or equal to the provided minimum.
        /// </summary>
        /// <remarks>
        /// The comparison takes into account a small tolerance for floating point values,
        /// to avoid false positives due to jittering.
        /// The default tolerance can be overwritten using the <paramref name="tolerance"/> parameter.
        /// </remarks>
        /// <param name="min">The comparison. The provided value must be greater than or equal to this value.</param>
        /// <param name="tolerance">Optional: overwrite the default tolerance.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TFloatingPoint> AtLeast(TFloatingPoint min, TFloatingPoint? tolerance = null, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).AtLeast(min, tolerance)
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value is less than or equal to the provided minimum.
        /// </summary>
        /// <remarks>
        /// The comparison takes into account a small tolerance for floating point values,
        /// to avoid false positives due to jittering.
        /// The default tolerance can be overwritten using the <paramref name="tolerance"/> parameter.
        /// </remarks>
        /// <param name="max">The comparison. The provided value must be less than or equal to this value.</param>
        /// <param name="tolerance">Optional: overwrite the default tolerance.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TFloatingPoint> AtMost(TFloatingPoint max, TFloatingPoint? tolerance = null, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).AtMost(max, tolerance)
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value is strictly less than the provided maximum.
        /// </summary>
        /// <param name="max">The comparison. The provided value must be strictly less than this value.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TFloatingPoint> LessThan(TFloatingPoint max, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).LessThan(max)
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value is between the provided lower and upper bound.
        /// </summary>
        /// <remarks>
        /// The comparison takes into account a small tolerance for floating point values,
        /// to avoid false positives due to jittering.
        /// The default tolerance can be overwritten using the <paramref name="tolerance"/> parameter.
        /// </remarks>
        /// <param name="min">Lower bound. The provided value must be greater than or equal to this value.</param>
        /// <param name="max">Upper bound. The provided value must be less than or equal to this value.</param>
        /// <param name="tolerance">Optional: overwrite the default tolerance.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TFloatingPoint> Between(TFloatingPoint min, TFloatingPoint max, TFloatingPoint? tolerance = null, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).Between(min, max, tolerance)
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value is positive, that is, strictly greater than 0.
        /// </summary>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TFloatingPoint> Positive(string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).Positive()
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value is at least 0.
        /// </summary>
        /// <remarks>
        /// The comparison takes into account a small tolerance for floating point values,
        /// to avoid false positives due to jittering.
        /// The default tolerance can be overwritten using the <paramref name="tolerance"/> parameter.
        /// </remarks>
        /// <param name="tolerance">Optional: overwrite the default tolerance.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TFloatingPoint> AtLeastZero(TFloatingPoint? tolerance = null, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).AtLeastZero(tolerance)
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value is at most 0.
        /// </summary>
        /// <remarks>
        /// The comparison takes into account a small tolerance for floating point values,
        /// to avoid false positives due to jittering.
        /// The default tolerance can be overwritten using the <paramref name="tolerance"/> parameter.
        /// </remarks>
        /// <param name="tolerance">Optional: overwrite the default tolerance.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TFloatingPoint> AtMostZero(TFloatingPoint? tolerance = null, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).AtMostZero(tolerance)
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value is negative, that is, strictly less than 0.
        /// </summary>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TFloatingPoint> Negative(string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).Negative()
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value is equal to 0.
        /// </summary>
        /// <remarks>
        /// The comparison takes into account a small tolerance for floating point values,
        /// to avoid false positives due to jittering.
        /// The default tolerance can be overwritten using the <paramref name="tolerance"/> parameter.
        /// </remarks>
        /// <param name="tolerance">Optional: overwrite the default tolerance.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TFloatingPoint> Zero(TFloatingPoint? tolerance = null, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).Zero(tolerance)
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value is not equal to 0.
        /// </summary>
        /// <remarks>
        /// The comparison takes into account a small tolerance for floating point values,
        /// to avoid false positives due to jittering.
        /// The default tolerance can be overwritten using the <paramref name="tolerance"/> parameter.
        /// </remarks>
        /// <param name="tolerance">Optional: overwrite the default tolerance.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TFloatingPoint> NotZero(TFloatingPoint? tolerance = null, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).NotZero(tolerance)
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value is a multiple of the provided factor.
        /// </summary>
        /// <remarks>
        /// The comparison takes into account a small tolerance for floating point values,
        /// to avoid false positives due to jittering.
        /// The default tolerance can be overwritten using the <paramref name="tolerance"/> parameter.
        /// </remarks>
        /// <param name="factor">The divisor this number must factor into at least once.</param>
        /// <param name="tolerance">Optional: overwrite the default tolerance.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TFloatingPoint> MultipleOf(TFloatingPoint factor, TFloatingPoint? tolerance = null, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).MultipleOf(factor, tolerance)
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value is a whole number, eg, has no decimal places.
        /// </summary>
        /// <remarks>
        /// The comparison takes into account a small tolerance for floating point values,
        /// to avoid false positives due to jittering.
        /// The default tolerance can be overwritten using the <paramref name="tolerance"/> parameter.
        /// </remarks>
        /// <param name="tolerance">Optional: overwrite the default tolerance.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TFloatingPoint> WholeNumber(TFloatingPoint? tolerance = null, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).WholeNumber(tolerance)
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value has no more that the provided decimal places.
        /// </summary>
        /// <remarks>
        /// The comparison takes into account a small tolerance for floating point values,
        /// to avoid false positives due to jittering.
        /// The default tolerance can be overwritten using the <paramref name="tolerance"/> parameter.
        /// </remarks>
        /// <param name="maxPlaces">The maximum allowed number of decimal places.</param>
        /// <param name="tolerance">Optional: overwrite the default tolerance.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TFloatingPoint> MaxDecimalPlaces(int maxPlaces, TFloatingPoint? tolerance = null, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).MaxDecimalPlaces(maxPlaces, tolerance)
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value is finite, that is, not NaN and not infinity.
        /// </summary>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TFloatingPoint> Finite(string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).Finite()
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value is not NaN.
        /// </summary>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TFloatingPoint> NotNaN(string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).NotNaN()
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);
    }
}