using System.Numerics;

namespace Toarnbeike.Results.Rules.Evaluations.Numbers;

/// <summary>
/// Checks if a given floating point value is a multiple of a specified factor, within an optional tolerance.
/// </summary>
/// <typeparam name="TFloat">The type of the floating point value.</typeparam>
/// <param name="factor">The factor to check multiples of.</param>
/// <param name="tolerance">The optional tolerance value.</param>
internal class MultipleOfWithToleranceEvaluator<TFloat>(TFloat factor, TFloat? tolerance) : IEvaluation<TFloat>
    where TFloat : struct, IFloatingPoint<TFloat>, IMinMaxValue<TFloat>
{
    public EvaluationResult Evaluate(TFloat value)
    {
        tolerance ??= RuleContext.Tolerance<TFloat>();
        ArgumentOutOfRangeException.ThrowIfNegative(tolerance.Value);
        ArgumentOutOfRangeException.ThrowIfZero(factor);

        var modulus = value % factor;
        var isValid = modulus <= tolerance.Value || modulus >= factor - tolerance.Value;

        return EvaluationResult.Create(
            attemptedValue: value,
            isValid: isValid,
            evaluatorName: "MultipleOfWithToleranceEvaluator",
            details: [("type", typeof(TFloat).Name), ("factor", factor), ("tolerance", tolerance)]);
    }
}