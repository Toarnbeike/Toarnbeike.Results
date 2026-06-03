using Toarnbeike.Results.Guards.Rules;

namespace Toarnbeike.Results.Guards.Tests.Rules;

public class DateTimeRuleTests
{
    private readonly DateTime _oneHourAgo = DateTime.UtcNow.AddHours(-1);
    private readonly DateTime _now = DateTime.UtcNow;
    private readonly DateTime _inAnHour = DateTime.UtcNow.AddHours(1);
    private readonly DateTime _monday = new(2026, 1, 5);
    private readonly DateTime _saturday = new(2026, 1, 3);

    [Test]
    public void OnOrAfter_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_oneHourAgo).OnOrAfter(_inAnHour);
        result.AssertFailure(
            expectedAttemptedValue: _oneHourAgo,
            expectedArgumentName: "_oneHourAgo",
            expectedGuardName: "Date.OnOrAfter",
            ("Min", _inAnHour)
        );
    }

    [Test]
    public void OnOrBefore_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_inAnHour).OnOrBefore(_oneHourAgo);
        result.AssertFailure(
            expectedAttemptedValue: _inAnHour,
            expectedArgumentName: "_inAnHour",
            expectedGuardName: "Date.OnOrBefore",
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
            expectedGuardName: "Date.Between",
            ("Min", _oneHourAgo),
            ("Max", _oneHourAgo)
        );
    }

    [Test]
    public void Future_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_oneHourAgo).Future();
        var failingResult = result.AssertFailure(
            expectedAttemptedValue: _oneHourAgo,
            expectedArgumentName: "_oneHourAgo",
            expectedGuardName: "Date.Future"
        );
        failingResult.RuleContext.Get<DateTime>("Now").ShouldBe(_now, TimeSpan.FromSeconds(1));
    }

    [Test]
    public void Past_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_inAnHour).Past();
        var failingResult = result.AssertFailure(
            expectedAttemptedValue: _inAnHour,
            expectedArgumentName: "_inAnHour",
            expectedGuardName: "Date.Past"
        );
        failingResult.RuleContext.Get<DateTime>("Now").ShouldBe(_now, TimeSpan.FromSeconds(1));
    }

    [Test]
    public void WithinFuture_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var timeSpan = TimeSpan.FromHours(10);
        var result = Result.Ensure().That(_oneHourAgo).WithinFuture(TimeSpan.FromHours(10));
        var failingResult = result.AssertFailure(
            expectedAttemptedValue: _oneHourAgo,
            expectedArgumentName: "_oneHourAgo",
            expectedGuardName: "Date.DateTime.WithinFuture",
            ("TimeSpan", timeSpan)
        );
        failingResult.RuleContext.Get<DateTime>("Now").ShouldBe(_now, TimeSpan.FromSeconds(1));
    }

    [Test]
    public void WithinPast_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var timeSpan = TimeSpan.FromHours(10);
        var result = Result.Ensure().That(_inAnHour).WithinPast(TimeSpan.FromHours(10));
        var failingResult = result.AssertFailure(
            expectedAttemptedValue: _inAnHour,
            expectedArgumentName: "_inAnHour",
            expectedGuardName: "Date.DateTime.WithinPast",
            ("TimeSpan", timeSpan)
        );
        failingResult.RuleContext.Get<DateTime>("Now").ShouldBe(_now, TimeSpan.FromSeconds(1));
    }

    [Test]
    public void Around_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var timeSpan = TimeSpan.FromMinutes(10);
        var result = Result.Ensure().That(_inAnHour).Around(_oneHourAgo, timeSpan);
        result.AssertFailure(
            expectedAttemptedValue: _inAnHour,
            expectedArgumentName: "_inAnHour",
            expectedGuardName: "Date.DateTime.Around",
            ("Tolerance", timeSpan)
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