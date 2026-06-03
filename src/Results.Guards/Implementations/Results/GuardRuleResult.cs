using Toarnbeike.Results.Guards.Messages;
using Toarnbeike.Results.Guards.Tolerances;

namespace Toarnbeike.Results.Guards.Implementations.Results;

internal sealed record GuardRuleResult<T> : IGuardRuleResult<T>
{
    public IGuardContext GuardContext { get; }
    public RuleContext RuleContext { get; }
    public T Value { get; }
    public bool IsValid { get; }
    public string Expression { get; }
    public string? CustomMessage { get; private set; }
    public string? CustomExpression { get; private set; }

    internal GuardRuleResult(IGuardContext guardContext, string expression, bool isValid, T value, RuleContext ruleContext)
    {
        GuardContext = guardContext;
        Expression = expression;
        IsValid = isValid;
        Value = value;
        RuleContext = ruleContext;
    }

    // DO NOT use init and with expressions here.
    // The IGuardRuleResult is referenced from the IGuardContext.Results collection, and that reference should be updated rather than creating a new instance.
    public IGuardRuleResult<T> WithCustomMessage(string? message)
    {
        CustomMessage = message;
        return this;
    }

    // DO NOT use init and with expressions here.
    // The IGuardRuleResult is referenced from the IGuardContext.Results collection, and that reference should be updated rather than creating a new instance.
    public IGuardRuleResult<T> WithCustomExpression(string? expression)
    {
        CustomExpression = expression;
        return this;
    }

    public IFailureMessageProvider FailureMessageProvider => GuardContext.FailureMessageProvider;
    public IToleranceProvider ToleranceProvider => GuardContext.ToleranceProvider;
    public TimeProvider TimeProvider => GuardContext.TimeProvider;
    public Result<TOther> ToResult<TOther>(TOther value) => GuardContext.ToResult(value);
    public bool ShouldContinueExecution => GuardContext.ShouldContinueExecution;
    public List<IGuardRuleResult> Results => GuardContext.Results;
}

