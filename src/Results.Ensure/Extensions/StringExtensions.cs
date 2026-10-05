using System.Text.RegularExpressions;
using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Ensure.Extensions;

public static class StringExtensions
{
    extension<T>(EnsureTarget<T, string> target)
    {
        /// <summary>
        /// Ensures that the string is not empty.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> NotEmpty(string? message = null) =>
            target.Apply(StringRules.NotEmpty(message));

        /// <summary>
        /// Ensures that the string is not null or whitespace.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> NotWhiteSpace(string? message = null) =>
            target.Apply(StringRules.NotWhiteSpace(message));

        /// <summary>
        /// Ensures that the string has at least the specified minimum length.
        /// </summary>
        /// <param name="minLength">The minimum required length.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> MinLength(int minLength, string? message = null) =>
            target.Apply(StringRules.MinLength(minLength, message));

        /// <summary>
        /// Ensures that the string has at most the specified maximum length.
        /// </summary>
        /// <param name="maxLength">The maximum allowed length.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> MaxLength(int maxLength, string? message = null) =>
            target.Apply(StringRules.MaxLength(maxLength, message));

        /// <summary>
        /// Ensures that the string length is between the specified minimum and maximum (inclusive).
        /// </summary>
        /// <param name="minLength">The minimum required length.</param>
        /// <param name="maxLength">The maximum allowed length.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> LengthBetween(int minLength, int maxLength, string? message = null) =>
            target.Apply(StringRules.LengthBetween(minLength, maxLength, message));

        /// <summary>
        /// Ensures that the string matches the provided regular expression.
        /// </summary>
        /// <param name="regex">The regular expression to match against.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> MatchesRegex(Regex regex, string? message = null) =>
            target.Apply(StringRules.Matches(regex, message));
        
        /// <summary>
        /// Ensures that the string matches the provided regular expression pattern.
        /// </summary>
        /// <param name="pattern">The regex pattern to match against.</param>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> MatchesRegex(string pattern, string? message = null) =>
            target.Apply(StringRules.Matches(pattern, message));
    }
}