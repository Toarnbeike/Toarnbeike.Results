using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class EnumValidationRuleBuilderExtensions
{
    extension<T, TEnum>(ValidationRuleBuilder<T, TEnum> builder)
    where TEnum : struct, Enum
    {
        /// <summary>
        /// Validates that the enum value is a defined value of the enum type.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TEnum> IsDefined(string? message = null) =>
            builder.Add(EnumRules.IsDefined<TEnum>(), message);

        /// <summary>
        /// Validates that the enum value is one of the specified accepted values.
        /// </summary>
        /// <param name="acceptedValues">The set of accepted enum values.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TEnum> OneOf(TEnum[] acceptedValues, string? message = null) =>
            builder.Add(EnumRules.OneOf(acceptedValues), message);

        /// <summary>
        /// Validates that the enum value is not one of the specified rejected values.
        /// </summary>
        /// <param name="rejectedValues">The set of rejected enum values.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, TEnum> NotOneOf(TEnum[] rejectedValues, string? message = null) =>
            builder.Add(EnumRules.NotOneOf(rejectedValues), message);
    }
}
