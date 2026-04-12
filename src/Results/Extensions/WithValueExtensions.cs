namespace Toarnbeike.Results.Extensions;

/// <summary>
/// WithValue: convert a non-generic <see cref="Result"/> into a typed <see cref="Result{TValue}"/> by providing a value.
/// </summary>
public static class WithValueExtensions
{
    /// <param name="result">The result to convert.</param>
    extension(Result result)
    {
        /// <summary>
        /// Converts a successful <see cref="Result"/> into a <see cref="Result{T}"/> with the provided <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of the value to return in case of success.</typeparam>
        /// <param name="value">The value to include in the result if it was successful.</param>
        /// <returns>
        /// A successful result with the provided value if the original result was a success.<br/>
        /// A failed result with the original failure if the original result was a failure.
        /// </returns>
        /// <remarks>
        /// This method is useful for supplying a value after a non-generic result has already confirmed success.
        /// </remarks>
        public Result<TValue> WithValue<TValue>(TValue value)
        {
            return result.TryGetFailure(out var failure)
                ? Result<TValue>.Failure(failure)
                : Result.Success(value);
        }

        /// <summary>
        /// Converts a successful <see cref="Result"/> into a <see cref="Result{T}"/> with the provided <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of the value to return in case of success.</typeparam>
        /// <param name="valueFunc">Func to generate the value to include in the result if it was successful.</param>
        /// <returns>
        /// A successful result with the provided value if the original result was a success.<br/>
        /// A failed result with the original failure if the original result was a failure.
        /// </returns>
        /// <remarks>
        /// This method is useful for supplying a value after a non-generic result has already confirmed success.
        /// </remarks>
        public Result<TValue> WithValue<TValue>(Func<TValue> valueFunc)
        {
            ArgumentNullException.ThrowIfNull(valueFunc);

            return result.TryGetFailure(out var failure)
                ? Result<TValue>.Failure(failure)
                : Result.Success(valueFunc());
        }

        /// <summary>
        /// Converts a successful <see cref="Result"/> into a <see cref="Result{T}"/> with the provided <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of the value to return in case of success.</typeparam>
        /// <param name="valueFunc">Async func to generate the value to include in the result if it was successful.</param>
        /// <returns>
        /// A successful result with the provided value if the original result was a success.<br/>
        /// A failed result with the original failure if the original result was a failure.
        /// </returns>
        /// <remarks>
        /// This method is useful for supplying a value after a non-generic result has already confirmed success.
        /// </remarks>
        public async Task<Result<TValue>> WithValueAsync<TValue>(Func<Task<TValue>> valueFunc)
        {
            ArgumentNullException.ThrowIfNull(valueFunc);

            return result.TryGetFailure(out var failure)
                ? Result<TValue>.Failure(failure)
                : Result.Success(await valueFunc().ConfigureAwait(false));
        }
    }

    /// <param name="resultTask">The async result to convert.</param>
    extension(Task<Result> resultTask)
    {
        /// <summary>
        /// Converts a successful <see cref="Result"/> into a <see cref="Result{T}"/> with the provided <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of the value to return in case of success.</typeparam>
        /// <param name="value">The value to include in the result if it was successful.</param>
        /// <returns>
        /// A successful result with the provided value if the original result was a success.<br/>
        /// A failed result with the original failure if the original result was a failure.
        /// </returns>
        /// <remarks>
        /// This method is useful for supplying a value after a non-generic result has already confirmed success.
        /// </remarks>
        public async Task<Result<TValue>> WithValue<TValue>(TValue value)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.WithValue(value);
        }

        /// <summary>
        /// Converts a successful <see cref="Result"/> into a <see cref="Result{T}"/> with the provided <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of the value to return in case of success.</typeparam>
        /// <param name="valueFunc">Func to generate the value to include in the result if it was successful.</param>
        /// <returns>
        /// A successful result with the provided value if the original result was a success.<br/>
        /// A failed result with the original failure if the original result was a failure.
        /// </returns>
        /// <remarks>
        /// This method is useful for supplying a value after a non-generic result has already confirmed success.
        /// </remarks>
        public async Task<Result<TValue>> WithValue<TValue>(Func<TValue> valueFunc)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.WithValue(valueFunc);
        }

        /// <summary>
        /// Converts a successful <see cref="Result"/> into a <see cref="Result{T}"/> with the provided <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of the value to return in case of success.</typeparam>
        /// <param name="valueFunc">Async func to generate the value to include in the result if it was successful.</param>
        /// <returns>
        /// A successful result with the provided value if the original result was a success.<br/>
        /// A failed result with the original failure if the original result was a failure.
        /// </returns>
        /// <remarks>
        /// This method is useful for supplying a value after a non-generic result has already confirmed success.
        /// </remarks>
        public async Task<Result<TValue>> WithValueAsync<TValue>(Func<Task<TValue>> valueFunc)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.WithValueAsync(valueFunc).ConfigureAwait(false);
        }
    }
}
