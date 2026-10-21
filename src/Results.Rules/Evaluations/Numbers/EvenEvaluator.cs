using System.Numerics;

namespace Toarnbeike.Results.Rules.Evaluations.Numbers;

/// <summary>
/// Checks if a given numeric value is even (least significant bit is 0).
/// </summary>
/// <typeparam name="TInt">The type of the integer numeric value.</typeparam>
internal class EvenEvaluator<TInt> : IEvaluation<TInt>
    where TInt : struct, IBinaryInteger<TInt>
{
    public EvaluationResult Evaluate(TInt value) =>
        EvaluationResult.Create(
            attemptedValue: value,
            isValid: (value & TInt.One) == TInt.Zero,
            evaluatorName: "EvenEvaluator",
            details: [("type", typeof(TInt).Name)]);
}
