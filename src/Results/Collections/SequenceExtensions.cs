namespace Toarnbeike.Results.Collections;

public static class SequenceExtensions
{
    /// <summary>
    /// Converts a sequence of <see cref="Result{TValue}"/> into a single <see cref="Result{IEnumerable}"/> using a fail-fast strategy.
    /// </summary>
    /// <param name="results">The collection of <see cref="Result{TValue}"/> instances to evaluate. Cannot be <c>null</c>.</param>
    /// <typeparam name="TValue">The type of the success values.</typeparam>
    /// <remarks>
    /// If any result in <paramref name="results"/> is a failure, the first failure is returned.
    /// If all results are successful, a single successful result containing all values is returned.
    /// </remarks>
    /// <returns>
    /// A <see cref="Result{IEnumerable}"/> containing all successful values or the first failure encountered.
    /// </returns>
    public static Result<IEnumerable<TValue>> Sequence<TValue>(this IEnumerable<Result<TValue>> results)
    {
        ArgumentNullException.ThrowIfNull(results);

        var successfulResults = new List<TValue>();
        foreach (var result in results)
        {
            if (!result.Deconstruct(out var value, out var failure))
            {
                return failure;
            }

            successfulResults.Add(value);
        }
        return successfulResults;
    }

    /// <summary>
    /// Converts a sequence of <see cref="Result{TValue}"/> into a single <see cref="Result{IEnumerable}"/> using a fail-fast strategy.
    /// </summary>
    /// <remarks>
    /// If any result in <paramref name="resultTasks"/> is a failure, the first failure is returned.
    /// If all results are successful, a single successful result containing all values is returned.
    /// </remarks>
    /// <typeparam name="TValue">The type of the success values.</typeparam>
    /// <returns>
    /// A <see cref="Result{IEnumerable}"/> containing all successful values or the first failure encountered.
    /// </returns>
    public static async Task<Result<IEnumerable<TValue>>> SequenceAsync<TValue>(this IEnumerable<Task<Result<TValue>>> resultTasks)
    {
        ArgumentNullException.ThrowIfNull(resultTasks);

        var results = await Task.WhenAll(resultTasks).ConfigureAwait(false);
        return results.Sequence();
    }
}