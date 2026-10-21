namespace Toarnbeike.Results.Rules.Evaluations.Enums;

/// <summary>
/// Checks if a given enum value is not one of the rejected values.
/// </summary>
/// <typeparam name="TEnum">The type of the enumeration.</typeparam>
/// <param name="rejectedValues">The rejected values.</param>
internal sealed class NotOneOfEvaluator<TEnum>(TEnum[] rejectedValues) : IEvaluation<TEnum>
    where TEnum : struct, Enum
{
    public EvaluationResult Evaluate(TEnum value) =>
        EvaluationResult.Create(
            attemptedValue: value,
            isValid: !rejectedValues.Contains(value),
            evaluatorName: "NotOneOfEvaluator",
            details: [
                ("enumType", typeof(TEnum).Name),
                ("rejectedValues", string.Join(", ", rejectedValues.Select(v => v.ToString())))
            ]);
}
