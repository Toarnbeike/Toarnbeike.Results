using System.Collections;
using System.Runtime.CompilerServices;

namespace Toarnbeike.Results.Ensure.Extensions;

/// <summary>
/// Ensure properties of a collection.
/// </summary>
public static class EnsureCollection
{
    extension<TCollection>(Ensure) where TCollection : IEnumerable
    {
        /// <summary>
        /// Ensure that the provided collection is not empty, that is, contains at least 1 member.
        /// </summary>
        /// <param name="collection">The collection to check.</param>
        /// <param name="message">Optional: failure message specific for this collection.</param>
        /// <param name="expr">Auto: CallerArgumentExpression of the incoming collection.</param>
        /// <returns>Result containing either the incoming collection or a <see cref="GuardFailure"/></returns>
        public static Result<TCollection> NotEmpty(TCollection collection, string? message = null,
            [CallerArgumentExpression(nameof(collection))] string? expr = null)
        {
            var result = Guards.NotEmpty(collection);
            return result.IsValid ? collection : new GuardFailure(result, nameof(NotEmpty), expr, collection, message);
        }
    }
}