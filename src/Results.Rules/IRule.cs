using System.Globalization;
using Toarnbeike.Results.Rules.EvaluationData;

namespace Toarnbeike.Results.Rules;

public interface IRule<in TInput, TOutput, TEvaluationData> 
    where TEvaluationData : IRuleEvaluationData
    where TOutput : notnull
{
    /// <summary>
    /// Determines whether the provided value satisfies the rule's condition.
    /// </summary>
    /// <param name="providedValue">The provided value</param>
    /// <param name="context">The validation context, used for tolerances and current time.</param>
    /// <returns>Boolean indicating if the value is valid given the rule.</returns>
    public RuleResult<TOutput, TEvaluationData> Validate(TInput providedValue, ValidationContext context);

    /// <summary>
    /// Get the localized failure message for the provided value.
    /// </summary>
    /// <param name="attemptedValue">The provided value</param>
    /// <param name="evaluationData">The evaluation data for the rule.</param>
    /// <param name="context">The validation message context.</param>
    /// <returns>The localized failure message</returns>
    public string GetFailureMessage(TInput attemptedValue, TEvaluationData evaluationData, ValidationMessageContext context);

    /// <summary>
    /// Get the localized message describing the condition of the rule.
    /// </summary>
    /// <param name="culture">The culture for localization.</param>
    /// <returns>The localized condition message</returns>
    public string GetConditionMessage(CultureInfo culture);
}

public interface IRule<TInput> : IRule<TInput, TInput, EmptyRuleEvaluationData>
    where TInput : notnull
{
    /// <summary>
    /// Determines whether the provided value satisfies the rule's condition.
    /// </summary>
    /// <param name="providedValue">The provided value</param>
    /// <param name="context">The validation context, used for tolerances and current time.</param>
    /// <returns>Boolean indicating if the value is valid given the rule.</returns>
    bool IsValid(TInput providedValue, ValidationContext context);

    /// <summary>
    /// Get the localized failure message for the provided value.
    /// </summary>
    /// <param name="attemptedValue">The provided value</param>
    /// <param name="context">The validation message context.</param>
    /// <returns>The localized failure message</returns>
    string GetFailureMessage(TInput attemptedValue, ValidationMessageContext context);

    RuleResult<TInput, EmptyRuleEvaluationData> IRule<TInput, TInput, EmptyRuleEvaluationData>.Validate(TInput providedValue, ValidationContext context)
    {
        var isValid = IsValid(providedValue, context);
        return new RuleResult<TInput, EmptyRuleEvaluationData>(isValid, providedValue, EmptyRuleEvaluationData.Instance);
    }

    string IRule<TInput, TInput, EmptyRuleEvaluationData>.GetFailureMessage(TInput attemptedValue, EmptyRuleEvaluationData _, ValidationMessageContext context)
    {
        return GetFailureMessage(attemptedValue, context);
    }
}

public interface IRule<TInput, TEvaluationData> : IRule<TInput, TInput, TEvaluationData>
    where TEvaluationData : IRuleEvaluationData
    where TInput : notnull
{
    /// <summary>
    /// Determines whether the provided value satisfies the rule's condition.
    /// </summary>
    /// <param name="providedValue">The provided value</param>
    /// <param name="context">The validation context, used for tolerances and current time.</param>
    /// <returns>Boolean indicating if the value is valid given the rule.</returns>
    public new RuleResult<TEvaluationData> Validate(TInput providedValue, ValidationContext context);

    RuleResult<TInput, TEvaluationData> IRule<TInput, TInput, TEvaluationData>.Validate(TInput providedValue, ValidationContext context)
    {
        var result = Validate(providedValue, context);
        return new RuleResult<TInput, TEvaluationData>(result.IsValid, providedValue, result.EvaluationData);
    }
}