namespace Toarnbeike.Results.Guards.Implementations;

public sealed record RuleContext
{
    private readonly Dictionary<string, object?> _values = [];

    public string RuleName { get; }

    /// <summary>
    /// The value that was guarded and caused the failure.
    /// </summary>
    public T? GetAttemptedValue<T>() => Get<T>("AttemptedValue");

    public object? AttemptedValue => Get<object?>("AttemptedValue");

    public T? Get<T>(string key) => 
        _values.TryGetValue(key, out var value) ? (T?)value : default;

    internal RuleContext(string ruleName, object? attemptedValue, params (string Key, object? Value)[] values)
    {
        RuleName = ruleName;
        foreach (var (key, value) in values)
        {
            _values[key] = value;
        }
        _values["AttemptedValue"] = attemptedValue;
    }
}