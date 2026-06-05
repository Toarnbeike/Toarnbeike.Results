using Toarnbeike.Results.Guards.Core.Guards;
using TUnit.FsCheck;

namespace Toarnbeike.Results.Guards.Tests.Guards;

public class DayOfWeekGuardTests
{
    private readonly DateOnly _saturday = new (2026, 1, 3);

    [Test, FsCheckProperty]
    public bool OnDayOfWeek_Should_ReturnTrue_WhenEqual(DateTime a)
    {
        return DayOfWeekGuards.OnDayOfWeek(DateOnly.FromDateTime(a), a.DayOfWeek);
    }

    [Test]
    public void OnDayOfWeek_Should_ReturnFalse_WhenUnequal()
    {
        DayOfWeekGuards.OnDayOfWeek(_saturday, DayOfWeek.Thursday).ShouldBeFalse();
    }

    [Test]
    public void OnDaysOfWeek_Should_ReturnTrue_WhenContains()
    {
        DayOfWeekGuards.OnDaysOfWeek(_saturday, [DayOfWeek.Monday, DayOfWeek.Saturday]).ShouldBeTrue();
    }

    [Test]
    public void OnDaysOfWeek_Should_ReturnFalse_WhenNotContains()
    {
        DayOfWeekGuards.OnDaysOfWeek(_saturday, [DayOfWeek.Monday, DayOfWeek.Thursday]).ShouldBeFalse();
    }
}