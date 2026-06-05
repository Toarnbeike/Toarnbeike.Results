namespace Toarnbeike.Results.Guards.Core.Guards;

internal static class DayOfWeekGuards
{
    public static bool OnDayOfWeek(DateOnly date, DayOfWeek expectedDayOfWeek) =>
        date.DayOfWeek == expectedDayOfWeek;

    public static bool OnDaysOfWeek(DateOnly date, DayOfWeek[] allowedDaysOfWeek) =>
        allowedDaysOfWeek.Contains(date.DayOfWeek);
}