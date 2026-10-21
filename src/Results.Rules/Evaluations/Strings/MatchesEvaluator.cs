using System.Text.RegularExpressions;

namespace Toarnbeike.Results.Rules.Evaluations.Strings;

/// <summary>
/// Checks if a given string matches a specified regular expression pattern.
/// </summary>
/// <param name="pattern">The regular expression pattern to match against the input string.</param>
internal class MatchesEvaluator(string pattern) : IEvaluation<string>
{
    public EvaluationResult Evaluate(string value) =>
        EvaluationResult.Create(
            attemptedValue: value,
            isValid: Regex.IsMatch(value, pattern, RegexOptions.None, TimeSpan.FromSeconds(1)),
            evaluatorName: "MatchesEvaluator",
            details: [("pattern", pattern)]);
}
