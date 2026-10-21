using System.Text.RegularExpressions;
using Toarnbeike.Results.Rules.Evaluations;

namespace Toarnbeike.Results.Rules.Messages;

/// <summary>
/// Provides functionality to format messages for rule evaluations based on the rule name and evaluation result.
/// It uses the message templates provided by the IRuleMessageProvider to generate user-friendly messages for rule violations.
/// Values in the message templates are replaced with corresponding values from the evaluation result's details.
/// These values are case-insensitive and are formatted according to the current culture.
/// </summary>
internal static class RuleMessageFormatter
{
    /// <summary>
    /// Formats a message for a given rule evaluation result based on the rule name and the evaluation details.
    /// </summary>
    /// <param name="ruleName">The name of the rule being evaluated.</param>
    /// <param name="result">The result of the rule evaluation.</param>
    /// <returns>A formatted message that is empty for valid results, or describing the rule evaluation failure.</returns>
    public static string FormatMessage(string ruleName, EvaluationResult result)
    {
        if (result.IsValid)
        {
            return string.Empty;
        }

        return RuleContext.MessageProvider.MessageTemplatesByRule.TryGetValue(ruleName, out var template)
            ? CreateTemplatedMessage(template, result)
            : CreateDefaultMessage(ruleName, result);
    }

    /// <summary>
    /// If no specific message template is found for the rule, this method creates a mostly computer readable message that includes the rule name, evaluator name, attempted value, and any additional evaluation details.
    /// </summary>
    /// <param name="ruleName">The name of the rule being evaluated.</param>
    /// <param name="result">The result of the rule evaluation.</param>
    /// <returns>A formatted message describing the rule evaluation failure.</returns>
    private static string CreateDefaultMessage(string ruleName, EvaluationResult result)
    {
        var attemptedValue = FormatValue(result.EvaluationDetails["attemptedValue"]);
        var details = string.Join(", ", result.EvaluationDetails.Select(kvp => $"{kvp.Key}: {FormatValue(kvp.Value)}"));
        return $"Evaluation failed | Rule: {ruleName} | Evaluator: {result.Evaluator} | Attempted Value: {attemptedValue} | Context: {details}";
    }

    /// <summary>
    /// Formats a message based on a provided template and the evaluation result. It replaces placeholders in the template with corresponding values from the evaluation details.
    /// </summary>
    /// <param name="template">The message template containing placeholders.</param>
    /// <param name="result">The result of the rule evaluation.</param>
    /// <returns>A formatted message with placeholders replaced by corresponding evaluation detail values.</returns>
    private static string CreateTemplatedMessage(string template, EvaluationResult result)
    {
        return Regex.Replace(template, @"\{(?<key>[^\{\}]+)\}", match =>
        {
            var key = match.Groups["key"].Value;
            if (!result.EvaluationDetails.TryGetValue(key, out var value))
            {
                return match.Value; // Return the original placeholder if the key is not found
            }
            return FormatValue(value);
        }, RegexOptions.None, matchTimeout: TimeSpan.FromSeconds(1));
    }

    /// <summary>
    /// Formats a value to a string representation based on the current culture. If the value is null, it returns an empty string.
    /// </summary>
    /// <param name="value">The value to format.</param>
    /// <returns>A string representation of the value.</returns>
    private static string FormatValue(object? value)
    {
        return Convert.ToString(value, RuleContext.CurrentCulture) ?? string.Empty;
    }
}