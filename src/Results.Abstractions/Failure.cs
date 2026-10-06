using System.Diagnostics;

namespace Toarnbeike.Results;

/// <summary>
/// Represents a generic reason why an operation failed.
/// </summary>
/// <param name="Message">A human-readable description of what caused the failure.</param>
/// <param name="Category">The type (category) of the failure.</param>
[DebuggerDisplay("{DebuggerToString(),nq}")]
public record Failure(string Message, FailureCategory Category)
{
    /// <summary>
    /// Returns the <see cref="Message"/> as the string representation of this failure reason.
    /// </summary>
    public override string ToString() => Message;

    /// <summary>
    /// Debugger string representation of the object.
    /// </summary>
    internal string DebuggerToString() => $"Failure: {Message}";

    public Failure(string message)
        : this(message, FailureCategory.Unknown) { }
}