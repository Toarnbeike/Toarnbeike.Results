using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Rules.Extensions;

/// <summary>
/// Provides fluent validation operations for <see cref="string"/> values on an <see cref="IRuleTarget{T, string}"/>.
/// </summary>
/// <remarks>Supports content-based validations such as length, emptiness, and pattern matching.</remarks>
public static class StringExtensions
{
    /// <typeparam name="T">The type associated with the rule target and validation result.</typeparam>
    /// <param name="target">The rule target for <see cref="string"/> values.</param>
    extension<T>(IRuleTarget<T, string> target)
    {
        /// <summary>
        /// Verifies that the string is not empty.
        /// </summary>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the string is not empty.</returns>
        public Result<T> NotEmpty(string? message = null) =>
            target.Apply(StringRules.NotEmpty(), message);

        /// <summary>
        /// Verifies that the string is not null or whitespace.
        /// </summary>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the string is not null or whitespace.</returns>
        public Result<T> NotWhiteSpace(string? message = null) =>
            target.Apply(StringRules.NotWhitespace(), message);

        /// <summary>
        /// Verifies that the string has at least the specified minimum length.
        /// </summary>
        /// <param name="minLength">The minimum required length.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the string has at least the specified minimum length.</returns>
        public Result<T> MinLength(int minLength, string? message = null) =>
            target.Apply(StringRules.MinLength(minLength), message);

        /// <summary>
        /// Verifies that the string has at most the specified maximum length.
        /// </summary>
        /// <param name="maxLength">The maximum allowed length.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the string has at most the specified maximum length.</returns>
        public Result<T> MaxLength(int maxLength, string? message = null) =>
            target.Apply(StringRules.MaxLength(maxLength), message);

        /// <summary>
        /// Verifies that the string length is between the specified minimum and maximum (inclusive).
        /// </summary>
        /// <param name="minLength">The minimum required length.</param>
        /// <param name="maxLength">The maximum allowed length.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the string length is between the specified minimum and maximum (inclusive).</returns>
        public Result<T> LengthBetween(int minLength, int maxLength, string? message = null) =>
            target.Apply(StringRules.LengthBetween(minLength, maxLength), message);
       
        /// <summary>
        /// Verifies that the string matches the provided regular expression pattern.
        /// </summary>
        /// <param name="pattern">The regex pattern to match against.</param>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the string matches the provided regular expression pattern.</returns>
        public Result<T> MatchesRegex(string pattern, string? patternDescription = null, string? message = null) =>
            target.Apply(StringRules.Matches(pattern, patternDescription), message);
    }
}