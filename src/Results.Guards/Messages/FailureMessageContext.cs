using Toarnbeike.Results.Guards.Implementations;

namespace Toarnbeike.Results.Guards.Messages;

public sealed record FailureMessageContext(
    string Expression,
    RuleContext RuleContext)
{
    public string GuardName => RuleContext.RuleName;

    public object? AttemptedValue => RuleContext.AttemptedValue;
    public T? Get<T>(string key) => RuleContext.Get<T>(key);

    internal string GetActualItems()
    {
        var actual = Get<int>("Actual");
        return actual == 1 ? "1 item" : $"{actual} items";
    }

    internal string GetItems(string key)
    {
        var actual = Get<int>(key);
        return actual == 1 ? "1 item" : $"{actual} items";
    }
};