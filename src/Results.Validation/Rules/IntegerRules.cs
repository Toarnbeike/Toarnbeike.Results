using System.Numerics;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Rules;

internal static class IntegerRules
{
    public static ValidationRule<TInteger> AtLeast<TInteger>(TInteger min, string? message)
        where TInteger : IComparable<TInteger> =>
        new(value => value is not null && value.CompareTo(min) >= 0,
            message ?? $"Value must be at least {min}.");

    public static ValidationRule<TInteger> AtMost<TInteger>(TInteger max, string? message)
        where TInteger : IComparable<TInteger> =>
        new(value => value is not null && value.CompareTo(max) <= 0,
            message ?? $"Value must be at most {max}.");

    public static ValidationRule<TInteger> Between<TInteger>(TInteger min, TInteger max, string? message)
        where TInteger : IComparable<TInteger> =>
        new(value => value is not null && value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0,
            message ?? $"Value must be between {min} and {max}.");

    public static ValidationRule<TInteger> MultipleOf<TInteger>(TInteger factor, string? message)
        where TInteger : IBinaryInteger<TInteger> =>
        new(value => value is not null && (value % factor).Equals(TInteger.Zero),
            message ?? $"Value must be a multiple of {factor}.");
}
