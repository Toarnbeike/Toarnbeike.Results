using Toarnbeike.Results.Ensure.Abstractions;
using Toarnbeike.Results.Ensure.Implementation.RuleResults;
using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.Ensure.Implementation.Chaining;

internal sealed class ValidateGuardChain(
    IFailureMessageProvider failureMessageProvider, IToleranceProvider toleranceProvider, TimeProvider timeProvider)
    : GuardChainBase(failureMessageProvider, toleranceProvider, timeProvider)
{
    private readonly List<IFailingRuleResult> _failures = [];

    protected override bool ShouldSkipExecution => false;

    protected override void HandleFailure<T>(FailingRuleResult<T> result) =>
        _failures.Add(result);

    public override Result ToResult()
    {
        return _failures.Count == 0
            ? Result.Success()
            : new ValidationFailureSummary(_failures.Select(result => 
                new ValidationFailure(result.ArgumentName, base.CreateMessage(result))));
    }
}