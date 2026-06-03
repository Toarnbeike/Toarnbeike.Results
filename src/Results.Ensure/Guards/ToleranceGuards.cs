using System.Numerics;

namespace Toarnbeike.Results.Ensure.Guards;

internal static class ToleranceGuards
{
    public static bool AtLeast<T>(T value, T min, T tolerance) where T : struct, INumber<T>
    {
        ThrowOnInvalidTolerance(tolerance);
        return value.CompareTo(min - tolerance) >= 0;
    }

    public static bool AtMost<T>(T value, T max, T tolerance) where T : struct, INumber<T>
    {
        ThrowOnInvalidTolerance(tolerance);
        return value.CompareTo(max + tolerance) <= 0;
    }

    public static bool Equal<T>(T value, T other, T tolerance) where T : struct, INumber<T>
    {
        ThrowOnInvalidTolerance(tolerance);
        return value.CompareTo(other - tolerance) >= 0 && value.CompareTo(other + tolerance) <= 0;
    }

    public static bool NotEqual<T>(T value, T other, T tolerance) where T : struct, INumber<T>
    {
        ThrowOnInvalidTolerance(tolerance);
        return value.CompareTo(other - tolerance) < 0 || value.CompareTo(other + tolerance) > 0;
    }

    private static void ThrowOnInvalidTolerance<T>(T tolerance) where T : struct, INumber<T>
    {
        if (tolerance < T.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(tolerance), tolerance,
                "Provided tolerance is negative. This changes semantics and causes unexpected guard behaviour. " +
                "Modify implementation if negative tolerance was deliberate, or verify before forwarding negative value.");
        }
    }
}