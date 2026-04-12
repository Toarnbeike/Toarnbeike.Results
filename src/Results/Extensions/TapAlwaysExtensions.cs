using System;

namespace Toarnbeike.Results.Extensions;

/// <summary>
/// TapAlways: Extension method for executing side effects on any <see cref="Result"/> or <see cref="Result{TValue}"/>, 
/// independent of the Success/Failure state and without modifying the result.
/// </summary>
[Obsolete("Use Tap and TapFailure sequentially instead.")]
public static class TapAlwaysExtensions
{
    /// <param name="result">The result to keep in the pipeline.</param>
    extension(Result result)
    {
        /// <summary>
        /// Executes the specified <paramref name="action"/> action.
        /// </summary>
        /// <param name="action">The side effect to perform.</param>
        /// <returns>The original result instance.</returns>
        /// <remarks>
        /// Use this method to perform a side effect (e.g., logging) on any result. The result is not modified.
        /// </remarks>
        [Obsolete("Use Tap and TapFailure sequentially instead.")]
        public Result TapAlways(Action action)
        {
            ArgumentNullException.ThrowIfNull(action);

            action();
            return result;
        }

        /// <summary>
        /// Executes the specified <paramref name="task"/> task.
        /// </summary>
        /// <param name="task">The side effect to perform.</param>
        /// <returns>The original result instance.</returns>
        /// <remarks>
        /// Use this method to perform a side effect (e.g., logging) on any result. The result is not modified.
        /// </remarks>
        [Obsolete("Use Tap and TapFailure sequentially instead.")]
        public async Task<Result> TapAlwaysAsync(Func<Task> task)
        {
            ArgumentNullException.ThrowIfNull(task);

            await task().ConfigureAwait(false);
            return result;
        }
    }

    /// <param name="resultTask">The async result to keep in the pipeline.</param>
    extension(Task<Result> resultTask)
    {
        /// <summary>
        /// Executes the specified <paramref name="action"/> action.
        /// </summary>
        /// <param name="action">The side effect to perform.</param>
        /// <returns>The original result instance.</returns>
        /// <remarks>
        /// Use this method to perform a side effect (e.g., logging) on any result. The result is not modified.
        /// </remarks>
        [Obsolete("Use Tap and TapFailure sequentially instead.")]
        public async Task<Result> TapAlways(Action action)
        {
            ArgumentNullException.ThrowIfNull(action);

            var result = await resultTask.ConfigureAwait(false);
            return result.TapAlways(action);
        }

        /// <summary>
        /// Executes the specified <paramref name="task"/> task.
        /// </summary>
        /// <param name="task">The side effect to perform.</param>
        /// <returns>The original result instance.</returns>
        /// <remarks>
        /// Use this method to perform a side effect (e.g., logging) on any result. The result is not modified.
        /// </remarks>
        [Obsolete("Use Tap and TapFailure sequentially instead.")]
        public async Task<Result> TapAlwaysAsync(Func<Task> task)
        {
            ArgumentNullException.ThrowIfNull(task);

            var result = await resultTask.ConfigureAwait(false);
            return await result.TapAlwaysAsync(task).ConfigureAwait(false);
        }
    }

    /// <param name="result">The result to keep in the pipeline.</param>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    extension<TValue>(Result<TValue> result)
    {
        /// <summary>
        /// Executes the specified <paramref name="action"/> action.
        /// </summary>
        /// <param name="action">The side effect to perform.</param>
        /// <returns>The original result instance.</returns>
        /// <remarks>
        /// Use this method to perform a side effect (e.g., logging) on any result. The result is not modified.
        /// </remarks>
        [Obsolete("Use Tap and TapFailure sequentially instead.")]
        public Result<TValue> TapAlways(Action action)
        {
            ArgumentNullException.ThrowIfNull(action);

            action();
            return result;
        }

        /// <summary>
        /// Executes the specified <paramref name="task"/> task.
        /// </summary>
        /// <param name="task">The side effect to perform.</param>
        /// <returns>The original result instance.</returns>
        /// <remarks>
        /// Use this method to perform a side effect (e.g., logging) on any result. The result is not modified.
        /// </remarks>
        [Obsolete("Use Tap and TapFailure sequentially instead.")]
        public async Task<Result<TValue>> TapAlwaysAsync(Func<Task> task)
        {
            ArgumentNullException.ThrowIfNull(task);

            await task().ConfigureAwait(false);
            return result;
        }
    }

    /// <param name="resultTask">The async result to inspect.</param>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    extension<TValue>(Task<Result<TValue>> resultTask)
    {
        /// <summary>
        /// Executes the specified <paramref name="action"/> action.
        /// </summary>
        /// <param name="action">The side effect to perform.</param>
        /// <returns>The original result instance.</returns>
        /// <remarks>
        /// Use this method to perform a side effect (e.g., logging) on any result. The result is not modified.
        /// </remarks>
        [Obsolete("Use Tap and TapFailure sequentially instead.")]
        public async Task<Result<TValue>> TapAlways(Action action)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.TapAlways(action);
        }

        /// <summary>
        /// Executes the specified <paramref name="task"/> task.
        /// </summary>
        /// <param name="task">The side effect to perform.</param>
        /// <returns>The original result instance.</returns>
        /// <remarks>
        /// Use this method to perform a side effect (e.g., logging) on any result. The result is not modified.
        /// </remarks>
        [Obsolete("Use Tap and TapFailure sequentially instead.")]
        public async Task<Result<TValue>> TapAlwaysAsync(Func<Task> task)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.TapAlwaysAsync(task).ConfigureAwait(false);
        }
    }
}
