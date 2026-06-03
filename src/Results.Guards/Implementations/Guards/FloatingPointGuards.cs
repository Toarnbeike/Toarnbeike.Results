using System.Numerics;

namespace Toarnbeike.Results.Guards.Implementations.Guards;

internal static class FloatingPointGuards
{
    public static bool MultipleOf<T>(T value, T factor, T tolerance) where T : struct, IFloatingPointIeee754<T>
    {
        ThrowOnInvalidTolerance(tolerance);
        var quotient = value / factor;
        var nearest = T.Round(quotient);

        return T.Abs(value - nearest * factor) <= tolerance;
    }

    public static bool Finite<T>(T value) where T : IFloatingPointIeee754<T> => T.IsFinite(value);
    public static bool NotNaN<T>(T value) where T : IFloatingPointIeee754<T> => !T.IsNaN(value);

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