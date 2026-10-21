using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class EnumValidationTargetExtensions
{
    extension<T, TEnum>(ValidationTarget<T, TEnum> target)
        where TEnum : struct, Enum
    {
        /// <summary>
        /// Validates that the enum value is a defined value of the enum type.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> IsDefined(string? message = null) =>
            target.Apply(EnumRules.IsDefined<TEnum>(), message);

        /// <summary>
        /// Validates that the enum value is one of the specified accepted values.
        /// </summary>
        /// <param name="acceptedValues">The set of accepted enum values.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> OneOf(TEnum[] acceptedValues, string? message = null) =>
            target.Apply(EnumRules.OneOf(acceptedValues), message);

        /// <summary>
        /// Validates that the enum value is not one of the specified rejected values.
        /// </summary>
        /// <param name="rejectedValues">The set of rejected enum values.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> NotOneOf(TEnum[] rejectedValues, string? message = null) =>
            target.Apply(EnumRules.NotOneOf(rejectedValues), message);
    }
}
