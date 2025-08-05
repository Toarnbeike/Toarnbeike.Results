namespace Toarnbeike.Results.Extensions.Unsafe;

/// <summary>
/// GetValueOrThrow: Get the failure of a <see cref="Result{TValue}"/> or throw an exception if it is a success.
/// </summary>
/// <remarks>
/// Intended for use when you are certain that the result is a failure, e.g. after a where clause on a collection.
/// </remarks>
public static class GetFailureOrThrowExtensions
{
    /// <summary>
    /// Gets the failure of a failing <see cref="IResult"/> or throws an exception if it is a success.
    /// </summary>
    /// <param name="result">The result to get the failure from.</param>
    /// <returns>The failure contained in the failing result.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the result is a success.</exception>
    public static Failure GetFailureOrThrow(this IResult result)
    {
        if (result.TryGetFailure(out var failure))
        {
            return failure;
        }
        throw new InvalidOperationException("Trying to get the failure of a success result. No failure available.");
    }

    /// <summary>
    /// Gets the failure of a failing <see cref="Result"/> or throws an exception if it is a success.
    /// </summary>
    /// <param name="resultTask">The async result to get the failure from.</param>
    /// <returns>The failure contained in the failing result.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the result is a success.</exception>
    public static async Task<Failure> GetFailureOrThrow(this Task<Result> resultTask)
    {
        var result = await resultTask.ConfigureAwait(false);
        return GetFailureOrThrow(result);
    }

    /// <summary>
    /// Gets the failure of a failing <see cref="Result{TValue}"/> or throws an exception if it is a success.
    /// </summary>
    /// <param name="resultTask">The async result to get the failure from.</param>
    /// <returns>The failure contained in the failing result.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the result is a success.</exception>
    public static async Task<Failure> GetFailureOrThrow<TValue>(this Task<Result<TValue>> resultTask)
    {
        var result = await resultTask.ConfigureAwait(false);
        return GetFailureOrThrow(result);
    }
}