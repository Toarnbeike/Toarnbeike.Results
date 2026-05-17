namespace Toarnbeike.Results.Ensure.Implementation.RuleResults;

internal sealed class FailingRuleResult<T> : GuardRuleResultBase, IFailingRuleResult
{
    public object? AttemptedValueAsObject => AttemptedValue;

    /// <summary>
    /// Message describing the failure.
    /// This can still be modified after the evaluation using the WithMessage methods,
    /// allowing for fluent customization of the failure message while still continuing the guard chain.
    /// </summary>
    public string Message { get; private set; }

    /// <summary>
    /// Argument name of the attempted value.
    /// This is captured automatically from the caller argument expression, but can be overridden using WithArgumentName.
    /// </summary>
    public string ArgumentName { get; private set; }

    /// <summary>
    /// The attempted value that caused the guard to fail.
    /// </summary>
    internal T AttemptedValue { get; }

    /// <summary>
    /// The name of the guard that detected the fail.
    /// </summary>
    public string GuardName { get; }

    /// <summary>
    /// Optionally the constraint associated with the guard that failed.
    /// This can be used to provide additional context for failure messages or for custom handling in the parent guard chain.
    /// </summary>
    public object? Constraint { get; }

    internal FailingRuleResult(IGuardChain chain, string message, string argumentName,
        T attemptedValue, string guardName, object? constraint) : base(chain)
    {
        Message = message;
        ArgumentName = argumentName;
        AttemptedValue = attemptedValue;
        GuardName = guardName;
        Constraint = constraint;
    }

    /// <inheritdoc/>
    public override IGuardRuleResult WithMessage(string? message)
    {
        if (message is not null)
        {
            Message = message;
        }
        return this;
    }

    /// <inheritdoc/>
    public override IGuardRuleResult WithMessage(Func<string, object?, string> messageBuilder)
    {
        Message = messageBuilder(ArgumentName, Constraint);
        return this;
    }

    /// <inheritdoc/>
    public override IGuardRuleResult WithArgumentName(string? argumentName)
    {
        if (argumentName is not null)
        {
            ArgumentName = argumentName;
        }

        return this;
    }
}