using System.Numerics;

namespace Toarnbeike.Results.Ensure.Abstractions;

public interface IToleranceProvider
{
    internal TFloatingPoint GetTolerance<TFloatingPoint>() where TFloatingPoint : IFloatingPointIeee754<TFloatingPoint>
    {
        return typeof(TFloatingPoint) switch
        {
            var t when t == typeof(float) => (TFloatingPoint)(object)FloatTolerance,
            var t when t == typeof(double) => (TFloatingPoint)(object)DoubleTolerance,
            var t when t == typeof(decimal) => (TFloatingPoint)(object)DecimalTolerance,
            var t when t == typeof(Half) => (TFloatingPoint)(object)HalfTolerance,
            _ => TFloatingPoint.Zero
        };
    }

    float FloatTolerance { get; }
    decimal DecimalTolerance { get; }
    double DoubleTolerance { get; }
    Half HalfTolerance { get; }
}