using Toarnbeike.Results.Ensure.Abstractions;
using Toarnbeike.Results.Ensure.Implementation.RuleResults;

namespace Toarnbeike.Results.Ensure.Implementation.Chaining;

internal sealed class EnsureGuardChain(
    IFailureMessageProvider failureMessageProvider, IToleranceProvider toleranceProvider) 
    : GuardChainBase(failureMessageProvider, toleranceProvider)
{
    private IFailingRuleResult? _failure;

    protected override bool ShouldSkipExecution => _failure is not null;

    protected override void HandleFailure<T>(FailingRuleResult<T> result)
    {
        _failure = result;
    }

    public override Result ToResult() 
        => _failure is null 
            ? Result.Success()
            : GuardFailure.FromRuleResult(_failure);
}