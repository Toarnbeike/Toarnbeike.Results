namespace Toarnbeike.Results.Rules.Evaluations.Enums;

/// <summary>
/// Checks if a given enum value is defined in its enumeration type.
/// </summary>
/// <typeparam name="TEnum">The type of the enumeration.</typeparam>
internal sealed class IsDefinedEvaluator<TEnum> : IEvaluation<TEnum> 
    where TEnum : struct, Enum
{
    public EvaluationResult Evaluate(TEnum value) => 
        EvaluationResult.Create(
            attemptedValue: value, 
            isValid: Enum.IsDefined(value), 
            evaluatorName: "IsDefinedEvaluator",
            details: [("enumType", typeof(TEnum).Name)]);
}
