namespace Toarnbeike.Results.Ensure.Implementation.RuleResults;

internal sealed class SuccessRuleResult : GuardRuleResultBase, IGuardRuleResult
{
    internal SuccessRuleResult(IGuardChain chain) : base(chain) { }

    public override IGuardRuleResult WithMessage(string? message) => this;

    public override IGuardRuleResult WithMessage(Func<string, RuleContext, string> messageBuilder) => this;

    public override IGuardRuleResult WithArgumentName(string? argumentName) => this;
}