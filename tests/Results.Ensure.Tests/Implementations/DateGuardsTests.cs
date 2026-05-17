using Toarnbeike.Results.Ensure.Implementations_Obsolete;

namespace Toarnbeike.Results.Ensure.Tests.Implementations;

public class DateGuardsTests
{
    private readonly DateOnly _date = new(2026, 1, 1); // Thursday
    private readonly DateOnly _earlier = new(2025, 12, 31);
    private readonly DateOnly _later = new(2026, 1, 2);

    /*
     * Please note: comparison feels unintuitive, but is correct.
     * WhenEarlier: comparison is after _date, so use _later as comparison.
     * WhenLater: comparison is before _date, so use _earlier as comparison.
    */

    [Test]
    public void After_Should_ReturnFailure_WhenEarlier()
    {
        var result = DateGuards.After(_date, _later);
        result.IsValid.ShouldBeFalse();
        result.Constraint.ShouldBe(_later);
    }

    [Test]
    public void After_Should_ReturnValid_WhenEqual()
    {
        var result = DateGuards.After(_date, _date);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void After_Should_ReturnValid_WhenLater()
    {
        var result = DateGuards.After(_date, _earlier);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void After_Should_UseTimeZoneInfo_WhenComparingDateTimeOffset()
    {
        var utc = new DateTimeOffset(2026, 1, 1, 12, 0, 0, TimeSpan.Zero);
        var cest = new DateTimeOffset(2026, 1, 1, 13, 0, 0, TimeSpan.FromHours(2));

        // Comparing 12:00:00+0 (UTC) with 13:00:00+2 (CEST). UTC happens after CEST
        var result = DateGuards.After(utc, cest);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Before_Should_ReturnFailure_WhenLater()
    {
        var result = DateGuards.Before(_date, _earlier);
        result.IsValid.ShouldBeFalse();
        result.Constraint.ShouldBe(_earlier);
    }

    [Test]
    public void Before_Should_ReturnValid_WhenEqual()
    {
        var result = DateGuards.Before(_date, _date);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Before_Should_ReturnValid_WhenEarlier()
    {
        var result = DateGuards.Before(_date, _later);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Before_Should_UseTimeZoneInfo_WhenComparingDateTimeOffset()
    {
        var utc = new DateTimeOffset(2026, 1, 1, 12, 0, 0, TimeSpan.Zero);
        var cest = new DateTimeOffset(2026, 1, 1, 13, 0, 0, TimeSpan.FromHours(2));

        // Comparing 12:00:00+0 (UTC) with 13:00:00+2 (CEST). UTC happens after CEST
        var result = DateGuards.Before(utc, cest);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void OnDayOfWeek_Should_ReturnFailure_WhenOtherDay()
    {
        var result = DateGuards.OnDayOfWeek(_date, DayOfWeek.Friday);
        result.IsValid.ShouldBeFalse();
        result.Constraint.ShouldBe(DayOfWeek.Friday);
    }

    [Test]
    public void OnDayOfWeek_Should_ReturnValid_WhenCorrectDay()
    {
        var result = DateGuards.OnDayOfWeek(_date, DayOfWeek.Thursday);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void OnDayOfWeekFrom_Should_ReturnFailure_WhenNotIncluded()
    {
        DayOfWeek[] acceptableDays = [DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday];
        var result = DateGuards.OnDayOfWeekFrom(_date, acceptableDays);
        result.IsValid.ShouldBeFalse();
        result.Constraint.ShouldBe(acceptableDays);
    }

    [Test]
    public void OnDayOfWeekFrom_Should_ReturnValid_WhenIncluded()
    {
        DayOfWeek[] acceptableDays = [DayOfWeek.Tuesday, DayOfWeek.Thursday, DayOfWeek.Saturday];
        var result = DateGuards.OnDayOfWeekFrom(_date, acceptableDays);
        result.IsValid.ShouldBeTrue();
    }
}
