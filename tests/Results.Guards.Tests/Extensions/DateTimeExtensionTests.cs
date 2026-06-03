using Microsoft.Extensions.Time.Testing;
using Toarnbeike.Results.Guards.Extensions;

namespace Toarnbeike.Results.Guards.Tests.Extensions;

// Please note: DateOnlyExtensionTests, DateTimeExtensionTests and DateTimeOffsetExtensionTests
// are copies of each other, besides the types and the failure messages.
// When modifying or adding a test here, check if the change must also be performed in the other classes.
public class DateTimeExtensionTests
{
    private readonly DateTime _date = new(2026, 1, 1); //Thursday
    private readonly DateTime _earlier = new(2025, 12, 31);
    private readonly DateTime _later = new(2026, 1, 2);
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
            expectedMessage: "'_date' must be on or after 01/02/2026 00:00:00, but is 01/01/2026 00:00:00."
        );
    }

    [Test]
    public void OnOrBefore_Should_ReturnFormattedFailure()
    {
        var result = _date.OnOrBefore(_earlier);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _date,
            expectedMessage: "'_date' must be on or before 12/31/2025 00:00:00, but is 01/01/2026 00:00:00."
        );
    }

    [Test]
    public void Between_Should_ReturnFormattedFailure()
    {
        var result = _date.Between(_later, _later.AddDays(5));
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _date,
            expectedMessage: "'_date' must be between 01/02/2026 00:00:00 and 01/07/2026 00:00:00, but is 01/01/2026 00:00:00."
        );
    }

    [Test]
    public void Future_Should_ReturnFormattedFailure()
    {
        _timeProvider.SetUtcNow(_date);
        var date = _date.AddDays(-1);
        var result = date.Future(_timeProvider);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: date,
            expectedMessage: "'date' must be in the future (on or after 01/01/2026 00:00:00), but is 12/31/2025 00:00:00."
        );
    }

    [Test]
    public void Past_Should_ReturnFormattedFailure()
    {
        _timeProvider.SetUtcNow(_date);
        var date = _date.AddDays(1);
        var result = date.Past(_timeProvider);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: date,
            expectedMessage: "'date' must be in the past (on or before 01/01/2026 00:00:00), but is 01/02/2026 00:00:00."
        );
    }

    [Test]
    public void WithinFuture_Should_ReturnFormattedFailure()
    {
        _timeProvider.SetUtcNow(_date);
        var date = _date.AddHours(11);
        var result = date.WithinFuture(TimeSpan.FromHours(10), _timeProvider);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: date,
            expectedMessage: "'date' must be within the next 10:00:00 (between 01/01/2026 00:00:00 and 01/01/2026 10:00:00), but is 01/01/2026 11:00:00."
        );
    }

    [Test]
    public void WithinPast_Should_ReturnFormattedFailure()
    {
        _timeProvider.SetUtcNow(_date);
        var date = _date.AddHours(-11);
        var result = date.WithinPast(TimeSpan.FromHours(10), _timeProvider);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: date,
            expectedMessage: "'date' must be within the last 10:00:00 (between 12/31/2025 14:00:00 and 01/01/2026 00:00:00), but is 12/31/2025 13:00:00."
        );
    }

    [Test]
    public void Around_Should_ReturnFormattedFailure()
    {
        var result = _date.Around(_earlier, TimeSpan.FromHours(10));
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _date,
            expectedMessage: "'_date' must be within 10:00:00 of 12/31/2025 00:00:00, but is 01/01/2026 00:00:00."
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