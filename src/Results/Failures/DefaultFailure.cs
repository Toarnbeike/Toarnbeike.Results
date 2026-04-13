namespace Toarnbeike.Results.Failures;

/// <summary>
/// Default implementation of the Failure.
/// Generally it is better to use a specific (either existing or custom) overload of the Failure type.
/// </summary>
public sealed record DefaultFailure : Failure
{
    /// <summary>
    /// A short description of the failure for automated parsing.
    /// </summary>
    public string Code { get; }

    public DefaultFailure(string code, string message)
    {
        Code = code;
        Message = message;
    }
}