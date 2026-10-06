namespace Toarnbeike.Results.Failures;

/// <summary>
/// A failure that indicates a domain-specific business rule violation.
/// </summary>
/// <param name="Message">A detailed description of the failure.</param>
public abstract record DomainFailure(string Message)
    : Failure(Message, FailureCategory.Business);