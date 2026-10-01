namespace Toarnbeike.Results.Guards;

public interface ICollectionGuardRuleResult<T> : ICollectionGuardTarget<T>, IGuardContext
{
    internal ICollectionGuardRuleResult<T> WithCustomMessage(string? message);
    internal ICollectionGuardRuleResult<T> WithCustomExpression(string? expression);
}