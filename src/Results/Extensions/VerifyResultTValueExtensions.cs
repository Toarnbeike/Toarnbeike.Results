namespace Toarnbeike.Results.Extensions;

/// <summary>
/// Validate: Quick check on an existing <see cref="Result{TValue}"/> and create a failure if the check fails.
/// </summary>
public static class VerifyResultTValueExtensions
{
    /// <summary>
    /// Verifies that the value contained in a successful result satisfies the given <paramref name="predicate"/>.
    /// </summary>    
    /// <remarks>
    /// This method allows enforcing additional invariants on the value inside a successful result.
    /// If the predicate fails, the result is transformed into a failure using the provided failure factory.
    /// </remarks>
    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    /// <param name="result">The result to verify.</param>
    /// <param name="predicate">A predicate that must return <c>true</c> for the value to be considered valid.</param>
    /// <param name="onFailure">A function that returns a <see cref="Failure"/> if the predicate is <c>false</c>.</param>
    /// <returns>
    /// The original result if it was already a failure or the predicate evaluates to <c>true</c>.<br/>
    /// A new failed result with the provided failure if the predicate evaluates to <c>false</c>>.
    /// </returns>
    public static Result<TValue> Verify<TValue>(this Result<TValue> result, Func<TValue, bool> predicate, Func<Failure> onFailure)
    {
        if (!result.TryGetValue(out var value))
        {
            return result;
        }

        return predicate(value)
            ? result
            : onFailure();
    }

    /// <summary>
    /// Verifies that the value contained in a successful result satisfies the given <paramref name="predicate"/>.
    /// </summary>    
    /// <remarks>
    /// This method allows enforcing additional invariants on the value inside a successful result.
    /// If the predicate fails, the result is transformed into a failure using the provided failure factory.
    /// </remarks>
    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    /// <param name="result">The result to verify.</param>
    /// <param name="predicate">A async predicate that must return <c>true</c> for the value to be considered valid.</param>
    /// <param name="onFailure">A function that returns a <see cref="Failure"/> if the predicate is <c>false</c>.</param>
    /// <returns>
    /// The original result if it was already a failure or the predicate evaluates to <c>true</c>.<br/>
    /// A new failed result with the provided failure if the predicate evaluates to <c>false</c>>.
    /// </returns>
    public static async Task<Result<TValue>> VerifyAsync<TValue>(this Result<TValue> result, Func<TValue, Task<bool>> predicate, Func<Failure> onFailure)
    {
        if (!result.TryGetValue(out var value))
        {
            return result;
        }

        return await predicate(value)
            ? result
            : onFailure();
    }

    /// <summary>
    /// Verifies that the value contained in a successful result satisfies the given <paramref name="predicate"/>.
    /// </summary>    
    /// <remarks>
    /// This method allows enforcing additional invariants on the value inside a successful result.
    /// If the predicate fails, the result is transformed into a failure using the provided failure factory.
    /// </remarks>
    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    /// <param name="resultTask">The async result to verify.</param>
    /// <param name="predicate">A predicate that must return <c>true</c> for the value to be considered valid.</param>
    /// <param name="onFailure">A function that returns a <see cref="Failure"/> if the predicate is <c>false</c>.</param>
    /// <returns>
    /// The original result if it was already a failure or the predicate evaluates to <c>true</c>.<br/>
    /// A new failed result with the provided failure if the predicate evaluates to <c>false</c>>.
    /// </returns>
    public static async Task<Result<TValue>> Verify<TValue>(this Task<Result<TValue>> resultTask, Func<TValue, bool> predicate, Func<Failure> onFailure)
    {
        var result = await resultTask;
        return Verify(result, predicate, onFailure);
    }

    /// <summary>
    /// Verifies that the value contained in a successful result satisfies the given <paramref name="predicate"/>.
    /// </summary>    
    /// <remarks>
    /// This method allows enforcing additional invariants on the value inside a successful result.
    /// If the predicate fails, the result is transformed into a failure using the provided failure factory.
    /// </remarks>
    /// <typeparam name="TValue">The type of the value in the result.</typeparam>
    /// <param name="resultTask">The async result to verify.</param>
    /// <param name="predicate">A async predicate that must return <c>true</c> for the value to be considered valid.</param>
    /// <param name="onFailure">A function that returns a <see cref="Failure"/> if the predicate is <c>false</c>.</param>
    /// <returns>
    /// The original result if it was already a failure or the predicate evaluates to <c>true</c>.<br/>
    /// A new failed result with the provided failure if the predicate evaluates to <c>false</c>>.
    /// </returns>
    public static async Task<Result<TValue>> VerifyAsync<TValue>(this Task<Result<TValue>> resultTask, Func<TValue, Task<bool>> predicate, Func<Failure> onFailure)
    {
        var result = await resultTask;
        return await VerifyAsync(result, predicate, onFailure);
    }
}
