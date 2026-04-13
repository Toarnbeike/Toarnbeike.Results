namespace Toarnbeike.Results.Collections;

public static class FailuresExtensions
{
    /// <summary>
    /// Extracts the failure objects from all failed <see cref="Result"/> instances in the collection.
    /// </summary>
    /// <param name="results">The collection of <see cref="Result"/> instances to evaluate. Cannot be <c>null</c>.</param>
    /// <returns>An <see cref="IEnumerable{Failure}"/> of failure values from failed results.</returns>
    public static IEnumerable<Failure> Failures(this IEnumerable<Result> results)
    {
        ArgumentNullException.ThrowIfNull(results);
        foreach (var result in results)
        {
            if (result.TryGetFailure(out var failure))
            {
                yield return failure!;
            }
        }
    }

    /// <summary>
    /// Extracts the failure objects from all failed <see cref="Result"/> instances in the collection.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{Failure}"/> of failure values from failed results.</returns>
    public static async Task<IEnumerable<Failure>> FailuresAsync(this IEnumerable<Task<Result>> resultTasks)
    {
        ArgumentNullException.ThrowIfNull(resultTasks);

        var results = await Task.WhenAll(resultTasks).ConfigureAwait(false);
        return results.Failures();
    }

    /// <summary>
    /// Extracts the failure objects from all failed <see cref="Result"/> instances in the collection.
    /// </summary>
    /// <param name="results">The collection of <see cref="Result{TValue}"/> instances to evaluate. Cannot be <c>null</c>.</param>
    /// <typeparam name="TValue">The type of the success values.</typeparam>
    /// <returns>An <see cref="IEnumerable{Failure}"/> of failure values from failed results.</returns>
    public static IEnumerable<Failure> Failures<TValue>(this IEnumerable<Result<TValue>> results)
    {
        ArgumentNullException.ThrowIfNull(results);
        foreach (var result in results)
        {
            if (result.TryGetFailure(out var failure))
            {
                yield return failure!;
            }
        }
    }

    /// <summary>
    /// Extracts the failure objects from all failed <see cref="Result{T}"/> instances in the collection.
    /// </summary>
    /// <remarks>
    /// If a result is marked successful but contains no value, an exception is thrown.
    /// </remarks>
    /// <typeparam name="TValue">The type of the success values.</typeparam>
    /// <returns>An <see cref="IEnumerable{Failure}"/> of values from successful results.</returns>
    public static async Task<IEnumerable<Failure>> FailuresAsync<TValue>(this IEnumerable<Task<Result<TValue>>> resultTasks)
    {
        ArgumentNullException.ThrowIfNull(resultTasks);

        var results = await Task.WhenAll(resultTasks).ConfigureAwait(false);
        return results.Failures();
    }
}