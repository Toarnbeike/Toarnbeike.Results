namespace Toarnbeike.Results.Extensions;

/// <summary>
/// Ensure: Performs a conditional check on the value of a successful <see cref="Result{T}"/>.
/// If the check fails, the result becomes a failure; otherwise, the original result is returned unchanged.
/// </summary>
public static class EnsureResultTValueExtensions
{
    /// <summary>
    /// Ensures that a successful <see cref="Result{TValue}"/> satisfies the provided check function.
    /// If the original result is a failure, it is returned unchanged.
    /// If the check function returns a failure, that failure is returned.
    /// If the check function returns a success, the original result is returned.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    /// <param name="result">The original result to validate.</param>
    /// <param name="checkFunc">A function that performs a check and returns a <see cref="IResult"/>.</param>
    /// <returns>
    /// The original result if it was a failure, or if the check succeeded;
    /// otherwise, the failure from the check function.
    /// </returns>
    public static Result<TValue> Ensure<TValue>(this Result<TValue> result, Func<TValue, IResult> checkFunc) =>
        result.TryGetValue(out var value) && checkFunc(value).TryGetFailure(out var checkFailure) 
            ? checkFailure 
            : result;

    /// <summary>
    /// Ensures that a successful <see cref="Result{TValue}"/> satisfies the provided asynchronous check function.
    /// If the original result is a failure, it is returned unchanged.
    /// If the check function returns a failure, that failure is returned.
    /// If the check function returns a success, the original result is returned.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    /// <param name="result">The original result to validate.</param>
    /// <param name="checkFunc">An async function that performs a check and returns a <see cref="Result"/>.</param>
    /// <returns>
    /// The original result if it was a failure, or if the check succeeded;
    /// otherwise, the failure from the check function.
    /// </returns>
    public static async Task<Result<TValue>> EnsureAsync<TValue>(this Result<TValue> result, Func<TValue, Task<Result>> checkFunc) =>
        result.TryGetValue(out var value) && (await checkFunc(value)).TryGetFailure(out var checkFailure)
            ? checkFailure
            : result;

    /// <summary>
    /// Ensures that a successful <see cref="Result{TValue}"/> satisfies the provided asynchronous check function.
    /// If the original result is a failure, it is returned unchanged.
    /// If the check function returns a failure, that failure is returned.
    /// If the check function returns a success, the original result is returned.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    /// <typeparam name="TCheck">The type used internally by the check function (not returned).</typeparam>
    /// <param name="result">The original result to validate.</param>
    /// <param name="checkFunc">An async function that performs a check and returns a <see cref="Result{TCheck}"/>.</param>
    /// <returns>
    /// The original result if it was a failure, or if the check succeeded;
    /// otherwise, the failure from the check function.
    /// </returns>
    public static async Task<Result<TValue>> EnsureAsync<TValue, TCheck>(this Result<TValue> result, Func<TValue, Task<Result<TCheck>>> checkFunc) =>
        result.TryGetValue(out var value) && (await checkFunc(value)).TryGetFailure(out var checkFailure)
            ? checkFailure
            : result;

    /// <summary>
    /// Ensures that a successful <see cref="Task{Result{TValue}}"/> satisfies the provided check function.
    /// If the original result is a failure, it is returned unchanged.
    /// If the check function returns a failure, that failure is returned.
    /// If the check function returns a success, the original result is returned.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    /// <param name="resultTask">The task that resolves to the result to validate.</param>
    /// <param name="checkFunc">A function that performs a check and returns a <see cref="IResult"/>.</param>
    /// <returns>
    /// The original result if it was a failure, or if the check succeeded;
    /// otherwise, the failure from the check function.
    /// </returns>
    public static async Task<Result<TValue>> Ensure<TValue>(this Task<Result<TValue>> resultTask, Func<TValue, IResult> checkFunc)
    {
        var result = await resultTask;
        return Ensure(result, checkFunc);
    }

    /// <summary>
    /// Ensures that a successful <see cref="Task{Result{TValue}}"/> satisfies the provided asynchronous check function.
    /// If the original result is a failure, it is returned unchanged.
    /// If the check function returns a failure, that failure is returned.
    /// If the check function returns a success, the original result is returned.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    /// <param name="resultTask">The task that resolves to the result to validate.</param>
    /// <param name="checkFunc">An async function that performs a check and returns a <see cref="Result"/>.</param>
    /// <returns>
    /// The original result if it was a failure, or if the check succeeded;
    /// otherwise, the failure from the check function.
    /// </returns>
    public static async Task<Result<TValue>> EnsureAsync<TValue>(this Task<Result<TValue>> resultTask, Func<TValue, Task<Result>> checkFunc)
    {
        var result = await resultTask;
        return await EnsureAsync(result, checkFunc);
    }

    /// <summary>
    /// Ensures that a successful <see cref="Task{Result{TValue}}"/> satisfies the provided asynchronous check function.
    /// If the original result is a failure, it is returned unchanged.
    /// If the check function returns a failure, that failure is returned.
    /// If the check function returns a success, the original result is returned.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    /// <param name="resultTask">The task that resolves to the result to validate.</param>
    /// <param name="checkFunc">An async function that performs a check and returns a <see cref="Result{TCheck}"/>.</param>
    /// <returns>
    /// The original result if it was a failure, or if the check succeeded;
    /// otherwise, the failure from the check function.
    /// </returns>
    public static async Task<Result<TValue>> EnsureAsync<TValue, TCheck>(this Task<Result<TValue>> resultTask, Func<TValue, Task<Result<TCheck>>> checkFunc)
    {
        var result = await resultTask;
        return await EnsureAsync(result, checkFunc);
    }
}