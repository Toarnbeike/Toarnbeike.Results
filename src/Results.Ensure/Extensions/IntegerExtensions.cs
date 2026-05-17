using System.Numerics;
using System.Runtime.CompilerServices;
using Toarnbeike.Results.Ensure.Rules;

namespace Toarnbeike.Results.Ensure.Extensions;

public static class IntegerExtensions
{
    extension<TInteger>(TInteger value) where TInteger : struct, IBinaryInteger<TInteger>
    {
        /// <summary>
        /// Ensure that the provided value is strictly greater than the provided minimum.
        /// </summary>
        /// <param name="min">The comparison. The provided value must be strictly greater than this value.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TInteger> GreaterThan(TInteger min, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).GreaterThan(min)
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value is greater than or equal to the provided minimum.
        /// </summary>
        /// <param name="min">The comparison. The provided value must be greater than or equal to this value.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TInteger> AtLeast(TInteger min, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).AtLeast(min)
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value is less than or equal to the provided minimum.
        /// </summary>
        /// <param name="max">The comparison. The provided value must be less than or equal to this value.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TInteger> AtMost(TInteger max, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).AtMost(max)
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value is strictly less than the provided maximum.
        /// </summary>
        /// <param name="max">The comparison. The provided value must be strictly less than this value.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TInteger> LessThan(TInteger max, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).LessThan(max)
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value is between the provided lower and upper bound.
        /// </summary>
        /// <param name="min">Lower bound. The provided value must be greater than or equal to this value.</param>
        /// <param name="max">Upper bound. The provided value must be less than or equal to this value.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TInteger> Between(TInteger min, TInteger max, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).Between(min, max)
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value is positive, that is, strictly greater than 0.
        /// </summary>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TInteger> Positive(string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).Positive()
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value is at least 0.
        /// </summary>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TInteger> AtLeastZero(string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).AtLeastZero()
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value is at most 0.
        /// </summary>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TInteger> AtMostZero(string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).AtMostZero()
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value is negative, that is, strictly less than 0.
        /// </summary>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TInteger> Negative(string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).Negative()
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value is not equal to 0.
        /// </summary>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TInteger> NotZero(string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).NotZero()
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value is a multiple of the provided factor.
        /// </summary>
        /// <param name="factor">The divisor this number must factor into at least once.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TInteger> MultipleOf(TInteger factor, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).MultipleOf(factor)
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value is even.
        /// </summary>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TInteger> Even(string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).Even()
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value is odd.
        /// </summary>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TInteger> Odd(string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).Odd()
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value is a power of 2.
        /// </summary>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TInteger> PowerOf2(string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).PowerOf2()
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value is a prime number.
        /// </summary>
        /// <remarks>
        /// Checking for primality is more expensive than other guards, so use this rule only when necessary.
        /// The implementation uses trial division, which is fine for small integers but may not be suitable for large values.
        /// </remarks>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TInteger> Prime(string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).Prime()
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);
    }
}