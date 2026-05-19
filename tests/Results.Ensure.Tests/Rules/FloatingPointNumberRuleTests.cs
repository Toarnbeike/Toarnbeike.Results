using Toarnbeike.Results.Ensure.Rules;

namespace Toarnbeike.Results.Ensure.Tests.Rules;

public class FloatingPointNumberRuleTests
{
    private readonly double _value = 1.5;
    private readonly double _negativeValue = -1.5;
    private readonly double _tolerance = 0.4;

    [Test]
    public void AtLeast_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var min = 2.0d;
        var result = Result.Ensure().That(_value).AtLeast(min, tolerance: _tolerance);
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "FloatingPointNumberRules.AtLeast",
            ("Min", min),
            ("Tolerance", _tolerance)
        );
    }

    [Test]
    public void AtMost_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var max = 1.0d;
        var result = Result.Ensure().That(_value).AtMost(max, tolerance: _tolerance);
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "FloatingPointNumberRules.AtMost",
            ("Max", max),
            ("Tolerance", _tolerance)
        );
    }

    [Test]
    public void Between_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var min = 0.0d;
        var max = 1.0d;
        var result = Result.Ensure().That(_value).Between(min, max, tolerance: _tolerance);
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "FloatingPointNumberRules.Between",
            ("Min", min),
            ("Max", max),
            ("Tolerance", _tolerance)
        );
    }

    [Test]
    public void AtLeastZero_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_negativeValue).AtLeastZero(tolerance: _tolerance);
        result.AssertFailure(
            expectedAttemptedValue: _negativeValue,
            expectedArgumentName: "_negativeValue",
            expectedGuardName: "FloatingPointNumberRules.AtLeastZero",
            ("Tolerance", _tolerance)
        );
    }

    [Test]
    public void AtMostZero_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_value).AtMostZero(tolerance: _tolerance);
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "FloatingPointNumberRules.AtMostZero",
            ("Tolerance", _tolerance)
        );
    }

    [Test]
    public void Zero_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_value).Zero(tolerance: _tolerance);
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "FloatingPointNumberRules.Zero",
            ("Tolerance", _tolerance)
        );
    }

    [Test]
    public void NotZero_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_value).NotZero(tolerance: _value);
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "FloatingPointNumberRules.NotZero",
            ("Tolerance", _value)
        );
    }

    [Test]
    public void MultipleOf_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var factor = 1.0d;
        var result = Result.Ensure().That(_value).MultipleOf(factor, tolerance: _tolerance);
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "FloatingPointNumberRules.MultipleOf",
            ("Factor", factor),
            ("Tolerance", _tolerance)
        );
    }

    [Test]
    public void WholeNumber_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_value).WholeNumber(tolerance: _tolerance);
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "FloatingPointNumberRules.WholeNumber",
            ("Tolerance", _tolerance)
        );
    }

    [Test]
    public void MaxDecimalPlaces_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_value).MaxDecimalPlaces(0, tolerance: _tolerance);
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "FloatingPointNumberRules.MaxDecimalPlaces",
            ("Tolerance", _tolerance)
        );
    }

    [Test]
    public void Finite_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var value = double.PositiveInfinity;
        var result = Result.Ensure().That(value).Finite();
        result.AssertFailure(
            expectedAttemptedValue: value,
            expectedArgumentName: "value",
            expectedGuardName: "FloatingPointNumberRules.Finite"
        );
    }

    [Test]
    public void NotNaN_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var value = double.NaN;
        var result = Result.Ensure().That(value).NotNaN();
        result.AssertFailure(
            expectedAttemptedValue: value,
            expectedArgumentName: "value",
            expectedGuardName: "FloatingPointNumberRules.NotNaN"
        );
    }

    [Test]
    public void Power10_Should_RaiseCorrectly_ForDifferentTypes()
    {
        FloatingPointNumberRules.Power10<float>(3).ShouldBe(1000f);
        FloatingPointNumberRules.Power10<double>(5).ShouldBe(100000d);
        FloatingPointNumberRules.Power10<decimal>(2).ShouldBe(100m);
        FloatingPointNumberRules.Power10<Half>(1).ShouldBe(Half.Parse("10"));
        FloatingPointNumberRules.Power10<float>(0).ShouldBe(1f);
    }
}