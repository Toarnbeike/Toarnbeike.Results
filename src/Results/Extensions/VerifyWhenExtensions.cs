namespace Toarnbeike.Results.Extensions;

/// <summary>
/// Verify When: Performs a conditional check that only runs if a predicate evaluates to true.
/// </summary>
[Obsolete("Use BindTap() with an internal check instead.")]
public static class VerifyWhenExtensions
{
    /// <param name="result">The original result to validate.</param>
    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    extension<TValue>(Result<TValue> result)
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
        /// <param name="predicate">Predicate to determine whether to apply the check.</param>
        /// <param name="checkFunc">A function that performs a check and returns a <see cref="IResult"/>.</param>
        /// <returns>
        /// The original result if the predicate is false, the result is a failure,
        /// or the check succeeds; otherwise, the failure from the check function.
        /// </returns>
        [Obsolete("Use BindTap() with an internal check instead.")]
        public Result<TValue> VerifyWhen<TResult>(Func<TValue, bool> predicate,
            Func<TValue, TResult> checkFunc) where TResult : IResult
        {
            ArgumentNullException.ThrowIfNull(predicate);
            ArgumentNullException.ThrowIfNull(checkFunc);

            return result.TryGetValue(out var value) && predicate(value) && checkFunc(value).TryGetFailure(out var checkFailure)
                ? checkFailure
                : result;
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
        /// <param name="predicate">Predicate to determine whether to apply the check.</param>
        /// <param name="checkFunc">An async function that performs a check and returns a <see cref="Result{TCheck}"/>.</param>
        /// <returns>
        /// The original result if the predicate is false, the result is a failure,
        /// or the check succeeds; otherwise, the failure from the check function.
        /// </returns>
        [Obsolete("Use BindTap() with an internal check instead.")]
        public async Task<Result<TValue>> VerifyWhenAsync(Func<TValue, bool> predicate,
            Func<TValue, Task<Result>> checkFunc)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            ArgumentNullException.ThrowIfNull(checkFunc);

            return result.TryGetValue(out var value) && predicate(value) && (await checkFunc(value).ConfigureAwait(false)).TryGetFailure(out var checkFailure)
                ? checkFailure
                : result;
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
        /// <typeparam name="TCheck">The type used internally by the check function (not returned).</typeparam>
        /// <param name="predicate">Predicate to determine whether to apply the check.</param>
        /// <param name="checkFunc">An async function that performs a check and returns a <see cref="Result{TCheck}"/>.</param>
        /// <returns>
        /// The original result if the predicate is false, the result is a failure,
        /// or the check succeeds; otherwise, the failure from the check function.
        /// </returns>
        [Obsolete("Use BindTap() with an internal check instead.")]
        public async Task<Result<TValue>> VerifyWhenAsync<TCheck>(Func<TValue, bool> predicate,
            Func<TValue, Task<Result<TCheck>>> checkFunc)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            ArgumentNullException.ThrowIfNull(checkFunc);

            return result.TryGetValue(out var value) && predicate(value) && (await checkFunc(value).ConfigureAwait(false)).TryGetFailure(out var checkFailure)
                ? checkFailure
                : result;
        }
    }

    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    extension<TValue>(Task<Result<TValue>> resultTask)
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
        /// <param name="result">The original result to validate.</param>
        /// <param name="predicate">Predicate to determine whether to apply the check.</param>
        /// <param name="checkFunc">A function that performs a check and returns a <see cref="IResult"/>.</param>
        /// <returns>
        /// The original result if the predicate is false, the result is a failure,
        /// or the check succeeds; otherwise, the failure from the check function.
        /// </returns>
        [Obsolete("Use BindTap() with an internal check instead.")]
        public async Task<Result<TValue>> VerifyWhen<TResult>(Func<TValue, bool> predicate,
            Func<TValue, TResult> checkFunc) where TResult : IResult
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.VerifyWhen(predicate, checkFunc);
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
        /// <param name="result">The original result to validate.</param>
        /// <param name="predicate">Predicate to determine whether to apply the check.</param>
        /// <param name="checkFunc">An async function that performs a check and returns a <see cref="Result{TCheck}"/>.</param>
        /// <returns>
        /// The original result if the predicate is false, the result is a failure,
        /// or the check succeeds; otherwise, the failure from the check function.
        /// </returns>
        [Obsolete("Use BindTap() with an internal check instead.")]
        public async Task<Result<TValue>> VerifyWhenAsync(Func<TValue, bool> predicate,
            Func<TValue, Task<Result>> checkFunc)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.VerifyWhenAsync(predicate, checkFunc).ConfigureAwait(false);
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
        /// <typeparam name="TCheck">The type used internally by the check function (not returned).</typeparam>
        /// <param name="result">The original result to validate.</param>
        /// <param name="predicate">Predicate to determine whether to apply the check.</param>
        /// <param name="checkFunc">An async function that performs a check and returns a <see cref="Result{TCheck}"/>.</param>
        /// <returns>
        /// The original result if the predicate is false, the result is a failure,
        /// or the check succeeds; otherwise, the failure from the check function.
        /// </returns>
        [Obsolete("Use BindTap() with an internal check instead.")]
        public async Task<Result<TValue>> VerifyWhenAsync<TCheck>(Func<TValue, bool> predicate,
            Func<TValue, Task<Result<TCheck>>> checkFunc)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.VerifyWhenAsync(predicate, checkFunc).ConfigureAwait(false);
        }
    }
}