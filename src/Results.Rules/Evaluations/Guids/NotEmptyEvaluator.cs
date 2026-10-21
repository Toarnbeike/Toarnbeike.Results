namespace Toarnbeike.Results.Rules.Evaluations.Guids;

/// <summary>
/// Checks if a given Guid value is not empty (i.e., not equal to Guid.Empty).
/// </summary>
internal class NotEmptyEvaluator : IEvaluation<Guid>
{
    public EvaluationResult Evaluate(Guid value) => 
        EvaluationResult.Create(
            attemptedValue: value,
            isValid: value != Guid.Empty && value != Guid.AllBitsSet,
            evaluatorName: "NotEmptyEvaluator",
            details: []);
}
