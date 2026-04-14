using System.Diagnostics;

namespace Toarnbeike.Results.TestExtensions;

/// <summary>
/// Provides assertion methods to verify that a <see cref="Result"/> or <see cref="Result{TValue}"/> represents a failing outcome.
/// These methods are intended exclusively for unit testing. Not recommended for use in production logic.
/// </summary>
public static class ResultFailureAssertions
{
    /// <param name="result">The result to verify.</param>
    extension(IResult result)
    {
        /// <summary>
        /// Asserts that the result is a failure and returns the associated <see cref="Failure"/>.
        /// </summary>
        /// <returns>The failure contained in the result.</returns>
        /// <exception cref="ResultAssertionException">Thrown when the result is null or not a failure.</exception>
        [DebuggerStepThrough]
        public Failure ShouldBeFailure()
        {
            if (!result.TryGetFailure(out var failure))
            {
                throw new ResultAssertionException("Expected failure result, but got success.");
            }

            return failure;
        }

        /// <summary>
        /// Asserts that the result is a failure of the expected failure type.
        /// </summary>
        /// <typeparam name="TFailure">The expected failure type.</typeparam>
        /// <param name="customMessage">Optional custom message for assertion failure.</param>
        /// <returns>The strongly typed failure instance.</returns>
        /// <exception cref="ResultAssertionException">Thrown when the failure is not of the expected type.</exception>
        [DebuggerStepThrough]
        public TFailure ShouldBeFailureOfType<TFailure>(string? customMessage = null)
            where TFailure : Failure
        {
            var actual = result.ShouldBeFailure();

            if (actual is not TFailure converted)
            {
                throw new ResultAssertionException(customMessage ?? $"Expected failure of type '{typeof(TFailure).Name}', but got '{actual.GetType().Name}'.");
            }

            return converted;
        }
    }

    /// <summary>
    /// Asserts that the async result is a failure and returns the associated <see cref="Failure"/>.
    /// </summary>
    /// <returns>The failure contained in the result.</returns>
    /// <exception cref="ResultAssertionException">Thrown when the result is null or not a failure.</exception>
    [DebuggerStepThrough]
    public static async Task<Failure> ShouldBeFailureAsync(this Task<Result> resultTask)
    {
        var result = await resultTask;
        return result.ShouldBeFailure();
    }

    /// <summary>
    /// Asserts that the async result is a failure and returns the associated <see cref="Failure"/>.
    /// </summary>
    /// <returns>The failure contained in the result.</returns>
    /// <exception cref="ResultAssertionException">Thrown when the result is null or not a failure.</exception>
    [DebuggerStepThrough]
    public static async Task<Failure> ShouldBeFailureAsync<TValue>(this Task<Result<TValue>> resultTask)
    {
        var result = await resultTask;
        return result.ShouldBeFailure();
    }

    /// <summary>
    /// Asserts that the async result is a failure of the expected failure type.
    /// </summary>
    /// <typeparam name="TFailure">The expected failure type.</typeparam>
    /// <param name="resultTask">The async result to verify.</param>
    /// <param name="customMessage">Optional custom message for assertion failure.</param>
    /// <returns>The strongly typed failure instance.</returns>
    /// <exception cref="ResultAssertionException">Thrown when the failure is not of the expected type.</exception>
    [DebuggerStepThrough]
    public static async Task<TFailure> ShouldBeFailureOfTypeAsync<TFailure>(this Task<Result> resultTask, string? customMessage = null) where TFailure : Failure
    {
        var result = await resultTask;
        return result.ShouldBeFailureOfType<TFailure>(customMessage);
    }

    /// <summary>
    /// Asserts that the async result is a failure of the expected failure type.
    /// </summary>
    /// <typeparam name="TValue">The type of the value of the result.</typeparam>
    /// <typeparam name="TFailure">The expected failure type.</typeparam>
    /// <param name="resultTask">The async result to verify.</param>
    /// <param name="customMessage">Optional custom message for assertion failure.</param>
    /// <returns>The strongly typed failure instance.</returns>
    /// <exception cref="ResultAssertionException">Thrown when the failure is not of the expected type.</exception>
    [DebuggerStepThrough]
    public static async Task<TFailure> ShouldBeFailureOfTypeAsync<TFailure, TValue>(this Task<Result<TValue>> resultTask, string? customMessage = null) where TFailure : Failure
    {
        var result = await resultTask;
        return result.ShouldBeFailureOfType<TFailure>(customMessage);
    }
}