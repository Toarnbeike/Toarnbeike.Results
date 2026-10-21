namespace Toarnbeike.Results.Rules.Evaluations;

/// <summary>
/// Represents the result of evaluating a property.
/// </summary>
public record EvaluationResult
{
    /// <summary>
    /// Gets a value indicating whether the evaluation was successful.
    /// </summary>
    public bool IsValid { get; }

    /// <summary>
    /// Gets the name of the evaluator that performed the evaluation.
    /// </summary>
    public string Evaluator { get; }

    /// <summary>
    /// Gets a read-only dictionary containing additional details about the evaluation, such as the attempted value and any other relevant information.
    /// </summary>
    public IReadOnlyDictionary<string, object?> EvaluationDetails { get; }

    /// <summary>
    /// Creates a new instance of <see cref="EvaluationResult"/> with the specified attempted value, validation result, and additional details.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property being evaluated.</typeparam>
    /// <param name="attemptedValue">The value that was attempted to be validated.</param>
    /// <param name="isValid">A value indicating whether the evaluation was successful.</param>
    /// <param name="details">Additional details about the evaluation.</param>
    /// <returns>An <see cref="EvaluationResult"/> representing the outcome of the evaluation.</returns>
    internal static EvaluationResult Create<TProperty>(TProperty attemptedValue, bool isValid, string evaluatorName, (string, object)[] details)
    {
        var evaluationDetails = new Dictionary<string, object?>
        {
            { "attemptedValue", attemptedValue },
        };

        foreach (var (key, value) in details)
        {
            evaluationDetails[key] = value;
        }

        return new EvaluationResult(isValid, evaluatorName, evaluationDetails);
    }

    /// <summary>
    /// Creates a new instance of <see cref="EvaluationResult"/> with the specified detail, preserving the existing evaluation details.
    /// </summary>
    /// <param name="message">The message key for the detail.</param>
    /// <param name="value">The value of the detail.</param>
    /// <returns>A new <see cref="EvaluationResult"/> instance with the updated detail.</returns>
    /// <remarks>These methods are internal to avoid modifying the evaluation details from outside the assembly.</remarks>
    internal EvaluationResult WithDetail(string message, object? value)
    {
        var updatedDetails = new Dictionary<string, object?>(EvaluationDetails)
        {
            [message] = value
        };
        return new EvaluationResult(IsValid, Evaluator, updatedDetails);
    }

    /// <summary>
    /// Creates a new instance of <see cref="EvaluationResult"/> with the supplied original value, preserving the existing evaluation details.
    /// </summary>
    /// <param name="originalValue">The original value to include in the evaluation details.</param>
    /// <returns>A new <see cref="EvaluationResult"/> instance with the updated original value.</returns>
    internal EvaluationResult WithOriginalValue(object? originalValue) => WithDetail("originalValue", originalValue);

    private EvaluationResult(bool isValid, string evaluator, Dictionary<string, object?> evaluationDetails)
    {
        IsValid = isValid;
        Evaluator = evaluator;
        EvaluationDetails = evaluationDetails;
    }
}