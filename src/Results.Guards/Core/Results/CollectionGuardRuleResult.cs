using Toarnbeike.Results.Guards.Messages;
using Toarnbeike.Results.Guards.Tolerances;

namespace Toarnbeike.Results.Guards.Core.Results;

internal sealed record CollectionGuardRuleResult<T> : ICollectionGuardRuleResult<T>
{
    public IGuardContext GuardContext { get; }
    public IList<T> Values { get; }
    public string Expression { get; }
    public IList<IGuardRuleResult> RuleSpecificResults { get; } = [];

    internal CollectionGuardRuleResult(IGuardContext guardContext, string expression, IList<T> values)
    {
        GuardContext = guardContext;
        Expression = expression;
        Values = values;
    }

    public ICollectionGuardRuleResult<T> WithCustomMessage(string? message)
    {
        foreach (var result in RuleSpecificResults)
        {
            result.CustomMessage = message;
        }
        return this;
    }

    public ICollectionGuardRuleResult<T> WithCustomExpression(string? expression)
    {
        for (var i = 0; i < RuleSpecificResults.Count; i++)
        {
            RuleSpecificResults[i].CustomExpression = expression + $"[{i}]";
        }
        return this;
    }



    public IFailureMessageProvider FailureMessageProvider => GuardContext.FailureMessageProvider;
    public IToleranceProvider ToleranceProvider => GuardContext.ToleranceProvider;
    public TimeProvider TimeProvider => GuardContext.TimeProvider;
    public Result<TOther> ToResult<TOther>(TOther value) => GuardContext.ToResult(value);
    public bool ShouldContinueExecution => GuardContext.ShouldContinueExecution;
    public List<IGuardRuleResult> Results => GuardContext.Results;
}