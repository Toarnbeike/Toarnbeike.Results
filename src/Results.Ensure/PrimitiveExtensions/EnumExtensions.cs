using System.Runtime.CompilerServices;

namespace Toarnbeike.Results.Ensure.PrimitiveExtensions;

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
            [CallerArgumentExpression(nameof(value))] string? expr = null)
        {
            var result = Guards.IsDefined(value);
            return result.IsValid ? value : new GuardFailure(result, nameof(IsDefined), expr, value, message);
        }
    }
}
