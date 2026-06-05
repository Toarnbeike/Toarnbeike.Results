namespace Toarnbeike.Results.Guards.Core.Guards;

internal static class EnumGuards
{
    public static bool IsDefined<TEnum>(TEnum value) where TEnum : struct, Enum => 
        Enum.IsDefined(value);

    public static bool OneOf<TEnum>(TEnum value, TEnum[] validValues) where TEnum : struct, Enum =>
        validValues.Contains(value);

    public static bool NotOneOf<TEnum>(TEnum value, TEnum[] invalidValues) where TEnum : struct, Enum =>
        !invalidValues.Contains(value);
}