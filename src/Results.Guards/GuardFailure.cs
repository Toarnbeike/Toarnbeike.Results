using Toarnbeike.Results.Guards.Core;

namespace Toarnbeike.Results.Guards;

/// <summary>
/// Failure that indicates that a Domain Guard (using Result.Ensure) failed.
/// </summary>
public sealed record GuardFailure : Failure
{
    /// <summary>
    /// The full path of the failed property
    /// </summary>
    public string Expression { get; }

    /// <summary>
    /// The context that was used to determine the validity of the rule that caused the failure.
    /// </summary>
    public RuleContext RuleContext { get; }

    internal static GuardFailure FromGuardRuleResult(IGuardRuleResult result, Func<IGuardRuleResult, string> messageFactory) =>
        new(messageFactory.Invoke(result), result.CapturedExpression, result.RuleContext);

    private GuardFailure(string message, string expression, RuleContext ruleContext)
    {
        Category = FailureCategory.Business;
        Message = message;
        Expression = expression;
        RuleContext = ruleContext;
    }
}