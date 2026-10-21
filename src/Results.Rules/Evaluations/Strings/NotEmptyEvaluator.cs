namespace Toarnbeike.Results.Rules.Evaluations.Strings;

/// <summary>
/// Checks if a given string is not null or empty.
/// </summary>
internal class NotEmptyEvaluator : IEvaluation<string>
{
    public EvaluationResult Evaluate(string value) =>
        EvaluationResult.Create(
            attemptedValue: value,
            isValid: !string.IsNullOrEmpty(value),
            evaluatorName: "NotEmptyEvaluator",
            details: []);
}
