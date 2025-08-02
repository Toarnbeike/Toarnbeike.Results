namespace Toarnbeike.Results.Extensions;

/// <summary>
/// Verify: Performs a conditional check on the value of a successful <see cref="Result"/>.
/// If the check fails, the result becomes a failure; otherwise, the original result is returned unchanged.
/// </summary>
public static class VerifyResultExtensions
{
    /// <summary>
    /// Verifys that a successful <see cref="Result"/> satisfies the provided check function.
    /// If the original result is a failure, it is returned unchanged.
    /// If the check function returns a failure, that failure is returned.
    /// If the check function returns a success, the original result is returned.
    /// </summary>
    /// <param name="result">The original result to validate.</param>
    /// <param name="checkFunc">A function that performs a check and returns a <see cref="IResult"/>.</param>
    /// <returns>
    /// The original result if it was a failure, or if the check succeeded;
    /// otherwise, the failure from the check function.
    /// </returns>
    public static Result Verify(this Result result, Func<IResult> checkFunc) =>
        result.IsSuccess && checkFunc().TryGetFailure(out var checkFailure)
            ? checkFailure
            : result;

    /// <summary>
    /// Verifys that a successful <see cref="Result"/> satisfies the provided asynchronous check function.
    /// If the original result is a failure, it is returned unchanged.
    /// If the check function returns a failure, that failure is returned.
    /// If the check function returns a success, the original result is returned.
    /// </summary>
    /// <param name="result">The original result to validate.</param>
    /// <param name="checkFunc">An async function that performs a check and returns a <see cref="Result"/>.</param>
    /// <returns>
    /// The original result if it was a failure, or if the check succeeded;
    /// otherwise, the failure from the check function.
    /// </returns>
    public static async Task<Result> VerifyAsync(this Result result, Func<Task<Result>> checkFunc) =>
        result.IsSuccess && (await checkFunc()).TryGetFailure(out var checkFailure)
            ? checkFailure
            : result;

    /// <summary>
    /// Verifys that a successful <see cref="Result"/> satisfies the provided asynchronous check function.
    /// If the original result is a failure, it is returned unchanged.
    /// If the check function returns a failure, that failure is returned.
    /// If the check function returns a success, the original result is returned.
    /// </summary>
    /// <typeparam name="TCheck">The type used internally by the check function (not returned).</typeparam>
    /// <param name="result">The original result to validate.</param>
    /// <param name="checkFunc">An async function that performs a check and returns a <see cref="Result{TCheck}"/>.</param>
    /// <returns>
    /// The original result if it was a failure, or if the check succeeded;
    /// otherwise, the failure from the check function.
    /// </returns>
    public static async Task<Result> VerifyAsync<TCheck>(this Result result, Func<Task<Result<TCheck>>> checkFunc) =>
        result.IsSuccess && (await checkFunc()).TryGetFailure(out var checkFailure)
            ? checkFailure
            : result;

    /// <summary>
    /// Verifys that a successful <see cref="Task{Result}"/> satisfies the provided check function.
    /// If the original result is a failure, it is returned unchanged.
    /// If the check function returns a failure, that failure is returned.
    /// If the check function returns a success, the original result is returned.
    /// </summary>
    /// <param name="resultTask">The task that resolves to the result to validate.</param>
    /// <param name="checkFunc">A function that performs a check and returns a <see cref="IResult"/>.</param>
    /// <returns>
    /// The original result if it was a failure, or if the check succeeded;
    /// otherwise, the failure from the check function.
    /// </returns>
    public static async Task<Result> Verify(this Task<Result> resultTask, Func<IResult> checkFunc)
    {
        var result = await resultTask;
        return Verify(result, checkFunc);
    }

    /// <summary>
    /// Verifys that a successful <see cref="Task{Result}"/> satisfies the provided asynchronous check function.
    /// If the original result is a failure, it is returned unchanged.
    /// If the check function returns a failure, that failure is returned.
    /// If the check function returns a success, the original result is returned.
    /// </summary>
    /// <param name="resultTask">The task that resolves to the result to validate.</param>
    /// <param name="checkFunc">An async function that performs a check and returns a <see cref="Result"/>.</param>
    /// <returns>
    /// The original result if it was a failure, or if the check succeeded;
    /// otherwise, the failure from the check function.
    /// </returns>
    public static async Task<Result> VerifyAsync(this Task<Result> resultTask, Func<Task<Result>> checkFunc)
    {
        var result = await resultTask;
        return await VerifyAsync(result, checkFunc);
    }

    /// <summary>
    /// Verifys that a successful <see cref="Task{Result}"/> satisfies the provided asynchronous check function.
    /// If the original result is a failure, it is returned unchanged.
    /// If the check function returns a failure, that failure is returned.
    /// If the check function returns a success, the original result is returned.
    /// </summary>
    /// <param name="resultTask">The task that resolves to the result to validate.</param>
    /// <param name="checkFunc">An async function that performs a check and returns a <see cref="Result{TCheck}"/>.</param>
    /// <returns>
    /// The original result if it was a failure, or if the check succeeded;
    /// otherwise, the failure from the check function.
    /// </returns>
    public static async Task<Result> VerifyAsync<TCheck>(this Task<Result> resultTask, Func<Task<Result<TCheck>>> checkFunc)
    {
        var result = await resultTask;
        return await VerifyAsync(result, checkFunc);
    }
}