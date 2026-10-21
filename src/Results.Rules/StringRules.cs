using Toarnbeike.Results.Rules.Evaluations.Comparisons;
using Toarnbeike.Results.Rules.Evaluations.Strings;

namespace Toarnbeike.Results.Rules;

/// <summary>
/// Set of Rules for <see cref="string"/> type.
/// </summary>
public static class StringRules
{
    /// <summary>
    /// Verifies that the string value is not null or empty.
    /// </summary>
    /// <returns>A rule delegate that varifies the string.</returns>
    public static RuleDelegate<string> NotEmpty() => value =>
        RuleResult.Create("String.NotEmpty", value, new NotEmptyEvaluator());

    /// <summary>
    /// Verifies that the string value is not null, empty, or whitespace.
    /// </summary>
    /// <returns>A rule delegate that verifies the string.</returns>
    public static RuleDelegate<string> NotWhitespace() => value =>
        RuleResult.Create("String.NotWhitespace", value, new NotWhitespaceEvaluator());

    /// <summary>
    /// Verifies that the string value has a length greater than or equal to the specified minimum length.
    /// </summary>
    /// <param name="minLength">The minimum length to verify.</param>
    /// <returns>A rule delegate that verifies the string.</returns>
    public static RuleDelegate<string> MinLength(int minLength) => value =>
        RuleResult.Create("String.MinLength", value, value => value.Length, new AtLeastEvaluator<int>(minLength));

    /// <summary>
    /// Verifies that the string value has a length less than or equal to the specified maximum length.
    /// </summary>
    /// <param name="maxLength">The maximum length to verify.</param>
    /// <returns>A rule delegate that verifies the string.</returns>
    public static RuleDelegate<string> MaxLength(int maxLength) => value =>
        RuleResult.Create("String.MaxLength", value, value => value.Length, new AtMostEvaluator<int>(maxLength));

    /// <summary>
    /// Verifies that the string value has a length between the specified minimum and maximum lengths (inclusive).
    /// </summary>
    /// <param name="minLength">The minimum length to verify.</param>
    /// <param name="maxLength">The maximum length to verify.</param>
    /// <returns>A rule delegate that verifies the string.</returns>
    public static RuleDelegate<string> LengthBetween(int minLength, int maxLength) => value =>
        RuleResult.Create("String.LengthBetween", value, value => value.Length, new BetweenEvaluator<int>(minLength, maxLength));

    /// <summary>
    /// Verifies that the string value is a valid email address.
    /// </summary>
    /// <returns>A rule delegate that verifies the string.</returns>
    public static RuleDelegate<string> IsEmailAddress() => value =>
        RuleResult.Create("String.IsEmailAddress", value, new IsEmailAddressEvaluator());

    /// <summary>
    /// Verifies that the string value is a valid URI.
    /// </summary>
    /// <returns>A rule delegate that verifies the string.</returns>
    public static RuleDelegate<string> IsUri(UriKind? kind = null) => value =>
        RuleResult.Create("String.IsUri", value, new IsUriEvaluator(kind));

    /// <summary>
    /// Verifies that the string value matches the specified regular expression pattern.
    /// </summary>
    /// <param name="pattern">The regular expression pattern to match.</param>
    /// <param name="patternDescription">An optional description of the pattern.</param>
    /// <returns>A rule delegate that verifies the string.</returns>
    public static RuleDelegate<string> Matches(string pattern, string? patternDescription = null) => value =>
        RuleResult.Create("String.Matches", value, new MatchesEvaluator(pattern), ("description", patternDescription ?? pattern));
}
