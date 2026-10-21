using System.Numerics;

namespace Toarnbeike.Results.Rules.Evaluations.Numbers;

/// <summary>
/// Checks if a given numeric value is odd (least significant bit is 1).
/// </summary>
/// <typeparam name="TInt">The type of the integer numeric value.</typeparam>
internal class OddEvaluator<TInt> : IEvaluation<TInt>
    where TInt : struct, IBinaryInteger<TInt>
{
    public EvaluationResult Evaluate(TInt value) =>
        EvaluationResult.Create(
            attemptedValue: value,
            isValid: (value & TInt.One) == TInt.One,
            evaluatorName: "OddEvaluator",
            details: [("type", typeof(TInt).Name)]);
}
