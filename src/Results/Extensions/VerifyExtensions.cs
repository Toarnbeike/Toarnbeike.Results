namespace Toarnbeike.Results.Extensions;

/// <summary>
/// Verify: Performs a conditional check on the value of a successful <see cref="Result"/> or <see cref="Result{TValue}"/>.
/// If the check fails, the result becomes a failure; otherwise, the original result is returned unchanged.
/// </summary>
[Obsolete("Use BindTap() for binding results without taking their values instead.")]
public static class VerifyExtensions
{
    /// <param name="result">The original result to validate.</param>
    extension(Result result)
    {
        /// <summary>
        /// Verifies that a successful <see cref="Result"/> satisfies the provided check function.
        /// If the original result is a failure, it is returned unchanged.
        /// If the check function returns a failure, that failure is returned.
        /// If the check function returns a success, the original result is returned.
        /// </summary>
        /// <param name="checkFunc">A function that performs a check and returns a <see cref="IResult"/>.</param>
        /// <returns>
        /// The original result if it was a failure, or if the check succeeded;
        /// otherwise, the failure from the check function.
        /// </returns>
        [Obsolete("Use BindTap() for binding results without taking their values instead.")]
        public Result Verify<TResult>(Func<TResult> checkFunc) where TResult : IResult
        {
            ArgumentNullException.ThrowIfNull(checkFunc);

            return result.IsSuccess && checkFunc().TryGetFailure(out var checkFailure)
                ? checkFailure
                : result;
        }

        /// <summary>
        /// Verifies that a successful <see cref="Result"/> satisfies the provided asynchronous check function.
        /// If the original result is a failure, it is returned unchanged.
        /// If the check function returns a failure, that failure is returned.
        /// If the check function returns a success, the original result is returned.
        /// </summary>
        /// <param name="checkFunc">An async function that performs a check and returns a <see cref="Result"/>.</param>
        /// <returns>
        /// The original result if it was a failure, or if the check succeeded;
        /// otherwise, the failure from the check function.
        /// </returns>
        [Obsolete("Use BindTap() for binding results without taking their values instead.")]
        public async Task<Result> VerifyAsync(Func<Task<Result>> checkFunc)
        {
            ArgumentNullException.ThrowIfNull(checkFunc);

            return result.IsSuccess && (await checkFunc().ConfigureAwait(false)).TryGetFailure(out var checkFailure)
                ? checkFailure
                : result;
        }

        /// <summary>
        /// Verifies that a successful <see cref="Result"/> satisfies the provided asynchronous check function.
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
        [Obsolete("Use BindTap() for binding results without taking their values instead.")]
        public async Task<Result> VerifyAsync<TCheck>(Func<Task<Result<TCheck>>> checkFunc)
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
        /// Verifies that a successful <see cref="Task{Result}"/> satisfies the provided check function.
        /// If the original result is a failure, it is returned unchanged.
        /// If the check function returns a failure, that failure is returned.
        /// If the check function returns a success, the original result is returned.
        /// </summary>
        /// <param name="checkFunc">A function that performs a check and returns a <see cref="IResult"/>.</param>
        /// <returns>
        /// The original result if it was a failure, or if the check succeeded;
        /// otherwise, the failure from the check function.
        /// </returns>
        [Obsolete("Use BindTap() for binding results without taking their values instead.")]
        public async Task<Result> Verify<TResult>(Func<TResult> checkFunc) where TResult : IResult
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Verify(checkFunc);
        }

        /// <summary>
        /// Verifies that a successful <see cref="Task{Result}"/> satisfies the provided asynchronous check function.
        /// If the original result is a failure, it is returned unchanged.
        /// If the check function returns a failure, that failure is returned.
        /// If the check function returns a success, the original result is returned.
        /// </summary>
        /// <param name="checkFunc">An async function that performs a check and returns a <see cref="Result"/>.</param>
        /// <returns>
        /// The original result if it was a failure, or if the check succeeded;
        /// otherwise, the failure from the check function.
        /// </returns>
        [Obsolete("Use BindTap() for binding results without taking their values instead.")]
        public async Task<Result> VerifyAsync(Func<Task<Result>> checkFunc)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.VerifyAsync(checkFunc).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that a successful <see cref="Task{Result}"/> satisfies the provided asynchronous check function.
        /// If the original result is a failure, it is returned unchanged.
        /// If the check function returns a failure, that failure is returned.
        /// If the check function returns a success, the original result is returned.
        /// </summary>
        /// <param name="checkFunc">An async function that performs a check and returns a <see cref="Result{TCheck}"/>.</param>
        /// <returns>
        /// The original result if it was a failure, or if the check succeeded;
        /// otherwise, the failure from the check function.
        /// </returns>
        [Obsolete("Use BindTap() for binding results without taking their values instead.")]
        public async Task<Result> VerifyAsync<TCheck>(Func<Task<Result<TCheck>>> checkFunc)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.VerifyAsync(checkFunc).ConfigureAwait(false);
        }
    }

    /// <param name="result">The original result to validate.</param>
    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    extension<TValue>(Result<TValue> result)
    {
        /// <summary>
        /// Verify that a successful <see cref="Result{TValue}"/> satisfies the provided check function.
        /// If the original result is a failure, it is returned unchanged.
        /// If the check function returns a failure, that failure is returned.
        /// If the check function returns a success, the original result is returned.
        /// </summary>
        /// <param name="checkFunc">A function that performs a check and returns a <see cref="IResult"/>.</param>
        /// <returns>
        /// The original result if it was a failure, or if the check succeeded;
        /// otherwise, the failure from the check function.
        /// </returns>
        [Obsolete("Use BindTap() for binding results without taking their values instead.")]
        public Result<TValue> Verify<TResult>(Func<TValue, TResult> checkFunc) where TResult: IResult
        {
            ArgumentNullException.ThrowIfNull(checkFunc);

            return result.TryGetValue(out var value) && checkFunc(value).TryGetFailure(out var checkFailure)
                ? checkFailure
                : result;
        }

        /// <summary>
        /// Verify that a successful <see cref="Result{TValue}"/> satisfies the provided asynchronous check function.
        /// If the original result is a failure, it is returned unchanged.
        /// If the check function returns a failure, that failure is returned.
        /// If the check function returns a success, the original result is returned.
        /// </summary>
        /// <param name="checkFunc">An async function that performs a check and returns a <see cref="Result"/>.</param>
        /// <returns>
        /// The original result if it was a failure, or if the check succeeded;
        /// otherwise, the failure from the check function.
        /// </returns>
        [Obsolete("Use BindTap() for binding results without taking their values instead.")]
        public async Task<Result<TValue>> VerifyAsync(Func<TValue, Task<Result>> checkFunc)
        {
            ArgumentNullException.ThrowIfNull(checkFunc);

            return result.TryGetValue(out var value) && (await checkFunc(value).ConfigureAwait(false)).TryGetFailure(out var checkFailure)
                ? checkFailure
                : result;
        }

        /// <summary>
        /// Verify that a successful <see cref="Result{TValue}"/> satisfies the provided asynchronous check function.
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
        [Obsolete("Use BindTap() for binding results without taking their values instead.")]
        public async Task<Result<TValue>> VerifyAsync<TCheck>(Func<TValue, Task<Result<TCheck>>> checkFunc)
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
        /// Verify that a successful <see cref="Task{Result{TValue}}"/> satisfies the provided check function.
        /// If the original result is a failure, it is returned unchanged.
        /// If the check function returns a failure, that failure is returned.
        /// If the check function returns a success, the original result is returned.
        /// </summary>
        /// <param name="checkFunc">A function that performs a check and returns a <see cref="IResult"/>.</param>
        /// <returns>
        /// The original result if it was a failure, or if the check succeeded;
        /// otherwise, the failure from the check function.
        /// </returns>
        [Obsolete("Use BindTap() for binding results without taking their values instead.")]
        public async Task<Result<TValue>> Verify<TResult>(Func<TValue, TResult> checkFunc) where TResult : IResult
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Verify(checkFunc);
        }

        /// <summary>
        /// Verify that a successful <see cref="Task{Result{TValue}}"/> satisfies the provided asynchronous check function.
        /// If the original result is a failure, it is returned unchanged.
        /// If the check function returns a failure, that failure is returned.
        /// If the check function returns a success, the original result is returned.
        /// </summary>
        /// <param name="checkFunc">An async function that performs a check and returns a <see cref="Result"/>.</param>
        /// <returns>
        /// The original result if it was a failure, or if the check succeeded;
        /// otherwise, the failure from the check function.
        /// </returns>
        [Obsolete("Use BindTap() for binding results without taking their values instead.")]
        public async Task<Result<TValue>> VerifyAsync(Func<TValue, Task<Result>> checkFunc)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.VerifyAsync(checkFunc).ConfigureAwait(false);
        }

        /// <summary>
        /// Verify that a successful <see cref="Task{Result{TValue}}"/> satisfies the provided asynchronous check function.
        /// If the original result is a failure, it is returned unchanged.
        /// If the check function returns a failure, that failure is returned.
        /// If the check function returns a success, the original result is returned.
        /// </summary>
        /// <param name="checkFunc">An async function that performs a check and returns a <see cref="Result{TCheck}"/>.</param>
        /// <returns>
        /// The original result if it was a failure, or if the check succeeded;
        /// otherwise, the failure from the check function.
        /// </returns>
        [Obsolete("Use BindTap() for binding results without taking their values instead.")]
        public async Task<Result<TValue>> VerifyAsync<TCheck>(Func<TValue, Task<Result<TCheck>>> checkFunc)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.VerifyAsync(checkFunc).ConfigureAwait(false);
        }
    }
}