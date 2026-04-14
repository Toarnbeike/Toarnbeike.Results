using System.Collections;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Toarnbeike.Results.Ensure;

internal static class Guards
{
    public static GuardResult NotEmpty(IEnumerable collection) =>
        new(collection.Cast<object?>().Any(), expr => $"'{expr}' must not be empty.", null);

    public static GuardResult After<TDate>(TDate date, TDate other) where TDate : IComparable<TDate> =>
        new(date.CompareTo(other) > 0, expr => $"'{expr}' must be after {other}.", other);

    public static GuardResult Before<TDate>(TDate date, TDate other) where TDate : IComparable<TDate> =>
        new(date.CompareTo(other) < 0, expr => $"'{expr}' must be before {other}.", other);

    public static GuardResult IsDefined<TEnum>(TEnum value) where TEnum : struct, Enum =>
        new(Enum.IsDefined(value), expr => $"'{expr}' is not a valid value.", null);

    public static GuardResult GreaterThan<TNumber>(TNumber value, TNumber min) where TNumber : INumber<TNumber> =>
        new(value > min, expr => $"'{expr}' must be greater than {min}.", min);

    public static GuardResult LessThan<TNumber>(TNumber value, TNumber max) where TNumber : INumber<TNumber> =>
        new(value < max, expr => $"'{expr}' must be less than {max}.", max);

    public static GuardResult InRange<TNumber>(TNumber value, TNumber min, TNumber max) where TNumber : INumber<TNumber> =>
        new(value <= max && value >= min, expr => $"'{expr}' must be between {min} and {max}.", (min, max));

    public static GuardResult NotEmpty(string? value) => 
        new(!string.IsNullOrEmpty(value), expr => $"'{expr}' must not be empty.", null);

    public static GuardResult NotWhiteSpace(string? value) =>
        new(!string.IsNullOrWhiteSpace(value), expr => $"'{expr}' must not be whitespace.", null);

    public static GuardResult MinLength(string value, int minLength) =>
        new(value.Length >= minLength, expr => $"'{expr}' must be at least {minLength} characters.", minLength);

    public static GuardResult MaxLength(string value, int maxLength) =>
        new(value.Length <= maxLength, expr => $"'{expr}' must be at most {maxLength} characters.", maxLength);

    public static GuardResult Matches(string value, Regex regex) =>
        new(regex.IsMatch(value), expr => $"'{expr}' is not in the correct format.", regex);
}