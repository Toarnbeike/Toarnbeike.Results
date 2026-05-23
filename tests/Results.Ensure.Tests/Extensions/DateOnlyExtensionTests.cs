using Microsoft.Extensions.Time.Testing;
using Toarnbeike.Results.Ensure.Extensions;

namespace Toarnbeike.Results.Ensure.Tests.Extensions;

// Please note: DateOnlyExtensionTests, DateTimeExtensionTests and DateTimeOffsetExtensionTests
// are copies of each other, besides the types and the failure messages.
// When modifying or adding a test here, check if the change must also be performed in the other classes.
public class DateOnlyExtensionTests : InvariantCultureTestBase
{
    private readonly DateOnly _date = new(2026, 1, 1); //Thursday
    private readonly DateOnly _earlier = new(2025, 12, 31);
    private readonly DateOnly _later = new(2026, 1, 2);
    private readonly FakeTimeProvider _timeProvider = new();

    /*
     * Please note: comparison feels unintuitive, but is correct.
     * WhenEarlier: comparison is after _date, so use _later as comparison.
     * WhenLater: comparison is before _date, so use _earlier as comparison.
     */

    [Test]
    public void OnOrAfter_Should_ReturnFormattedFailure()
    {
        var result = _date.OnOrAfter(_later);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _date,
            expectedMessage: "'_date' must be on or after 01/02/2026, but is 01/01/2026."
        );
    }

    [Test]
    public void OnOrBefore_Should_ReturnFormattedFailure()
    {
        var result = _date.OnOrBefore(_earlier);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _date,
            expectedMessage: "'_date' must be on or before 12/31/2025, but is 01/01/2026."
        );
    }

    [Test]
    public void Between_Should_ReturnFormattedFailure()
    {
        var result = _date.Between(_later, _later.AddDays(5));
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _date,
            expectedMessage: "'_date' must be between 01/02/2026 and 01/07/2026, but is 01/01/2026."
        );
    }

    [Test]
    public void Future_Should_ReturnFormattedFailure()
    {
        _timeProvider.SetUtcNow(_date.ToDateTime(TimeOnly.MinValue));
        var date = _date.AddDays(-1);
        var result = date.Future(_timeProvider);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: date,
            expectedMessage: "'date' must be in the future (on or after 01/01/2026), but is 12/31/2025."
        );
    }

    [Test]
    public void Past_Should_ReturnFormattedFailure()
    {
        _timeProvider.SetUtcNow(_date.ToDateTime(TimeOnly.MinValue));
        var date = _date.AddDays(1);
        var result = date.Past(_timeProvider);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: date,
            expectedMessage: "'date' must be in the past (on or before 01/01/2026), but is 01/02/2026."
        );
    }

    [Test]
    public void WithinFuture_Should_ReturnFormattedFailure()
    {
        _timeProvider.SetUtcNow(_date.ToDateTime(TimeOnly.MinValue));
        var date = _date.AddDays(11);
        var result = date.WithinFuture(10, _timeProvider);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: date,
            expectedMessage: "'date' must be within the next 10 days (between 01/01/2026 and 01/11/2026), but is 01/12/2026."
        );
    }

    [Test]
    public void WithinPast_Should_ReturnFormattedFailure()
    {
        _timeProvider.SetUtcNow(_date.ToDateTime(TimeOnly.MinValue));
        var date = _date.AddDays(-11);
        var result = date.WithinPast(10, _timeProvider);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: date,
            expectedMessage: "'date' must be within the last 10 days (between 12/22/2025 and 01/01/2026), but is 12/21/2025."
        );
    }

    [Test]
    public void OnDayOfWeek_Should_ReturnFormattedFailure()
    {
        var result = _date.OnDayOfWeek(DayOfWeek.Monday);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _date,
            expectedMessage: "'_date' must be on a Monday, but is a Thursday."
        );
    }

    [Test]
    public void OnDayOfWeekFrom_Should_ReturnFormattedFailure()
    {
        var result = _date.OnDaysOfWeek([DayOfWeek.Monday, DayOfWeek.Tuesday]);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _date,
            expectedMessage: "'_date' must be on one of the following days: [Monday, Tuesday], but is a Thursday."
        );
    }

    [Test]
    public void OnWeekday_Should_ReturnFormattedFailure()
    {
        var date = _date.AddDays(2); // to make it a saturday.
        var result = date.OnWeekday();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: date,
            expectedMessage: "'date' must be on a weekday, but is a Saturday."
        );
    }

    [Test]
    public void OnWeekend_Should_ReturnFormattedFailure()
    {
        var result = _date.OnWeekend();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _date,
            expectedMessage: "'_date' must be on a weekend day, but is a Thursday."
        );
    }
}