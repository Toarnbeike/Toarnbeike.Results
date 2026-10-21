namespace Toarnbeike.Results.Rules.Evaluations.Predicates;

/// <summary>
/// Checks if a given value does not satisfy a specified predicate.
/// </summary>
/// <typeparam name="TProperty">The type of the property to evaluate.</typeparam>
/// <param name="predicate">The predicate to evaluate against the property value.</param>
internal class NotSatisfiesEvaluator<TProperty>(Func<TProperty, bool> predicate) : IEvaluation<TProperty>
{
    public EvaluationResult Evaluate(TProperty value) =>
        EvaluationResult.Create(
            attemptedValue: value,
            isValid: !predicate(value),
            evaluatorName: "NotSatisfiesEvaluator",
            details: [("type", typeof(TProperty).Name)]);
}
