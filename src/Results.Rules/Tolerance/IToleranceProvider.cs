using System.Numerics;

namespace Toarnbeike.Results.Rules.Tolerance;

/// <summary>
/// Provides a mechanism to retrieve tolerance values for numeric types.
/// </summary>
public interface IToleranceProvider
{
    /// <summary>
    /// Gets the tolerance value for the specified numeric type.
    /// </summary>
    /// <typeparam name="TValue">The numeric type.</typeparam>
    /// <returns>The tolerance value for the specified numeric type.</returns>
    public TValue GetTolerance<TValue>()
         where TValue : INumber<TValue>;
}