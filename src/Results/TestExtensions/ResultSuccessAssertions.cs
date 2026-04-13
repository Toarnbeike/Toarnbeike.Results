using System.Diagnostics;

namespace Toarnbeike.Results.TestExtensions;

/// <summary>
/// Provides assertion methods to verify that a <see cref="Result"/> or <see cref="Result{TValue}"/> represents a successful outcome.
/// These methods are intended exclusively for unit testing. Not recommended for use in production logic.
/// </summary>
public static class ResultSuccessAssertions
{
    /// <summary>
    /// Asserts that the result is a success.
    /// </summary>
    /// <param name="result">The result to verify.</param>
    /// <exception cref="ResultAssertionException">Thrown when the result is a failure.</exception>
    [DebuggerStepThrough]
    public static void ShouldBeSuccess(this Result result)
    {
        if (result.Equals(default))
        {
            throw new ResultAssertionException("Expected result to be non-null.");
        }

        if (result.TryGetFailure(out var failure))
        {

            throw new ResultAssertionException($"Expected success result, but got failure: '{failure.Message}'.");
        }
    }

    /// <summary>
    /// Asserts that the result is a success and returns the contained value.
    /// </summary>
    /// <param name="result">The result to verify.</param>
    /// <typeparam name="TValue">The expected type of the result value.</typeparam>
    /// <returns>The value contained in the successful result.</returns>
    /// <exception cref="ResultAssertionException">Thrown when the result is a failure.</exception>
    [DebuggerStepThrough]
    public static TValue ShouldBeSuccess<TValue>(this Result<TValue> result)
    {
        if (result.Equals(default))
        {
            throw new ResultAssertionException("Expected result to be non-null.");
        }

        if (!result.Deconstruct(out var actual, out var failure))
        {
            throw new ResultAssertionException($"Expected success result, but got failure: '{failure.Message}'.");
        }

        return actual;
    }

    /// <summary>
    /// Asserts that the async result is a success.
    /// </summary>
    /// <param name="resultTask">The result to verify.</param>
    /// <exception cref="ResultAssertionException">Thrown when the result is a failure.</exception>
    [DebuggerStepThrough]
    public static async Task ShouldBeSuccessAsync(this Task<Result> resultTask)
    {
        var result = await resultTask;
        result.ShouldBeSuccess();
    }

    /// <summary>
    /// Asserts that the async result is a success and returns the contained value.
    /// </summary>
    /// <param name="resultTask">The result to verify.</param>
    /// <typeparam name="TValue">The expected type of the result value.</typeparam>
    /// <returns>The value contained in the successful result.</returns>
    /// <exception cref="ResultAssertionException">Thrown when the result is a failure.</exception>
    [DebuggerStepThrough]
    public static async Task<TValue> ShouldBeSuccessAsync<TValue>(this Task<Result<TValue>> resultTask)
    {
        var result = await resultTask;
        return result.ShouldBeSuccess();
    }
}
