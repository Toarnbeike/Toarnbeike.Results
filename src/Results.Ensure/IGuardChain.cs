using System.Runtime.CompilerServices;
using Toarnbeike.Results.Ensure.Abstractions;
using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.Ensure;

/// <summary>
/// Represents a fluent guard evaluation pipeline.
/// </summary>
/// <remarks>
/// A guard chain coordinates guard execution and determines whether
/// evaluation should short-circuit or continue accumulating failures.
/// </remarks>
public interface IGuardChain
{
    /// <summary>
    /// Begins evaluation of a new value within the current guard pipeline.
    /// </summary>
    /// <typeparam name="T">The type of the value being guarded.</typeparam>
    /// <param name="value">The value to guard.</param>
    /// <param name="expr"> Automatically captured caller argument expression representing the guarded value. </param>
    /// <returns> A guard target that exposes the available guard extensions for the value. </returns>
    IGuardTarget<T> That<T>(T value, [CallerArgumentExpression(nameof(value))] string? expr = null);

    /// <summary>
    /// Finalizes the current guard pipeline and converts it into a <see cref="Result"/>.
    /// </summary>
    /// <returns>
    /// A Success result when no failures occured, or, depending on the context either
    /// a <see cref="GuardFailure"/> representing the first failure (Ensure, Fail fast) or
    /// a <see cref="ValidationFailureSummary"/> representing all failures (Validate, Accumulate).
    /// </returns>
    Result ToResult();

    /// <summary>
    /// Finalizes the current guard pipeline and converts it into a <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="value">The value to include in the result.</param>
    /// <returns>
    /// A Success result when no failures occured, or, depending on the context either
    /// a <see cref="GuardFailure"/> representing the first failure (Ensure, Fail fast) or
    /// a <see cref="ValidationFailureSummary"/> representing all failures (Validate, Accumulate).
    /// </returns>
    Result<T> ToResult<T>(T value);

    internal IGuardRuleResult RegisterEvaluation<T>(T attemptedValue, string expression, bool isValid, string guardName, RuleContext context);

    internal IToleranceProvider ToleranceProvider { get; }
}