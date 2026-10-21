namespace Toarnbeike.Results.Rules.Evaluations.Enums;

/// <summary>
/// Checks if a given enum value is one of the accepted values.
/// </summary>
/// <typeparam name="TEnum">The type of the enumeration.</typeparam>
/// <param name="acceptedValues">The accepted values.</param>
internal sealed class OneOfEvaluator<TEnum>(TEnum[] acceptedValues) : IEvaluation<TEnum>
    where TEnum : struct, Enum
{
    public EvaluationResult Evaluate(TEnum value) =>
        EvaluationResult.Create(
            attemptedValue: value,
            isValid: acceptedValues.Contains(value),
            evaluatorName: "OneOfEvaluator",
            details: [
                ("enumType", typeof(TEnum).Name), 
                ("acceptedValues", string.Join(", ", acceptedValues.Select(v => v.ToString())))
            ]);
}
