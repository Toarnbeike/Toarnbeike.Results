namespace Toarnbeike.Results.Rules.Evaluations.Comparisons;

/// <summary>
/// Checks if a given value is strictly greater than a specified minimum value.
/// </summary>
/// <typeparam name="TComparable">The type of the value being compared.</typeparam>
/// <param name="min">The minimum value to compare against.</param>
internal sealed class GreaterThanEvaluator<TComparable>(TComparable min) : IEvaluation<TComparable>
    where TComparable : IComparable<TComparable>
{
    public EvaluationResult Evaluate(TComparable value) =>
        EvaluationResult.Create(
            attemptedValue: value,
            isValid: value.CompareTo(min) > 0,
            evaluatorName: "GreaterThanEvaluator",
            details: [("type", typeof(TComparable).Name), ("min", min)]);
}
