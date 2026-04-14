namespace Toarnbeike.Results.Extensions;

/// <summary>
/// TapFailure: Extension method for executing side effects on a failing <see cref="Result"/> or <see cref="Result{TValue}"/>, 
/// without modifying the result.
/// </summary>
public static class TapFailureExtensions
{
    /// <param name="result">The result to inspect.</param>
    extension<TResult>(TResult result) where TResult : IResult
    {
        /// <summary>
        /// Executes the specified <paramref name="onFailure"/> action if the result is a failure.
        /// </summary>
        /// <param name="onFailure">The side effect to perform if the result is a failure.</param>
        /// <returns>The original result instance.</returns>
        /// <remarks>
        /// Use this method to perform a side effect (e.g., logging) when the result is a failure.
        /// The error of the result is not modified.
        /// </remarks>
        public TResult TapFailure(Action<Failure> onFailure)
        {
            ArgumentNullException.ThrowIfNull(onFailure);

            if (result.IsFailure && result.TryGetFailure(out var failure))
            {
                onFailure(failure);
            }

            return result;
        }

        /// <summary>
        /// Executes the specified <paramref name="onFailure"/> action if the result is a failure.
        /// </summary>
        /// <param name="onFailure">The async side effect to perform if the result is a failure.</param>
        /// <returns>The original result instance.</returns>
        /// <remarks>
        /// Use this method to perform a side effect (e.g., logging) when the result is a failure.
        /// The error of the result is not modified.
        /// </remarks>
        public async Task<TResult> TapFailureAsync(Func<Failure, Task> onFailure)
        {
            ArgumentNullException.ThrowIfNull(onFailure);

            if (result.IsFailure && result.TryGetFailure(out var failure))
            {
                await onFailure(failure).ConfigureAwait(false);
            }

            return result;
        }
    }

    /// <param name="resultTask">The async result to inspect.</param>
    extension(Task<Result> resultTask)
    {
        /// <summary>
        /// Executes the specified <paramref name="onFailure"/> action if the result is a failure.
        /// </summary>
        /// <param name="onFailure">The side effect to perform if the result is a failure.</param>
        /// <returns>The original result instance.</returns>
        /// <remarks>
        /// Use this method to perform a side effect (e.g., logging) when the result is a failure.
        /// The error of the result is not modified.
        /// </remarks>
        public async Task<Result> TapFailure(Action<Failure> onFailure)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.TapFailure(onFailure);
        }

        /// <summary>
        /// Executes the specified <paramref name="onFailure"/> action if the result is a failure.
        /// </summary>
        /// <param name="onFailure">The async side effect to perform if the result is a failure.</param>
        /// <returns>The original result instance.</returns>
        /// <remarks>
        /// Use this method to perform a side effect (e.g., logging) when the result is a failure.
        /// The error of the result is not modified.
        /// </remarks>
        public async Task<Result> TapFailureAsync(Func<Failure, Task> onFailure)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.TapFailureAsync(onFailure).ConfigureAwait(false);
        }
    }

    ///// <param name="result">The result to inspect.</param>
    ///// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    //extension<TValue>(Result<TValue> result)
    //{
    //    /// <summary>
    //    /// Executes the specified <paramref name="onFailure"/> action if the result is a failure.
    //    /// </summary>
    //    /// <param name="onFailure">The side effect to perform if the result is a failure.</param>
    //    /// <returns>The original result instance.</returns>
    //    /// <remarks>
    //    /// Use this method to perform a side effect (e.g., logging) when the result is a failure.
    //    /// The error of the result is not modified.
    //    /// </remarks>
    //    public Result<TValue> TapFailure(Action<Failure> onFailure)
    //    {
    //        ArgumentNullException.ThrowIfNull(onFailure);

    //        if (result.IsFailure && result.TryGetFailure(out var failure))
    //        {
    //            onFailure(failure);
    //        }

    //        return result;
    //    }

    //    /// <summary>
    //    /// Executes the specified <paramref name="onFailure"/> action if the result is a failure.
    //    /// </summary>
    //    /// <param name="onFailure">The async side effect to perform if the result is a failure.</param>
    //    /// <returns>The original result instance.</returns>
    //    /// <remarks>
    //    /// Use this method to perform a side effect (e.g., logging) when the result is a failure.
    //    /// The error of the result is not modified.
    //    /// </remarks>
    //    public async Task<Result<TValue>> TapFailureAsync(Func<Failure, Task> onFailure)
    //    {
    //        ArgumentNullException.ThrowIfNull(onFailure);

    //        if (result.IsFailure && result.TryGetFailure(out var failure))
    //        {
    //            await onFailure(failure).ConfigureAwait(false);
    //        }

    //        return result;
    //    }
    //}

    /// <param name="resultTask">The async result to inspect.</param>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    extension<TValue>(Task<Result<TValue>> resultTask)
    {
        /// <summary>
        /// Executes the specified <paramref name="onFailure"/> action if the result is a failure.
        /// </summary>
        /// <param name="onFailure">The side effect to perform if the result is a failure.</param>
        /// <returns>The original result instance.</returns>
        /// <remarks>
        /// Use this method to perform a side effect (e.g., logging) when the result is a failure.
        /// The error of the result is not modified.
        /// </remarks>
        public async Task<Result<TValue>> TapFailure(Action<Failure> onFailure)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.TapFailure(onFailure);
        }

        /// <summary>
        /// Executes the specified <paramref name="onFailure"/> action if the result is a failure.
        /// </summary>
        /// <param name="onFailure">The async side effect to perform if the result is a failure.</param>
        /// <returns>The original result instance.</returns>
        /// <remarks>
        /// Use this method to perform a side effect (e.g., logging) when the result is a failure.
        /// The error of the result is not modified.
        /// </remarks>
        public async Task<Result<TValue>> TapFailureAsync(Func<Failure, Task> onFailure)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.TapFailureAsync(onFailure).ConfigureAwait(false);
        }
    }
}