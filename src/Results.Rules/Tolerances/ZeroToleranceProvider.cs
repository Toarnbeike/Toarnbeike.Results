using System.Numerics;

namespace Toarnbeike.Results.Rules.Tolerances;

internal sealed class ZeroToleranceProvider : IToleranceProvider
{
    public TFloatingPoint GetTolerance<TFloatingPoint>() where TFloatingPoint : IFloatingPointIeee754<TFloatingPoint>
    {
        return TFloatingPoint.Zero;
    }
}