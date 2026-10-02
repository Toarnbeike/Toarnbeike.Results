using System.Diagnostics.CodeAnalysis;

namespace Toarnbeike.Results.Validation.Rules;

public readonly record struct RuleResult
{
    public bool IsValid { get; }
    public string? ErrorMessage { get; }

    public static RuleResult Valid() => new(true, null);
    public static RuleResult Invalid(string message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(message);
        return new(false, message);
    }

    public bool IsInvalid([MaybeNullWhen(false)] out string errorMessage)
    {
        errorMessage = ErrorMessage;
        return !IsValid;
    }

    private RuleResult(bool isValid, string? errorMessage)
    {
        IsValid = isValid;
        ErrorMessage = errorMessage;
    }
}
