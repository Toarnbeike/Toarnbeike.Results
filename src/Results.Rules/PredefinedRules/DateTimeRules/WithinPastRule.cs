using System.Globalization;
using Toarnbeike.Results.Rules.Attributes;
using Toarnbeike.Results.Rules.EvaluationData;

namespace Toarnbeike.Results.Rules.PredefinedRules.DateTimeRules;

/// <summary>
/// Rule that the targeted date must be in the future, but by no more than the provided time span.
/// </summary>
/// <param name="timeSpan">The maximum time span by which the date can be in the past.  </param>
[GenerateTargetExtension]
[GeneratePrimitiveExtension]
[GenerateCollectionTargetExtension]
internal class WithinPastRule(TimeSpan timeSpan) : IRule<DateTime, UtcNowEvaluationData>
{
    public RuleResult<UtcNowEvaluationData> Validate(DateTime providedValue, ValidationContext context)
    {
        var utcNow = context.TimeProvider.GetUtcNow().DateTime;
        var isValid = providedValue >= utcNow - timeSpan && providedValue <= utcNow;
        var evaluationData = new UtcNowEvaluationData(utcNow);

        return new RuleResult<UtcNowEvaluationData>(isValid, evaluationData);
    }

    public string GetFailureMessage(DateTime attemptedValue, UtcNowEvaluationData evaluationData, ValidationMessageContext context)
    {
        var utcNow = evaluationData.UtcNow;
        FormattableString message = context.Culture.DisplayName switch
        {
            "nl-NL" => $"{context.PropertyName} moet binnen de afgelopen {timeSpan} liggen (tussen {utcNow - timeSpan} en {utcNow}), maar is {attemptedValue}",
            _ => $"{context.PropertyName} must be within the last {timeSpan} (between {utcNow - timeSpan} and {utcNow}), but is {attemptedValue}"
        };
        return message.ToString(context.Culture);
    }

    public string GetConditionMessage(CultureInfo culture)
    {
        FormattableString message = culture.DisplayName switch
        {
            "nl-NL" => $"maximaal {timeSpan} geleden",
            _ => $"within the last {timeSpan}"
        };
        return message.ToString(culture);
    }
}