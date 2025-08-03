using FluentValidation;
using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.FluentValidation;

public static class ValidateExtensions
{
    /// <summary>
    /// Validates the result value using one or more <see cref="IValidator{T}"/> instances.
    /// If the result is a failure or contains validation errors, a <see cref="ValidationFailures"/> is returned.
    /// </summary>
    /// <remarks>
    /// ⚠️ WARNING:
    /// Use Validate() only when your know validators do not include asynchronous rules. 
    /// If you're unsure or using 3rd-party validators, prefer <see cref="ValidateAsync"/> to avoid skipping async logic.
    /// </remarks>
    /// <typeparam name="TValue">The type of the result value.</typeparam>
    /// <param name="result">The result to validate.</param>
    /// <param name="validators">One or more validators to apply to the result value.</param>
    /// <returns>
    /// The original result if it is a failure or passes validation; otherwise a <see cref="ValidationFailures"/> failure.
    /// </returns>
    public static Result<TValue> Validate<TValue>(this Result<TValue> result, params IEnumerable<IValidator<TValue>> validators)
    {
        if (!result.TryGetValue(out var value))
        {
            return result;
        }

        var context = new ValidationContext<TValue>(value);
        var failures = validators
            .Select(v => v.Validate(context))
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();

        return failures.Count == 0
            ? result 
            : failures.ToValidationFailures();
    }

    /// <summary>
    /// Asynchronously validates the result value using one or more <see cref="IValidator{T}"/> instances.
    /// If the result is a failure or contains validation errors, a <see cref="ValidationFailures"/> is returned.
    /// </summary>
    /// <typeparam name="TValue">The type of the result value.</typeparam>
    /// <param name="result">The result to validate.</param>
    /// <param name="validators">One or more validators to apply to the result value.</param>
    /// <returns>
    /// The original result if it is a failure or passes validation; otherwise a <see cref="ValidationFailures"/> failure.
    /// </returns>
    public static async Task<Result<TValue>> ValidateAsync<TValue>(this Result<TValue> result, params IEnumerable<IValidator<TValue>> validators)
    {
        if (!result.TryGetValue(out var value))
        {
            return result;
        }

        var context = new ValidationContext<TValue>(value);
        var failures = new List<global::FluentValidation.Results.ValidationFailure>();

        foreach (var validator in validators)
        {
            var validationResult = await validator.ValidateAsync(context);
            failures.AddRange(validationResult.Errors.Where(f => f is not null));
        }

        return failures.Count == 0
            ? result
            : failures.ToValidationFailures();
    }

    /// <summary>
    /// Validates the result value using one or more <see cref="IValidator{T}"/> instances.
    /// If the result is a failure or contains validation errors, a <see cref="ValidationFailures"/> is returned.
    /// </summary>
    /// <remarks>
    /// ⚠️ WARNING:
    /// Use Validate() only when your know validators do not include asynchronous rules. 
    /// If you're unsure or using 3rd-party validators, prefer <see cref="ValidateAsync"/> to avoid skipping async logic.
    /// </remarks>
    /// <typeparam name="TValue">The type of the result value.</typeparam>
    /// <param name="resultTask">The async result to validate.</param>
    /// <param name="validators">One or more validators to apply to the result value.</param>
    /// <returns>
    /// The original result if it is a failure or passes validation; otherwise a <see cref="ValidationFailures"/> failure.
    /// </returns>
    public static async Task<Result<TValue>> Validate<TValue>(this Task<Result<TValue>> resultTask, params IEnumerable<IValidator<TValue>> validators)
    {
        var result = await resultTask;
        return Validate(result, validators);
    }

    /// <summary>
    /// Asynchronously validates the result value using one or more <see cref="IValidator{T}"/> instances.
    /// If the result is a failure or contains validation errors, a <see cref="ValidationFailures"/> is returned.
    /// </summary>
    /// <typeparam name="TValue">The type of the result value.</typeparam>
    /// <param name="resultTask">The async result to validate.</param>
    /// <param name="validators">One or more validators to apply to the result value.</param>
    /// <returns>
    /// The original result if it is a failure or passes validation; otherwise a <see cref="ValidationFailures"/> failure.
    /// </returns>
    public static async Task<Result<TValue>> ValidateAsync<TValue>(this Task<Result<TValue>> resultTask, params IEnumerable<IValidator<TValue>> validators)
    {
        var result = await resultTask;
        return await ValidateAsync(result, validators);
    }
}
