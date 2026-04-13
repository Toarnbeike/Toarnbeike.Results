namespace Toarnbeike.Results;

/// <summary>
/// The Type (Category) of the failure.
/// </summary>
public enum FailureCategory
{
    Unknown = 0,
    Validation,
    Business,
    External,
    System,
}