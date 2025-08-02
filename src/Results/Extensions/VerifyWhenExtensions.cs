namespace Toarnbeike.Results.Extensions;

/// <summary>
/// Verify When: Performs a conditional check that only runs if a predicate evaluates to true.
/// </summary>
public static class VerifyWhenExtensions
{
    /// <summary>
    /// Verify that a successful <see cref="Result{TValue}"/> satisfies the provided check function,
    /// but only when the <paramref name="predicate"/> evaluates to true.
    /// </summary>
    /// <remarks>
    /// If the original result is a failure, it is returned unchanged.
    /// If the predicate is false, the original result is returned unchanged.
    /// If the check function returns a failure, that failure is returned.
    /// If the check function returns a success, the original result is returned.
    /// </remarks>
    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    /// <param name="result">The original result to validate.</param>
    /// <param name="predicate">Predicate to determine whether to apply the check.</param>
    /// <param name="checkFunc">A function that performs a check and returns a <see cref="IResult"/>.</param>
    /// <returns>
    /// The original result if the predicate is false, the result is a failure,
    /// or the check succeeds; otherwise, the failure from the check function.
    /// </returns>
    public static Result<TValue> VerifyWhen<TValue>(
        this Result<TValue> result,
        Func<TValue, bool> predicate, 
        Func<TValue, IResult> checkFunc) =>
        result.TryGetValue(out var value) && predicate(value) && checkFunc(value).TryGetFailure(out var checkFailure)
            ? checkFailure
            : result;

    /// <summary>
    /// Verify that a successful <see cref="Result{TValue}"/> satisfies the provided async check function,
    /// but only when the <paramref name="predicate"/> evaluates to true.
    /// </summary>
    /// <remarks>
    /// If the original result is a failure, it is returned unchanged.
    /// If the predicate is false, the original result is returned unchanged.
    /// If the check function returns a failure, that failure is returned.
    /// If the check function returns a success, the original result is returned.
    /// </remarks>
    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    /// <param name="result">The original result to validate.</param>
    /// <param name="predicate">Predicate to determine whether to apply the check.</param>
    /// <param name="checkFunc">An async function that performs a check and returns a <see cref="Result{TCheck}"/>.</param>
    /// <returns>
    /// The original result if the predicate is false, the result is a failure,
    /// or the check succeeds; otherwise, the failure from the check function.
    /// </returns>
    public static async Task<Result<TValue>> VerifyWhenAsync<TValue>(
        this Result<TValue> result, 
        Func<TValue, bool> predicate, 
        Func<TValue, Task<Result>> checkFunc) =>
        result.TryGetValue(out var value) && predicate(value) && (await checkFunc(value).ConfigureAwait(false)).TryGetFailure(out var checkFailure)
            ? checkFailure
            : result;

    /// <summary>
    /// Verify that a successful <see cref="Result{TValue}"/> satisfies the provided async check function,
    /// but only when the <paramref name="predicate"/> evaluates to true.
    /// </summary>
    /// <remarks>
    /// If the original result is a failure, it is returned unchanged.
    /// If the predicate is false, the original result is returned unchanged.
    /// If the check function returns a failure, that failure is returned.
    /// If the check function returns a success, the original result is returned.
    /// </remarks>
    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    /// <typeparam name="TCheck">The type used internally by the check function (not returned).</typeparam>
    /// <param name="result">The original result to validate.</param>
    /// <param name="predicate">Predicate to determine whether to apply the check.</param>
    /// <param name="checkFunc">An async function that performs a check and returns a <see cref="Result{TCheck}"/>.</param>
    /// <returns>
    /// The original result if the predicate is false, the result is a failure,
    /// or the check succeeds; otherwise, the failure from the check function.
    /// </returns>
    public static async Task<Result<TValue>> VerifyWhenAsync<TValue, TCheck>(
        this Result<TValue> result, 
        Func<TValue, bool> predicate, 
        Func<TValue, Task<Result<TCheck>>> checkFunc) =>
        result.TryGetValue(out var value) && predicate(value) && (await checkFunc(value).ConfigureAwait(false)).TryGetFailure(out var checkFailure)
            ? checkFailure
            : result;

    /// <summary>
    /// Verify that a successful <see cref="Result{TValue}"/> satisfies the provided check function,
    /// but only when the <paramref name="predicate"/> evaluates to true.
    /// </summary>
    /// <remarks>
    /// If the original result is a failure, it is returned unchanged.
    /// If the predicate is false, the original result is returned unchanged.
    /// If the check function returns a failure, that failure is returned.
    /// If the check function returns a success, the original result is returned.
    /// </remarks>
    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    /// <param name="result">The original result to validate.</param>
    /// <param name="predicate">Predicate to determine whether to apply the check.</param>
    /// <param name="checkFunc">A function that performs a check and returns a <see cref="IResult"/>.</param>
    /// <returns>
    /// The original result if the predicate is false, the result is a failure,
    /// or the check succeeds; otherwise, the failure from the check function.
    /// </returns>
    public static async Task<Result<TValue>> VerifyWhen<TValue>(
        this Task<Result<TValue>> resultTask,
        Func<TValue, bool> predicate,
        Func<TValue, IResult> checkFunc)
    {
        var result = await resultTask.ConfigureAwait(false);
        return VerifyWhen(result, predicate, checkFunc);
    }

    /// <summary>
    /// Verify that a successful <see cref="Result{TValue}"/> satisfies the provided async check function,
    /// but only when the <paramref name="predicate"/> evaluates to true.
    /// </summary>
    /// <remarks>
    /// If the original result is a failure, it is returned unchanged.
    /// If the predicate is false, the original result is returned unchanged.
    /// If the check function returns a failure, that failure is returned.
    /// If the check function returns a success, the original result is returned.
    /// </remarks>
    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    /// <param name="result">The original result to validate.</param>
    /// <param name="predicate">Predicate to determine whether to apply the check.</param>
    /// <param name="checkFunc">An async function that performs a check and returns a <see cref="Result{TCheck}"/>.</param>
    /// <returns>
    /// The original result if the predicate is false, the result is a failure,
    /// or the check succeeds; otherwise, the failure from the check function.
    /// </returns>
    public static async Task<Result<TValue>> VerifyWhenAsync<TValue>(
        this Task<Result<TValue>> resultTask,
        Func<TValue, bool> predicate,
        Func<TValue, Task<Result>> checkFunc)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await VerifyWhenAsync(result, predicate, checkFunc).ConfigureAwait(false);
    }

    /// <summary>
    /// Verify that a successful <see cref="Result{TValue}"/> satisfies the provided async check function,
    /// but only when the <paramref name="predicate"/> evaluates to true.
    /// </summary>
    /// <remarks>
    /// If the original result is a failure, it is returned unchanged.
    /// If the predicate is false, the original result is returned unchanged.
    /// If the check function returns a failure, that failure is returned.
    /// If the check function returns a success, the original result is returned.
    /// </remarks>
    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    /// <typeparam name="TCheck">The type used internally by the check function (not returned).</typeparam>
    /// <param name="result">The original result to validate.</param>
    /// <param name="predicate">Predicate to determine whether to apply the check.</param>
    /// <param name="checkFunc">An async function that performs a check and returns a <see cref="Result{TCheck}"/>.</param>
    /// <returns>
    /// The original result if the predicate is false, the result is a failure,
    /// or the check succeeds; otherwise, the failure from the check function.
    /// </returns>
    public static async Task<Result<TValue>> VerifyWhenAsync<TValue, TCheck>(
        this Task<Result<TValue>> resultTask,
        Func<TValue, bool> predicate,
        Func<TValue, Task<Result<TCheck>>> checkFunc)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await VerifyWhenAsync(result, predicate, checkFunc).ConfigureAwait(false);
    }
}