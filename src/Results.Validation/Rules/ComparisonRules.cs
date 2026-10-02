namespace Toarnbeike.Results.Validation.Rules;

internal static class ComparisonRules
{
    public static bool GreaterThan<T>(T value, T min) where T : IComparable<T> =>
        value.CompareTo(min) > 0;

    public static bool AtLeast<T>(T value, T min) where T : IComparable<T> =>
        value.CompareTo(min) >= 0;

    public static bool AtMost<T>(T value, T max) where T : IComparable<T> =>
        value.CompareTo(max) <= 0;

    public static bool LessThan<T>(T value, T max) where T : IComparable<T> =>
        value.CompareTo(max) < 0;

    public static bool Equal<T>(T value, T other) where T : IComparable<T> =>
        value.CompareTo(other) == 0;

    public static bool NotEqual<T>(T value, T other) where T : IComparable<T> =>
        value.CompareTo(other) != 0;
}
