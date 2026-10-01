namespace Toarnbeike.Results.Rules.EvaluationData;

/// <summary>
/// Base implementation of rule evaluation data, containing no additional information.
/// </summary>
public sealed record EmptyRuleEvaluationData : IRuleEvaluationData
{
    public static EmptyRuleEvaluationData Instance => new();
}