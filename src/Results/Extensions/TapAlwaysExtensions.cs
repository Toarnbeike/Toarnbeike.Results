namespace Toarnbeike.Results.Extensions;

/// <summary>
/// TapAlways: Extension method for executing side-effects on any <see cref="Result"/> or <see cref="Result{TValue}"/>, 
/// independent of the Success/Failure state and without modifying the result.
/// </summary>
public static class TapAlwaysExtensions
{
    /// <summary>
    /// Executes the specified <paramref name="action"/> action.
    /// </summary>
    /// <param name="result">The result to keep in the pipeline.</param>
    /// <param name="action">The side-effect to perform.</param>
    /// <returns>The original result instance.</returns>
    /// <remarks>
    /// Use this method to perform a side-effect (e.g., logging) on any result. The result is not modified.
    /// </remarks>
    public static Result TapAlways(this Result result, Action action)
    {
        action();
        return result;
    }

    /// <summary>
    /// Executes the specified <paramref name="task"/> task.
    /// </summary>
    /// <param name="result">The result to keep in the pipeline.</param>
    /// <param name="task">The side-effect to perform.</param>
    /// <returns>The original result instance.</returns>
    /// <remarks>
    /// Use this method to perform a side-effect (e.g., logging) on any result. The result is not modified.
    /// </remarks>
    public static async Task<Result> TapAlwaysAsync(this Result result, Func<Task> task)
    {
         await task().ConfigureAwait(false);
         return result;
    }

    /// <summary>
    /// Executes the specified <paramref name="action"/> action.
    /// </summary>
    /// <param name="resultTask">The async result to keep in the pipeline.</param>
    /// <param name="action">The side-effect to perform.</param>
    /// <returns>The original result instance.</returns>
    /// <remarks>
    /// Use this method to perform a side-effect (e.g., logging) on any result. The result is not modified.
    /// </remarks>
    public static async Task<Result> TapAlways(this Task<Result> resultTask, Action action)
    {
        var result = await resultTask.ConfigureAwait(false);
        return TapAlways(result, action);
    }

    /// <summary>
    /// Executes the specified <paramref name="task"/> task.
    /// </summary>
    /// <param name="resultTask">The async result to keep in the pipeline.</param>
    /// <param name="task">The side-effect to perform.</param>
    /// <returns>The original result instance.</returns>
    /// <remarks>
    /// Use this method to perform a side-effect (e.g., logging) on any result. The result is not modified.
    /// </remarks>
    public static async Task<Result> TapAlwaysAsync(this Task<Result> resultTask, Func<Task> task)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await TapAlwaysAsync(result, task).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes the specified <paramref name="action"/> action.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    /// <param name="result">The result to keep in the pipeline.</param>
    /// <param name="action">The side-effect to perform.</param>
    /// <returns>The original result instance.</returns>
    /// <remarks>
    /// Use this method to perform a side-effect (e.g., logging) on any result. The result is not modified.
    /// </remarks>
    public static Result<TValue> TapAlways<TValue>(this Result<TValue> result, Action action)
    {
        action();
        return result;
    }

    /// <summary>
    /// Executes the specified <paramref name="task"/> task.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    /// <param name="result">The result to keep in the pipeline.</param>
    /// <param name="task">The side-effect to perform.</param>
    /// <returns>The original result instance.</returns>
    /// <remarks>
    /// Use this method to perform a side-effect (e.g., logging) on any result. The result is not modified.
    /// </remarks>
    public static async Task<Result<TValue>> TapAlwaysAsync<TValue>(this Result<TValue> result, Func<Task> task)
    {
        await task().ConfigureAwait(false);
        return result;
    }

    /// <summary>
    /// Executes the specified <paramref name="action"/> action.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    /// <param name="resultTask">The async result to inspect.</param>
    /// <param name="action">The side-effect to perform.</param>
    /// <returns>The original result instance.</returns>
    /// <remarks>
    /// Use this method to perform a side-effect (e.g., logging) on any result. The result is not modified.
    /// </remarks>
    public static async Task<Result<TValue>> TapAlways<TValue>(this Task<Result<TValue>> resultTask, Action action)
    {
        var result = await resultTask.ConfigureAwait(false);
        return TapAlways(result, action);
    }

    /// <summary>
    /// Executes the specified <paramref name="task"/> task.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    /// <param name="resultTask">The async result to inspect.</param>
    /// <param name="task">The side-effect to perform.</param>
    /// <returns>The original result instance.</returns>
    /// <remarks>
    /// Use this method to perform a side-effect (e.g., logging) on any result. The result is not modified.
    /// </remarks>
    public static async Task<Result<TValue>> TapAlwaysAsync<TValue>(this Task<Result<TValue>> resultTask, Func<Task> task)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await TapAlwaysAsync(result, task).ConfigureAwait(false);
    }
}
