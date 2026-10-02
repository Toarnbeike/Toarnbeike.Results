using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Rules;

internal static class DateRules
{
    public static ValidationRule<TDate> OnOrAfter<TDate>(TDate min, string? message)
        where TDate : IComparable<TDate> =>
        new(value => value is not null && value.CompareTo(min) >= 0,
            message ?? $"Date must be on or after {min}.");

    public static ValidationRule<TDate> OnOrBefore<TDate>(TDate max, string? message)
        where TDate : IComparable<TDate> =>
        new(value => value is not null && value.CompareTo(max) <= 0,
            message ?? $"Date must be on or before {max}.");

    public static ValidationRule<TDate> MustBeBetween<TDate>(TDate min, TDate max, string? message)
        where TDate : IComparable<TDate> =>
        new(value => value is not null && value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0,
            message ?? $"Date must be between {min} and {max}.");
}