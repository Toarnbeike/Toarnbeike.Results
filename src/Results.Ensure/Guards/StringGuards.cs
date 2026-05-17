using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Toarnbeike.Results.Ensure.Guards;

internal static partial class StringGuards
{
    [GeneratedRegex(@"^[\p{L}]+$", RegexOptions.CultureInvariant)]
    private static partial Regex AlphaRegex { get; }

    [GeneratedRegex(@"^[\p{L}\p{Nd}]+$", RegexOptions.CultureInvariant)]
    private static partial Regex AlphaNumericRegex { get; }

    [GeneratedRegex(@"^\d+$", RegexOptions.CultureInvariant)]
    private static partial Regex DigitsOnlyRegex { get; }

    [GeneratedRegex(@"^[a-z0-9]+(?:-[a-z0-9]+)*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)]
    private static partial Regex SlugRegex { get; }

    public static bool NotEmpty(string? value) => !string.IsNullOrEmpty(value);
    public static bool NotWhiteSpace(string? value) => !string.IsNullOrWhiteSpace(value);

    public static bool Matches(string value, Regex regex) => regex.IsMatch(value);
    public static bool Alphabetic(string value) => AlphaRegex.IsMatch(value);
    public static bool AlphaNumeric(string value) => AlphaNumericRegex.IsMatch(value);
    public static bool Digits(string value) => DigitsOnlyRegex.IsMatch(value);
    public static bool Ascii(string value) => value.All(char.IsAscii);

    public static bool EmailAddress(string value) => MailAddress.TryCreate(value, out _);
    public static bool Uri(string value) => System.Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out _); 
    public static bool AbsoluteUri(string value) => System.Uri.TryCreate(value, UriKind.Absolute, out _); 
    public static bool RelativeUri(string value) => System.Uri.TryCreate(value, UriKind.Relative, out _); 
    public static bool IpAddress(string value) => IPAddress.TryParse(value, out _);
    public static bool Slug(string value) => SlugRegex.IsMatch(value);

}