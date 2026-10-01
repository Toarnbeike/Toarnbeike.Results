using System.Numerics;

namespace Toarnbeike.Results.Rules.EvaluationData;

/// <summary>
/// Rule evaluation data for numeric rules, containing the applied tolerance.
/// </summary>
/// <typeparam name="T">The type of the tolerance.</typeparam>
/// <param name="Tolerance">The tolerance that was applied.</param>
public sealed record AppliedToleranceEvaluationData<T>(T Tolerance)
    : IRuleEvaluationData where T : IFloatingPointIeee754<T>;