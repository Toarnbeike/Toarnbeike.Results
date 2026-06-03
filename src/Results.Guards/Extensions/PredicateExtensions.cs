using System.Runtime.CompilerServices;
using Toarnbeike.Results.Guards.Rules;

namespace Toarnbeike.Results.Guards.Extensions;

public static class PredicateExtensions
{
    extension<TValue>(TValue value)
    {
        /// <summary>
        /// Ensure that the provided value satisfies the provided predicate.
        /// </summary>
        /// <param name="predicate">The predicate this value must satisfy.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TValue> Satisfies(Func<TValue, bool> predicate, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).Satisfies(predicate)
                .WithCustomExpression(expr).WithCustomMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value does not satisfy the provided predicate.
        /// </summary>
        /// <param name="predicate">The predicate this value must not satisfy.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public Result<TValue> NotSatisfies(Func<TValue, bool> predicate, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            Result.Ensure().That(value).NotSatisfies(predicate)
                .WithCustomExpression(expr).WithCustomMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value satisfies the provided async predicate.
        /// </summary>
        /// <param name="predicate">The predicate this value must satisfy.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public async Task<Result<TValue>> SatisfiesAsync(Func<TValue, Task<bool>> predicate, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            await Result.Ensure().That(value).SatisfiesAsync(predicate)
                .WithCustomExpression(expr).WithCustomMessage(message)
                .ToResult(value);

        /// <summary>
        /// Ensure that the provided value does not satisfy the provided async predicate.
        /// </summary>
        /// <param name="predicate">The predicate this value must not satisfy.</param>
        /// <param name="message">Optional: failure message specific for this value.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming value.</param>
        /// <returns>Result containing either the incoming value or a <see cref="GuardFailure"/></returns>
        public async Task<Result<TValue>> NotSatisfiesAsync(Func<TValue, Task<bool>> predicate, string? message = null,
            [CallerArgumentExpression(nameof(value))] string? expr = null) =>
            await Result.Ensure().That(value).NotSatisfiesAsync(predicate)
                .WithCustomExpression(expr).WithCustomMessage(message)
                .ToResult(value);
    }
}