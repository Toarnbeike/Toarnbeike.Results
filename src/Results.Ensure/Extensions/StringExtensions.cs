using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Toarnbeike.Results.Ensure.Rules;

namespace Toarnbeike.Results.Ensure.Extensions;

public static class StringExtensions
{
    extension(string? value)
    {
        /// <summary>
        /// Ensure that the provided string is not empty, that is, contains at least 1 character.
        /// </summary>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<string> NotEmpty(string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).NotEmpty()
            .WithArgumentName(expr).WithMessage(message)
            .ToResult(value!);

        public Result<string> NotWhiteSpace(string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).NotWhiteSpace()
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value!);
    }

    extension(string value)
    {
        /// <summary>
        /// Ensure that the provided string has a minimum length, that is, contains at least <paramref name="minLength"/> characters.
        /// </summary>
        /// <param name="minLength">The minimum length of the string. The provided value must be at least this length.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<string> MinLength(int minLength, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).MinLength(minLength)
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided string has a maximum length, that is, contains at most <paramref name="maxLength"/> characters.
        /// </summary>
        /// <param name="maxLength">The maximum length of the string. The provided value must be at most this length.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<string> MaxLength(int maxLength, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).MaxLength(maxLength)
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided string has a string length between the provided minimum and maximum allowed characters.
        /// </summary>
        /// <param name="minLength">The minimum length of the string. The provided value must be at least this length.</param>
        /// <param name="maxLength">The maximum length of the string. The provided value must be at most this length.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<string> LengthBetween(int minLength, int maxLength, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).LengthBetween(minLength, maxLength)
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided string matches against the provided <paramref name="pattern"/>.
        /// </summary>
        /// <param name="pattern">The regex pattern to match against.</param>
        /// <param name="options">Optional: Regex options. Defaults to None.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<string> Matches(string pattern, RegexOptions options = RegexOptions.None, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).Matches(pattern, options)
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided string contains only alphabetic characters.
        /// </summary>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<string> Alphabetic(string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).Alphabetic()
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided string contains only alphabetic and/or numeric characters.
        /// </summary>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<string> AlphaNumeric(string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).AlphaNumeric()
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided string contains only digits.
        /// </summary>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<string> Numeric(string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).Numeric()
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided string contains only ascii characters.
        /// </summary>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<string> Ascii(string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).Ascii()
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided string is an email address as defined by RFC5322
        /// </summary>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<string> EmailAddress(string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).EmailAddress()
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided string is a valid Uri
        /// </summary>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<string> Uri(string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).Uri()
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided string is a valid absolute Uri
        /// </summary>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<string> AbsoluteUri(string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).AbsoluteUri()
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided string is a valid relative Uri
        /// </summary>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<string> RelativeUri(string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).RelativeUri()
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided string is a valid Ip address
        /// </summary>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<string> IpAddress(string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).IpAddress()
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided string is a valid Slug
        /// </summary>
        /// <remarks>
        /// This is a check for the common format for URL slugs, consisting of lowercase letters, numbers,
        /// and hyphens, and do not contain spaces or special characters.
        /// The exact definition of a "slug" can vary depending on the context,
        /// so the implementation of this rule may not fit specific requirements.
        /// </remarks>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<string> Slug(string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).Slug()
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(value);
    }
}
