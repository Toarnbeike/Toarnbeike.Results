using Toarnbeike.Results.Ensure.Rules;

namespace Toarnbeike.Results.Ensure.Tests.Rules;

public class IntegerNumberRuleTests
{
    private readonly int _value = 1;
    private readonly int _negativeValue = -1;

    [Test]
    public void AtLeast_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var min = 2;
        var result = Result.Ensure().That(_value).AtLeast(min);
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "IntegerNumberRules.AtLeast",
            ("Min", min)
        );
    }

    [Test]
    public void AtMost_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var max = 0;
        var result = Result.Ensure().That(_value).AtMost(max);
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "IntegerNumberRules.AtMost",
            ("Max", max)
        );
    }

    [Test]
    public void Between_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var min = -1;
        var max = 0;
        var result = Result.Ensure().That(_value).Between(min, max);
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "IntegerNumberRules.Between",
            ("Min", min),
            ("Max", max)
        );
    }

    [Test]
    public void AtLeastZero_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_negativeValue).AtLeastZero();
        result.AssertFailure(
            expectedAttemptedValue: _negativeValue,
            expectedArgumentName: "_negativeValue",
            expectedGuardName: "IntegerNumberRules.AtLeastZero"
        );
    }

    [Test]
    public void AtMostZero_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_value).AtMostZero();
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "IntegerNumberRules.AtMostZero"
        );
    }

    [Test]
    public void NotZero_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var value = 0;
        var result = Result.Ensure().That(value).NotZero();
        result.AssertFailure(
            expectedAttemptedValue: value,
            expectedArgumentName: "value",
            expectedGuardName: "IntegerNumberRules.NotZero"
        );
    }

    [Test]
    public void MultipleOf_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var factor = 3;
        var result = Result.Ensure().That(_value).MultipleOf(factor);
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "IntegerNumberRules.MultipleOf",
            ("Factor", factor)
        );
    }

    [Test]
    public void Even_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var value = 1;
        var result = Result.Ensure().That(value).Even();
        result.AssertFailure(
            expectedAttemptedValue: value,
            expectedArgumentName: "value",
            expectedGuardName: "IntegerNumberRules.Even"
        );
    }

    [Test]
    public void Odd_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var value = 0;
        var result = Result.Ensure().That(value).Odd();
        result.AssertFailure(
            expectedAttemptedValue: value,
            expectedArgumentName: "value",
            expectedGuardName: "IntegerNumberRules.Odd"
        );
    }

    [Test]
    public void PowerOf2_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var value = 3;
        var result = Result.Ensure().That(value).PowerOf2();
        result.AssertFailure(
            expectedAttemptedValue: value,
            expectedArgumentName: "value",
            expectedGuardName: "IntegerNumberRules.PowerOf2"
        );
    }

    [Test]
    public void Prime_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var value = 4;
        var result = Result.Ensure().That(value).Prime();
        result.AssertFailure(
            expectedAttemptedValue: value,
            expectedArgumentName: "value",
            expectedGuardName: "IntegerNumberRules.Prime"
        );
    }
}