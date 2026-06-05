using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Guards.Messages;
using Toarnbeike.Results.Guards.Tolerances;

namespace Toarnbeike.Results.Guards.Core.GuardContext;

internal abstract class GuardContextBase(
    IToleranceProvider? toleranceProvider = null,
    TimeProvider? timeProvider = null) : IGuardContext
{
    public List<IGuardRuleResult> Results { get; } = [];

    public abstract IFailureMessageProvider FailureMessageProvider { get; }
    public IToleranceProvider ToleranceProvider => toleranceProvider ?? DefaultToleranceProvider.Instance;
    public TimeProvider TimeProvider => timeProvider ?? TimeProvider.System;
    public Result<T> ToResult<T>(T value) => ToResult().WithValue(value);

    public abstract bool ShouldContinueExecution { get; }

    protected abstract Result ToResult();

    protected string CreateMessage(IGuardRuleResult result) =>
        result.CustomMessage ?? FailureMessageProvider.CreateMessage(
            new FailureMessageContext(result.CustomExpression ?? result.CapturedExpression, result.RuleContext));
}