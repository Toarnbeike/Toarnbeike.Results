namespace Toarnbeike.Results.Rules;

/// <summary>
/// Result of a rule validation, containing the validity status, output, and evaluation data.
/// </summary>
/// <typeparam name="TOutput">The type of the output of the rule.</typeparam>
/// <typeparam name="TEvaluationData">The type of the evaluation data for the rule.</typeparam>
/// <param name="IsValid">Indicates if the rule is valid.</param>
/// <param name="Output">The output of the rule. Rules can modify the output.</param>
/// <param name="EvaluationData">The evaluation data for the rule.</param>
public record RuleResult<TOutput, TEvaluationData>(
    bool IsValid, 
    TOutput Output,
    TEvaluationData EvaluationData)
    where TOutput : notnull
    where TEvaluationData : IRuleEvaluationData;

public sealed record RuleResult<TEvaluationData>(
    bool IsValid,
    TEvaluationData EvaluationData)
    where TEvaluationData : IRuleEvaluationData;