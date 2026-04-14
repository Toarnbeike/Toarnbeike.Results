using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.Collections;

public static class AggregateExtensions
{
    /// <summary>
    /// Converts a sequence of <see cref="Result"/> into a single <see cref="Result"/> using an aggregate strategy.
    /// </summary>
    /// <param name="results">The collection of <see cref="Result"/> instances to evaluate. Cannot be <c>null</c>.</param>
    /// <remarks>
    /// All failures in <paramref name="results"/> are collected into an <see cref="AggregateFailureSummary"/>.
    /// If all results are successful, a single success result is returned.
    /// </remarks>
    /// <returns>
    /// A successful <see cref="Result"/> if all results succeeded;
    /// otherwise, a failure with <see cref="AggregateFailureSummary"/>.
    /// </returns>
    public static Result Aggregate(this IEnumerable<Result> results)
    {
        ArgumentNullException.ThrowIfNull(results);
        var failures = new List<Failure>();
        foreach (var result in results)
        {
            if (result.TryGetFailure(out var failure))
            {
                failures.Add(failure);
            }
        }

        return failures.Count == 0
            ? Result.Success()
            : new AggregateFailureSummary(failures);
    }

    /// <summary>
    /// Converts a sequence of async <see cref="Result"/> into a single <see cref="Result"/> using an aggregate strategy.
    /// </summary>
    /// <remarks>
    /// All failures in <paramref name="resultTasks"/> are collected into an <see cref="AggregateFailureSummary"/>.
    /// If all results are successful, a single successful result is returned.
    /// </remarks>
    /// <returns>
    /// A successful <see cref="Result"/> if all results succeeded;
    /// otherwise, a failure result with an <see cref="AggregateFailureSummary"/>.
    /// </returns>
    public static async Task<Result> AggregateAsync(this IEnumerable<Task<Result>> resultTasks)
    {
        ArgumentNullException.ThrowIfNull(resultTasks);

        var results = await Task.WhenAll(resultTasks).ConfigureAwait(false);
        return results.Aggregate();
    }

    /// <summary>
    /// Converts a sequence of <see cref="Result{T}"/> into a single <see cref="Result{IEnumerable}"/> using an aggregate strategy.
    /// </summary>
    /// <param name="results">The collection of <see cref="Result{TValue}"/> instances to evaluate. Cannot be <c>null</c>.</param>
    /// <typeparam name="TValue">The type of the success values.</typeparam>
    /// <remarks>
    /// All failures in <paramref name="results"/> are collected into an <see cref="AggregateFailureSummary"/>.
    /// If all results are successful, a single successful result containing all values is returned.
    /// </remarks>
    /// <returns>
    /// A successful <see cref="Result{IEnumerable}"/> if all results succeeded;
    /// otherwise, a failure with <see cref="AggregateFailureSummary"/>.
    /// </returns>
    public static Result<IEnumerable<TValue>> Aggregate<TValue>(this IEnumerable<Result<TValue>> results)
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

        return failures.Count == 0
            ? Result.Success(successfulResults.AsEnumerable())
            : new AggregateFailureSummary(failures);
    }

    /// <summary>
    /// Converts a sequence of <see cref="Result{T}"/> into a single <see cref="Result{IEnumerable}"/> using an aggregate strategy.
    /// </summary>
    /// <remarks>
    /// All failures in <paramref name="resultTasks"/> are collected into an <see cref="AggregateFailureSummary"/>.
    /// If all results are successful, a single successful result is returned.
    /// </remarks>
    /// <returns>
    /// A successful <see cref="Result{IEnumerable}"/> if all results succeeded;
    /// otherwise, a failure result with an <see cref="AggregateFailureSummary"/>.
    /// </returns>
    public static async Task<Result<IEnumerable<TValue>>> AggregateAsync<TValue>(this IEnumerable<Task<Result<TValue>>> resultTasks)
    {
        ArgumentNullException.ThrowIfNull(resultTasks);

        var results = await Task.WhenAll(resultTasks).ConfigureAwait(false);
        return results.Aggregate();
    }
}