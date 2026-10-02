using System.Numerics;

namespace Toarnbeike.Results.Validation.Implementation;

internal static class ToleranceHelper
{
    public static void ThrowOnInvalidTolerance<T>(T tolerance) where T : struct, INumber<T>
    {
        if (tolerance < T.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(tolerance), tolerance,
                "Provided tolerance is negative. This changes semantics and causes unexpected rule behaviour. " +
                "Modify implementation if negative tolerance was deliberate, or verify before forwarding negative value.");
        }
    }

    public static TFloating Default<TFloating>()
        where TFloating : struct, IFloatingPointIeee754<TFloating>
    {
        return typeof(TFloating) switch
        {
            var t when t == typeof(float) => (TFloating)(object)1e-6f,
            var t when t == typeof(double) => (TFloating)(object)1e-9d,
            var t when t == typeof(decimal) => (TFloating)(object)1e-9m,
            var t when t == typeof(Half) => (TFloating)(object)Half.Parse("1e-2"),
            _ => TFloating.Zero
        };
    }
}