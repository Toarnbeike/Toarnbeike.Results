using System.Text.RegularExpressions;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Rules;

internal static class StringRules
{
    public static ValidationRule<string> NotEmpty(string? message) =>
        new(value => !string.IsNullOrEmpty(value), message ?? "Value must not be empty.");

    public static ValidationRule<string> NotWhiteSpace(string? message) =>
        new(value => !string.IsNullOrWhiteSpace(value), message ?? "Value must not be whitespace.");

    public static ValidationRule<string> MinLength(int minLength, string? message) =>
        new(value => value is not null && value.Length >= minLength,
            message ?? $"Value must be at least {minLength} characters long.");

    public static ValidationRule<string> MaxLength(int maxLength, string? message) =>
        new(value => value is not null && value.Length <= maxLength,
            message ?? $"Value must be at most {maxLength} characters long.");

    public static ValidationRule<string> LengthBetween(int minLength, int maxLength, string? message) =>
        new(value => value is not null && value.Length >= minLength && value.Length <= maxLength,
            message ?? $"Value must be between {minLength} and {maxLength} characters long.");

    public static ValidationRule<string> Matches(string pattern, string? message)
    {
        ArgumentNullException.ThrowIfNull(pattern);
        return new(value => value is not null && Regex.IsMatch(value, pattern, RegexOptions.None, TimeSpan.FromSeconds(1)),
            message ?? $"Value must match the pattern '{pattern}'.");
    }

    public static ValidationRule<string> Matches(Regex regex, string? message)
    {
        ArgumentNullException.ThrowIfNull(regex);
        return new(value => value is not null && regex.IsMatch(value),
            message ?? $"Value must match the provided regex.");
    }
}