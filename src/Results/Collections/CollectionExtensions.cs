using System.Diagnostics;
using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.Collections;

public static class CollectionExtensions
{
    /// <summary>
    /// Determines whether all results in the collection indicate success.
    /// </summary>
    /// <param name="results">The collection of <see cref="IResult"/> instances to evaluate. Cannot be <c>null</c>.</param>
    /// <returns><c>true</c> if all <paramref name="results"/> are successful; otherwise, <c>false</c>.</returns>
    public static bool AllSuccess(this IEnumerable<IResult> results)
    {
        ArgumentNullException.ThrowIfNull(results);
        return results.All(result => result.IsSuccess);
    }

    /// <summary>
    /// Converts a sequence of <see cref="Result{T}"/> into a single <see cref="Result{IEnumerable{T}}"/> using a fail-fast strategy.
    /// </summary>
    /// <remarks>
    /// If any result in <paramref name="results"/> is a failure, the first failure is returned.
    /// If all results are successful, a single successful result containing all values is returned.
    /// </remarks>
    /// <typeparam name="T">The type of the success values.</typeparam>
    /// <param name="results">The collection to convert. Cannot be <c>null</c>.</param>
    /// <returns>
    /// A <see cref="Result{IEnumerable{T}}"/> containing all successful values or the first failure encountered.
    /// </returns>
    public static Result<IEnumerable<T>> Sequence<T>(this IEnumerable<Result<T>> results)
    {
        ArgumentNullException.ThrowIfNull(results);

        var successfulResults = new List<T>();
        foreach (var result in results)
        {
            if (!result.TryGetValue(out var value, out var failure))
            {
                return failure;
            }

            successfulResults.Add(value);
        }
        return successfulResults;
    }

    /// <summary>
    /// Converts a sequence of <see cref="Result{T}"/> into a single <see cref="Result{IEnumerable{T}}"/> using an aggregate strategy.
    /// </summary>
    /// <remarks>
    /// All failures in <paramref name="results"/> are collected into an <see cref="AggregateFailure"/>.
    /// If all results are successful, a single successful result containing all values is returned.
    /// </remarks>
    /// <typeparam name="T">The type of the success values.</typeparam>
    /// <param name="results">The collection to convert. Cannot be <c>null</c>.</param>
    /// <returns>
    /// A successful <see cref="Result{IEnumerable{T}}"/> if all results succeeded;
    /// otherwise, a failure with <see cref="AggregateFailure"/>.
    /// </returns>
    public static Result<IEnumerable<T>> Aggregate<T>(this IEnumerable<Result<T>> results)
    {
        ArgumentNullException.ThrowIfNull(results);

        var successfulResults = new List<T>();
        var failures = new List<Failure>();
        foreach (var result in results)
        {
            if (result.TryGetValue(out var value, out var failure))
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
            : new AggregateFailure(failures);
    }

    /// <summary>
    /// Extracts the values from all successful <see cref="Result{T}"/> instances in the collection.
    /// </summary>
    /// <remarks>
    /// If a result is marked successful but contains no value, an exception is thrown.
    /// </remarks>
    /// <typeparam name="T">The type of the value contained in each result.</typeparam>
    /// <param name="results">The collection to filter. Cannot be <c>null</c>.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of values from successful results.</returns>
    public static IEnumerable<T> SuccessValues<T>(this IEnumerable<Result<T>> results)
    {
        ArgumentNullException.ThrowIfNull(results);

        return results
            .Where(result => result.IsSuccess)
            .Select(result => result.TryGetValue(out var value) ? value : throw new UnreachableException("unreachable"));
    }

    /// <summary>
    /// Extracts the failure objects from all failed <see cref="Result{T}"/> instances in the collection.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in each result.</typeparam>
    /// <param name="results">The collection to evaluate. Cannot be <c>null</c>.</param>
    /// <returns>An <see cref="IEnumerable{Failure}"/> of failure values from failed results.</returns>
    public static IEnumerable<Failure> Failures<T>(this IEnumerable<Result<T>> results)
    {
        ArgumentNullException.ThrowIfNull(results);
        return results
            .Where(result => result.IsFailure)
            .Select(result => result.TryGetFailure(out var failure) ? failure : throw new UnreachableException("unreachable"));
    }

    /// <summary>
    /// Splits a collection of <see cref="Result{T}"/> into successful values and failures.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in successful results.</typeparam>
    /// <param name="results">The collection to split. Cannot be <c>null</c>.</param>
    /// <returns>
    /// A tuple containing:
    /// <list type="bullet">
    ///   <item><term><c>successes</c></term>: The values from all successful results.</item>
    ///   <item><term><c>failures</c></term>: The failure objects from all failed results.</item>
    /// </list>
    /// </returns>
    public static (IEnumerable<T> successes, IEnumerable<Failure> failures) Split<T>(this IEnumerable<Result<T>> results)
    {
        ArgumentNullException.ThrowIfNull(results);

        var successfulResults = new List<T>();
        var failures = new List<Failure>();
        foreach (var result in results)
        {
            if (result.TryGetValue(out var value, out var failure))
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
}