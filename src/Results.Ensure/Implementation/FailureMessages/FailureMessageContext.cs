namespace Toarnbeike.Results.Ensure.Implementation.FailureMessages;

public sealed record FailureMessageContext(
    string Expression,
    object? AttemptedValue,
    RuleContext RuleContext,
    string GuardName)
{
    public T? Get<T>(string key) => RuleContext.Get<T>(key);
};