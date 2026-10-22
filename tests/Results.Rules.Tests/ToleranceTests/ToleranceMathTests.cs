using Toarnbeike.Results.Rules.Tolerance;

namespace Toarnbeike.Results.Rules.Tests.ToleranceTests;

public class ToleranceMathTests
{
    [Test]
    public void GetAdjustedMin_ShouldReturnMinValue_WhenMinMinusToleranceIsLessThanMinValue()
    {
        double min = double.MinValue + 1; // A value just above the minimum
        double tolerance = 2; // A tolerance that would push the value below the minimum
        double adjustedMin = ToleranceMath<double>.GetAdjustedMin(min, tolerance);
        adjustedMin.ShouldBe(double.MinValue);
    }

    [Test]
    public void GetAdjustedMin_ShouldReturnAdjustedValue_WhenMinMinusToleranceIsGreaterThanMinValue()
    {
        double min = 10;
        double tolerance = 2;
        double adjustedMin = ToleranceMath<double>.GetAdjustedMin(min, tolerance);
        adjustedMin.ShouldBe(8);
    }

    [Test]
    public void GetAdjustedMin_ShouldReturnMinValue_WhenMinIsMinValueAndToleranceIsPositive()
    {
        double min = double.MinValue;
        double tolerance = double.Epsilon; // Any positive tolerance
        double adjustedMin = ToleranceMath<double>.GetAdjustedMin(min, tolerance);
        adjustedMin.ShouldBe(double.MinValue);
    }

    [Test]
    public void GetAdjustedMax_ShouldReturnMaxValue_WhenMaxPlusToleranceIsGreaterThanMaxValue()
    {
        double max = double.MaxValue - 1; // A value just below the maximum
        double tolerance = 2; // A tolerance that would push the value above the maximum
        double adjustedMax = ToleranceMath<double>.GetAdjustedMax(max, tolerance);
        adjustedMax.ShouldBe(double.MaxValue);
    }

    [Test]
    public void GetAdjustedMax_ShouldReturnAdjustedValue_WhenMaxPlusToleranceIsLessThanMaxValue()
    {
        double max = 10;
        double tolerance = 2;
        double adjustedMax = ToleranceMath<double>.GetAdjustedMax(max, tolerance);
        adjustedMax.ShouldBe(12);
    }

    [Test]
    public void GetAdjustedMax_ShouldReturnMaxValue_WhenMaxIsMaxValueAndToleranceIsPositive()
    {
        double max = double.MaxValue;
        double tolerance = 1; // Any positive tolerance
        double adjustedMax = ToleranceMath<double>.GetAdjustedMax(max, tolerance);
        adjustedMax.ShouldBe(double.MaxValue);
    }
}
