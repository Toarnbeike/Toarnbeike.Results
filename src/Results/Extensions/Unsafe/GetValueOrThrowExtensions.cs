namespace Toarnbeike.Results.Extensions.Unsafe;

/// <summary>
/// GetValueOrThrow: Get the value of a <see cref="Result{TValue}"/> or throw an exception if it is a failure.
/// </summary>
/// <remarks>
/// Intended for use when you are certain that the result is a success, e.g. after a where clause on a collection.
/// </remarks>
public static class GetValueOrThrowExtensions
{
    /// <summary>
    /// Gets the value of a successful <see cref="Result{TValue}"/> or throws an exception if it is a failure.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    /// <param name="result">The result to get the value from.</param>
    /// <returns>The value contained in the successful result.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the result is a failure.</exception>
    public static TValue GetValueOrThrow<TValue>(this Result<TValue> result)
    {
        if (result.TryGetValue(out var value, out var failure))
        {
            return value;
        }
        throw new InvalidOperationException($"Trying to get the value of a failure result. Failure: '{failure.Message}'.");
    }

    /// <summary>
    /// Gets the value of a successful <see cref="Result{TValue}"/> or throws an exception if it is a failure.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    /// <param name="resultTask">The async result to get the value from.</param>
    /// <returns>The value contained in the successful result.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the result is a failure.</exception>
    public static async Task<TValue> GetValueOrThrow<TValue>(this Task<Result<TValue>> resultTask)
    {
        var result = await resultTask.ConfigureAwait(false);
        return GetValueOrThrow(result);
    }
}