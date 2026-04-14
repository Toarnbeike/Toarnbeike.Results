namespace Toarnbeike.Results.Extensions;

/// <summary>
/// Zip: Combine the values of two successful <see cref="Result{TValue}"/> into a tuple result.
/// </summary>
[Obsolete("Use Combine() or private methods to either return the tuple or avoid having to use tuples at all.")]
public static class ZipExtensions
{
    /// <param name="first">The first result.</param>
    /// <typeparam name="T1">The type of the value in the first result.</typeparam>
    extension<T1>(Result<T1> first)
    {
        /// <summary>
        /// Combines two successful <see cref="Result{T}"/> instances into a single result containing a tuple of their values.
        /// </summary>
        /// <typeparam name="T2">The type of the value in the second result.</typeparam>
        /// <param name="second">The function to generate the second result from the first's result's value.</param>
        /// <returns>
        /// A successful result containing a tuple <c>(T1, T2)</c> if both results are successful.
        /// If either result failed, the failure is propagated and the other result is not evaluated.
        /// </returns>
        [Obsolete("Use Combine() or private methods to either return the tuple or avoid having to use tuples at all.")]
        public Result<(T1, T2)> Zip<T2>(Func<T1, Result<T2>> second)
        {
            ArgumentNullException.ThrowIfNull(second);

            if (!first.TryGetValue(out var firstValue, out var firstFailure))
            {
                return Result<(T1, T2)>.Failure(firstFailure);
            }

            var secondResult = second(firstValue);
            if (!secondResult.TryGetValue(out var secondValue, out var secondFailure))
            {
                return Result<(T1, T2)>.Failure(secondFailure);
            }

            return (firstValue, secondValue);
        }

        /// <summary>
        /// Combines two successful <see cref="Result{T}"/> instances into a single result containing a tuple of their values.
        /// </summary>
        /// <typeparam name="T2">The type of the value in the second result.</typeparam>
        /// <typeparam name="TResult">The type of the resulting tuple, that is named using the <paramref name="projector"/></typeparam>
        /// <param name="second">The function to generate the second result from the first's result's value.</param>
        /// <param name="projector">Projection to give the parts of the resulting tuple meaningful names.</param>
        /// <returns>
        /// A successful result containing a tuple <c>(T1, T2)</c> if both results are successful.
        /// If either result failed, the failure is propagated and the other result is not evaluated.
        /// </returns>
        [Obsolete("Use Combine() or private methods to either return the tuple or avoid having to use tuples at all.")]
        public Result<TResult> Zip<T2, TResult>(Func<T1, Result<T2>> second, Func<T1, T2, TResult> projector)
        {
            ArgumentNullException.ThrowIfNull(second);
            ArgumentNullException.ThrowIfNull(projector);

            if (!first.TryGetValue(out var firstValue, out var firstFailure))
            {
                return Result<TResult>.Failure(firstFailure);
            }

            var secondResult = second(firstValue);
            if (!secondResult.TryGetValue(out var secondValue, out var secondFailure))
            {
                return Result<TResult>.Failure(secondFailure);
            }

            return projector(firstValue, secondValue);
        }

        /// <summary>
        /// Combines two successful <see cref="Result{T}"/> instances into a single result containing a tuple of their values.
        /// </summary>
        /// <typeparam name="T2">The type of the value in the second result.</typeparam>
        /// <param name="secondTask">The async function to generate the second result from the first's result's value.</param>
        /// <returns>
        /// A successful result containing a tuple <c>(T1, T2)</c> if both results are successful.
        /// If either result failed, the failure is propagated and the other result is not evaluated.
        /// </returns>
        [Obsolete("Use Combine() or private methods to either return the tuple or avoid having to use tuples at all.")]
        public async Task<Result<(T1, T2)>> ZipAsync<T2>(Func<T1, Task<Result<T2>>> secondTask)
        {
            ArgumentNullException.ThrowIfNull(secondTask);

            if (!first.TryGetValue(out var firstValue, out var firstFailure))
            {
                return Result<(T1, T2)>.Failure(firstFailure);
            }

            var secondResult = await secondTask(firstValue).ConfigureAwait(false);
            if (!secondResult.TryGetValue(out var secondValue, out var secondFailure))
            {
                return Result<(T1, T2)>.Failure(secondFailure);
            }

            return (firstValue, secondValue);
        }

        /// <summary>
        /// Combines two successful <see cref="Result{T}"/> instances into a single result containing a tuple of their values.
        /// </summary>
        /// <typeparam name="T2">The type of the value in the second result.</typeparam>
        /// <typeparam name="TResult">The type of the resulting tuple, that is named using the <paramref name="projector"/></typeparam>
        /// <param name="secondTask">The async function to generate the second result from the first's result's value.</param>
        /// <param name="projector">Projection to give the parts of the resulting tuple meaningful names.</param>
        /// <returns>
        /// A successful result containing a tuple <c>(T1, T2)</c> if both results are successful.
        /// If either result failed, the failure is propagated and the other result is not evaluated.
        /// </returns>
        [Obsolete("Use Combine() or private methods to either return the tuple or avoid having to use tuples at all.")]
        public async Task<Result<TResult>> ZipAsync<T2, TResult>(Func<T1, Task<Result<T2>>> secondTask, Func<T1, T2, TResult> projector)
        {
            ArgumentNullException.ThrowIfNull(secondTask);
            ArgumentNullException.ThrowIfNull(projector);

            if (!first.TryGetValue(out var firstValue, out var firstFailure))
            {
                return Result<TResult>.Failure(firstFailure);
            }

            var secondResult = await secondTask(firstValue).ConfigureAwait(false);
            if (!secondResult.TryGetValue(out var secondValue, out var secondFailure))
            {
                return Result<TResult>.Failure(secondFailure);
            }

            return projector(firstValue, secondValue);
        }
    }

    /// <param name="firstTask">The first async result.</param>
    /// <typeparam name="T1">The type of the value in the first result.</typeparam>
    extension<T1>(Task<Result<T1>> firstTask)
    {
        /// <summary>
        /// Combines two successful <see cref="Result{T}"/> instances into a single result containing a tuple of their values.
        /// </summary>
        /// <typeparam name="T2">The type of the value in the second result.</typeparam>
        /// <param name="second">The function to generate the second result from the first's result's value.</param>
        /// <returns>
        /// A successful result containing a tuple <c>(T1, T2)</c> if both results are successful.
        /// If either result failed, the failure is propagated and the other result is not evaluated.
        /// </returns>
        [Obsolete("Use map, bind or private methods to either return the tuple or avoid having to use tuples at all.")]
        public async Task<Result<(T1, T2)>> Zip<T2>(Func<T1, Result<T2>> second)
        {
            ArgumentNullException.ThrowIfNull(second);

            var first = await firstTask.ConfigureAwait(false);
            return first.Zip(second);
        }

        /// <summary>
        /// Combines two successful <see cref="Result{T}"/> instances into a single result containing a tuple of their values.
        /// </summary>
        /// <typeparam name="T2">The type of the value in the second result.</typeparam>
        /// <typeparam name="TResult">The type of the resulting tuple, that is named using the <paramref name="projector"/></typeparam>
        /// <param name="second">The function to generate the second result from the first's result's value.</param>
        /// <param name="projector">Projection to give the parts of the resulting tuple meaningful names.</param>
        /// <returns>
        /// A successful result containing a tuple <c>(T1, T2)</c> if both results are successful.
        /// If either result failed, the failure is propagated and the other result is not evaluated.
        /// </returns>
        [Obsolete("Use map, bind or private methods to either return the tuple or avoid having to use tuples at all.")]
        public async Task<Result<TResult>> Zip<T2, TResult>(Func<T1, Result<T2>> second, Func<T1, T2, TResult> projector)
        {
            ArgumentNullException.ThrowIfNull(second);
            ArgumentNullException.ThrowIfNull(projector);

            var first = await firstTask.ConfigureAwait(false);
            return first.Zip(second, projector);
        }

        /// <summary>
        /// Combines two successful <see cref="Result{T}"/> instances into a single result containing a tuple of their values.
        /// </summary>
        /// <typeparam name="T2">The type of the value in the second result.</typeparam>
        /// <param name="secondTask">The async function to generate the second result from the first's result's value.</param>
        /// <returns>
        /// A successful result containing a tuple <c>(T1, T2)</c> if both results are successful.
        /// If either result failed, the failure is propagated and the other result is not evaluated.
        /// </returns>
        [Obsolete("Use map, bind or private methods to either return the tuple or avoid having to use tuples at all.")]
        public async Task<Result<(T1, T2)>> ZipAsync<T2>(Func<T1, Task<Result<T2>>> secondTask)
        {
            var first = await firstTask.ConfigureAwait(false);
            return await first.ZipAsync(secondTask).ConfigureAwait(false);
        }

        /// <summary>
        /// Combines two successful <see cref="Result{T}"/> instances into a single result containing a tuple of their values.
        /// </summary>
        /// <typeparam name="T2">The type of the value in the second result.</typeparam>
        /// <typeparam name="TResult">The type of the resulting tuple, that is named using the <paramref name="projector"/></typeparam>
        /// <param name="secondTask">The async function to generate the second result from the first's result's value.</param>
        /// <param name="projector">Projection to give the parts of the resulting tuple meaningful names.</param>
        /// <returns>
        /// A successful result containing a tuple <c>(T1, T2)</c> if both results are successful.
        /// If either result failed, the failure is propagated and the other result is not evaluated.
        /// </returns>
        [Obsolete("Use map, bind or private methods to either return the tuple or avoid having to use tuples at all.")]
        public async Task<Result<TResult>> ZipAsync<T2, TResult>(Func<T1, Task<Result<T2>>> secondTask, Func<T1, T2, TResult> projector)
        {
            var first = await firstTask.ConfigureAwait(false);
            return await first.ZipAsync(secondTask, projector).ConfigureAwait(false);
        }
    }
}