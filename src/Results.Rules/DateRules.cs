namespace Toarnbeike.Results.Rules;

public static class DateRules
{
    public static Rule<TDate> OnOrAfter<TDate>(TDate min, string? message)
        where TDate : IComparable<TDate> =>
        new(value => value is not null && value.CompareTo(min) >= 0,
            message ?? $"Date must be on or after {min}.");

    public static Rule<TDate> OnOrBefore<TDate>(TDate max, string? message)
        where TDate : IComparable<TDate> =>
        new(value => value is not null && value.CompareTo(max) <= 0,
            message ?? $"Date must be on or before {max}.");

    public static Rule<TDate> MustBeBetween<TDate>(TDate min, TDate max, string? message)
        where TDate : IComparable<TDate> =>
        new(value => value is not null && value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0,
            message ?? $"Date must be between {min} and {max}.");
}