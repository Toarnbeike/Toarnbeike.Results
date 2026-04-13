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
}