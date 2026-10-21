namespace Toarnbeike.Results.Rules.Evaluations.Comparisons;

/// <summary>
/// Checks if a given value is between a specified minimum and maximum value (inclusive).
/// </summary>
/// <typeparam name="TComparable">The type of the value being compared.</typeparam>
/// <param name="min">The minimum value.</param>
/// <param name="max">The maximum value.</param>
internal sealed class BetweenEvaluator<TComparable>(TComparable min, TComparable max) : IEvaluation<TComparable>
    where TComparable : IComparable<TComparable>
{
    public EvaluationResult Evaluate(TComparable value) =>
        EvaluationResult.Create(
            attemptedValue: value,
            isValid: value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0,
            evaluatorName: "BetweenEvaluator",
            details: [("type", typeof(TComparable).Name), ("min", min), ("max", max)]);
}
