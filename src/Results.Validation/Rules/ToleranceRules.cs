using System.Numerics;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Rules;

internal static class ToleranceRules
{
    public static bool AtLeast<T>(T value, T min, T tolerance) where T : struct, INumber<T>
    {
        ToleranceHelper.ThrowOnInvalidTolerance(tolerance);
        return value.CompareTo(min - tolerance) >= 0;
    }

    public static bool AtMost<T>(T value, T max, T tolerance) where T : struct, INumber<T>
    {
        ToleranceHelper.ThrowOnInvalidTolerance(tolerance);
        return value.CompareTo(max + tolerance) <= 0;
    }

    public static bool Equal<T>(T value, T other, T tolerance) where T : struct, INumber<T>
    {
        ToleranceHelper.ThrowOnInvalidTolerance(tolerance);
        return value.CompareTo(other - tolerance) >= 0 && value.CompareTo(other + tolerance) <= 0;
    }

    public static bool NotEqual<T>(T value, T other, T tolerance) where T : struct, INumber<T>
    {
        ToleranceHelper.ThrowOnInvalidTolerance(tolerance);
        return value.CompareTo(other - tolerance) < 0 || value.CompareTo(other + tolerance) > 0;
    }

}
