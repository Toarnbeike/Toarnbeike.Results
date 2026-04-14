using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Toarnbeike.Results.Ensure.Extensions;

/// <summary>
/// Ensure properties of a string.
/// </summary>
public static class EnsureString
{
    extension(Ensure)
    {
        /// <summary>
        /// Ensure that the provided string is not empty, that is, contains at least 1 character.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public static Result<string> NotEmpty(string? value, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null)
        {
            var result = Guards.NotEmpty(value);
            return result.IsValid ? value! : new GuardFailure(result, nameof(NotEmpty), expr, value, message);
        }

        /// <summary>
        /// Ensure that the provided string is not whitespace, that is, contains at least 1 non-whitespace character.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public static Result<string> NotWhiteSpace(string? value, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null)
        {
            var result = Guards.NotWhiteSpace(value);
            return result.IsValid ? value! : new GuardFailure(result, nameof(NotWhiteSpace), expr, value, message);
        }

        /// <summary>
        /// Ensure that the provided string has a minimum length, that is, contains at least <paramref name="minLength"/> characters.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="minLength">The minimum length of the string. The provided value must be at least this length.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public static Result<string> MinLength(string value, int minLength, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null)
        {
            var result = Guards.MinLength(value, minLength);
            return result.IsValid ? value : new GuardFailure(result, nameof(MinLength), expr, value, message);
        }

        /// <summary>
        /// Ensure that the provided string has a maximum length, that is, contains at most <paramref name="maxLength"/> characters.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="maxLength">The maximum length of the string. The provided value must be at most this length.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public static Result<string> MaxLength(string value, int maxLength, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null)
        {
            var result = Guards.MaxLength(value, maxLength);
            return result.IsValid ? value : new GuardFailure(result, nameof(MaxLength), expr, value, message);
        }

        /// <summary>
        /// Ensure that the provided string matches against the provided <paramref name="regex"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="regex">The regex to match against.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public static Result<string> Matches(string value, Regex regex, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null)
        {
            var result = Guards.Matches(value, regex);
            return result.IsValid ? value : new GuardFailure(result, nameof(Matches), expr, value, message);
        }
    }
}