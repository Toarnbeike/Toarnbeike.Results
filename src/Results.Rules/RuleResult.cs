using Toarnbeike.Results.Rules.Evaluations;
using Toarnbeike.Results.Rules.Messages;

namespace Toarnbeike.Results.Rules;

/// <summary>
/// Represents a delegate that evaluates a property of type <typeparamref name="TProperty"/> and returns a <see cref="RuleResult"/> indicating the outcome of the evaluation.
/// </summary>
/// <typeparam name="TProperty">The type of the property to evaluate.</typeparam>
/// <param name="value">The value of the property to evaluate.</param>
/// <returns>A <see cref="RuleResult"/> representing the outcome of the evaluation.</returns>
public delegate RuleResult RuleDelegate<in TProperty>(TProperty value);

/// <summary>
/// Represents a delegate that evaluates a property of type <typeparamref name="TProperty"/> asynchronously and returns a <see cref="Task{RuleResult}"/> indicating the outcome of the evaluation.
/// </summary>
/// <typeparam name="TProperty">The type of the property to evaluate.</typeparam>
/// <param name="value">The value of the property to evaluate.</param>
/// <returns>A <see cref="Task{RuleResult}"/> representing the outcome of the evaluation.</returns>
public delegate Task<RuleResult> AsyncRuleDelegate<in TProperty>(TProperty value);


/// <summary>
/// Represents the result of a rule evaluation, indicating whether the rule passed or failed, along with an associated message.
/// </summary>
public readonly record struct RuleResult
{
    public static RuleResult Success() => 
        new(true, string.Empty, string.Empty, new Dictionary<string, object?>());
    
    public static RuleResult Failure(string evaluatorName, string message, IReadOnlyDictionary<string, object?>? context = null) =>
        new(false, message, evaluatorName, context ?? new Dictionary<string, object?>());

    /// <summary>
    /// Indicates whether the rule evaluation was successful.
    /// </summary>
    public bool IsValid { get; }

    /// <summary>
    /// Provides a message describing the failure reason, if failed.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Provides the name of the evaluator that was used for the rule evaluation.
    /// </summary>
    public string EvaluatorName { get; }

    /// <summary>
    /// Provides additional context information related to the rule evaluation, if any.
    /// </summary>
    public IReadOnlyDictionary<string, object?> Context { get; }

    /// <summary>
    /// Create a <see cref="RuleResult"/> from a rule name, a value and an <see cref="IEvaluation{TProperty}"/>.
    /// </summary>
    /// <typeparam name="TProperty">The type of the value to evaluate.</typeparam>
    /// <param name="ruleName">The name of the rule.</param>
    /// <param name="value">The value to evaluate.</param>
    /// <param name="evaluator">The evaluator to use.</param>
    /// <returns>A <see cref="RuleResult"/> representing the outcome of the rule evaluation.</returns>
    internal static RuleResult Create<TProperty>(string ruleName, TProperty value, IEvaluation<TProperty> evaluator, params (string, object?)[] context)
    {
        var evaluationResult = evaluator.Evaluate(value);
        return Create(ruleName, evaluationResult, context);
    }

    /// <summary>
    /// Create a <see cref="RuleResult"/> from a rule name, a value, a conversion function and an <see cref="IEvaluation{TConverted}"/>.
    /// The conversion function is used to convert the original value to a different type before evaluation.
    /// The context in the EvaluationResult will contain the original value for reference in the message formatting.
    /// </summary>
    /// <typeparam name="TProperty">The type of the original value.</typeparam>
    /// <typeparam name="TConverted">The type of the converted value.</typeparam>
    /// <param name="ruleName">The name of the rule.</param>
    /// <param name="value">The value to evaluate.</param>
    /// <param name="conversion">A function to convert the original value to the converted value.</param>
    /// <param name="evaluator">The evaluator to use for the converted value.</param>
    /// <returns>
    /// A <see cref="RuleResult"/> representing the outcome of the rule evaluation.</returns>
    internal static RuleResult Create<TProperty, TConverted>(string ruleName, TProperty value, Func<TProperty, TConverted> conversion, IEvaluation<TConverted> evaluator, params (string, object?)[] context)
    {
        var convertedValue = conversion(value);
        var evaluationResult = evaluator.Evaluate(convertedValue).WithOriginalValue(value);
        return Create(ruleName, evaluationResult, context);
    }

    /// <summary>
    /// Create a <see cref="RuleResult"/> from a rule name, a value and an <see cref="IAsyncEvaluation{TProperty}"/>.
    /// </summary>
    /// <typeparam name="TProperty">The type of the value to evaluate.</typeparam>
    /// <param name="ruleName">The name of the rule.</param>
    /// <param name="value">The value to evaluate.</param>
    /// <param name="evaluator">The asynchronous evaluator to use.</param>
    /// <returns>A <see cref="RuleResult"/> representing the outcome of the rule evaluation.</returns>
    internal static async Task<RuleResult> CreateAsync<TProperty>(string ruleName, TProperty value, IAsyncEvaluation<TProperty> evaluator, params (string, object?)[] context)
    {
        var evaluationResult = await evaluator.EvaluateAsync(value);
        return Create(ruleName, evaluationResult, context);
    }

    /// <summary>
    /// The actual creation implementation after a <see cref="EvaluationResult"/> has been obtained, to avoid code duplication between sync and async creation.
    /// </summary>
    /// <param name="ruleName">The name of the rule.</param>
    /// <param name="evaluationResult">The result of the evaluation.</param>
    /// <param name="context">Additional context details.</param>
    /// <returns>A <see cref="RuleResult"/> representing the outcome of the rule evaluation.</returns>
    private static RuleResult Create(string ruleName, EvaluationResult evaluationResult, params (string, object?)[] context)
    {
        foreach (var (key, val) in context)
        {
            evaluationResult = evaluationResult.WithDetail(key, val);
        }

        if (evaluationResult.IsValid)
        {
            return new RuleResult(true, string.Empty, evaluationResult.Evaluator, evaluationResult.EvaluationDetails);
        }

        var message = RuleMessageFormatter.FormatMessage(ruleName, evaluationResult);
        return new RuleResult(false, message, evaluationResult.Evaluator, evaluationResult.EvaluationDetails);
    }

    private RuleResult(bool isValid, string message, string evaluatorName, IReadOnlyDictionary<string, object?> context)
    {
        IsValid = isValid;
        Message = message;
        EvaluatorName = evaluatorName;
        Context = context;
    }
}
