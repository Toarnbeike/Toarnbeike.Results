using Toarnbeike.Results.Extensions;

namespace Toarnbeike.Results.Linq;

/// <summary>
/// Provides LINQ-style extension methods for working with <see cref="Result{T}"/> instances.
/// </summary>
public static class LinqExtensions
{
    /// <summary>
    /// Projects the value of a successful <see cref="Result{T}"/> into a new form.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the source <see cref="Result{T}"/>.</typeparam>
    /// <typeparam name="TResult">The type of the value returned by the <paramref name="selector"/> function.</typeparam>
    /// <param name="result">The source result. Must not be <see langword="null"/>.</param>
    /// <param name="selector">A function to transform the contained value. Must not be <see langword="null"/>.</param>
    /// <returns>
    /// A <see cref="Result{TResult}"/> containing the transformed value if the source result is successful;
    /// otherwise, a failure result propagating the same error.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="result"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
    public static Result<TResult> Select<T, TResult>(this Result<T> result, Func<T, TResult> selector)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(selector);

        return result.Map(selector);
    }

    /// <summary>
    /// Projects the result of a computation into a new form by applying a binding function followed by a projection function.
    /// Supports query syntax with <c>let</c> bindings.
    /// </summary>
    /// <typeparam name="T">The type of the original value.</typeparam>
    /// <typeparam name="TIntermediate">The type of the intermediate result produced by the binder.</typeparam>
    /// <typeparam name="TResult">The final result type produced by the projection.</typeparam>
    /// <param name="result">The source result. Must not be <see langword="null"/>.</param>
    /// <param name="binder">A function that returns an intermediate result. Must not be <see langword="null"/>.</param>
    /// <param name="projector">A function that combines the original and intermediate values. Must not be <see langword="null"/>.</param>
    /// <returns>
    /// A <see cref="Result{TResult}"/> containing the final projected value if both the source and intermediate results succeed;
    /// otherwise, the first encountered failure.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if any argument is <see langword="null"/>.</exception>
    public static Result<TResult> SelectMany<T, TIntermediate, TResult>(
        this Result<T> result,
        Func<T, Result<TIntermediate>> binder,
        Func<T, TIntermediate, TResult> projector)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(binder);
        ArgumentNullException.ThrowIfNull(projector);

        return result.Bind(t =>
            binder(t).Map(intermediate => projector(t, intermediate)));
    }

    /// <summary>
    /// Filters the value of a successful <see cref="Result{T}"/> based on a predicate.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the result.</typeparam>
    /// <param name="result">The source result. Must not be <see langword="null"/>.</param>
    /// <param name="predicate">A condition the value must satisfy. Must not be <see langword="null"/>.</param>
    /// <returns>
    /// The original result if the predicate is satisfied;
    /// otherwise, a failure result with a predefined error.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="result"/> or <paramref name="predicate"/> is <see langword="null"/>.</exception>
    public static Result<T> Where<T>(this Result<T> result, Func<T, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(predicate);

        return result.Ensure(predicate, () => new Failure("whereLinq", "LINQ predicate was not satisfied."));
    }
}