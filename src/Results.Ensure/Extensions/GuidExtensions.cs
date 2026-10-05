using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Ensure.Extensions;

public static class GuidExtensions
{
    extension<T>(EnsureTarget<T, Guid> target)
    {
        /// <summary>
        /// Ensures that the Guid is not empty (Guid.Empty).
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> NotEmpty(string? message = null) =>
            target.Apply(GuidRules.NotEmpty(message));

        /// <summary>
        /// Ensures that the Guid is a version 4 GUID.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> Version4(string? message = null) =>
            target.Apply(GuidRules.Version4(message));

        /// <summary>
        /// Ensures that the Guid is a version 7 GUID.
        /// </summary>
        /// <param name="message">Optionally a custom message to use if the guard fails.</param>
        /// <returns>The result of the guard, either a success or a failure <see cref="GuardFailure"/>.</returns>
        public Result<T> Version7(string? message = null) =>
            target.Apply(GuidRules.Version7(message));
    }
}
