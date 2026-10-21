using System.Numerics;

namespace Toarnbeike.Results.Rules.Evaluations.Numbers;

/// <summary>
/// Checks if a given numeric value is a power of 2 (only one bit set in its binary representation).
/// </summary>
/// <typeparam name="TInt">The type of the integer numeric value.</typeparam>
internal class PowerOf2Evaluator<TInt> : IEvaluation<TInt>
    where TInt : struct, IBinaryInteger<TInt>
{
    public EvaluationResult Evaluate(TInt value) =>
        EvaluationResult.Create(
            attemptedValue: value,
            isValid: value > TInt.Zero && (value & (value - TInt.One)) == TInt.Zero,
            evaluatorName: "PowerOf2Evaluator",
            details: [("type", typeof(TInt).Name)]);
}