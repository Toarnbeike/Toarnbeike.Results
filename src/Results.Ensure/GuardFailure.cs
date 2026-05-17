using Toarnbeike.Results.Ensure.Implementation.RuleResults;

namespace Toarnbeike.Results.Ensure;

//[Obsolete]
internal readonly record struct GuardResult(bool IsValid, Func<string, string> MessageBuilder, object? Constraint);

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
    public object? Constraint { get; }

    /// <summary>
    /// The value that was guarded and caused the failure.
    /// </summary>
    public object? AttemptedValue { get; }

    /// <summary>
    /// The parameter name of the failed property. Contains only the last part of the full expression.
    /// </summary>
    public string ParameterName =>
        Expression.Contains('.') ? Expression[(Expression.LastIndexOf('.') + 1)..] : Expression;

    //[Obsolete]
    internal GuardFailure(GuardResult error, string guardName, string? expr, object? attemptedValue, string? customMessage)
    {
        var expression = expr ?? "<unknown>";
        GuardName = guardName;
        Expression = expression;
        Constraint = error.Constraint;
        AttemptedValue = attemptedValue;
        Message = customMessage ?? error.MessageBuilder(expression);
        Category = FailureCategory.Business;
    }

    internal static GuardFailure FromRuleResult(IFailingRuleResult result) =>
        new(result.Message, result.GuardName, result.ArgumentName, result.Constraint, result.AttemptedValueAsObject);

    private GuardFailure(string message, string guardName, string expression, object? constraint, object? attemptedValue)
    {
        Category = FailureCategory.Business;
        GuardName = guardName;
        Expression = expression;
        Constraint = constraint;
        AttemptedValue = attemptedValue;
        Message = message;
    }
}