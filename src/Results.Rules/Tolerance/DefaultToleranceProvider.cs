using System.Numerics;

namespace Toarnbeike.Results.Rules.Tolerance;

/// <summary>
/// A default tolerance provider that returns predefined tolerance values for specific numeric types.
/// </summary>
/// <remarks>
/// The following tolerance values are used for specific numeric types:
/// <para>- <c>float</c>: 1e-6</para>
/// <para>- <c>double</c>: 1e-9</para>
/// <para>- <c>decimal</c>: 1e-9</para>
/// <para>- <c>Half</c>: 1e-2</para>
/// </remarks>
internal sealed class DefaultToleranceProvider : IToleranceProvider
{
    /// <inheritdoc />
    public TValue GetTolerance<TValue>()
     where TValue : INumber<TValue>
    {
        return typeof(TValue) switch
        {
            var t when t == typeof(float) => (TValue)(object)1e-6f,
            var t when t == typeof(double) => (TValue)(object)1e-9d,
            var t when t == typeof(decimal) => (TValue)(object)1e-9m,
            var t when t == typeof(Half) => (TValue)(object)Half.Parse("1e-2"),
            _ => TValue.Zero
        };
    }
}