using System.Numerics;

namespace Toarnbeike.Results.Rules.Tolerance;

/// <summary>
/// A tolerance provider that returns zero tolerance for all numeric types.
/// </summary>
internal sealed class ZeroToleranceProvider : IToleranceProvider
{
    /// <inheritdoc />
    public TValue GetTolerance<TValue>() 
         where TValue : INumber<TValue>
    {
        return TValue.Zero;
    }
}
