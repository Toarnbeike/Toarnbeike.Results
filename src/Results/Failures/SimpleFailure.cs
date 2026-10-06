namespace Toarnbeike.Results.Failures;

/// <summary>
/// Basic implementation of the Failure.
/// Generally it is better to use a specific (either existing or custom) overload of the Failure type.
/// </summary>
/// <param name="Code">A short description of the failure for automated parsing.</param>
/// <param name="Message">A detailed description of the failure.</param>
[Obsolete("Use a generic Failure type for inline failures or create a custom Failure type for more specific failures.")]
public sealed record SimpleFailure(string Code, string Message) : Failure(Message);
