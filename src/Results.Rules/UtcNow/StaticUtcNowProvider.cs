namespace Toarnbeike.Results.Rules.UtcNow;

/// <summary>
/// Provides the current UTC date and time as a static value.
/// </summary>
internal sealed class StaticUtcNowProvider(DateTimeOffset staticUtcNow) : IUctNowProvider
{
    /// <inheritdoc />
    public TDate GetUtcNow<TDate>() where TDate : IComparable<TDate>
    {
        return typeof(TDate) switch
        {
            Type t when t == typeof(DateTime) => (TDate)(object)staticUtcNow.UtcDateTime,
            Type t when t == typeof(DateTimeOffset) => (TDate)(object)staticUtcNow,
            Type t when t == typeof(DateOnly) => (TDate)(object)DateOnly.FromDateTime(staticUtcNow.UtcDateTime),
            Type t when t == typeof(TimeOnly) => (TDate)(object)TimeOnly.FromDateTime(staticUtcNow.UtcDateTime),
            _ => throw new NotSupportedException($"Type {typeof(TDate)} is not supported.")
        };
    }
}