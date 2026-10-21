namespace Toarnbeike.Results.Rules.Evaluations;

/// <summary>
/// Defines a contract for asynchronously evaluating a property of type <typeparamref name="TProperty"/> and returning an <see cref="EvaluationResult"/> indicating the outcome of the evaluation.
/// </summary>
/// <typeparam name="TProperty">The type of the property to evaluate.</typeparam>
internal interface IAsyncEvaluation<in TProperty>
{
    /// <summary>
    /// Asynchronously evaluates the specified property value and returns an <see cref="EvaluationResult"/> indicating whether the evaluation was successful or not.
    /// </summary>
    /// <param name="value">The property value to evaluate.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="EvaluationResult"/> indicating the outcome of the evaluation.</returns>
    Task<EvaluationResult> EvaluateAsync(TProperty value);
}