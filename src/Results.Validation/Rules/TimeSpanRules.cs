using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Rules;

internal static class TimeSpanRules
{
    public static ValidationRule<TimeSpan> AtLeast(TimeSpan min, string? message) =>
        new(value => value >= min, message ?? $"TimeSpan must be at least {min}.");
    
    public static ValidationRule<TimeSpan> AtMost(TimeSpan max, string? message) =>
        new(value => value <= max, message ?? $"TimeSpan must be at most {max}.");
    
    public static ValidationRule<TimeSpan> MustBeBetween(TimeSpan min, TimeSpan max, string? message) =>
        new(value => value >= min && value <= max,
            message ?? $"TimeSpan must be between {min} and {max}.");

    public static ValidationRule<TimeSpan> Around(TimeSpan expected, TimeSpan tolerance, string? message)
    {
        if (tolerance < TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(tolerance), "Tolerance must be non-negative.");
        }

        var min = expected < TimeSpan.MinValue + tolerance ? TimeSpan.MinValue : expected - tolerance;
        var max = expected > TimeSpan.MaxValue - tolerance ? TimeSpan.MaxValue : expected + tolerance;

        return new(value => value >= min && value <= max,
            message ?? $"TimeSpan must be around {expected} with a tolerance of {tolerance}.");
    }
}