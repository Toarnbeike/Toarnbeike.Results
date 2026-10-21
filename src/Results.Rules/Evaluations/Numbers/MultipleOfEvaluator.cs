using System.Numerics;

namespace Toarnbeike.Results.Rules.Evaluations.Numbers;

/// <summary>
/// Checks if a given numeric value is a multiple of a specified factor.
/// </summary>
/// <typeparam name="TNumber">The type of the numeric value.</typeparam>
/// <param name="factor">The factor to check multiples of.</param>
internal class MultipleOfEvaluator<TNumber>(TNumber factor) : IEvaluation<TNumber>
    where TNumber : struct, INumber<TNumber>
{
    public EvaluationResult Evaluate(TNumber value)
    {
        ArgumentOutOfRangeException.ThrowIfZero(factor);
        var isValid = value % factor == TNumber.Zero;

        return EvaluationResult.Create(
            attemptedValue: value,
            isValid: isValid,
            evaluatorName: "MultipleOfEvaluator",
            details: [("type", typeof(TNumber).Name), ("factor", factor)]);
    }
}
