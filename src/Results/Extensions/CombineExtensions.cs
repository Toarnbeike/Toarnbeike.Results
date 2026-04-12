namespace Toarnbeike.Results.Extensions;

/// <summary>
/// Combine: Combine the pipelines of two instances of <see cref="Result{TIn}" />
/// to a new <see cref="Result{TOut}"/> using a function.
/// </summary>
public static class CombineExtensions
{
    /// <param name="first"> The first result to combine.</param>
    extension<T1>(Result<T1> first)
    {
        /// <summary>
        /// Maps the values of two successful <see cref="Result{T}"/> instances to a new <see cref="Result{TOut}"/>>
        /// using the provided mapping function.
        /// </summary>
        /// <typeparam name="T2">The type of the second result value to combine.</typeparam>
        /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
        /// <param name="second">The second result to combine with this result.</param>
        /// <param name="map">
        /// A function to apply to the values if both results are successful. 
        /// If any result is a failure, the failure is preserved and <paramref name="map"/> is not called.
        /// </param>
        /// <returns>
        /// A new <see cref="Result{TOut}"/> containing the transformed values if both input results were successful,
        /// or the original failure otherwise.
        /// </returns>
        public Result<TOut> Combine<T2, TOut>(Result<T2> second, Func<T1, T2, TOut> map)
        {
            ArgumentNullException.ThrowIfNull(map);

            return first.Deconstruct(out var value1, out var failure) && second.Deconstruct(out var value2, out failure)
                ? map(value1, value2)
                : failure;
        }

        /// <summary>
        /// Maps the values of two successful <see cref="Result{T}"/> instances to a new <see cref="Result{TOut}"/>>
        /// using the provided mapping function.
        /// </summary>
        /// <typeparam name="T2">The type of the second result value to combine.</typeparam>
        /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
        /// <param name="second">The second result to combine with this result.</param>
        /// <param name="map">
        /// A function to apply to the values if both results are successful. 
        /// If any result is a failure, the failure is preserved and <paramref name="map"/> is not called.
        /// </param>
        /// <returns>
        /// A new <see cref="Result{TOut}"/> containing the transformed values if both input results were successful,
        /// or the original failure otherwise.
        /// </returns>
        public async Task<Result<TOut>> CombineAsync<T2, TOut>(Result<T2> second, Func<T1, T2, Task<TOut>> map)
        {
            ArgumentNullException.ThrowIfNull(map);

            return first.Deconstruct(out var value1, out var failure) && second.Deconstruct(out var value2, out failure)
                ?await map(value1, value2)
                : failure;
        }
    }

    /// <param name="firstTask"> The first result to combine.</param>
    extension<T1>(Task<Result<T1>> firstTask)
    {
        /// <summary>
        /// Maps the values of two successful <see cref="Result{T}"/> instances to a new <see cref="Result{TOut}"/>>
        /// using the provided mapping function.
        /// </summary>
        /// <typeparam name="T2">The type of the second result value to combine.</typeparam>
        /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
        /// <param name="secondTask">The other result Task to combine with this result.</param>
        /// <param name="map">
        /// A function to apply to the values if both results are successful. 
        /// If any result is a failure, the failure is preserved and <paramref name="map"/> is not called.
        /// </param>
        /// <returns>
        /// A new <see cref="Result{TOut}"/> containing the transformed values if both input results were successful,
        /// or the original failure otherwise.
        /// </returns>
        public async Task<Result<TOut>> Combine<T2, TOut>(Task<Result<T2>> secondTask, Func<T1, T2, TOut> map)
        {
            var result = await firstTask.ConfigureAwait(false);
            var second = await secondTask.ConfigureAwait(false);
            return result.Combine(second, map);
        }

        /// <summary>
        /// Maps the values of two successful <see cref="Result{T}"/> instances to a new <see cref="Result{TOut}"/>>
        /// using the provided mapping function.
        /// </summary>
        /// <typeparam name="T2">The type of the second result value to combine.</typeparam>
        /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
        /// <param name="secondTask">The other result Task to combine with this result.</param>
        /// <param name="map">
        /// A function to apply to the values if both results are successful. 
        /// If any result is a failure, the failure is preserved and <paramref name="map"/> is not called.
        /// </param>
        /// <returns>
        /// A new <see cref="Result{TOut}"/> containing the transformed values if both input results were successful,
        /// or the original failure otherwise.
        /// </returns>
        public async Task<Result<TOut>> CombineAsync<T2, TOut>(Task<Result<T2>> secondTask, Func<T1, T2, Task<TOut>> map)
        {
            var result = await firstTask.ConfigureAwait(false);
            var second = await secondTask.ConfigureAwait(false);
            return await result.CombineAsync(second, map);
        }
    }
}