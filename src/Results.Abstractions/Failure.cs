using System.Diagnostics;

namespace Toarnbeike.Results;

/// <summary>
/// Represents a generic reason why an operation failed.
/// </summary>
[DebuggerDisplay("{DebuggerToString(),nq}")]
public abstract record Failure
{
    /// <summary>
    /// A human-readable description of what caused the failure.
    /// </summary>
#if NET7_0_OR_HIGHER
    public required string Message { get; init; }
#else
    public string Message { get; init; } = null!;
#endif

    /// <summary>
    /// Returns the <see cref="Message"/> as the string representation of this failure reason.
    /// </summary>
    public override string ToString() => Message;

    public FailureCategory Category { get; init; } = FailureCategory.Unknown;

    /// <summary>
    /// Debugger string representation of the object.
    /// </summary>
    internal string DebuggerToString() => $"Failure: {Message}";
}