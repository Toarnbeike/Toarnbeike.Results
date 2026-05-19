using Toarnbeike.Results.Ensure.Abstractions;
using Toarnbeike.Results.Ensure.Implementation.RuleResults;

namespace Toarnbeike.Results.Ensure.Implementation.Targets;

/// <summary>
/// Implementation detail: For fast failing guard chains, we want to skip all subsequent evaluations after the first failure, but we still want to allow the fluent configuration methods (WithMessage, WithArgumentName) to be called without throwing an exception.
/// This class represents a dummy rule result that is returned for all evaluations after the first failure in a fast-fail guard chain.
/// It ignores all configuration and simply delegates back to the parent chain for any further evaluations or when ToResult is called.
/// </summary>
internal sealed class ShortCircuitedGuardTarget<T>(IGuardChain chain) : IGuardTarget<T>
{
    public T Value => default!;
    public string CapturedExpression => string.Empty;
    public IToleranceProvider ToleranceProvider => chain.ToleranceProvider;

    public IGuardRuleResult Evaluate(bool isValid, string guardName, params (string Key, object? Value)[] context)
        => new SuccessRuleResult(chain);

    public IGuardTarget<TOther> As<TOther>(Func<T, TOther> converter) =>
        new ShortCircuitedGuardTarget<TOther>(chain);
}