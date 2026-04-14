using Toarnbeike.Results.Extensions.Unsafe;

namespace Toarnbeike.Results.Collections;

public static class SuccessValuesExtensions
{
    /// <summary>
    /// Extracts the values from all successful <see cref="Result{TValue}"/> instances in the collection.
    /// </summary>
    /// <param name="results">The collection of <see cref="Result{TValue}"/> instances to evaluate. Cannot be <c>null</c>.</param>
    /// <typeparam name="TValue">The type of the success values.</typeparam>
    /// <remarks>
    /// If a result is marked successful but contains no value, an exception is thrown.
    /// </remarks>
    /// <returns>An <see cref="IEnumerable{TValue}"/> of values from successful results.</returns>
    public static IEnumerable<TValue> SuccessValues<TValue>(this IEnumerable<Result<TValue>> results)
    {
        ArgumentNullException.ThrowIfNull(results);

        return results
            .Where(result => result.IsSuccess)
            .Select(result => result.GetValueOrThrow<TValue>());
    }

    /// <summary>
    /// Extracts the values from all successful <see cref="Result{TValue}"/> instances in the collection.
    /// </summary>
    /// <remarks>
    /// If a result is marked successful but contains no value, an exception is thrown.
    /// </remarks>
    /// <typeparam name="TValue">The type of the value contained in each result.</typeparam>
    /// <returns>An <see cref="IEnumerable{TValue}"/> of values from successful results.</returns>
    public static async Task<IEnumerable<TValue>> SuccessValuesAsync<TValue>(this IEnumerable<Task<Result<TValue>>> resultTasks)
    {
        ArgumentNullException.ThrowIfNull(resultTasks);

        var results = await Task.WhenAll(resultTasks).ConfigureAwait(false);
        return results.SuccessValues();
    }
}