using Toarnbeike.Results.Ensure.Rules;

namespace Toarnbeike.Results.Ensure.Tests.Rules;

public class DateOnlyRuleTests
{
    private readonly DateOnly _yesterday = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(-1);
    private readonly DateOnly _today = DateOnly.FromDateTime(DateTime.UtcNow);
    private readonly DateOnly _tomorrow = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(1);
    private readonly DateOnly _monday = new (2026, 1, 5);
    private readonly DateOnly _saturday = new (2026, 1, 3);

    [Test]
    public void OnOrAfter_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_yesterday).OnOrAfter(_tomorrow);
        result.AssertFailure(
            expectedAttemptedValue: _yesterday,
            expectedArgumentName: "_yesterday",
            expectedGuardName: "Date.OnOrAfter",
            ("Min", _tomorrow)
        );
    }

    [Test]
    public void OnOrBefore_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_tomorrow).OnOrBefore(_yesterday);
        result.AssertFailure(
            expectedAttemptedValue: _tomorrow,
            expectedArgumentName: "_tomorrow",
            expectedGuardName: "Date.OnOrBefore",
            ("Max", _yesterday)
        );
    }

    [Test]
    public void Between_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_tomorrow).Between(_yesterday, _yesterday);
        result.AssertFailure(
            expectedAttemptedValue: _tomorrow,
            expectedArgumentName: "_tomorrow",
            expectedGuardName: "Date.Between",
            ("Min", _yesterday),
            ("Max", _yesterday)
        );
    }

    [Test]
    public void Future_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_yesterday).Future();
        result.AssertFailure(
            expectedAttemptedValue: _yesterday,
            expectedArgumentName: "_yesterday",
            expectedGuardName: "Date.Future",
            ("Now", _today)
        );
    }

    [Test]
    public void Past_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_tomorrow).Past();
        result.AssertFailure(
            expectedAttemptedValue: _tomorrow,
            expectedArgumentName: "_tomorrow",
            expectedGuardName: "Date.Past",
            ("Now", _today)
        );
    }

    [Test]
    public void WithinFuture_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_yesterday).WithinFuture(10);
        result.AssertFailure(
            expectedAttemptedValue: _yesterday,
            expectedArgumentName: "_yesterday",
            expectedGuardName: "Date.DateOnly.WithinFuture",
            ("Today", _today),
            ("Days", 10)
        );
    }

    [Test]
    public void WithinPast_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_tomorrow).WithinPast(10);
        result.AssertFailure(
            expectedAttemptedValue: _tomorrow,
            expectedArgumentName: "_tomorrow",
            expectedGuardName: "Date.DateOnly.WithinPast",
            ("Today", _today),
            ("Days", 10)
        );
    }

    [Test]
    public void OnDayOfWeek_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_saturday).OnDayOfWeek(DayOfWeek.Friday);
        result.AssertFailure(
            expectedAttemptedValue: _saturday,
            expectedArgumentName: "_saturday",
            expectedGuardName: "Date.OnDayOfWeek",
            ("ExpectedDay", DayOfWeek.Friday),
            ("Actual", DayOfWeek.Saturday)
        );
    }

    [Test]
    public void OnDaysOfWeek_Should_ReturnFailingRuleResult_WhenFailure()
    {
        DayOfWeek[] allowedDays = [DayOfWeek.Monday, DayOfWeek.Friday];
        var result = Result.Ensure().That(_saturday).OnDaysOfWeek(allowedDays);
        result.AssertFailure(
            expectedAttemptedValue: _saturday,
            expectedArgumentName: "_saturday",
            expectedGuardName: "Date.OnDaysOfWeek",
            ("Allowed", allowedDays),
            ("Actual", DayOfWeek.Saturday)
        );
    }

    [Test]
    public void OnWeekday_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_saturday).OnWeekday();
        result.AssertFailure(
            expectedAttemptedValue: _saturday,
            expectedArgumentName: "_saturday",
            expectedGuardName: "Date.OnWeekday",
            ("Actual", DayOfWeek.Saturday)
        );
    }

    [Test]
    public void OnWeekend_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_monday).OnWeekend();
        result.AssertFailure(
            expectedAttemptedValue: _monday,
            expectedArgumentName: "_monday",
            expectedGuardName: "Date.OnWeekend",
            ("Actual", DayOfWeek.Monday)
        );
    }
}