using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class StringValidationRuleBuilderExtensions
{
    extension<T>(ValidationRuleBuilder<T, string> builder)
    {
        /// <summary>
        /// Validates that the string is not empty.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, string> NotEmpty(string? message = null) =>
            builder.Add(StringRules.NotEmpty(), message);

        /// <summary>
        /// Validates that the string is not null or whitespace.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, string> NotWhiteSpace(string? message = null) =>
            builder.Add(StringRules.NotWhitespace(), message);

        /// <summary>
        /// Validates that the string has at least the specified minimum length.
        /// </summary>
        /// <param name="minLength">The minimum required length.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, string> MinLength(int minLength, string? message = null) =>
            builder.Add(StringRules.MinLength(minLength), message);

        /// <summary>
        /// Validates that the string has at most the specified maximum length.
        /// </summary>
        /// <param name="maxLength">The maximum allowed length.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, string> MaxLength(int maxLength, string? message = null) =>
            builder.Add(StringRules.MaxLength(maxLength), message);

        /// <summary>
        /// Validates that the string length is between the specified minimum and maximum (inclusive).
        /// </summary>
        /// <param name="minLength">The minimum required length.</param>
        /// <param name="maxLength">The maximum allowed length.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, string> LengthBetween(int minLength, int maxLength, string? message = null) =>
            builder.Add(StringRules.LengthBetween(minLength, maxLength), message);

        /// <summary>
        /// Validates that the string matches the provided regular expression pattern.
        /// </summary>
        /// <param name="pattern">The regex pattern to match against.</param>
        /// <param name="description">An optional description of the pattern.</param>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, string> Matches(string pattern, string? description = null, string? message = null) => 
            builder.Add(StringRules.Matches(pattern, description), message);
    }
}