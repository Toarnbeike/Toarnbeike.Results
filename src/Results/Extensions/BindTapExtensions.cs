namespace Toarnbeike.Results.Extensions;

/// <summary>
/// BindTap: Bind a result, but keep (tap) the original <see cref="Result"/> or <see cref="Result{TValue}"/>.
/// If the check fails, the result becomes a failure; otherwise, the original result is returned unchanged.
/// </summary>
public static class BindTapExtensions
{
    /// <param name="result">The original result to validate.</param>
    extension(Result result)
    {
        /// <summary>
        /// Check that a successful <see cref="Result"/> satisfies the provided check function.
        /// If the original result is a failure, it is returned unchanged.
        /// If the check function returns a failure, that failure is returned.
        /// If the check function returns a success, the original result is returned.
        /// </summary>
        /// <param name="checkFunc">A function that performs a check and returns a <see cref="IResult"/>.</param>
        /// <returns>
        /// The original result if it was a failure, or if the check succeeded;
        /// otherwise, the failure from the check function.
        /// </returns>
        public Result BindTap<TResult>(Func<TResult> checkFunc) where TResult : IResult
        {
            ArgumentNullException.ThrowIfNull(checkFunc);

            return result.IsSuccess && checkFunc().TryGetFailure(out var checkFailure)
                ? checkFailure
                : result;
        }

        /// <summary>
        /// Check that a successful <see cref="Result"/> satisfies the provided asynchronous check function.
        /// If the original result is a failure, it is returned unchanged.
        /// If the check function returns a failure, that failure is returned.
        /// If the check function returns a success, the original result is returned.
        /// </summary>
        /// <param name="checkFunc">An async function that performs a check and returns a <see cref="Result"/>.</param>
        /// <returns>
        /// The original result if it was a failure, or if the check succeeded;
        /// otherwise, the failure from the check function.
        /// </returns>
        public async Task<Result> BindTapAsync(Func<Task<Result>> checkFunc)
        {
            ArgumentNullException.ThrowIfNull(checkFunc);

            return result.IsSuccess && (await checkFunc().ConfigureAwait(false)).TryGetFailure(out var checkFailure)
                ? checkFailure
                : result;
        }

        /// <summary>
        /// Check that a successful <see cref="Result"/> satisfies the provided asynchronous check function.
        /// If the original result is a failure, it is returned unchanged.
        /// If the check function returns a failure, that failure is returned.
        /// If the check function returns a success, the original result is returned.
        /// </summary>
        /// <typeparam name="TCheck">The type used internally by the check function (not returned).</typeparam>
        /// <param name="checkFunc">An async function that performs a check and returns a <see cref="Result{TCheck}"/>.</param>
        /// <returns>
        /// The original result if it was a failure, or if the check succeeded;
        /// otherwise, the failure from the check function.
        /// </returns>
        public async Task<Result> BindTapAsync<TCheck>(Func<Task<Result<TCheck>>> checkFunc)
        {
            ArgumentNullException.ThrowIfNull(checkFunc);

            return result.IsSuccess && (await checkFunc().ConfigureAwait(false)).TryGetFailure(out var checkFailure)
                ? checkFailure
                : result;
        }
    }

    /// <param name="resultTask">The task that resolves to the result to validate.</param>
    extension(Task<Result> resultTask)
    {
        /// <summary>
        /// Check that a successful <see cref="Task{Result}"/> satisfies the provided check function.
        /// If the original result is a failure, it is returned unchanged.
        /// If the check function returns a failure, that failure is returned.
        /// If the check function returns a success, the original result is returned.
        /// </summary>
        /// <param name="checkFunc">A function that performs a check and returns a <see cref="IResult"/>.</param>
        /// <returns>
        /// The original result if it was a failure, or if the check succeeded;
        /// otherwise, the failure from the check function.
        /// </returns>
        public async Task<Result> BindTap<TResult>(Func<TResult> checkFunc) where TResult : IResult
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.BindTap(checkFunc);
        }

        /// <summary>
        /// Check that a successful <see cref="Task{Result}"/> satisfies the provided asynchronous check function.
        /// If the original result is a failure, it is returned unchanged.
        /// If the check function returns a failure, that failure is returned.
        /// If the check function returns a success, the original result is returned.
        /// </summary>
        /// <param name="checkFunc">An async function that performs a check and returns a <see cref="Result"/>.</param>
        /// <returns>
        /// The original result if it was a failure, or if the check succeeded;
        /// otherwise, the failure from the check function.
        /// </returns>
        public async Task<Result> BindTapAsync(Func<Task<Result>> checkFunc)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.BindTapAsync(checkFunc).ConfigureAwait(false);
        }

        /// <summary>
        /// Check that a successful <see cref="Task{Result}"/> satisfies the provided asynchronous check function.
        /// If the original result is a failure, it is returned unchanged.
        /// If the check function returns a failure, that failure is returned.
        /// If the check function returns a success, the original result is returned.
        /// </summary>
        /// <param name="checkFunc">An async function that performs a check and returns a <see cref="Result{TCheck}"/>.</param>
        /// <returns>
        /// The original result if it was a failure, or if the check succeeded;
        /// otherwise, the failure from the check function.
        /// </returns>
        public async Task<Result> BindTapAsync<TCheck>(Func<Task<Result<TCheck>>> checkFunc)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.BindTapAsync(checkFunc).ConfigureAwait(false);
        }
    }

    /// <param name="result">The original result to validate.</param>
    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    extension<TValue>(Result<TValue> result)
    {
        /// <summary>
        /// BindTap that a successful <see cref="Result{TValue}"/> satisfies the provided check function.
        /// If the original result is a failure, it is returned unchanged.
        /// If the check function returns a failure, that failure is returned.
        /// If the check function returns a success, the original result is returned.
        /// </summary>
        /// <param name="checkFunc">A function that performs a check and returns a <see cref="IResult"/>.</param>
        /// <returns>
        /// The original result if it was a failure, or if the check succeeded;
        /// otherwise, the failure from the check function.
        /// </returns>
        public Result<TValue> BindTap<TResult>(Func<TValue, TResult> checkFunc) where TResult: IResult
        {
            ArgumentNullException.ThrowIfNull(checkFunc);

            return result.TryGetValue(out var value) && checkFunc(value).TryGetFailure(out var checkFailure)
                ? checkFailure
                : result;
        }

        /// <summary>
        /// BindTap that a successful <see cref="Result{TValue}"/> satisfies the provided asynchronous check function.
        /// If the original result is a failure, it is returned unchanged.
        /// If the check function returns a failure, that failure is returned.
        /// If the check function returns a success, the original result is returned.
        /// </summary>
        /// <param name="checkFunc">An async function that performs a check and returns a <see cref="Result"/>.</param>
        /// <returns>
        /// The original result if it was a failure, or if the check succeeded;
        /// otherwise, the failure from the check function.
        /// </returns>
        public async Task<Result<TValue>> BindTapAsync(Func<TValue, Task<Result>> checkFunc)
        {
            ArgumentNullException.ThrowIfNull(checkFunc);

            return result.TryGetValue(out var value) && (await checkFunc(value).ConfigureAwait(false)).TryGetFailure(out var checkFailure)
                ? checkFailure
                : result;
        }

        /// <summary>
        /// BindTap that a successful <see cref="Result{TValue}"/> satisfies the provided asynchronous check function.
        /// If the original result is a failure, it is returned unchanged.
        /// If the check function returns a failure, that failure is returned.
        /// If the check function returns a success, the original result is returned.
        /// </summary>
        /// <typeparam name="TCheck">The type used internally by the check function (not returned).</typeparam>
        /// <param name="checkFunc">An async function that performs a check and returns a <see cref="Result{TCheck}"/>.</param>
        /// <returns>
        /// The original result if it was a failure, or if the check succeeded;
        /// otherwise, the failure from the check function.
        /// </returns>
        public async Task<Result<TValue>> BindTapAsync<TCheck>(Func<TValue, Task<Result<TCheck>>> checkFunc)
        {
            ArgumentNullException.ThrowIfNull(checkFunc);

            return result.TryGetValue(out var value) && (await checkFunc(value).ConfigureAwait(false)).TryGetFailure(out var checkFailure)
                ? checkFailure
                : result;
        }
    }

    /// <param name="resultTask">The task that resolves to the result to validate.</param>
    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    extension<TValue>(Task<Result<TValue>> resultTask)
    {
        /// <summary>
        /// BindTap that a successful  <see cref="Task{TResult}"/> satisfies the provided check function.
        /// If the original result is a failure, it is returned unchanged.
        /// If the check function returns a failure, that failure is returned.
        /// If the check function returns a success, the original result is returned.
        /// </summary>
        /// <param name="checkFunc">A function that performs a check and returns a <see cref="IResult"/>.</param>
        /// <returns>
        /// The original result if it was a failure, or if the check succeeded;
        /// otherwise, the failure from the check function.
        /// </returns>
        public async Task<Result<TValue>> BindTap<TResult>(Func<TValue, TResult> checkFunc) where TResult : IResult
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.BindTap(checkFunc);
        }

        /// <summary>
        /// BindTap that a successful  <see cref="Task{TResult}"/> satisfies the provided asynchronous check function.
        /// If the original result is a failure, it is returned unchanged.
        /// If the check function returns a failure, that failure is returned.
        /// If the check function returns a success, the original result is returned.
        /// </summary>
        /// <param name="checkFunc">An async function that performs a check and returns a <see cref="Result"/>.</param>
        /// <returns>
        /// The original result if it was a failure, or if the check succeeded;
        /// otherwise, the failure from the check function.
        /// </returns>
        public async Task<Result<TValue>> BindTapAsync(Func<TValue, Task<Result>> checkFunc)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.BindTapAsync(checkFunc).ConfigureAwait(false);
        }

        /// <summary>
        /// BindTap that a successful <see cref="Task{TResult}"/> satisfies the provided asynchronous check function.
        /// If the original result is a failure, it is returned unchanged.
        /// If the check function returns a failure, that failure is returned.
        /// If the check function returns a success, the original result is returned.
        /// </summary>
        /// <param name="checkFunc">An async function that performs a check and returns a <see cref="Result{TCheck}"/>.</param>
        /// <returns>
        /// The original result if it was a failure, or if the check succeeded;
        /// otherwise, the failure from the check function.
        /// </returns>
        public async Task<Result<TValue>> BindTapAsync<TCheck>(Func<TValue, Task<Result<TCheck>>> checkFunc)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.BindTapAsync(checkFunc).ConfigureAwait(false);
        }
    }
}