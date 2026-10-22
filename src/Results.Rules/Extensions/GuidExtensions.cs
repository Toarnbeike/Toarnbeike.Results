using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Rules.Extensions;

/// <summary>
/// Provides fluent validation operations for <see cref="Guid"/> values on an <see cref="RuleTarget{T, Guid}"/>.
/// </summary>
/// <remarks>Supports validations such as not empty and specific version checks.</remarks>
public static class GuidExtensions
{
    /// <typeparam name="T">The type associated with the rule target and validation result.</typeparam>
    /// <param name="target">The rule target for <see cref="Guid"/> values.</param>
    extension<T>(RuleTarget<T, Guid> target)
    {
        /// <summary>
        /// Verifies that the Guid is not empty (Guid.Empty).
        /// </summary>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the Guid is not empty.</returns>
        public Result<T> NotEmpty(string? message = null) =>
            target.Apply(GuidRules.NotEmpty(), message);

        /// <summary>
        /// Verifies that the Guid is a version 4 GUID.
        /// </summary>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the Guid is a version 4 GUID.</returns>
        public Result<T> Version4(string? message = null) =>
            target.Apply(GuidRules.IsVersion4(), message);

        /// <summary>
        /// Verifies that the Guid is a version 7 GUID.
        /// </summary>
        /// <param name="message">An optional custom failure message.</param>
        /// <returns>A result representing whether the Guid is a version 7 GUID.</returns>
        public Result<T> Version7(string? message = null) =>
            target.Apply(GuidRules.IsVersion7(), message);
    }
}
