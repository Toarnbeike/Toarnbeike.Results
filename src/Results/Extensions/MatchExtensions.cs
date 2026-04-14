namespace Toarnbeike.Results.Extensions;

/// <summary>
/// Match: extract a value from a <see cref="Result"/> or a <see cref="Result{TValue}"/>
/// by matching on success or failure.
/// </summary>
public static class MatchExtensions
{
    /// <param name="result">The result to match on.</param>
    extension(Result result)
    {
        /// <summary>
        /// Projects the result by applying one of the two provided functions,
        /// depending on whether the result is a success or a failure.
        /// </summary>
        /// <param name="onSuccess">The action to apply if the result is a success.</param>
        /// <param name="onFailure">The action to apply if the result is a failure.</param>
        /// <remarks>
        /// This method is useful when you want to transform a result into another value based on whether it succeeded or failed.
        /// Both match functions are required and must be non-null.
        /// </remarks>
        public void Match(Action onSuccess, Action<Failure> onFailure)
        {
            ArgumentNullException.ThrowIfNull(onSuccess);
            ArgumentNullException.ThrowIfNull(onFailure);

            if (result.TryGetFailure(out var failure))
            {
                onFailure(failure);
            }
            else
            {
                onSuccess();
            }
        }

        /// <summary>
        /// Projects the result into a value of type <typeparamref name="TOut"/> by applying one of the two provided functions,
        /// depending on whether the result is a success or a failure.
        /// </summary>
        /// <typeparam name="TOut">The type of the value returned by the match.</typeparam>
        /// <param name="onSuccess">The function to apply if the result is a success.</param>
        /// <param name="onFailure">The function to apply if the result is a failure.</param>
        /// <returns>The value returned by either <paramref name="onSuccess"/> or <paramref name="onFailure"/>.</returns>
        /// <remarks>
        /// This method is useful when you want to transform a result into another value based on whether it succeeded or failed.
        /// Both match functions are required and must be non-null.
        /// </remarks>
        public TOut Match<TOut>(Func<TOut> onSuccess, Func<Failure, TOut> onFailure)
        {
            ArgumentNullException.ThrowIfNull(onSuccess);
            ArgumentNullException.ThrowIfNull(onFailure);

            return result.TryGetFailure(out var failure)
                ? onFailure(failure)
                : onSuccess();
        }

        /// <summary>
        /// Projects the result by applying one of the two provided async functions,
        /// depending on whether the result is a success or a failure.
        /// </summary>
        /// <param name="onSuccess">The async action to apply if the result is a success.</param>
        /// <param name="onFailure">The async action to apply if the result is a failure.</param>
        /// <remarks>
        /// This method is useful when you want to transform a result into another value based on whether it succeeded or failed.
        /// Both match functions are required and must be non-null.
        /// </remarks>
        public async Task MatchAsync(Func<Task> onSuccess, Func<Failure, Task> onFailure)
        {
            ArgumentNullException.ThrowIfNull(onSuccess);
            ArgumentNullException.ThrowIfNull(onFailure);

            if (result.TryGetFailure(out var failure))
            {
                await onFailure(failure);
            }
            else
            {
                await onSuccess();
            }
        }

        /// <summary>
        /// Projects the result into a value of type <typeparamref name="TOut"/> by applying one of the two provided async functions,
        /// depending on whether the result is a success or a failure.
        /// </summary>
        /// <typeparam name="TOut">The type of the value returned by the match.</typeparam>
        /// <param name="onSuccess">The async function to apply if the result is a success.</param>
        /// <param name="onFailure">The async function to apply if the result is a failure.</param>
        /// <returns>The value returned by either <paramref name="onSuccess"/> or <paramref name="onFailure"/>.</returns>
        /// <remarks>
        /// This method is useful when you want to transform a result into another value based on whether it succeeded or failed.
        /// Both match functions are required and must be non-null.
        /// </remarks>
        public async Task<TOut> MatchAsync<TOut>(Func<Task<TOut>> onSuccess, Func<Failure, Task<TOut>> onFailure)
        {
            ArgumentNullException.ThrowIfNull(onSuccess);
            ArgumentNullException.ThrowIfNull(onFailure);

            return result.TryGetFailure(out var failure)
                ? await onFailure(failure).ConfigureAwait(false)
                : await onSuccess().ConfigureAwait(false);
        }
    }

    /// <param name="resultTask">The async result to match on.</param>
    extension(Task<Result> resultTask)
    {
        /// <summary>
        /// Projects the result by applying one of the two provided functions,
        /// depending on whether the result is a success or a failure.
        /// </summary>
        /// <param name="onSuccess">The action to apply if the result is a success.</param>
        /// <param name="onFailure">The action to apply if the result is a failure.</param>
        /// <remarks>
        /// This method is useful when you want to transform a result into another value based on whether it succeeded or failed.
        /// Both match functions are required and must be non-null.
        /// </remarks>
        public async Task Match(Action onSuccess, Action<Failure> onFailure)
        {
            var result = await resultTask.ConfigureAwait(false);
            result.Match(onSuccess, onFailure);
        }

        /// <summary>
        /// Projects the result into a value of type <typeparamref name="TOut"/> by applying one of the two provided functions,
        /// depending on whether the result is a success or a failure.
        /// </summary>
        /// <typeparam name="TOut">The type of the value returned by the match.</typeparam>
        /// <param name="onSuccess">The function to apply if the result is a success.</param>
        /// <param name="onFailure">The function to apply if the result is a failure.</param>
        /// <returns>The value returned by either <paramref name="onSuccess"/> or <paramref name="onFailure"/>.</returns>
        /// <remarks>
        /// This method is useful when you want to transform a result into another value based on whether it succeeded or failed.
        /// Both match functions are required and must be non-null.
        /// </remarks>
        public async Task<TOut> Match<TOut>(Func<TOut> onSuccess, Func<Failure, TOut> onFailure)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Match(onSuccess, onFailure);
        }

        /// <summary>
        /// Projects the result by applying one of the two provided async functions,
        /// depending on whether the result is a success or a failure.
        /// </summary>
        /// <param name="onSuccess">The async action to apply if the result is a success.</param>
        /// <param name="onFailure">The async action to apply if the result is a failure.</param>
        /// <remarks>
        /// This method is useful when you want to transform a result into another value based on whether it succeeded or failed.
        /// Both match functions are required and must be non-null.
        /// </remarks>
        public async Task MatchAsync(Func<Task> onSuccess, Func<Failure, Task> onFailure)
        {
            var result = await resultTask;
            await result.MatchAsync(onSuccess, onFailure);
        }

        /// <summary>
        /// Projects the result into a value of type <typeparamref name="TOut"/> by applying one of the two provided async functions,
        /// depending on whether the result is a success or a failure.
        /// </summary>
        /// <typeparam name="TOut">The type of the value returned by the match.</typeparam>
        /// <param name="onSuccess">The async function to apply if the result is a success.</param>
        /// <param name="onFailure">The async function to apply if the result is a failure.</param>
        /// <returns>The value returned by either <paramref name="onSuccess"/> or <paramref name="onFailure"/>.</returns>
        /// <remarks>
        /// This method is useful when you want to transform a result into another value based on whether it succeeded or failed.
        /// Both match functions are required and must be non-null.
        /// </remarks>
        public async Task<TOut> MatchAsync<TOut>(Func<Task<TOut>> onSuccess, Func<Failure, Task<TOut>> onFailure)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.MatchAsync(onSuccess, onFailure).ConfigureAwait(false);
        }
    }

    /// <param name="result">The result to match on.</param>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    extension<TValue>(Result<TValue> result)
    {
        /// <summary>
        /// Projects the result by applying one of the two provided functions,
        /// depending on whether the result is a success or a failure.
        /// </summary>
        /// <param name="onSuccess">The action to apply if the result is a success.</param>
        /// <param name="onFailure">The action to apply if the result is a failure.</param>
        /// <remarks>
        /// This method is useful when you want to transform a result into another value based on whether it succeeded or failed.
        /// Both match functions are required and must be non-null.
        /// </remarks>
        public void Match(Action<TValue> onSuccess, Action<Failure> onFailure)
        {
            ArgumentNullException.ThrowIfNull(onSuccess);
            ArgumentNullException.ThrowIfNull(onFailure);

            if (result.Deconstruct(out var value, out var failure))
            {
                onSuccess(value);
            }
            else
            {
                onFailure(failure);
            }
        }

        /// <summary>
        /// Projects the result into a value of type <typeparamref name="TOut"/> by applying one of the two provided functions,
        /// depending on whether the result is a success or a failure.
        /// </summary>
        /// <typeparam name="TOut">The type of the value returned by the match.</typeparam>
        /// <param name="onSuccess">The function to apply if the result is a success.</param>
        /// <param name="onFailure">The function to apply if the result is a failure.</param>
        /// <returns>The value returned by either <paramref name="onSuccess"/> or <paramref name="onFailure"/>.</returns>
        /// <remarks>
        /// This method is useful when you want to transform a result into another value based on whether it succeeded or failed.
        /// Both match functions are required and must be non-null.
        /// </remarks>
        public TOut Match<TOut>(Func<TValue, TOut> onSuccess, Func<Failure, TOut> onFailure)
        {
            ArgumentNullException.ThrowIfNull(onSuccess);
            ArgumentNullException.ThrowIfNull(onFailure);

            return result.Deconstruct(out var value, out var failure)
                ? onSuccess(value)
                : onFailure(failure);
        }

        /// <summary>
        /// Projects the result by applying one of the two provided async functions,
        /// depending on whether the result is a success or a failure.
        /// </summary>
        /// <param name="onSuccess">The async action to apply if the result is a success.</param>
        /// <param name="onFailure">The async action to apply if the result is a failure.</param>
        /// <remarks>
        /// This method is useful when you want to transform a result into another value based on whether it succeeded or failed.
        /// Both match functions are required and must be non-null.
        /// </remarks>
        public async Task MatchAsync(Func<TValue, Task> onSuccess, Func<Failure, Task> onFailure)
        {
            ArgumentNullException.ThrowIfNull(onSuccess);
            ArgumentNullException.ThrowIfNull(onFailure);

            if (result.Deconstruct(out var value, out var failure))
            {
                await onSuccess(value);
            }
            else
            {
                await onFailure(failure);
            }
        }

        /// <summary>
        /// Projects the result into a value of type <typeparamref name="TOut"/> by applying one of the two provided async functions,
        /// depending on whether the result is a success or a failure.
        /// </summary>
        /// <typeparam name="TOut">The type of the value returned by the match.</typeparam>
        /// <param name="onSuccess">The async function to apply if the result is a success.</param>
        /// <param name="onFailure">The async function to apply if the result is a failure.</param>
        /// <returns>The value returned by either <paramref name="onSuccess"/> or <paramref name="onFailure"/>.</returns>
        /// <remarks>
        /// This method is useful when you want to transform a result into another value based on whether it succeeded or failed.
        /// Both match functions are required and must be non-null.
        /// </remarks>
        public async Task<TOut> MatchAsync<TOut>(Func<TValue, Task<TOut>> onSuccess, Func<Failure, Task<TOut>> onFailure)
        {
            ArgumentNullException.ThrowIfNull(onSuccess);
            ArgumentNullException.ThrowIfNull(onFailure);

            return result.Deconstruct(out var value, out var failure)
                ? await onSuccess(value).ConfigureAwait(false)
                : await onFailure(failure).ConfigureAwait(false);
        }
    }

    /// <param name="resultTask">The async result to match on.</param>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    extension<TValue>(Task<Result<TValue>> resultTask)
    {
        /// <summary>
        /// Projects the result by applying one of the two provided functions,
        /// depending on whether the result is a success or a failure.
        /// </summary>
        /// <param name="onSuccess">The action to apply if the result is a success.</param>
        /// <param name="onFailure">The action to apply if the result is a failure.</param>
        /// <remarks>
        /// This method is useful when you want to transform a result into another value based on whether it succeeded or failed.
        /// Both match functions are required and must be non-null.
        /// </remarks>
        public async Task Match(Action<TValue> onSuccess, Action<Failure> onFailure)
        {
            var result = await resultTask.ConfigureAwait(false);
            result.Match(onSuccess, onFailure);
        }

        /// <summary>
        /// Projects the result into a value of type <typeparamref name="TOut"/> by applying one of the two provided functions,
        /// depending on whether the result is a success or a failure.
        /// </summary>
        /// <typeparam name="TOut">The type of the value returned by the match.</typeparam>
        /// <param name="onSuccess">The function to apply if the result is a success.</param>
        /// <param name="onFailure">The function to apply if the result is a failure.</param>
        /// <returns>The value returned by either <paramref name="onSuccess"/> or <paramref name="onFailure"/>.</returns>
        /// <remarks>
        /// This method is useful when you want to transform a result into another value based on whether it succeeded or failed.
        /// Both match functions are required and must be non-null.
        /// </remarks>
        public async Task<TOut> Match<TOut>(Func<TValue, TOut> onSuccess, Func<Failure, TOut> onFailure)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Match(onSuccess, onFailure);
        }

        /// <summary>
        /// Projects the result by applying one of the two provided async functions,
        /// depending on whether the result is a success or a failure.
        /// </summary>
        /// <param name="onSuccess">The async action to apply if the result is a success.</param>
        /// <param name="onFailure">The async action to apply if the result is a failure.</param>
        /// <remarks>
        /// This method is useful when you want to transform a result into another value based on whether it succeeded or failed.
        /// Both match functions are required and must be non-null.
        /// </remarks>
        public async Task MatchAsync(Func<TValue, Task> onSuccess, Func<Failure, Task> onFailure)
        {
            var result = await resultTask;
            await result.MatchAsync(onSuccess, onFailure);
        }

        /// <summary>
        /// Projects the result into a value of type <typeparamref name="TOut"/> by applying one of the two provided async functions,
        /// depending on whether the result is a success or a failure.
        /// </summary>
        /// <typeparam name="TOut">The type of the value returned by the match.</typeparam>
        /// <param name="onSuccess">The async function to apply if the result is a success.</param>
        /// <param name="onFailure">The async function to apply if the result is a failure.</param>
        /// <returns>The value returned by either <paramref name="onSuccess"/> or <paramref name="onFailure"/>.</returns>
        /// <remarks>
        /// This method is useful when you want to transform a result into another value based on whether it succeeded or failed.
        /// Both match functions are required and must be non-null.
        /// </remarks>
        public async Task<TOut> MatchAsync<TOut>(Func<TValue, Task<TOut>> onSuccess, Func<Failure, Task<TOut>> onFailure)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.MatchAsync(onSuccess, onFailure).ConfigureAwait(false);
        }
    }
}
