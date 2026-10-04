using System.Numerics;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Rules;

internal static class NumericRules
{
    public static ValidationRule<TNumber> GreaterThan<TNumber>(TNumber min, string? message)
        where TNumber : IComparable<TNumber> =>
        new(value => value is not null && value.CompareTo(min) > 0,
            message ?? $"Value must be greater than {min}.");


    public static ValidationRule<TNumber> LessThan<TNumber>(TNumber max, string? message)
        where TNumber : IComparable<TNumber> =>
        new(value => value is not null && value.CompareTo(max) < 0,
            message ?? $"Value must be less than {max}.");

    public static ValidationRule<TNumber> Positive<TNumber>(string? message)
        where TNumber : INumber<TNumber> => 
        new(value => value is not null && value.CompareTo(TNumber.Zero) > 0,
            message ?? "Value must be positive.");
}