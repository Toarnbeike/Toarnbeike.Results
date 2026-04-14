namespace Toarnbeike.Results.Failures;

/// <summary>
/// Failure that indicates that a single property does not pass validation.
/// </summary>
public sealed record ValidationFailure : Failure
{
    /// <summary>
    /// Failure that indicates that a single property does not pass validation.
    /// </summary>
    /// <param name="property">The name of the failing property.</param>
    /// <param name="validationMessage">The reason the property does not pass validation.</param>
    public ValidationFailure(string property, string validationMessage)
    {
        Property = property;
        ValidationMessage = validationMessage;
        Message = $"Validation failed for {Property} with message {ValidationMessage}";
        Category = FailureCategory.Validation;
    }

    /// <summary>The name of the failing property.</summary>
    public string Property { get; init; }

    /// <summary>The reason the property does not pass validation.</summary>
    public string ValidationMessage { get; init; }
}
