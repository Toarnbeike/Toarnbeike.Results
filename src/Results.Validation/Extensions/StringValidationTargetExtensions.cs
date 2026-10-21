using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class StringValidationTargetExtensions
{
    extension<T>(ValidationTarget<T, string> target)
    {
        /// <summary>
        /// Validates that the string is not empty.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> NotEmpty(string? message = null) =>
            target.Apply(StringRules.NotEmpty(), message);

        /// <summary>
        /// Validates that the string is not null or whitespace.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> NotWhiteSpace(string? message = null) =>
            target.Apply(StringRules.NotWhitespace(), message);

        /// <summary>
        /// Validates that the string has at least the specified minimum length.
        /// </summary>
        /// <param name="minLength">The minimum required length.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> MinLength(int minLength, string? message = null) =>
            target.Apply(StringRules.MinLength(minLength), message);

        /// <summary>
        /// Validates that the string has at most the specified maximum length.
        /// </summary>
        /// <param name="maxLength">The maximum allowed length.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> MaxLength(int maxLength, string? message = null) =>
            target.Apply(StringRules.MaxLength(maxLength), message);

        /// <summary>
        /// Validates that the string length is between the specified minimum and maximum (inclusive).
        /// </summary>
        /// <param name="minLength">The minimum required length.</param>
        /// <param name="maxLength">The maximum allowed length.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> LengthBetween(int minLength, int maxLength, string? message = null) =>
            target.Apply(StringRules.LengthBetween(minLength, maxLength), message);
     
        /// <summary>
        /// Validates that the string matches the provided regular expression pattern.
        /// </summary>
        /// <param name="pattern">The regex pattern to match against.</param>
        /// <param name="description">Optionally a description of the pattern.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> Matches(string pattern, string? description = null, string? message = null) =>
            target.Apply(StringRules.Matches(pattern, description), message);
    }
}