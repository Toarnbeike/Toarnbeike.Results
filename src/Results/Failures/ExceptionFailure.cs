namespace Toarnbeike.Results.Failures;

/// <summary>
/// A failure that wraps an <see cref="Exception"/>.
/// </summary>
/// <param name="Exception">The exception that occurred.</param>
public sealed record ExceptionFailure(Exception Exception)
    : Failure($"Exception: {Exception.Message}", FailureCategory.System);