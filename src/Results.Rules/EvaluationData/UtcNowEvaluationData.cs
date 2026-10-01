namespace Toarnbeike.Results.Rules.EvaluationData;

/// <summary>
/// Rule evaluation data for numeric rules, containing the applied current time.
/// </summary>
/// <param name="UtcNow">The time at the moment of evaluation.</param>
public sealed record UtcNowEvaluationData(DateTimeOffset UtcNow) : IRuleEvaluationData;