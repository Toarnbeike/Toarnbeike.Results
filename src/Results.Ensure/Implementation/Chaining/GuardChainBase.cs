using System.Runtime.CompilerServices;
using Toarnbeike.Results.Ensure.Abstractions;
using Toarnbeike.Results.Ensure.Implementation.RuleResults;
using Toarnbeike.Results.Ensure.Implementation.Targets;
using Toarnbeike.Results.Ensure.Implementation.Tolerances;
using Toarnbeike.Results.Extensions;

namespace Toarnbeike.Results.Ensure.Implementation.Chaining;

internal abstract class GuardChainBase(IToleranceProvider? toleranceProvider = null) : IGuardChain
{
    protected abstract bool ShouldSkipExecution { get; }
    public IGuardTarget<T> That<T>(T value, [CallerArgumentExpression(nameof(value))] string? expr = null)
    {
        return ShouldSkipExecution
            ? new ShortCircuitedGuardTarget<T>(this)
            : new GuardTarget<T>(this, value, expr ?? "<unknown>");
    }

    public IGuardRuleResult RegisterEvaluation<T>(T attemptedValue, string expression, bool isValid,
        string guardName, object? constraint)
    {
        if (isValid)
            return new SuccessRuleResult(this);

        var failingResult = new FailingRuleResult<T>(
            this,
            $"{guardName} failed for {expression}.",
            expression,
            attemptedValue,
            guardName,
            constraint);

        HandleFailure(failingResult);

        return failingResult;
    }

    public IToleranceProvider ToleranceProvider => toleranceProvider ?? new DefaultToleranceProvider();

    protected abstract void HandleFailure<T>(FailingRuleResult<T> result);

    public abstract Result ToResult();

    public Result<T> ToResult<T>(T value) => ToResult().WithValue(value);
}