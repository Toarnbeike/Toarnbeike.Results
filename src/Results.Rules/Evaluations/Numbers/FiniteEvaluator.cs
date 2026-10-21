using System.Numerics;

namespace Toarnbeike.Results.Rules.Evaluations.Numbers;

/// <summary>
/// Checks if a given floating point value is finite (not NaN or Infinity).
/// </summary>
/// <typeparam name="TFloat">The type of the floating point value.</typeparam>
internal class FiniteEvaluator<TFloat> : IEvaluation<TFloat>
    where TFloat : struct, IFloatingPoint<TFloat>
{
    public EvaluationResult Evaluate(TFloat value)
    {
        var isValid = !TFloat.IsNaN(value) && TFloat.IsFinite(value);
        return EvaluationResult.Create(
            attemptedValue: value,
            isValid: isValid,
            evaluatorName: "FiniteEvaluator",
            details: [("type", typeof(TFloat).Name)]);
    }
}
