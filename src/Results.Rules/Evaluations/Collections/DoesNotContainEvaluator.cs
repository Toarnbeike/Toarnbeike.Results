namespace Toarnbeike.Results.Rules.Evaluations.Collections;

/// <summary>
/// Checks if a given collection does not contain a specified unexpected item.
/// </summary>
/// <typeparam name="TItem">The type of the items in the collection.</typeparam>
/// <param name="unexpected">The unexpected item to check the collection does not contain.</param>
internal class DoesNotContainEvaluator<TItem>(TItem unexpected) : IEvaluation<IEnumerable<TItem>>
{
    public EvaluationResult Evaluate(IEnumerable<TItem> value)
    {
        ArgumentNullException.ThrowIfNull(unexpected);
        return EvaluationResult.Create(
            attemptedValue: value,
            isValid: value == null || !value.Contains(unexpected),
            evaluatorName: "DoesNotContainEvaluator",
            details: [("type", typeof(TItem).Name), ("unexpected", unexpected)]);
    }
}