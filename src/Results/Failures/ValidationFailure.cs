namespace Toarnbeike.Results.Failures;

/// <summary>
/// Failure that indicates that a single property does not pass validation.
/// </summary>
/// <param name="Property">The name of the failing property.</param>
/// <param name="ValidationMessage">The reason the property does not pass validation.</param>
public sealed record ValidationFailure(string Property, string ValidationMessage) : Failure($"validation_{Property}", $"{Property}: {ValidationMessage}");
