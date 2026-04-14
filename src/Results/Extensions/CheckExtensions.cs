namespace Toarnbeike.Results.Extensions;

/// <summary>
/// Check: Quick check on an existing <see cref="Result{TValue}"/> and create a failure if the check fails.
/// </summary>
[Obsolete("Check is syntactic sugar around Bind. Use Bind() from now on, and use private methods to improve readability.")]
public static class CheckExtensions
{
    /// <param name="result">The result to check.</param>
    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    extension<TValue>(Result<TValue> result)
    {
        /// <summary>
        /// Check that the value contained in a successful result satisfies the given <paramref name="predicate"/>.
        /// </summary>    
        /// <remarks>
        /// This method allows enforcing additional invariants on the value inside a successful result.
        /// If the predicate fails, the result is transformed into a failure using the provided failure factory.
        /// </remarks>
        /// <param name="predicate">A predicate that must return <c>true</c> for the value to be considered valid.</param>
        /// <param name="onFailure">A function that returns a <see cref="Failure"/> if the predicate is <c>false</c>.</param>
        /// <returns>
        /// The original result if it was already a failure or the predicate evaluates to <c>true</c>.<br/>
        /// A new failed result with the provided failure if the predicate evaluates to <c>false</c>>.
        /// </returns>
        [Obsolete("Check is syntactic sugar around Bind. Use Bind() from now on, and use private methods to improve readability.")]
        public Result<TValue> Check(Func<TValue, bool> predicate, Func<Failure> onFailure)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            ArgumentNullException.ThrowIfNull(onFailure);

            if (!result.TryGetValue(out var value))
            {
                return result;
            }

            return predicate(value)
                ? result
                : onFailure();
        }

        /// <summary>
        /// Check that the value contained in a successful result satisfies the given <paramref name="predicate"/>.
        /// </summary>    
        /// <remarks>
        /// This method allows enforcing additional invariants on the value inside a successful result.
        /// If the predicate fails, the result is transformed into a failure using the provided failure factory.
        /// </remarks>
        /// <param name="predicate">A async predicate that must return <c>true</c> for the value to be considered valid.</param>
        /// <param name="onFailure">A function that returns a <see cref="Failure"/> if the predicate is <c>false</c>.</param>
        /// <returns>
        /// The original result if it was already a failure or the predicate evaluates to <c>true</c>.<br/>
        /// A new failed result with the provided failure if the predicate evaluates to <c>false</c>>.
        /// </returns>
        [Obsolete("Check is syntactic sugar around Bind. Use Bind() from now on, and use private methods to improve readability.")]
        public async Task<Result<TValue>> CheckAsync(Func<TValue, Task<bool>> predicate, Func<Failure> onFailure)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            ArgumentNullException.ThrowIfNull(onFailure);

            if (!result.TryGetValue(out var value))
            {
                return result;
            }

            return await predicate(value).ConfigureAwait(false)
                ? result
                : onFailure();
        }
    }

    /// <param name="resultTask">The async result to check.</param>
    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    extension<TValue>(Task<Result<TValue>> resultTask)
    {
        /// <summary>
        /// Check that the value contained in a successful result satisfies the given <paramref name="predicate"/>.
        /// </summary>    
        /// <remarks>
        /// This method allows enforcing additional invariants on the value inside a successful result.
        /// If the predicate fails, the result is transformed into a failure using the provided failure factory.
        /// </remarks>
        /// <param name="predicate">A predicate that must return <c>true</c> for the value to be considered valid.</param>
        /// <param name="onFailure">A function that returns a <see cref="Failure"/> if the predicate is <c>false</c>.</param>
        /// <returns>
        /// The original result if it was already a failure or the predicate evaluates to <c>true</c>.<br/>
        /// A new failed result with the provided failure if the predicate evaluates to <c>false</c>>.
        /// </returns>
        [Obsolete("Check is syntactic sugar around Bind. Use Bind() from now on, and use private methods to improve readability.")]
        public async Task<Result<TValue>> Check(Func<TValue, bool> predicate, Func<Failure> onFailure)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Check(predicate, onFailure);
        }

        /// <summary>
        /// Check that the value contained in a successful result satisfies the given <paramref name="predicate"/>.
        /// </summary>    
        /// <remarks>
        /// This method allows enforcing additional invariants on the value inside a successful result.
        /// If the predicate fails, the result is transformed into a failure using the provided failure factory.
        /// </remarks>
        /// <param name="predicate">A async predicate that must return <c>true</c> for the value to be considered valid.</param>
        /// <param name="onFailure">A function that returns a <see cref="Failure"/> if the predicate is <c>false</c>.</param>
        /// <returns>
        /// The original result if it was already a failure or the predicate evaluates to <c>true</c>.<br/>
        /// A new failed result with the provided failure if the predicate evaluates to <c>false</c>>.
        /// </returns>
        [Obsolete("Check is syntactic sugar around Bind. Use Bind() from now on, and use private methods to improve readability.")]
        public async Task<Result<TValue>> CheckAsync(Func<TValue, Task<bool>> predicate, Func<Failure> onFailure)
        {
            var result = await resultTask.ConfigureAwait(false);
            return await result.CheckAsync(predicate, onFailure).ConfigureAwait(false);
        }
    }
}