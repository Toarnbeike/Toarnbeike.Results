namespace Toarnbeike.Results.Rules.Evaluations.Predicates;

/// <summary>
/// Checks if a given value satisfies a specified asynchronous predicate.
/// </summary>
/// <typeparam name="TProperty">The type of the property to evaluate.</typeparam>
/// <param name="predicate">The asynchronous predicate to evaluate against the property value.</param>
internal class SatisfiesAsyncEvaluator<TProperty>(Func<TProperty, Task<bool>> predicate) : IAsyncEvaluation<TProperty>
{
    public async Task<EvaluationResult> EvaluateAsync(TProperty value)
    {
        var isValid = await predicate(value);
        return EvaluationResult.Create(
            attemptedValue: value,
            isValid: isValid,
            evaluatorName: "SatisfiesAsyncEvaluator",
            details: [("type", typeof(TProperty).Name)]);
    }
}
