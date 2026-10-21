namespace Toarnbeike.Results.Rules.Evaluations.Collections;

/// <summary>
/// Checks if a given collection contains a specified expected item.
/// </summary>
/// <typeparam name="TItem">The type of the items in the collection.</typeparam>
/// <param name="expected">The expected item to check for in the collection.</param>
internal class ContainsEvaluator<TItem>(TItem expected) : IEvaluation<IEnumerable<TItem>>
{
    public EvaluationResult Evaluate(IEnumerable<TItem> value)
    {
        ArgumentNullException.ThrowIfNull(expected);
        return EvaluationResult.Create(
            attemptedValue: value,
            isValid: value != null && value.Contains(expected),
            evaluatorName: "ContainsEvaluator",
            details: [("type", typeof(TItem).Name), ("expected", expected)]);
    }
}
