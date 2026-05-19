using Toarnbeike.Results.Ensure.Rules;

namespace Toarnbeike.Results.Ensure.Tests.Rules;

public class NumberRuleTests
{
    private readonly int _value = 1;
    private readonly int _negativeValue = -1;

    [Test]
    public void GreaterThan_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var min = 2;
        var result = Result.Ensure().That(_value).GreaterThan(min);
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "NumberRules.GreaterThan",
            ("Min", min)
        );
    }

    [Test]
    public void LessThan_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var max = 0;
        var result = Result.Ensure().That(_value).LessThan(max);
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "NumberRules.LessThan",
            ("Max", max)
        );
    }

    [Test]
    public void Positive_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_negativeValue).Positive();
        result.AssertFailure(
            expectedAttemptedValue: _negativeValue,
            expectedArgumentName: "_negativeValue",
            expectedGuardName: "NumberRules.Positive"
        );
    }

    [Test]
    public void Negative_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_value).Negative();
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "NumberRules.Negative"
        );
    }

}