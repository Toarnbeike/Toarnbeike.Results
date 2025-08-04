namespace Toarnbeike.Results.TestHelpers;

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
    public static void ShouldBeSuccess(this Result result)
    {
        if (result is null)
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
    /// <typeparam name="TValue">The expected type of the result value.</typeparam>
    /// <param name="result">The result to verify.</param>
    /// <returns>The value contained in the successful result.</returns>
    /// <exception cref="ResultAssertionException">Thrown when the result is a failure.</exception>
    public static TValue ShouldBeSuccess<TValue>(this Result<TValue> result)
    {
        if (result is null)
        {
            throw new ResultAssertionException("Expected result to be non-null.");
        }

        if (!result.TryGetValue(out var actual, out var failure))
        {
            throw new ResultAssertionException($"Expected success result, but got failure: '{failure.Message}'.");
        }

        return actual;
    }

    /// <summary>
    /// Asserts that the result is a success and that the contained value equals the expected value.
    /// </summary>
    /// <typeparam name="TValue">The expected type of the result value.</typeparam>
    /// <param name="result">The result to verify.</param>
    /// <param name="expected">The expected value to compare with the result's value.</param>
    /// <param name="customMessage">Optional custom message for assertion failure.</param>
    /// <returns>The value contained in the successful result.</returns>
    /// <exception cref="ResultAssertionException">Thrown when the result is a failure or the value does not match.</exception>
    public static TValue ShouldBeSuccessWithValue<TValue>(this Result<TValue> result, TValue expected, string? customMessage = null)
    {
        var actual = result.ShouldBeSuccess();

        if (!Equals(actual, expected))
        {
            throw new ResultAssertionException(customMessage ?? $"Expected success result with value '{expected}', but got '{actual}'.");
        }

        return actual;
    }

    /// <summary>
    /// Asserts that the result is a success and that the contained value satisfies the given predicate.
    /// </summary>
    /// <typeparam name="TValue">The expected type of the result value.</typeparam>
    /// <param name="result">The result to verify.</param>
    /// <param name="predicate">The predicate that the result's value should satisfy.</param>
    /// <param name="customMessage">Optional custom message for assertion failure.</param>
    /// <returns>The value contained in the successful result.</returns>
    /// <exception cref="ResultAssertionException">Thrown when the result is a failure or the predicate is not satisfied.</exception>
    public static TValue ShouldBeSuccessThatSatisfiesPredicate<TValue>(this Result<TValue> result, Func<TValue, bool> predicate, string? customMessage = null)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        var actual = result.ShouldBeSuccess();

        if (!predicate(actual))
        {
            throw new ResultAssertionException(customMessage ?? "Expected success result with value that satisfies the predicate, but it did not.");
        }

        return actual;
    }
}
