namespace Toarnbeike.Results.Rules.Evaluations.Predicates;

/// <summary>
/// Checks if a given value satisfies a specified predicate.
/// </summary>
/// <typeparam name="TProperty">The type of the property to evaluate.</typeparam>
/// <param name="predicate">The predicate to evaluate against the property value.</param>
internal class SatisfiesEvaluator<TProperty>(Func<TProperty, bool> predicate) : IEvaluation<TProperty>
{
    public EvaluationResult Evaluate(TProperty value) =>
        EvaluationResult.Create(
            attemptedValue: value,
            isValid: predicate(value),
            evaluatorName: "SatisfiesEvaluator",
            details: [("type", typeof(TProperty).Name)]);
}
