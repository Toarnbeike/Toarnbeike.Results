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
        /// Asserts that the result is a failure and that the failure has the expected code.
        /// </summary>
        /// <param name="expectedCode">The expected failure code.</param>
        /// <param name="customMessage">Optional custom message for assertion failure.</param>
        /// <returns>The failure contained in the result.</returns>
        /// <exception cref="ResultAssertionException">Thrown when the code does not match.</exception>
        public Failure ShouldBeFailureWithCode(string expectedCode, string? customMessage = null)
        {
            var actual = result.ShouldBeFailure();

            if (actual.Code != expectedCode)
            {
                throw new ResultAssertionException(customMessage ?? $"Expected failure result with code '{expectedCode}', but got '{actual.Code}'.");
            }

            return actual;
        }

        /// <summary>
        /// Asserts that the result is a failure and that the failure has the expected message.
        /// </summary>
        /// <param name="expectedMessage">The expected failure message.</param>
        /// <param name="customMessage">Optional custom message for assertion failure.</param>
        /// <returns>The failure contained in the result.</returns>
        /// <exception cref="ResultAssertionException">Thrown when the message does not match.</exception>
        public Failure ShouldBeFailureWithMessage(string expectedMessage, string? customMessage = null)
        {
            var actual = result.ShouldBeFailure();

            if (actual.Message != expectedMessage)
            {
                throw new ResultAssertionException(customMessage ?? $"Expected failure result with message '{expectedMessage}', but got '{actual.Message}'.");
            }

            return actual;
        }

        /// <summary>
        /// Asserts that the result is a failure with the specified code and message.
        /// </summary>
        /// <param name="expectedCode">The expected failure code.</param>
        /// <param name="expectedMessage">The expected failure message.</param>
        /// <param name="customMessage">Optional custom message for assertion failure.</param>
        /// <returns>The failure contained in the result.</returns>
        /// <exception cref="ResultAssertionException">Thrown when the code or message does not match.</exception>
        public Failure ShouldBeFailureWithCodeAndMessage(string expectedCode, string expectedMessage, string? customMessage = null)
        {
            var actual = result.ShouldBeFailure();

            if (actual.Code != expectedCode || actual.Message != expectedMessage)
            {
                throw new ResultAssertionException(customMessage ?? $"Expected failure result with code '{expectedCode}' and message '{expectedMessage}', but got code '{actual.Code}' and message '{actual.Message}'.");
            }

            return actual;
        }

        /// <summary>
        /// Asserts that the result is a failure and that the failure satisfies the given predicate.
        /// </summary>
        /// <param name="predicate">A predicate the failure must satisfy.</param>
        /// <param name="customMessage">Optional custom message for assertion failure.</param>
        /// <returns>The failure contained in the result.</returns>
        /// <exception cref="ResultAssertionException">Thrown when the predicate is not satisfied.</exception>
        public Failure ShouldBeFailureThatSatisfiesPredicate(Func<Failure, bool> predicate, string? customMessage = null)
        {
            ArgumentNullException.ThrowIfNull(predicate);

            var actual = result.ShouldBeFailure();

            if (!predicate(actual))
            {
                throw new ResultAssertionException(customMessage ?? "Expected failure result with a failure that satisfies the predicate, but it did not.");
            }

            return actual;
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