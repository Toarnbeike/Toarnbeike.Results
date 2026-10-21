namespace Toarnbeike.Results.Rules.Evaluations.Comparisons;

/// <summary>
/// Checks if a given value is less than or equal to a specified maximum value.
/// </summary>
/// <typeparam name="TComparable">The type of the value being compared.</typeparam>
/// <param name="max">The maximum value to compare against.</param>
internal sealed class AtMostEvaluator<TComparable>(TComparable max) : IEvaluation<TComparable>
    where TComparable : IComparable<TComparable>
{
    public EvaluationResult Evaluate(TComparable value) =>
        EvaluationResult.Create(
            attemptedValue: value,
            isValid: value.CompareTo(max) <= 0,
            evaluatorName: "AtMostEvaluator",
            details: [("type", typeof(TComparable).Name), ("max", max)]);
}
