using Toarnbeike.Optional;

namespace Toarnbeike.Results.Optional;

/// <summary>
/// ToOption: Extension methods to convert <see cref="Result{T}"/> to <see cref="Option{T}"/>.
/// </summary>
public static class ToOptionExtensions
{
    /// <summary>
    /// Converts a <see cref="Result{T}"/> to an <see cref="Option{T}"/>.
    /// </summary>
    /// <remarks>
    /// This method loses the failure information from the <see cref="Result{T}"/>.
    /// If the result is successful, the value is wrapped in <c>Some</c>;
    /// otherwise, the result is <c>None</c>.
    /// </remarks>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="result">The result to convert.</param>
    /// <returns>
    /// A <see cref="Option{T}"/> with <c>Some</c> when the result is a success, 
    /// or a <c>None</c> option when the result has failed.
    /// </returns>
    public static Option<T> ToOption<T>(this Result<T> result)
    {
        return result.TryGetValue(out var value) ? Option.Some(value) : Option.None;
    }

    /// <summary>
    /// Converts a <see cref="Task{TResult}"/> containing a <see cref="Result{T}"/> to an <see cref="Option{T}"/>.
    /// </summary>
    /// <remarks>
    /// This method loses the failure information from the <see cref="Result{T}"/>.
    /// If the result is successful, the value is wrapped in <c>Some</c>;
    /// otherwise, the result is <c>None</c>.
    /// </remarks>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="resultTask">The task that produces a result to convert.</param>
    /// <returns>
    /// A <see cref="Option{T}"/> with <c>Some</c> when the result is a success, 
    /// or a <c>None</c> option when the result has failed.
    /// </returns>
    public static async Task<Option<T>> ToOption<T>(this Task<Result<T>> resultTask)
    {
        var result = await resultTask.ConfigureAwait(false);
        return ToOption(result);
    }
}