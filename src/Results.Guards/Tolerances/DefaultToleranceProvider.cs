using System.Numerics;

namespace Toarnbeike.Results.Guards.Tolerances;

internal sealed class DefaultToleranceProvider : IToleranceProvider
{
    private const float FloatTolerance = 1e-6f;
    private const decimal DecimalTolerance = 1e-9m;
    private const double DoubleTolerance = 1e-9d;
    private readonly Half _halfTolerance = (Half)0.01;

    public static IToleranceProvider Instance { get; } = new DefaultToleranceProvider();

    public TFloatingPoint GetTolerance<TFloatingPoint>() where TFloatingPoint : IFloatingPointIeee754<TFloatingPoint>
    {
        return typeof(TFloatingPoint) switch
        {
            var t when t == typeof(float) => (TFloatingPoint)(object)FloatTolerance,
            var t when t == typeof(double) => (TFloatingPoint)(object)DoubleTolerance,
            var t when t == typeof(decimal) => (TFloatingPoint)(object)DecimalTolerance,
            var t when t == typeof(Half) => (TFloatingPoint)(object)_halfTolerance,
            _ => TFloatingPoint.Zero
        };
    }

}