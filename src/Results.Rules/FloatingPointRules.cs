using System.Numerics;
using Toarnbeike.Results.Rules.Helpers;

namespace Toarnbeike.Results.Rules;

public static class FloatingPointRules
{
    private static TFloating GetTolerance<TFloating>(TFloating? provided)
        where TFloating : struct, IFloatingPoint<TFloating> =>
        provided ?? ToleranceHelper.Default<TFloating>();

    public static Rule<TFloat> AtLeast<TFloat>(TFloat min, TFloat? tolerance, string? message)
        where TFloat : struct, IFloatingPoint<TFloat>
    {
        tolerance ??= GetTolerance(tolerance);
        ToleranceHelper.ThrowOnInvalidTolerance(tolerance.Value);
        return new(value =>  value.CompareTo(min - tolerance.Value) >= 0, 
            message ?? $"Value must be at least {min}.");
    }

    public static Rule<TFloat> AtMost<TFloat>(TFloat max, TFloat? tolerance, string? message)
        where TFloat : struct, IFloatingPoint<TFloat>
    {
        tolerance ??= GetTolerance(tolerance);
        ToleranceHelper.ThrowOnInvalidTolerance(tolerance.Value);
        return new(value => !TFloat.IsNaN(value) && value.CompareTo(max + tolerance.Value) <= 0,
            message ?? $"Value must be at most {max}.");
    }

    public static Rule<TFloat> Between<TFloat>(TFloat min, TFloat max, TFloat? tolerance, string? message)
        where TFloat : struct, IFloatingPoint<TFloat>
    {
        tolerance ??= GetTolerance(tolerance);
        ToleranceHelper.ThrowOnInvalidTolerance(tolerance.Value);
        return new(value => !TFloat.IsNaN(value) && value.CompareTo(min - tolerance.Value) >= 0 && value.CompareTo(max + tolerance.Value) <= 0,
            message ?? $"Value must be between {min} and {max}.");
    }

    public static Rule<TFloat> MultipleOf<TFloat>(TFloat factor, TFloat? tolerance, string? message)
        where TFloat : struct, IFloatingPoint<TFloat>
    {
        tolerance ??= GetTolerance(tolerance);
        ToleranceHelper.ThrowOnInvalidTolerance(tolerance.Value);
        ArgumentOutOfRangeException.ThrowIfZero(factor);
        return new(value => !TFloat.IsNaN(value) && IsMultipleOf(value, factor, tolerance.Value),
            message ?? $"Value must be a multiple of {factor}.");
    }

    public static Rule<TFloat> Finite<TFloat>(string? message)
        where TFloat : struct, IFloatingPoint<TFloat> =>
        new(TFloat.IsFinite,
            message ?? $"Value must be a finite {typeof(TFloat).Name}.");

    public static Rule<TFloat> NotNaN<TFloat>(string? message)
        where TFloat : struct, IFloatingPoint<TFloat> =>
        new(value => !TFloat.IsNaN(value),
            message ?? "Value must not be NaN.");

    private static bool IsMultipleOf<TFloat>(TFloat value, TFloat factor, TFloat tolerance)
        where TFloat : struct, IFloatingPoint<TFloat>
    {
        var quotient = value / factor;
        var nearest = TFloat.Round(quotient);
        return TFloat.Abs(value - nearest * factor) <= tolerance;
    }
}