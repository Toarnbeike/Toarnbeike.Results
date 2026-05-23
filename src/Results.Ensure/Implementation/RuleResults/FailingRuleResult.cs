namespace Toarnbeike.Results.Ensure.Implementation.RuleResults;

internal sealed class FailingRuleResult<T> : GuardRuleResultBase, IFailingRuleResult
{
    public object? AttemptedValueAsObject => AttemptedValue;

    /// <summary>
    /// Custom message describing the failure.
    /// This is the value that is saved using the WithMessage methods,
    /// allowing for fluent customization of the failure message while still continuing the guard chain.
    /// </summary>
    public string? CustomMessage { get; private set; } = null;

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
    public RuleContext Context { get; }

    internal FailingRuleResult(IGuardChain chain, string argumentName,
        T attemptedValue, string guardName, RuleContext context) : base(chain)
    {
        ArgumentName = argumentName;
        AttemptedValue = attemptedValue;
        GuardName = guardName;
        Context = context;
    }

    /// <inheritdoc/>
    public override IGuardRuleResult WithMessage(string? message)
    {
        if (message is not null)
        {
            CustomMessage = message;
        }
        return this;
    }

    /// <inheritdoc/>
    public override IGuardRuleResult WithMessage(Func<string, RuleContext, string> messageBuilder)
    {
        CustomMessage = messageBuilder(ArgumentName, Context);
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