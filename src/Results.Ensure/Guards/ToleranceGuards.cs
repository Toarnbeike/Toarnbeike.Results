using System.Numerics;

namespace Toarnbeike.Results.Ensure.Guards;

internal static class ToleranceGuards
{
    public static bool AtLeast<T>(T value, T min, T tolerance) where T : struct, INumber<T> => 
        value.CompareTo(min - tolerance) >= 0;

    public static bool AtMost<T>(T value, T max, T tolerance) where T : struct, INumber<T> => 
        value.CompareTo(max + tolerance) <= 0;

    public static bool Equal<T>(T value, T other, T tolerance) where T : struct, INumber<T> => 
        value.CompareTo(other - tolerance) >= 0 && value.CompareTo(other + tolerance) <= 0;

    public static bool NotEqual<T>(T value, T other, T tolerance) where T : struct, INumber<T> => 
        value.CompareTo(other - tolerance) < 0 || value.CompareTo(other + tolerance) > 0;

    public static bool MultipleOf<T>(T value, T factor, T tolerance) where T : struct, INumber<T>
    {
        var remainder = value % factor;
        return T.Abs(remainder) <= tolerance || T.Abs(remainder-factor) <= tolerance;
    }
}