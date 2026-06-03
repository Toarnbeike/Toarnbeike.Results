using System.Runtime.CompilerServices;
using Toarnbeike.Results.Guards.Rules;

namespace Toarnbeike.Results.Guards.Extensions;

public static class GuidExtensions
{
    extension(Guid value)
    {
        /// <summary>
        /// Ensure that the provided guid is not empty.
        /// </summary>
        /// <param name="message">Optional: failure message specific for this guid.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming guid.</param>
        /// <returns>Result containing either the incoming guid or a <see cref="GuardFailure"/></returns>
        public Result<Guid> NotEmpty(string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).NotEmpty().WithCustomMessage(message).WithCustomExpression(expr).ToResult(value);

        /// <summary>
        /// Ensure that the provided guid is created in version 4.
        /// </summary>
        /// <param name="message">Optional: failure message specific for this guid.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming guid.</param>
        /// <returns>Result containing either the incoming guid or a <see cref="GuardFailure"/></returns>
        public Result<Guid> IsVersion4(string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).Version4().WithCustomMessage(message).WithCustomExpression(expr).ToResult(value);

        /// <summary>
        /// Ensure that the provided guid is created in version 7.
        /// </summary>
        /// <param name="message">Optional: failure message specific for this guid.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming guid.</param>
        /// <returns>Result containing either the incoming guid or a <see cref="GuardFailure"/></returns>
        public Result<Guid> IsVersion7(string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).Version7().WithCustomMessage(message).WithCustomExpression(expr).ToResult(value);
    }
}