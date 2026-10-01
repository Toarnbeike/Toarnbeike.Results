using System.Globalization;
using Toarnbeike.Results.Rules.Attributes;
using Toarnbeike.Results.Rules.EvaluationData;

namespace Toarnbeike.Results.Rules.PredefinedRules.StringRules;

/// <summary>
/// Rule that a string must not be null or empty.
/// </summary>
[GenerateTargetExtension]
[GenerateCollectionTargetExtension]
internal class NotEmptyRule : IRule<string?, string, EmptyRuleEvaluationData>
{
    public RuleResult<string, EmptyRuleEvaluationData> Validate(string? value, ValidationContext context)
    {
        return new RuleResult<string, EmptyRuleEvaluationData>(!string.IsNullOrEmpty(value), value!, EmptyRuleEvaluationData.Instance);
    }

    public string GetFailureMessage(string? attemptedValue, EmptyRuleEvaluationData _, ValidationMessageContext context)
    {
        return context.Culture.DisplayName switch
        {
            "nl-NL" => $"{context.PropertyName} mag niet leeg zijn.",
            _ => $"{context.PropertyName} must not be empty."
        };
    }

    public string GetConditionMessage(CultureInfo culture)
    {
        return culture.DisplayName switch
        {
            "nl-NL" => "Niet leeg of witruimte.",
            _ => "Not empty or whitespace."
        };
    }
}