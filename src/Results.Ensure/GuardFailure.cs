namespace Toarnbeike.Results.Ensure;

/// <summary>
/// Failure that indicates that a Domain Guard (using Ensure) failed.
/// </summary>
public sealed record GuardFailure : Failure
{
    /// <summary>
    /// The name of the Parameter that caused the failure
    /// </summary>
    public string ParameterName { get; }

    public GuardFailure(string parameter, string message)
    {
        ParameterName = parameter;
        Message = message;
        Category = FailureCategory.Business;
    }
}