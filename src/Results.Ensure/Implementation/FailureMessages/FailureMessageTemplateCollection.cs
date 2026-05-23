using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Toarnbeike.Results.Ensure.Implementation.FailureMessages;

internal class FailureMessageTemplateCollection(string ruleName) : IEnumerable
{
    private readonly Dictionary<string, Func<FailureMessageContext, string>> _dictionary = new();

    public string RuleName => ruleName;
    public bool TryGetMessage(string guard, FailureMessageContext context, [NotNullWhen(true)] out string? message)
    {
        var contains = _dictionary.TryGetValue(guard, out var func);
        message = func?.Invoke(context);
        return contains;
    }

    public Func<FailureMessageContext, string> this[string key]
    {
        get => _dictionary[key];
        set => _dictionary[key] = value;
    }

    public IEnumerator GetEnumerator() => _dictionary.GetEnumerator();
}