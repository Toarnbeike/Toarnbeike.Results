using System.Numerics;

namespace Toarnbeike.Results.Guards.Tolerances;

public interface IToleranceProvider
{
    TFloatingPoint GetTolerance<TFloatingPoint>() where TFloatingPoint : IFloatingPointIeee754<TFloatingPoint>;
}