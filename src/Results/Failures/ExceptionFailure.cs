namespace Toarnbeike.Results.Failures;

/// <summary>
/// A failure that wraps an <see cref="Exception"/>.
/// </summary>
public sealed record ExceptionFailure : Failure
{
    /// <summary>
    /// The original exception that was caught.
    /// </summary>
    public Exception Exception { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionFailure"/> class.
    /// </summary>
    /// <param name="exception">The exception that occurred.</param>
    public ExceptionFailure(Exception exception)
    {
        Exception = exception;
        Message = $"Exception: {exception.Message}";
        Category = FailureCategory.System;
    }
}