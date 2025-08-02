namespace Toarnbeike.Results.Extensions;

/// <summary>
/// Tap: Extension method for executing side-effects on a successful <see cref="Result"/> or <see cref="Result{TValue}"/>, 
/// without modifying the result.
/// </summary>
public static class TapExtensions
{
    /// <summary>
    /// Executes the specified <paramref name="onSuccess"/> action if the result is a success.
    /// </summary>
    /// <param name="result">The result to inspect.</param>
    /// <param name="onSuccess">The side-effect to perform if the result is a success.</param>
    /// <returns>The original result instance.</returns>
    /// <remarks>
    /// Use this method to perform a side-effect (e.g., logging) when the result is successful.
    /// The value of the result is not modified.
    /// </remarks>
    public static Result Tap(this Result result, Action onSuccess)
    {
        if (result.IsSuccess)
        {
            onSuccess();
        }

        return result;
    }

    /// <summary>
    /// Executes the specified <paramref name="onSuccess"/> task if the result is a success.
    /// </summary>
    /// <param name="result">The result to inspect.</param>
    /// <param name="onSuccess">The side-effect to perform if the result is a success.</param>
    /// <returns>The original result instance.</returns>
    /// <remarks>
    /// Use this method to perform a side-effect (e.g., logging) when the result is successful.
    /// The value of the result is not modified.
    /// </remarks>
    public static async Task<Result> TapAsync(this Result result, Func<Task> onSuccess)
    {
        if (result.IsSuccess)
        {
            await onSuccess().ConfigureAwait(false);
        }

        return result;
    }

    /// <summary>
    /// Executes the specified <paramref name="onSuccess"/> action if the result is a success.
    /// </summary>
    /// <param name="resultTask">The async result to inspect.</param>
    /// <param name="onSuccess">The side-effect to perform if the result is a success.</param>
    /// <returns>The original result instance.</returns>
    /// <remarks>
    /// Use this method to perform a side-effect (e.g., logging) when the result is successful.
    /// The value of the result is not modified.
    /// </remarks>
    public static async Task<Result> Tap(this Task<Result> resultTask, Action onSuccess)
    {
        var result = await resultTask.ConfigureAwait(false);
        return Tap(result, onSuccess);
    }

    /// <summary>
    /// Executes the specified <paramref name="onSuccess"/> task if the result is a success.
    /// </summary>
    /// <param name="resultTask">The async result to inspect.</param>
    /// <param name="onSuccess">The side-effect to perform if the result is a success.</param>
    /// <returns>The original result instance.</returns>
    /// <remarks>
    /// Use this method to perform a side-effect (e.g., logging) when the result is successful.
    /// The value of the result is not modified.
    /// </remarks>
    public static async Task<Result> TapAsync(this Task<Result> resultTask, Func<Task> onSuccess)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await TapAsync(result, onSuccess);
    }

    /// <summary>
    /// Executes the specified <paramref name="onSuccess"/> action if the result is a success.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    /// <param name="result">The result to inspect.</param>
    /// <param name="onSuccess">The side-effect to perform if the result is a success.</param>
    /// <returns>The original result instance.</returns>
    /// <remarks>
    /// Use this method to perform a side-effect (e.g., logging) when the result is successful.
    /// The value of the result is not modified.
    /// </remarks>
    public static Result<TValue> Tap<TValue>(this Result<TValue> result, Action<TValue> onSuccess)
    {
        if (result.TryGetValue(out var value))
        {
            onSuccess(value);
        }

        return result;
    }

    /// <summary>
    /// Executes the specified <paramref name="onSuccess"/> task if the result is a success.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    /// <param name="result">The result to inspect.</param>
    /// <param name="onSuccess">The side-effect to perform if the result is a success.</param>
    /// <returns>The original result instance.</returns>
    /// <remarks>
    /// Use this method to perform a side-effect (e.g., logging) when the result is successful.
    /// The value of the result is not modified.
    /// </remarks>
    public static async Task<Result<TValue>> TapAsync<TValue>(this Result<TValue> result, Func<TValue, Task> onSuccess)
    {
        if (result.TryGetValue(out var value))
        {
            await onSuccess(value).ConfigureAwait(false);
        }

        return result;
    }

    /// <summary>
    /// Executes the specified <paramref name="onSuccess"/> action if the result is a success.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    /// <param name="resultTask">The async result to inspect.</param>
    /// <param name="onSuccess">The side-effect to perform if the result is a success.</param>
    /// <returns>The original result instance.</returns>
    /// <remarks>
    /// Use this method to perform a side-effect (e.g., logging) when the result is successful.
    /// The value of the result is not modified.
    /// </remarks>
    public static async Task<Result<TValue>> Tap<TValue>(this Task<Result<TValue>> resultTask, Action<TValue> onSuccess)
    {
        var result = await resultTask.ConfigureAwait(false);
        return Tap(result, onSuccess);
    }

    /// <summary>
    /// Executes the specified <paramref name="onSuccess"/> task if the result is a success.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    /// <param name="resultTask">The async result to inspect.</param>
    /// <param name="onSuccess">The side-effect to perform if the result is a success.</param>
    /// <returns>The original result instance.</returns>
    /// <remarks>
    /// Use this method to perform a side-effect (e.g., logging) when the result is successful.
    /// The value of the result is not modified.
    /// </remarks>
    public static async Task<Result<TValue>> TapAsync<TValue>(this Task<Result<TValue>> resultTask, Func<TValue, Task> onSuccess)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await TapAsync(result, onSuccess).ConfigureAwait(false);
    }
}
