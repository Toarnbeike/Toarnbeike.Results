namespace Toarnbeike.Results.Ensure;

/// <summary>
/// Represents the outcome of a single guard evaluation.
/// </summary>
/// <remarks>
/// A guard rule result supports fluent customization of the produced failure
/// metadata while still allowing continuation of the parent guard chain.
/// </remarks>
public interface IGuardRuleResult : IGuardChain
{
    /// <summary>
    /// Overrides the generated failure message.
    /// </summary>
    /// <param name="message">The custom failure message.</param>
    /// <returns>The updated guard rule result.</returns>
    IGuardRuleResult WithMessage(string? message);

    /// <summary>
    /// Overrides the generated failure message using a custom message factory.
    /// </summary>
    /// <param name="messageBuilder">
    /// A delegate that creates the failure message based on the argument name and guard constraint.
    /// </param>
    /// <returns>The updated guard rule result.</returns>
    IGuardRuleResult WithMessage(Func<string, object?, string> messageBuilder);

    /// <summary>
    /// Overrides the captured argument name used in generated failures.
    /// </summary>
    /// <param name="name">The custom argument name.</param>
    /// <returns>The updated guard rule result.</returns>
    IGuardRuleResult WithArgumentName(string? name);
}