namespace Toarnbeike.Results.Collections;

public static class SplitExtensions
{
    /// <summary>
    /// Splits a collection of <see cref="Result{TValue}"/> into successful values and failures.
    /// </summary>
    /// <param name="results">The collection of <see cref="Result{TValue}"/> instances to evaluate. Cannot be <c>null</c>.</param>
    /// <typeparam name="TValue">The type of the success values.</typeparam>
    /// <returns>
    /// A tuple containing:
    /// <list type="bullet">
    ///   <item><c>successes</c>: The values from all successful results.</item>
    ///   <item><c>failures</c>: The failure objects from all failed results.</item>
    /// </list>
    /// </returns>
    public static (IEnumerable<TValue> successes, IEnumerable<Failure> failures) Split<TValue>(this IEnumerable<Result<TValue>> results)
    {
        ArgumentNullException.ThrowIfNull(results);

        var successfulResults = new List<TValue>();
        var failures = new List<Failure>();
        foreach (var result in results)
        {
            if (result.Deconstruct(out var value, out var failure))
            {
                successfulResults.Add(value);
            }
            else
            {
                failures.Add(failure);
            }
        }

        return (successfulResults.AsEnumerable(), failures.AsEnumerable());
    }

    /// <summary>
    /// Splits a collection of <see cref="Result{TValue}"/> into successful values and failures.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in successful results.</typeparam>
    /// <returns>
    /// A tuple containing:
    /// <list type="bullet">
    ///   <item><c>successes</c>: The values from all successful results.</item>
    ///   <item><c>failures</c>: The failure objects from all failed results.</item>
    /// </list>
    /// </returns>
    public static async Task<(IEnumerable<TValue> successes, IEnumerable<Failure> failures)> SplitAsync<TValue>(this IEnumerable<Task<Result<TValue>>> resultTasks)
    {
        ArgumentNullException.ThrowIfNull(resultTasks);

        var results = await Task.WhenAll(resultTasks).ConfigureAwait(false);
        return results.Split();
    }
}