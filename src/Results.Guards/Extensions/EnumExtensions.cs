using System.Runtime.CompilerServices;
using Toarnbeike.Results.Guards.Rules;

namespace Toarnbeike.Results.Guards.Extensions;

public static class EnumExtensions
{
    extension<TEnum>(TEnum value) where TEnum : struct, Enum
    {
        /// <summary>
        /// Ensure that the provided value is defined on the enum.
        /// </summary>
        /// <param name="message">Optional: failure message specific for this enum.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming enum value.</param>
        /// <returns>Result containing either the incoming enum value or a <see cref="GuardFailure"/></returns>
        public Result<TEnum> IsDefined(string? message = null,
            [CallerArgumentExpression(nameof(value))]
            string? expr = null) =>
            Result.Ensure().That(value).IsDefined()
                .WithCustomExpression(expr).WithCustomMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value is defined on the enum.
        /// </summary>
        /// <param name="allowedValues">The allowed values for the enum.</param>
        /// <param name="message">Optional: failure message specific for this enum.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming enum value.</param>
        /// <returns>Result containing either the incoming enum value or a <see cref="GuardFailure"/></returns>
        public Result<TEnum> OneOf(IEnumerable<TEnum> allowedValues, string? message = null,
            [CallerArgumentExpression(nameof(value))]
            string? expr = null) =>
            Result.Ensure().That(value).OneOf([..allowedValues])
                .WithCustomExpression(expr).WithCustomMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value is defined on the enum.
        /// </summary>
        /// <param name="notAllowedValues">The values that are not allowed for the enum.</param>
        /// <param name="message">Optional: failure message specific for this enum.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming enum value.</param>
        /// <returns>Result containing either the incoming enum value or a <see cref="GuardFailure"/></returns>
        public Result<TEnum> NotOneOf(IEnumerable<TEnum> notAllowedValues, string? message = null,
            [CallerArgumentExpression(nameof(value))]
            string? expr = null) =>
            Result.Ensure().That(value).NotOneOf([..notAllowedValues])
                .WithCustomExpression(expr).WithCustomMessage(message)
                .ToResult(value);
    }
}
