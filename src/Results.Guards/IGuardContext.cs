using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Guards.Messages;
using Toarnbeike.Results.Guards.Tolerances;

namespace Toarnbeike.Results.Guards;

/// <summary>
/// Represents a fluent guard evaluation pipeline without a target.
/// It contains abstractions required for evaluating rules and creating failure messages.
/// </summary>
/// <remarks>
/// A guard chain coordinates guard execution and determines whether
/// evaluation should short-circuit or continue accumulating failures.
/// </remarks>
public interface IGuardContext
{
    internal IFailureMessageProvider FailureMessageProvider { get; }
    internal IToleranceProvider ToleranceProvider { get; }
    internal TimeProvider TimeProvider { get; }

    /// <summary>
    /// Finalizes the current guard pipeline and converts it into a <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="value">The value to include in the result.</param>
    /// <returns>
    /// A Success result when no failures occured, or, depending on the context either
    /// a <see cref="GuardFailure"/> representing the first failure (Ensure, Fail fast) or
    /// a <see cref="ValidationFailureSummary"/> representing all failures (Validate, Accumulate).
    /// </returns>
    internal Result<T> ToResult<T>(T value);

    internal bool ShouldContinueExecution { get; }
    internal List<IGuardRuleResult> Results { get; }
}