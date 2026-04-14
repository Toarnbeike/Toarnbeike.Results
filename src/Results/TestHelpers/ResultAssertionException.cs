namespace Toarnbeike.Results.TestHelpers;

/// <summary>
/// Represents a test assertion failure when working with result types.
/// </summary>
[Obsolete("Moved to the Toarnbeike.Extensions.TestExtensions namespace")]
public sealed class ResultAssertionException(string message) : Exception(message);