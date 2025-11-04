namespace Toarnbeike.Results.Extensions;

/// <summary>
/// Zip: Combine the values of two successful <see cref="Result{TValue}"/> into a tuple result.
/// </summary>
public static class ZipExtensions
{
    /// <summary>
    /// Combines two successful <see cref="Result{T}"/> instances into a single result containing a tuple of their values.
    /// </summary>
    /// <typeparam name="T1">The type of the value in the first result.</typeparam>
    /// <typeparam name="T2">The type of the value in the second result.</typeparam>
    /// <param name="first">The first result.</param>
    /// <param name="second">The function to generate the second result from the first's result's value.</param>
    /// <returns>
    /// A successful result containing a tuple <c>(T1, T2)</c> if both results are successful.
    /// If either result failed, the failure is propagated and the other result is not evaluated.
    /// </returns>
    public static Result<(T1, T2)> Zip<T1, T2>(this Result<T1> first, Func<T1, Result<T2>> second)
    {
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
    /// <typeparam name="T1">The type of the value in the first result.</typeparam>
    /// <typeparam name="T2">The type of the value in the second result.</typeparam>
    /// <typeparam name="TResult">The type of the resulting tuple, that is named using the <paramref name="projector"/></typeparam>
    /// <param name="first">The first result.</param>
    /// <param name="second">The function to generate the second result from the first's result's value.</param>
    /// <param name="projector">Projection to give the parts of the resulting tuple meaningful names.</param>
    /// <returns>
    /// A successful result containing a tuple <c>(T1, T2)</c> if both results are successful.
    /// If either result failed, the failure is propagated and the other result is not evaluated.
    /// </returns>
    public static Result<TResult> Zip<T1, T2, TResult>(this Result<T1> first, Func<T1, Result<T2>> second, Func<T1, T2, TResult> projector)
    {
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
    /// <typeparam name="T1">The type of the value in the first result.</typeparam>
    /// <typeparam name="T2">The type of the value in the second result.</typeparam>
    /// <param name="first">The first result.</param>
    /// <param name="secondTask">The async function to generate the second result from the first's result's value.</param>
    /// <returns>
    /// A successful result containing a tuple <c>(T1, T2)</c> if both results are successful.
    /// If either result failed, the failure is propagated and the other result is not evaluated.
    /// </returns>
    public static async Task<Result<(T1, T2)>> ZipAsync<T1, T2>(this Result<T1> first, Func<T1, Task<Result<T2>>> secondTask)
    {
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
    /// <typeparam name="T1">The type of the value in the first result.</typeparam>
    /// <typeparam name="T2">The type of the value in the second result.</typeparam>
    /// <typeparam name="TResult">The type of the resulting tuple, that is named using the <paramref name="projector"/></typeparam>
    /// <param name="first">The first result.</param>
    /// <param name="secondTask">The async function to generate the second result from the first's result's value.</param>
    /// <param name="projector">Projection to give the parts of the resulting tuple meaningful names.</param>
    /// <returns>
    /// A successful result containing a tuple <c>(T1, T2)</c> if both results are successful.
    /// If either result failed, the failure is propagated and the other result is not evaluated.
    /// </returns>
    public static async Task<Result<TResult>> ZipAsync<T1, T2, TResult>(this Result<T1> first, Func<T1, Task<Result<T2>>> secondTask, Func<T1, T2, TResult> projector)
    {
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

    /// <summary>
    /// Combines two successful <see cref="Result{T}"/> instances into a single result containing a tuple of their values.
    /// </summary>
    /// <typeparam name="T1">The type of the value in the first result.</typeparam>
    /// <typeparam name="T2">The type of the value in the second result.</typeparam>
    /// <param name="firstTask">The first async result.</param>
    /// <param name="second">The function to generate the second result from the first's result's value.</param>
    /// <returns>
    /// A successful result containing a tuple <c>(T1, T2)</c> if both results are successful.
    /// If either result failed, the failure is propagated and the other result is not evaluated.
    /// </returns>
    public static async Task<Result<(T1, T2)>> Zip<T1, T2>(this Task<Result<T1>> firstTask, Func<T1, Result<T2>> second)
    {
        var first = await firstTask.ConfigureAwait(false);
        return Zip(first, second);
    }

    /// <summary>
    /// Combines two successful <see cref="Result{T}"/> instances into a single result containing a tuple of their values.
    /// </summary>
    /// <typeparam name="T1">The type of the value in the first result.</typeparam>
    /// <typeparam name="T2">The type of the value in the second result.</typeparam>
    /// <typeparam name="TResult">The type of the resulting tuple, that is named using the <paramref name="projector"/></typeparam>
    /// <param name="firstTask">The first async result.</param>
    /// <param name="second">The function to generate the second result from the first's result's value.</param>
    /// <param name="projector">Projection to give the parts of the resulting tuple meaningful names.</param>
    /// <returns>
    /// A successful result containing a tuple <c>(T1, T2)</c> if both results are successful.
    /// If either result failed, the failure is propagated and the other result is not evaluated.
    /// </returns>
    public static async Task<Result<TResult>> Zip<T1, T2, TResult>(this Task<Result<T1>> firstTask, Func<T1, Result<T2>> second, Func<T1, T2, TResult> projector)
    {
        var first = await firstTask.ConfigureAwait(false);
        return Zip(first, second, projector);
    }

    /// <summary>
    /// Combines two successful <see cref="Result{T}"/> instances into a single result containing a tuple of their values.
    /// </summary>
    /// <typeparam name="T1">The type of the value in the first result.</typeparam>
    /// <typeparam name="T2">The type of the value in the second result.</typeparam>
    /// <param name="firstTask">The first async result.</param>
    /// <param name="secondTask">The async function to generate the second result from the first's result's value.</param>
    /// <returns>
    /// A successful result containing a tuple <c>(T1, T2)</c> if both results are successful.
    /// If either result failed, the failure is propagated and the other result is not evaluated.
    /// </returns>
    public static async Task<Result<(T1, T2)>> ZipAsync<T1, T2>(this Task<Result<T1>> firstTask, Func<T1, Task<Result<T2>>> secondTask)
    {
        var first = await firstTask.ConfigureAwait(false);
        return await ZipAsync(first, secondTask).ConfigureAwait(false);
    }

    /// <summary>
    /// Combines two successful <see cref="Result{T}"/> instances into a single result containing a tuple of their values.
    /// </summary>
    /// <typeparam name="T1">The type of the value in the first result.</typeparam>
    /// <typeparam name="T2">The type of the value in the second result.</typeparam>
    /// <typeparam name="TResult">The type of the resulting tuple, that is named using the <paramref name="projector"/></typeparam>
    /// <param name="firstTask">The first async result.</param>
    /// <param name="secondTask">The async function to generate the second result from the first's result's value.</param>
    /// <param name="projector">Projection to give the parts of the resulting tuple meaningful names.</param>
    /// <returns>
    /// A successful result containing a tuple <c>(T1, T2)</c> if both results are successful.
    /// If either result failed, the failure is propagated and the other result is not evaluated.
    /// </returns>
    public static async Task<Result<TResult>> ZipAsync<T1, T2, TResult>(this Task<Result<T1>> firstTask, Func<T1, Task<Result<T2>>> secondTask, Func<T1, T2, TResult> projector)
    {
        var first = await firstTask.ConfigureAwait(false);
        return await ZipAsync(first, secondTask, projector).ConfigureAwait(false);
    }
}