namespace Toarnbeike.Results.Rules.Evaluations.Strings;

/// <summary>
/// Checks if a given string is a valid URI.
/// </summary>
/// <param name="kind">The type of URI to check for (Relative, Absolute, or RelativeOrAbsolute).</param>
internal class IsUriEvaluator(UriKind? kind = null) : IEvaluation<string>
{
    public EvaluationResult Evaluate(string value) =>
        EvaluationResult.Create(
            attemptedValue: value,
            isValid: Uri.TryCreate(value, kind ?? UriKind.RelativeOrAbsolute, out var formatted),
            evaluatorName: "IsUriEvaluator",
            details: [("formatted", formatted!)]);
}
