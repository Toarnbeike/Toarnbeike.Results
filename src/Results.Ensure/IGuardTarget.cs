using Toarnbeike.Results.Ensure.Abstractions;

namespace Toarnbeike.Results.Ensure;

/// <summary>
/// Represents a value currently participating in a guard pipeline.
/// </summary>
/// <typeparam name="T">The type of the guarded value.</typeparam>
public interface IGuardTarget<out T>
{
    /// <summary>
    /// Gets the guarded value.
    /// </summary>
    internal T Value { get; }

    /// <summary>
    /// Gets the captured caller argument expression for the guarded value.
    /// </summary>
    internal string CapturedExpression { get; }

    internal IToleranceProvider ToleranceProvider { get; }

    /// <summary>
    /// Evaluates a guard condition for the current value.
    /// </summary>
    /// <param name="isValid"> Indicates whether the guard condition succeeded. </param>
    /// <param name="guardName"> The name of the guard that performed the evaluation. </param>
    /// <param name="context"> Optional rule context associated with the guard. </param>
    /// <returns>
    /// A configurable rule result representing the outcome of the evaluation.
    /// </returns>
    internal IGuardRuleResult Evaluate(
        bool isValid,
        string guardName,
        params (string Key, object? Value)[] context);

    internal IGuardTarget<TOther> As<TOther>(Func<T, TOther> converter);
}