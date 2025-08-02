namespace Toarnbeike.Results.Failures;

/// <summary>
/// A failure that wraps a caught <see cref="Exception"/>.
/// </summary>
public sealed record ExceptionFailure : Failure
{
    /// <summary>
    /// The original exception that was caught.
    /// </summary>
    public Exception Exception { get; }

    /// <summary>
    /// Gets the short type name of the exception (e.g., <c>ArgumentNullException</c>).
    /// </summary>
    public string ExceptionType { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionFailure"/> class.
    /// </summary>
    /// <param name="exception">The exception that occurred.</param>
    public ExceptionFailure(Exception exception)
        : base($"exception:{exception.GetType().Name}", exception.Message)
    {
        Exception = exception;
        ExceptionType = exception.GetType().Name;
    }
}