using Toarnbeike.Results.Ensure.Implementation.RuleResults;

namespace Toarnbeike.Results.Ensure;

/// <summary>
/// Failure that indicates that a Domain Guard (using Ensure) failed.
/// </summary>
public sealed record GuardFailure : Failure
{
    /// <summary>
    /// The name of the Guard that detected the failure
    /// </summary>
    public string GuardName { get; }

    /// <summary>
    /// The full path of the failed property
    /// </summary>
    public string Expression { get; }

    /// <summary>
    /// The constraint this Guard imposed.
    /// </summary>
    public RuleContext? Context { get; }

    /// <summary>
    /// The value that was guarded and caused the failure.
    /// </summary>
    public object? AttemptedValue { get; }

    /// <summary>
    /// The parameter name of the failed property. Contains only the last part of the full expression.
    /// </summary>
    public string ParameterName =>
        Expression.Contains('.') ? Expression[(Expression.LastIndexOf('.') + 1)..] : Expression;

    internal static GuardFailure FromRuleResult(IFailingRuleResult result) =>
        new(result.Message, result.GuardName, result.ArgumentName, result.Context, result.AttemptedValueAsObject);

    private GuardFailure(string message, string guardName, string expression, RuleContext context, object? attemptedValue)
    {
        Category = FailureCategory.Business;
        GuardName = guardName;
        Expression = expression;
        Context = context;
        AttemptedValue = attemptedValue;
        Message = message;
    }
}