namespace Toarnbeike.Results.Failures;

/// <summary>
/// Failure that indicates that a Domain Guard (using Rules) failed for the provided parameter.
/// </summary>
public sealed record GuardFailure : DomainFailure
{
    /// <summary>
    /// The name of the Parameter that caused the failure
    /// </summary>
    public string ParameterName { get; }

    public GuardFailure(string parameter, string message)
        : base(message)
    {
        ParameterName = parameter;
    }
}