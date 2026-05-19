using System.Runtime.CompilerServices;
using Toarnbeike.Results.Ensure.Abstractions;
using Toarnbeike.Results.Ensure.Implementation.FailureMessages;
using Toarnbeike.Results.Ensure.Implementation.RuleResults;
using Toarnbeike.Results.Ensure.Implementation.Targets;
using Toarnbeike.Results.Extensions;

namespace Toarnbeike.Results.Ensure.Implementation.Chaining;

internal abstract class GuardChainBase(IFailureMessageProvider messageProvider, IToleranceProvider toleranceProvider) : IGuardChain
{
    protected abstract bool ShouldSkipExecution { get; }
    public IGuardTarget<T> That<T>(T value, [CallerArgumentExpression(nameof(value))] string? expr = null)
    {
        return ShouldSkipExecution
            ? new ShortCircuitedGuardTarget<T>(this)
            : new GuardTarget<T>(this, value, expr ?? "<unknown>");
    }

    public IGuardRuleResult RegisterEvaluation<T>(T attemptedValue, string expression, bool isValid,
        string guardName, RuleContext ruleContext)
    {
        if (isValid)
            return new SuccessRuleResult(this);

        var failingResult = new FailingRuleResult<T>(
            this,
            messageProvider.CreateMessage(new FailureMessageContext(expression, attemptedValue, ruleContext, guardName)),
            expression,
            attemptedValue,
            guardName,
            ruleContext);

        HandleFailure(failingResult);

        return failingResult;
    }

    public IToleranceProvider ToleranceProvider => toleranceProvider;

    protected abstract void HandleFailure<T>(FailingRuleResult<T> result);

    public abstract Result ToResult();

    public Result<T> ToResult<T>(T value) => ToResult().WithValue(value);
}