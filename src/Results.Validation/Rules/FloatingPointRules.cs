using System.Numerics;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Rules;

internal static class FloatingPointRules
{
    public static bool MultipleOf<T>(T value, T factor, T tolerance) where T : struct, IFloatingPointIeee754<T>
    {
        ToleranceHelper.ThrowOnInvalidTolerance(tolerance);
        var quotient = value / factor;
        var nearest = T.Round(quotient);

        return T.Abs(value - nearest * factor) <= tolerance;
    }

    public static bool Finite<T>(T value) where T : IFloatingPointIeee754<T> => T.IsFinite(value);
    public static bool NotNaN<T>(T value) where T : IFloatingPointIeee754<T> => !T.IsNaN(value);

}
