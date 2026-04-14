using System.Numerics;
using System.Runtime.CompilerServices;

namespace Toarnbeike.Results.Ensure.PrimitiveExtensions;

public static class NumberExtensions
{
    extension<TNumber>(TNumber value) where TNumber : INumber<TNumber>
    {
        /// <summary>
        /// Ensure that the provided value is greater than the provided minimum.
        /// </summary>
        /// <param name="min">The comparison. The provided value must be strictly greater than this value.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TNumber> GreaterThan(TNumber min, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null)
        {
            var result = Guards.GreaterThan(value, min);
            return result.IsValid ? value : new GuardFailure(result, nameof(GreaterThan), expr, value, message);
        }

        /// <summary>
        /// Ensure that the provided value is less than the provided maximum.
        /// </summary>
        /// <param name="max">The comparison. The provided value must be strictly less than this value.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TNumber> LessThan(TNumber max, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null)
        {
            var result = Guards.LessThan(value, max);
            return result.IsValid ? value : new GuardFailure(result, nameof(LessThan), expr, value, message);
        }

        /// <summary>
        /// Ensure that the provided value is between the provided lower and upper bound.
        /// </summary>
        /// <param name="min">Lower bound. The provided value must be greater than or equal to this value.</param>
        /// <param name="max">Upper bound. The provided value must be less than or equal to this value.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TNumber> InRange(TNumber min, TNumber max, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null)
        {
            var result = Guards.InRange(value, min, max);
            return result.IsValid ? value : new GuardFailure(result, nameof(InRange), expr, value, message);
        }
    }
}
