using Toarnbeike.Results.Rules.Tolerance;
using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Rules.Tests.ToleranceTests;

public class ZeroToleranceProviderTests
{
    [Test]
    public void GetTolerance_ShouldReturnZero_ForAllNumericTypes()
    {
        var provider = new ZeroToleranceProvider();

        int intTolerance = provider.GetTolerance<int>();
        intTolerance.ShouldBe(0);

        double doubleTolerance = provider.GetTolerance<double>();
        doubleTolerance.ShouldBe(0.0);

        decimal decimalTolerance = provider.GetTolerance<decimal>();
        decimalTolerance.ShouldBe(0.0m);

        float floatTolerance = provider.GetTolerance<float>();
        floatTolerance.ShouldBe(0.0f);
    }

    [Test, NotInParallel]
    public void EndToEndTest_WithZeroToleranceProvider()
    {
        RuleContext.UseZeroTolerance();

        var toleranceFromContext = RuleContext.Tolerance<double>();
        toleranceFromContext.ShouldBe(0.0);

        // Check if the FloatingPointRules.EqualTo rule fails with zero tolerance
        var zeroToleranceResult = FloatingPointRules.EqualTo(5.0 + 1e-10, null)(5.0);
        zeroToleranceResult.IsValid.ShouldBeFalse();

        RuleContext.UseDefaultTolerance();
        var defaultToleranceResult = FloatingPointRules.EqualTo(5.0 + 1e-10, null)(5.0);
        defaultToleranceResult.IsValid.ShouldBeTrue();
    }
}
