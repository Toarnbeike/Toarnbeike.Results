namespace Toarnbeike.Results.Rules;

public static class EnumRules
{
    public static Rule<TEnum> IsDefined<TEnum>(string? message)
        where TEnum : struct, Enum =>
        new(value => Enum.IsDefined(value),
            message ?? $"Value must be a defined value of the {typeof(TEnum).Name} enum.");

    public static Rule<TEnum> OneOf<TEnum>(TEnum[] acceptedValues, string? message)
        where TEnum : struct, Enum
    {
        ArgumentNullException.ThrowIfNull(acceptedValues);
        return new(value => acceptedValues.Contains(value),
            message ?? $"Value must be one of [{string.Join(", ", acceptedValues.Select(v => v.ToString()))}].");
    }

    public static Rule<TEnum> NotOneOf<TEnum>(TEnum[] rejectedValues, string? message)
        where TEnum : struct, Enum
    {
        ArgumentNullException.ThrowIfNull(rejectedValues);
        return new(value => !rejectedValues.Contains(value),
            message ?? $"Value must not be one of [{string.Join(", ", rejectedValues.Select(v => v.ToString()))}].");
    }
}