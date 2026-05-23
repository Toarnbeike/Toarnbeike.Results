using Toarnbeike.Results.Ensure.Rules;

namespace Toarnbeike.Results.Ensure.Tests.Rules;

public class DateTimeOffsetRuleTests
{
    private readonly DateTimeOffset _oneHourAgo = DateTimeOffset.UtcNow.AddHours(-1);
    private readonly DateTimeOffset _now = DateTimeOffset.UtcNow;
    private readonly DateTimeOffset _inAnHour = DateTimeOffset.UtcNow.AddHours(1);
    private readonly DateTimeOffset _monday = new(2026, 1, 5, 0,0,0,TimeSpan.Zero);
    private readonly DateTimeOffset _saturday = new(2026, 1, 3, 0,0,0,TimeSpan.Zero);

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
        failingResult.Context.Get<DateTimeOffset>("Now").ShouldBe(_now, TimeSpan.FromSeconds(1));
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
        failingResult.Context.Get<DateTimeOffset>("Now").ShouldBe(_now, TimeSpan.FromSeconds(1));
    }

    [Test]
    public void WithinFuture_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var timeSpan = TimeSpan.FromHours(10);
        var result = Result.Ensure().That(_oneHourAgo).WithinFuture(TimeSpan.FromHours(10));
        var failingResult = result.AssertFailure(
            expectedAttemptedValue: _oneHourAgo,
            expectedArgumentName: "_oneHourAgo",
            expectedGuardName: "Date.DateTimeOffset.WithinFuture",
            ("TimeSpan", timeSpan)
        );
        failingResult.Context.Get<DateTimeOffset>("Now").ShouldBe(_now, TimeSpan.FromSeconds(1));
    }

    [Test]
    public void WithinPast_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var timeSpan = TimeSpan.FromHours(10);
        var result = Result.Ensure().That(_inAnHour).WithinPast(TimeSpan.FromHours(10));
        var failingResult = result.AssertFailure(
            expectedAttemptedValue: _inAnHour,
            expectedArgumentName: "_inAnHour",
            expectedGuardName: "Date.DateTimeOffset.WithinPast",
            ("TimeSpan", timeSpan)
        );
        failingResult.Context.Get<DateTimeOffset>("Now").ShouldBe(_now, TimeSpan.FromSeconds(1));
    }

    [Test]
    public void Around_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var timeSpan = TimeSpan.FromMinutes(10);
        var result = Result.Ensure().That(_inAnHour).Around(_oneHourAgo, timeSpan);
        result.AssertFailure(
            expectedAttemptedValue: _inAnHour,
            expectedArgumentName: "_inAnHour",
            expectedGuardName: "Date.DateTimeOffset.Around",
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