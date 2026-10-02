using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Rules;

internal static class EnumRules
{
    public static ValidationRule<TEnum> IsDefined<TEnum>(string? message)
        where TEnum : struct, Enum =>
        new(value => Enum.IsDefined(value),
        message ?? $"Value must be a defined member of the {typeof(TEnum).Name} enum."
    );

    public static ValidationRule<TEnum> OneOf<TEnum>(TEnum[] acceptedValues, string? message)
        where TEnum : struct, Enum =>
        new(value => acceptedValues.Contains(value),
        message ?? $"Value must be one of [{string.Join(", ", acceptedValues.Select(v => v.ToString()))}]."
    );

    public static ValidationRule<TEnum> NotOneOf<TEnum>(TEnum[] rejectedValues, string? message)
        where TEnum : struct, Enum =>
        new(value => !rejectedValues.Contains(value),
        message ?? $"Value must not be one of [{string.Join(", ", rejectedValues.Select(v => v.ToString()))}]."
    );
}