namespace Toarnbeike.Results.Rules.Evaluations.Objects;

/// <summary>
/// Evaluates whether a candidate value is not equal to a predefined expected value.
/// </summary>
/// <remarks>Uses <see cref="EqualityComparer{T}.Default"/> to perform comparisons for the specified type.</remarks>
/// <typeparam name="TProperty">The type of value to compare.</typeparam>
/// <param name="expectedValue">The value used as the equality target.</param>
internal class NotEqualityEvaluator<TProperty>(TProperty expectedValue) : IEvaluation<TProperty>
{
    public EvaluationResult Evaluate(TProperty value)
    {
        var isValid = !EqualityComparer<TProperty>.Default.Equals(value, expectedValue);
        return EvaluationResult.Create(
            attemptedValue: value,
            isValid: isValid,
            evaluatorName: "NotEqualityEvaluator",
            details: [("type", typeof(TProperty).Name), ("expected", expectedValue?.ToString() ?? "null")]);
    }
}