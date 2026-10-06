namespace Toarnbeike.Results.Failures;

/// <summary>
/// A failure that indicates a test failure with a specific identifier.
/// </summary>
public sealed record TestFailure(string Identifier)
    : Failure(Identifier);