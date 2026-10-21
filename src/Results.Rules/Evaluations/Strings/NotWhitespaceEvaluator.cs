namespace Toarnbeike.Results.Rules.Evaluations.Strings;

/// <summary>
/// Checks if a given string is not null, empty, or consists only of whitespace characters.
/// </summary>
internal class NotWhitespaceEvaluator : IEvaluation<string>
{
    public EvaluationResult Evaluate(string value) =>
        EvaluationResult.Create(
            attemptedValue: value,
            isValid: !string.IsNullOrWhiteSpace(value),
            evaluatorName: "NotWhitespaceEvaluator",
            details: []);
}