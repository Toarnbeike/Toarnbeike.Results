using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Ensure.Extensions;

public static class EnumExtensions
{
    extension<T, TEnum>(EnsureTarget<T, TEnum> target)
        where TEnum : struct, Enum
    {
        /// <summary>
        /// Ensures that the enum value is a defined value of the enum type.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> IsDefined(string? message = null) =>
            target.Apply(EnumRules.IsDefined<TEnum>(message));

        /// <summary>
        /// Ensures that the enum value is one of the specified accepted values.
        /// </summary>
        /// <param name="acceptedValues">The set of accepted enum values.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> OneOf(TEnum[] acceptedValues, string? message = null) =>
            target.Apply(EnumRules.OneOf(acceptedValues, message));

        /// <summary>
        /// Ensures that the enum value is not one of the specified rejected values.
        /// </summary>
        /// <param name="rejectedValues">The set of rejected enum values.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> NotOneOf(TEnum[] rejectedValues, string? message = null) =>
            target.Apply(EnumRules.NotOneOf(rejectedValues, message));
    }
}
