using Toarnbeike.Results.Ensure.Abstractions;

namespace Toarnbeike.Results.Ensure.Implementation.RuleResults;

/// <summary>
/// Base for the <see cref="FailingRuleResult{T}"/> and <see cref="SuccessRuleResult"/>.
/// Provides the methods for continuing the guard chain after a guard evaluation, by delegating to the parent chain. This allows both the actual rule result and the omitted rule result to support fluent configuration and continuation of the guard chain.
/// </summary>
internal abstract class GuardRuleResultBase(IGuardChain chain) : IGuardRuleResult
{
    /// <inheritdoc />
    public IGuardTarget<T> That<T>(T value, string? expr = null) => chain.That(value, expr);

    /// <inheritdoc />
    public Result ToResult() => chain.ToResult();
    public Result<T> ToResult<T>(T value) => chain.ToResult(value);

    /// <inheritdoc />
    public IGuardRuleResult RegisterEvaluation<T>(T attemptedValue, string expression, bool isValid, string guardName,
        RuleContext ruleContext) =>
        chain.RegisterEvaluation(attemptedValue, expression, isValid, guardName, ruleContext);

    public IToleranceProvider ToleranceProvider => chain.ToleranceProvider;
    public TimeProvider TimeProvider => chain.TimeProvider;

    /// <inheritdoc />
    public abstract IGuardRuleResult WithMessage(string? message);

    /// <inheritdoc />
    public abstract IGuardRuleResult WithMessage(Func<string, RuleContext, string> messageBuilder);

    /// <inheritdoc />
    public abstract IGuardRuleResult WithArgumentName(string? name);
}