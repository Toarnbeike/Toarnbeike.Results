namespace Toarnbeike.Results.Rules.Evaluations.Comparisons;

/// <summary>
/// Checks if a given value is greater than or equal to a specified minimum value.
/// </summary>
/// <typeparam name="TComparable">The type of the value being compared.</typeparam>
/// <param name="min">The minimum value to compare against.</param>
internal sealed class AtLeastEvaluator<TComparable>(TComparable min) : IEvaluation<TComparable> 
    where TComparable : IComparable<TComparable>
{
    public EvaluationResult Evaluate(TComparable value) =>
        EvaluationResult.Create(
            attemptedValue: value, 
            isValid: value.CompareTo(min) >= 0, 
            evaluatorName: "AtLeastEvaluator", 
            details: [("type", typeof(TComparable).Name), ("min", min)]);
}