namespace Toarnbeike.Results.Ensure.Implementation.FailureMessages;

public sealed record FailureMessageContext(
    string Expression,
    object? AttemptedValue,
    RuleContext RuleContext,
    string GuardName)
{
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