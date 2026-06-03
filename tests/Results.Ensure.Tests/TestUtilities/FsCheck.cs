using FsCheck;
using FsCheck.Fluent;

namespace Toarnbeike.Results.Ensure.Tests.TestUtilities;

public class FiniteDoubleArbitrary
{
    public static Arbitrary<double> FiniteDouble()
    {
        return ArbMap.Default.GeneratorFor<double>()
            .Where(x =>
                !double.IsNaN(x) &&
                !double.IsInfinity(x))
            .ToArbitrary();
    }
}

public class NonZeroFiniteDoubleArbitrary
{
    public static Arbitrary<double> NonZeroFiniteDouble()
    {
        return ArbMap.Default.GeneratorFor<double>()
            .Where(x =>
                !double.IsNaN(x) &&
                !double.IsInfinity(x) &&
                x != 0.0)
            .ToArbitrary();
    }
}

public class PositiveFiniteDoubleArbitrary
{
    public static Arbitrary<double> PositiveFiniteDouble()
    {
        return ArbMap.Default.GeneratorFor<double>()
            .Where(x =>
                !double.IsNaN(x) &&
                !double.IsInfinity(x) &&
                x > 0.0)
            .ToArbitrary();
    }
}