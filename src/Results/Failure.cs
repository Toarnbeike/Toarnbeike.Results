namespace Toarnbeike.Results;

/// <summary>
/// Represents the reason why an operation failed.
/// </summary>
/// <param name="Code">A short code identifying the type or category of failure.</param>
/// <param name="Message">A human-readable description of what caused the failure.</param>
public record Failure(string Code, string Message)
{
    /// <summary>
    /// Returns the <see cref="Message"/> as the string representation of this failure reason.
    /// </summary>
    public override string ToString() => Message;
}