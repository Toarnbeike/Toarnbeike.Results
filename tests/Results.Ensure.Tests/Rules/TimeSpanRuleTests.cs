using Toarnbeike.Results.Ensure.Rules;

namespace Toarnbeike.Results.Ensure.Tests.Rules;

public class TimeSpanRuleTests
{
    private readonly TimeSpan _oneHourAgo = TimeSpan.FromHours(-1);
    private readonly TimeSpan _inAnHour = TimeSpan.FromHours(1);

    [Test]
    public void AtLeast_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_oneHourAgo).AtLeast(_inAnHour);
        result.AssertFailure(
            expectedAttemptedValue: _oneHourAgo,
            expectedArgumentName: "_oneHourAgo",
            expectedGuardName: "TimeSpan.AtLeast",
            ("Min", _inAnHour)
        );
    }

    [Test]
    public void AtMost_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_inAnHour).AtMost(_oneHourAgo);
        result.AssertFailure(
            expectedAttemptedValue: _inAnHour,
            expectedArgumentName: "_inAnHour",
            expectedGuardName: "TimeSpan.AtMost",
            ("Max", _oneHourAgo)
        );
    }

    [Test]
    public void Between_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_inAnHour).Between(_oneHourAgo, _oneHourAgo);
        result.AssertFailure(
            expectedAttemptedValue: _inAnHour,
            expectedArgumentName: "_inAnHour",
            expectedGuardName: "TimeSpan.Between",
            ("Min", _oneHourAgo),
            ("Max", _oneHourAgo)
        );
    }

    [Test]
    public void Around_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var timeSpan = TimeSpan.FromMinutes(10);
        var result = Result.Ensure().That(_inAnHour).Around(_oneHourAgo, timeSpan);
        result.AssertFailure(
            expectedAttemptedValue: _inAnHour,
            expectedArgumentName: "_inAnHour",
            expectedGuardName: "TimeSpan.Around",
            ("Expected", _oneHourAgo),
            ("Tolerance", timeSpan)
        );
    }

    [Test]
    public void AtLeastZero_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_oneHourAgo).AtLeastZero();
        result.AssertFailure(
            expectedAttemptedValue: _oneHourAgo,
            expectedArgumentName: "_oneHourAgo",
            expectedGuardName: "TimeSpan.AtLeastZero"
        );
    }

    [Test]
    public void AtMostZero_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_inAnHour).AtMostZero();
        result.AssertFailure(
            expectedAttemptedValue: _inAnHour,
            expectedArgumentName: "_inAnHour",
            expectedGuardName: "TimeSpan.AtMostZero"
        );
    }
}