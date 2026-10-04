using System.Numerics;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Rules;

internal static class FloatingPointRules
{
    private static TFloating GetTolerance<TFloating>(TFloating? provided)
        where TFloating : struct, IFloatingPointIeee754<TFloating> =>
        provided ?? ToleranceHelper.Default<TFloating>();

    public static ValidationRule<TFloat> AtLeast<TFloat>(TFloat min, TFloat? tolerance, string? message)
        where TFloat : struct, IFloatingPointIeee754<TFloat>
    {
        tolerance ??= GetTolerance(tolerance);
        ToleranceHelper.ThrowOnInvalidTolerance(tolerance.Value);
        return new(value =>  value.CompareTo(min - tolerance.Value) >= 0, 
            message ?? $"Value must be at least {min}.");
    }

    public static ValidationRule<TFloat> AtMost<TFloat>(TFloat max, TFloat? tolerance, string? message)
        where TFloat : struct, IFloatingPointIeee754<TFloat>
    {
        tolerance ??= GetTolerance(tolerance);
        ToleranceHelper.ThrowOnInvalidTolerance(tolerance.Value);
        return new(value => !TFloat.IsNaN(value) && value.CompareTo(max + tolerance.Value) <= 0,
            message ?? $"Value must be at most {max}.");
    }

    public static ValidationRule<TFloat> Between<TFloat>(TFloat min, TFloat max, TFloat? tolerance, string? message)
        where TFloat : struct, IFloatingPointIeee754<TFloat>
    {
        tolerance ??= GetTolerance(tolerance);
        ToleranceHelper.ThrowOnInvalidTolerance(tolerance.Value);
        return new(value => !TFloat.IsNaN(value) && value.CompareTo(min - tolerance.Value) >= 0 && value.CompareTo(max + tolerance.Value) <= 0,
            message ?? $"Value must be between {min} and {max}.");
    }

    public static ValidationRule<TFloat> MultipleOf<TFloat>(TFloat factor, TFloat? tolerance, string? message)
        where TFloat : struct, IFloatingPointIeee754<TFloat>
    {
        tolerance ??= GetTolerance(tolerance);
        ToleranceHelper.ThrowOnInvalidTolerance(tolerance.Value);
        ArgumentOutOfRangeException.ThrowIfZero(factor);
        return new(value => !TFloat.IsNaN(value) && IsMultipleOf(value, factor, tolerance.Value),
            message ?? $"Value must be a multiple of {factor}.");
    }

    public static ValidationRule<TFloat> Finite<TFloat>(string? message)
        where TFloat : struct, IFloatingPointIeee754<TFloat> =>
        new(TFloat.IsFinite,
            message ?? $"Value must be a finite {typeof(TFloat).Name}.");

    public static ValidationRule<TFloat> NotNaN<TFloat>(string? message)
        where TFloat : struct, IFloatingPointIeee754<TFloat> =>
        new(value => !TFloat.IsNaN(value),
            message ?? "Value must not be NaN.");

    private static bool IsMultipleOf<TFloat>(TFloat value, TFloat factor, TFloat tolerance)
        where TFloat : struct, IFloatingPointIeee754<TFloat>
    {
        var quotient = value / factor;
        var nearest = TFloat.Round(quotient);
        return TFloat.Abs(value - nearest * factor) <= tolerance;
    }
}