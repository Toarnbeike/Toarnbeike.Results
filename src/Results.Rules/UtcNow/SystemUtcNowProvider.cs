namespace Toarnbeike.Results.Rules.UtcNow;

/// <summary>
/// Provides the current UTC date and time using the system clock.
/// </summary>
internal sealed class SystemUtcNowProvider : IUctNowProvider
{
    /// <inheritdoc />
    public TDate GetUtcNow<TDate>() where TDate : IComparable<TDate>
    {
        return typeof(TDate) switch
        {
            Type t when t == typeof(DateTime)       => (TDate)(object)DateTime.UtcNow,
            Type t when t == typeof(DateTimeOffset) => (TDate)(object)DateTimeOffset.UtcNow,
            Type t when t == typeof(DateOnly)       => (TDate)(object)DateOnly.FromDateTime(DateTime.UtcNow),
            Type t when t == typeof(TimeOnly)       => (TDate)(object)TimeOnly.FromDateTime(DateTime.UtcNow),
            _ => throw new NotSupportedException($"Type {typeof(TDate)} is not supported.")
        };
    }
}
