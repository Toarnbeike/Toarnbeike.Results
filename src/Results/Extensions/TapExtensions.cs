namespace Toarnbeike.Results.Extensions;

/// <summary>
/// Tap: Extension method for executing side effects on a successful <see cref="Result"/> or <see cref="Result{TValue}"/>, 
/// without modifying the result.
/// </summary>
public static class TapExtensions
{
    /// <param name="result">The result to inspect.</param>
    extension(Result result)
    {
        /// <summary>
        /// Executes the specified <paramref name="onSuccess"/> action if the result is a success.
        /// </summary>
        /// <param name="onSuccess">The side effect to perform if the result is a success.</param>
        /// <returns>The original result instance.</returns>
        /// <remarks>
        /// Use this method to perform a side effect (e.g., logging) when the result is successful.
        /// The value of the result is not modified.
        /// </remarks>
        public Result Tap(Action onSuccess)
        {
            ArgumentNullException.ThrowIfNull(onSuccess);

            if (result.IsSuccess)
            {
                onSuccess();
            }

            return result;
        }

        /// <summary>
        /// Executes the specified <paramref name="onSuccess"/> task if the result is a success.
        /// </summary>
        /// <param name="onSuccess">The side effect to perform if the result is a success.</param>
        /// <returns>The original result instance.</returns>
        /// <remarks>
        /// Use this method to perform a side effect (e.g., logging) when the result is successful.
        /// The value of the result is not modified.
        /// </remarks>
        public async Task<Result> TapAsync(Func<Task> onSuccess)
        {
            ArgumentNullException.ThrowIfNull(onSuccess);

            if (result.IsSuccess)
            {
                await onSuccess().ConfigureAwait(false);
            }

            return result;
        }
    }

    /// <param name="resultTask">The async result to inspect.</param>
    extension(Task<Result> resultTask)
    {
        /// <summary>
        /// Executes the specified <paramref name="onSuccess"/> action if the result is a success.
        /// </summary>
        /// <param name="onSuccess">The side effect to perform if the result is a success.</param>
        /// <returns>The original result instance.</returns>
        /// <remarks>
        /// Use this method to perform a side effect (e.g., logging) when the result is successful.
        /// The value of the result is not modified.
        /// </remarks>
        public async Task<Result> Tap(Action onSuccess)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Tap(onSuccess);
        }

        /// <summary>
        /// Executes the specified <paramref name="onSuccess"/> task if the result is a success.
        /// </summary>
        /// <param name="onSuccess">The side effect to perform if the result is a success.</param>
        /// <returns>The original result instance.</returns>
        /// <remarks>
        /// Use this method to perform a side effect (e.g., logging) when the result is successful.
        /// The value of the result is not modified.
        /// </remarks>
        public async Task<Result> TapAsync(Func<Task> onSuccess)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.TapAsync(onSuccess).ConfigureAwait(false);
        }
    }

    /// <param name="result">The result to inspect.</param>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    extension<TValue>(Result<TValue> result)
    {
        /// <summary>
        /// Executes the specified <paramref name="onSuccess"/> action if the result is a success.
        /// </summary>
        /// <param name="onSuccess">The side effect to perform if the result is a success.</param>
        /// <returns>The original result instance.</returns>
        /// <remarks>
        /// Use this method to perform a side effect (e.g., logging) when the result is successful.
        /// The value of the result is not modified.
        /// </remarks>
        public Result<TValue> Tap(Action<TValue> onSuccess)
        {
            ArgumentNullException.ThrowIfNull(onSuccess);

            if (result.TryGetValue(out var value))
            {
                onSuccess(value);
            }

            return result;
        }

        /// <summary>
        /// Executes the specified <paramref name="onSuccess"/> task if the result is a success.
        /// </summary>
        /// <param name="onSuccess">The side effect to perform if the result is a success.</param>
        /// <returns>The original result instance.</returns>
        /// <remarks>
        /// Use this method to perform a side effect (e.g., logging) when the result is successful.
        /// The value of the result is not modified.
        /// </remarks>
        public async Task<Result<TValue>> TapAsync(Func<TValue, Task> onSuccess)
        {
            ArgumentNullException.ThrowIfNull(onSuccess);

            if (result.TryGetValue(out var value))
            {
                await onSuccess(value).ConfigureAwait(false);
            }

            return result;
        }
    }

    /// <param name="resultTask">The async result to inspect.</param>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    extension<TValue>(Task<Result<TValue>> resultTask)
    {
        /// <summary>
        /// Executes the specified <paramref name="onSuccess"/> action if the result is a success.
        /// </summary>
        /// <param name="onSuccess">The side effect to perform if the result is a success.</param>
        /// <returns>The original result instance.</returns>
        /// <remarks>
        /// Use this method to perform a side effect (e.g., logging) when the result is successful.
        /// The value of the result is not modified.
        /// </remarks>
        public async Task<Result<TValue>> Tap(Action<TValue> onSuccess)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Tap(onSuccess);
        }

        /// <summary>
        /// Executes the specified <paramref name="onSuccess"/> task if the result is a success.
        /// </summary>
        /// <param name="onSuccess">The side effect to perform if the result is a success.</param>
        /// <returns>The original result instance.</returns>
        /// <remarks>
        /// Use this method to perform a side effect (e.g., logging) when the result is successful.
        /// The value of the result is not modified.
        /// </remarks>
        public async Task<Result<TValue>> TapAsync(Func<TValue, Task> onSuccess)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.TapAsync(onSuccess).ConfigureAwait(false);
        }
    }
}
