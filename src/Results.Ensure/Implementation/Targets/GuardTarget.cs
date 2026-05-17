using Toarnbeike.Results.Ensure.Abstractions;
using Toarnbeike.Results.Ensure.Implementation.RuleResults;

namespace Toarnbeike.Results.Ensure.Implementation.Targets;

internal sealed class GuardTarget<T> : IGuardTarget<T>
{
    private readonly IGuardChain _chain;

    public T Value { get; }
    public string CapturedExpression { get; }

    public IToleranceProvider ToleranceProvider => _chain.ToleranceProvider;

    /// <summary>
    /// The <see cref="IGuardChain"/> is used to delegate the actual registration of guard evaluations and the finalization of the guard pipeline to the parent chain.
    /// This allows the guard target to simply represent the current value and its captured expression,
    /// while all logic related to how evaluations are handled (short-circuiting, accumulating failures, etc.) is contained within the guard chain implementations.
    /// </summary>
    public IGuardRuleResult Evaluate(bool isValid, string guardName, object? constraint = null) =>
        _chain.RegisterEvaluation(Value, CapturedExpression, isValid, guardName, constraint);

    public IGuardTarget<TOther> As<TOther>(Func<T, TOther> converter) => 
        new GuardTarget<TOther>(_chain, converter(Value), CapturedExpression);

    internal GuardTarget(IGuardChain chain, T value, string capturedExpression) =>
        (_chain, Value, CapturedExpression) = (chain, value, capturedExpression);
}