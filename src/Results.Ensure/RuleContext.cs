namespace Toarnbeike.Results.Ensure;

public sealed record RuleContext
{
    private readonly Dictionary<string, object?> _values = [];

    internal RuleContext((string Key, object? Value)[] values)
    {
        foreach (var (key, value) in values)
        {
            _values[key] = value;
        }
    }

    public T? Get<T>(string key)
        => _values.TryGetValue(key, out var value)
            ? (T?)value
            : default;
}