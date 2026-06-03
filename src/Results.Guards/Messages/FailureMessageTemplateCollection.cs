using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Toarnbeike.Results.Guards.Messages;

internal class FailureMessageTemplateCollection(string ruleName) : IEnumerable
{
    private readonly Dictionary<string, Func<FailureMessageContext, FormattableString>> _dictionary = [];

    public string RuleName => ruleName;
    public bool TryGetMessage(string guard, FailureMessageContext context, [NotNullWhen(true)] out FormattableString? message)
    {
        var contains = _dictionary.TryGetValue(guard, out var func);
        message = func?.Invoke(context);
        return contains;
    }

    public Func<FailureMessageContext, FormattableString> this[string key]
    {
        get => _dictionary[key];
        set => _dictionary[key] = value;
    }

    public IEnumerator GetEnumerator() => _dictionary.GetEnumerator();
}