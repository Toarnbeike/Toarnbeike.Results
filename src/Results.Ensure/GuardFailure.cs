namespace Toarnbeike.Results.Ensure;

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

    public GuardFailure(string guardName, string message, string? expr)
    {
        var expression = expr ?? "<unknown>";
        GuardName = guardName;
        Expression = expression;
        Constraint = null;
        AttemptedValue = null;
        Message = message;
        Category = FailureCategory.Business;
    }
}