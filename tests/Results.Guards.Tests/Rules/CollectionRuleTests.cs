using Toarnbeike.Results.Guards.Rules;

namespace Toarnbeike.Results.Guards.Tests.Rules;

public class CollectionRuleTests
{
    private readonly List<string> _value = ["A", "B", "C"];
    private readonly List<string> _empty = [];

    [Test]
    public void NotEmpty_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_empty).NotEmpty();
        result.AssertFailure(
            expectedAttemptedValue: _empty,
            expectedArgumentName: "_empty",
            expectedGuardName: "Collection.NotEmpty"
        );
    }

    [Test]
    public void Empty_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_value).Empty();
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "Collection.Empty",
            ("Actual", _value.Count)
        );
    }

    [Test]
    public void AtLeast_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_value).AtLeast(4);
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "Collection.AtLeast",
            ("Actual", _value.Count),
            ("Min", 4)
        );
    }

    [Test]
    public void AtMost_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_value).AtMost(2);
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "Collection.AtMost",
            ("Actual", _value.Count),
            ("Max", 2)
        );
    }

    [Test]
    public void Between_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_value).Between(4, 6);
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "Collection.Between",
            ("Actual", _value.Count),
            ("Min", 4),
            ("Max", 6)
        );
    }

    [Test]
    public void Exactly_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_value).Exactly(2);
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "Collection.Exactly",
            ("Actual", _value.Count),
            ("Expected", 2)
        );
    }

    [Test]
    public void Single_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_value).Single();
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "Collection.Single",
            ("Actual", _value.Count)
        );
    }
}