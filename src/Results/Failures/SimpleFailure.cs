namespace Toarnbeike.Results.Failures;

/// <summary>
/// Basic implementation of the Failure.
/// Generally it is better to use a specific (either existing or custom) overload of the Failure type.
/// </summary>
public sealed record SimpleFailure : Failure
{
    /// <summary>
    /// A short description of the failure for automated parsing.
    /// </summary>
    public string Code { get; }

    public SimpleFailure(string code, string message)
    {
        Code = code;
        Message = message;
    }
}