namespace Toarnbeike.Results.TestHelpers;

/// <summary>
/// Represents a test assertion failure when working with result types.
/// </summary>
public sealed class ResultAssertionException(string message) : Exception(message);