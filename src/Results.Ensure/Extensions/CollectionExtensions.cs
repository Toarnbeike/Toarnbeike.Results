using System.Collections;
using System.Runtime.CompilerServices;
using Toarnbeike.Results.Ensure.Rules;

namespace Toarnbeike.Results.Ensure.Extensions;

/// <summary>
/// Collection extensions are not separately defined for ICollections.
/// The extension method definitions for collections are defined for IEnumerables,
/// since that is the most general type for collections, and only one can be defined is the
/// generic type must be preserved in the result.
/// </summary>
public static class CollectionExtensions
{
    /// <param name="collection">The iEnumerable to check.</param>
    extension<TEnumerable>(TEnumerable collection) where TEnumerable : IEnumerable
    {
        /// <summary>
        /// Ensure that the provided collection is not empty, that is, contains at least 1 item.
        /// </summary>
        /// <param name="message">Optional: failure message specific for this collection.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming collection.</param>
        /// <returns>Result containing either the incoming collection or a <see cref="GuardFailure"/></returns>
        public Result<TEnumerable> NotEmpty(string? message = null,
            [CallerArgumentExpression(nameof(collection))] string? expr = null) =>
            Result.Ensure()
                .That(collection).NotEmpty()
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(collection);

        /// <summary>
        /// Ensure that the provided collection is empty, that is, contains exactly 0 items.
        /// </summary>
        /// <param name="message">Optional: failure message specific for this collection.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming collection.</param>
        /// <returns>Result containing either the incoming collection or a <see cref="GuardFailure"/></returns>
        public Result<TEnumerable> Empty(string? message = null,
            [CallerArgumentExpression(nameof(collection))] string? expr = null) =>
            Result.Ensure()
                .That(collection).Empty()
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(collection);

        /// <summary>
        /// Ensure that the provided collection contains at least the provided threshold amount of items.
        /// </summary>
        /// <param name="min">The minimum amount of items the collection should contain.</param>
        /// <param name="message">Optional: failure message specific for this collection.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming collection.</param>
        /// <returns>Result containing either the incoming collection or a <see cref="GuardFailure"/></returns>
        public Result<TEnumerable> AtLeast(int min, string? message = null,
            [CallerArgumentExpression(nameof(collection))] string? expr = null) =>
            Result.Ensure()
                .That(collection).AtLeast(min)
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(collection);

        /// <summary>
        /// Ensure that the provided collection contains at most the provided threshold amount of items.
        /// </summary>
        /// <param name="max">The maximum amount of items the collection may contain.</param>
        /// <param name="message">Optional: failure message specific for this collection.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming collection.</param>
        /// <returns>Result containing either the incoming collection or a <see cref="GuardFailure"/></returns>
        public Result<TEnumerable> AtMost(int max, string? message = null,
            [CallerArgumentExpression(nameof(collection))] string? expr = null) =>
            Result.Ensure()
                .That(collection).AtMost(max)
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(collection);

        /// <summary>
        /// Ensure that the provided collection contains between the provided minimum and maximum amount of items, inclusive.
        /// </summary>
        /// <param name="min">The minimum amount of items the collection should contain.</param>
        /// <param name="max">The maximum amount of items the collection may contain.</param>
        /// <param name="message">Optional: failure message specific for this collection.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming collection.</param>
        /// <returns>Result containing either the incoming collection or a <see cref="GuardFailure"/></returns>
        public Result<TEnumerable> Between(int min, int max, string? message = null,
            [CallerArgumentExpression(nameof(collection))] string? expr = null) =>
            Result.Ensure()
                .That(collection).Between(min, max)
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(collection);

        /// <summary>
        /// Ensure that the provided collection contains exactly the specified number of items.
        /// </summary>
        /// <param name="count">The exact number of elements.</param>
        /// <param name="message">Optional: failure message specific for this collection.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming collection.</param>
        /// <returns>Result containing either the incoming collection or a <see cref="GuardFailure"/></returns>
        public Result<TEnumerable> Exactly(int count, string? message = null,
            [CallerArgumentExpression(nameof(collection))] string? expr = null) =>
            Result.Ensure()
                .That(collection).Exactly(count)
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(collection);

        /// <summary>
        /// Ensure that the provided collection contains exactly one item.
        /// </summary>
        /// <param name="message">Optional: failure message specific for this collection.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming collection.</param>
        /// <returns>Result containing either the incoming collection or a <see cref="GuardFailure"/></returns>
        public Result<TEnumerable> Single(string? message = null,
            [CallerArgumentExpression(nameof(collection))] string? expr = null) =>
            Result.Ensure()
                .That(collection).Single()
                .WithArgumentName(expr).WithMessage(message)
                .ToResult(collection);
    }
}