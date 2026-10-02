using System.Text.RegularExpressions;

namespace Toarnbeike.Results.Validation.Rules;

internal static class StringRules
{
    public static bool NotEmpty(string? value) => !string.IsNullOrEmpty(value);
    public static bool NotWhiteSpace(string? value) => !string.IsNullOrWhiteSpace(value);

    public static bool Matches(string value, Regex regex) => regex.IsMatch(value);
}
