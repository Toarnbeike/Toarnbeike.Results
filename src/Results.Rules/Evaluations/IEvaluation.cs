namespace Toarnbeike.Results.Rules.Evaluations;

/// <summary>
/// Defines a contract for evaluating a property of type <typeparamref name="TProperty"/> and returning an <see cref="EvaluationResult"/> indicating the outcome of the evaluation.
/// </summary>
/// <typeparam name="TProperty">The type of the property to evaluate.</typeparam>
internal interface IEvaluation<in TProperty>
{
    /// <summary>
    /// Evaluates the specified property value and returns an <see cref="EvaluationResult"/> indicating whether the evaluation was successful or not.
    /// </summary>
    /// <param name="value">The property value to evaluate.</param>
    /// <returns>An <see cref="EvaluationResult"/> indicating the outcome of the evaluation.</returns>
    EvaluationResult Evaluate(TProperty value);
}