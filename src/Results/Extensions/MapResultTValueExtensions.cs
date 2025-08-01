namespace Toarnbeike.Results.Extensions;

/// <summary>
/// Map: Map the successful value of a <see cref="Result{TIn}"/> to a <see cref="Result{TOut}"/> by 
/// providing a mapping function: <see cref="Func{TIn, TOut}"/>.
/// </summary>
public static class MapResultTValueExtensions
{
    /// <summary>
    /// Maps the value of a successful <see cref="Result{TIn}"/> to a new <see cref="Result{TOut}"/> 
    /// using the specified mapping function.
    /// </summary>
    /// <typeparam name="TIn">The type of the value in the input result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="result">The input result to transform.</param>
    /// <param name="map">
    /// A function to apply to the value if the result is successful. 
    /// If the result is a failure, the failure is preserved and <paramref name="map"/> is not called.
    /// </param>
    /// <returns>
    /// A new <see cref="Result{TOut}"/> containing the transformed value if the input result was successful,
    /// or the original failure otherwise.
    /// </returns>
    public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> map)
    {
        return result.TryGetValue(out var value, out var failure) 
            ? Result.Success(map(value)) 
            : Result<TOut>.Failure(failure);
    }

    /// <summary>
    /// Maps the value of a successful <see cref="Result{TIn}"/> to a new <see cref="Result{TOut}"/> 
    /// using the specified async mapping function.
    /// </summary>
    /// <typeparam name="TIn">The type of the value in the input result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="result">The input result to transform.</param>
    /// <param name="map">
    /// An async function to apply to the value if the result is successful. 
    /// If the result is a failure, the failure is preserved and <paramref name="map"/> is not called.
    /// </param>
    /// <returns>
    /// A new <see cref="Result{TOut}"/> containing the transformed value if the input result was successful,
    /// or the original failure otherwise.
    /// </returns>
    public static async Task<Result<TOut>> MapAsync<TIn, TOut>(this Result<TIn> result, Func<TIn, Task<TOut>> map)
    {
        return result.TryGetValue(out var value, out var failure)
            ? Result.Success(await map(value))
            : Result<TOut>.Failure(failure);
    }

    /// <summary>
    /// Maps the value of a successful <see cref="Task{Result{TIn}}"/> to a new <see cref="Result{TOut}"/> 
    /// using the specified mapping function.
    /// </summary>
    /// <typeparam name="TIn">The type of the value in the input result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="resultTask">The input result task to transform.</param>
    /// <param name="map">
    /// A function to apply to the value if the result is successful. 
    /// If the result is a failure, the failure is preserved and <paramref name="map"/> is not called.
    /// </param>
    /// <returns>
    /// A new <see cref="Result{TOut}"/> containing the transformed value if the input result was successful,
    /// or the original failure otherwise.
    /// </returns>
    public static async Task<Result<TOut>> Map<TIn, TOut>(this Task<Result<TIn>> resultTask, Func<TIn, TOut> map)
    {
        var result = await resultTask;
        return Map(result, map);
    }

    /// <summary>
    /// Maps the value of a successful <see cref="Task{Result{TIn}}"/> to a new <see cref="Result{TOut}"/> 
    /// using the specified async mapping function.
    /// </summary>
    /// <typeparam name="TIn">The type of the value in the input result.</typeparam>
    /// <typeparam name="TOut">The type of the value in the resulting result.</typeparam>
    /// <param name="resultTask">The input result task to transform.</param>
    /// <param name="map">
    /// An async function to apply to the value if the result is successful. 
    /// If the result is a failure, the failure is preserved and <paramref name="map"/> is not called.
    /// </param>
    /// <returns>
    /// A new <see cref="Result{TOut}"/> containing the transformed value if the input result was successful,
    /// or the original failure otherwise.
    /// </returns>
    public static async Task<Result<TOut>> MapAsync<TIn, TOut>(this Task<Result<TIn>> resultTask, Func<TIn, Task<TOut>> map)
    {
        var result = await resultTask;
        return await MapAsync(result, map);
    }
}
