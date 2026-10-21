namespace Toarnbeike.Results.Rules.Evaluations.Comparisons;

/// <summary>
/// Checks if a given value is not equal to a specified target value.
/// </summary>
/// <typeparam name="TComparable">The type of the value being compared.</typeparam>
/// <param name="target">The target value to compare against.</param>
internal sealed class NotEqualToEvaluator<TComparable>(TComparable target) : IEvaluation<TComparable>
    where TComparable : IComparable<TComparable>
{
    public EvaluationResult Evaluate(TComparable value) =>
        EvaluationResult.Create(
            attemptedValue: value,
            isValid: value.CompareTo(target) != 0,
            evaluatorName: "NotEqualToEvaluator",
            details: [("type", typeof(TComparable).Name), ("target", target)]);
}