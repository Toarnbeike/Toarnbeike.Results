namespace Toarnbeike.Results.Collections;

public static class AllSuccessExtensions
{
    /// <summary>
    /// Determines whether all results in the collection indicate success.
    /// </summary>
    /// <param name="results">The collection of <see cref="Result"/> instances to evaluate. Cannot be <c>null</c>.</param>
    /// <returns><c>true</c> if all <paramref name="results"/> are successful; otherwise, <c>false</c>.</returns>
    public static bool AllSuccess(this IEnumerable<Result> results)
    {
        ArgumentNullException.ThrowIfNull(results);
        return results.All(result => result.IsSuccess);
    }

    /// <summary>
    /// Determines whether all results in the collection indicate success.
    /// </summary>
    /// <returns><c>true</c> if all <paramref name="resultTasks"/> are successful; otherwise, <c>false</c>.</returns>
    public static async Task<bool> AllSuccessAsync(this IEnumerable<Task<Result>> resultTasks)
    {
        ArgumentNullException.ThrowIfNull(resultTasks);

        var results = await Task.WhenAll(resultTasks).ConfigureAwait(false);
        return results.AllSuccess();
    }

    /// <summary>
    /// Determines whether all results in the collection indicate success.
    /// </summary>
    /// <param name="results">The collection of <see cref="Result{TValue}"/> instances to evaluate. Cannot be <c>null</c>.</param>
    /// <typeparam name="TValue">The type of the success values.</typeparam>
    /// <returns><c>true</c> if all <paramref name="results"/> are successful; otherwise, <c>false</c>.</returns>
    public static bool AllSuccess<TValue>(this IEnumerable<Result<TValue>> results)
    {
        ArgumentNullException.ThrowIfNull(results);
        return results.All(result => result.IsSuccess);
    }

    /// <summary>
    /// Determines whether all results in the collection indicate success.
    /// </summary>
    /// <returns><c>true</c> if all <paramref name="resultTasks"/> are successful; otherwise, <c>false</c>.</returns>
    public static async Task<bool> AllSuccessAsync<TValue>(this IEnumerable<Task<Result<TValue>>> resultTasks)
    {
        ArgumentNullException.ThrowIfNull(resultTasks);

        var results = await Task.WhenAll(resultTasks).ConfigureAwait(false);
        return results.All(result => result.IsSuccess);
    }
}