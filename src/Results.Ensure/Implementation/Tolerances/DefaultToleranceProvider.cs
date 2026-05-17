using Toarnbeike.Results.Ensure.Abstractions;

namespace Toarnbeike.Results.Ensure.Implementation.Tolerances;

internal sealed class DefaultToleranceProvider : IToleranceProvider
{
    public float FloatTolerance => 1e-6f;
    public decimal DecimalTolerance => 1e-9m;
    public double DoubleTolerance => 1e-9d;
    public Half HalfTolerance => Half.Parse("1e-2");
}
