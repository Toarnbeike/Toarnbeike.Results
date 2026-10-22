using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Rules.Extensions;

/// <summary>
/// Provides fluent validation operations for enum values on an <see cref="IRuleTarget{T, TEnum}"/>.
/// </summary>
/// <remarks>Supports checking if the enum value is defined, one of a set of accepted values, or not one of a set of rejected values.</remarks>
public static class EnumExtensions
{
    /// <typeparam name="T">The type associated with the rule target and validation result.</typeparam>
    /// <typeparam name="TEnum">The type of the enum value being evaluated by the rule.</typeparam>
    /// <param name="target">The rule target for <typeparamref name="TEnum"/> values.</param>
    extension<T, TEnum>(IRuleTarget<T, TEnum> target)
        where TEnum : struct, Enum
    {
        /// <summary>
        /// Verifies that the enum value is a defined value of the enum type.
        /// </summary>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the enum value is a defined value of the enum type.</returns>
        public Result<T> IsDefined(string? message = null) =>
            target.Apply(EnumRules.IsDefined<TEnum>(), message);

        /// <summary>
        /// Verifies that the enum value is one of the specified accepted values.
        /// </summary>
        /// <param name="acceptedValues">The set of accepted enum values.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the enum value is one of the specified accepted values.</returns>
        public Result<T> OneOf(TEnum[] acceptedValues, string? message = null) =>
            target.Apply(EnumRules.OneOf(acceptedValues), message);

        /// <summary>
        /// Verifies that the enum value is not one of the specified rejected values.
        /// </summary>
        /// <param name="rejectedValues">The set of rejected enum values.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the enum value is not one of the specified rejected values.</returns>
        public Result<T> NotOneOf(TEnum[] rejectedValues, string? message = null) =>
            target.Apply(EnumRules.NotOneOf(rejectedValues), message);
    }
}
