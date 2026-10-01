using System.Numerics;

namespace Toarnbeike.Results.Rules.Tolerances;

public interface IToleranceProvider
{
    TFloatingPoint GetTolerance<TFloatingPoint>() 
        where TFloatingPoint : IFloatingPointIeee754<TFloatingPoint>;
}