using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class GuidValidationTargetExtensions
{


    extension<T>(ValidationTarget<T, Guid> target)
    {
        /// <summary>
        /// Validates that the Guid is not empty (Guid.Empty).
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> NotEmpty(string? message = null) =>
            target.Apply(GuidRules.NotEmpty(), message);

        /// <summary>
        /// Validates that the Guid is a version 4 GUID.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> Version4(string? message = null) =>
            target.Apply(GuidRules.IsVersion4(), message);

        /// <summary>
        /// Validates that the Guid is a version 7 GUID.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the validation fails.</param>
        /// <returns>The result of the validation, either a success or a failure <see cref="ValidationFailure"/>.</returns>
        public Result<T> Version7(string? message = null) =>
            target.Apply(GuidRules.IsVersion7(), message);
    }
}
