using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class GuidValidationRuleBuilderExtensions
{
    extension<T>(ValidationRuleBuilder<T, Guid> builder)
    {
        /// <summary>
        /// Validates that the Guid is not empty (Guid.Empty).
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, Guid> NotEmpty(string? message = null) =>
            builder.Add(GuidRules.NotEmpty(), message);

        /// <summary>
        /// Validates that the Guid is a version 4 GUID.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, Guid> Version4(string? message = null) =>
            builder.Add(GuidRules.IsVersion4(), message);

        /// <summary>
        /// Validates that the Guid is a version 7 GUID.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The validation rule builder to add to the <see cref="ValidationRuleCollection{T}"/>.</returns>
        public ValidationRuleBuilder<T, Guid> Version7(string? message = null) =>
            builder.Add(GuidRules.IsVersion7(), message);
    }
}