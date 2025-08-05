using Toarnbeike.Optional;

namespace Toarnbeike.Results.Optional;

/// <summary>
/// ToResult: extension methods to convert <see cref="Option{T}"/> to <see cref="Result{T}"/>.
/// </summary>
public static class ToResultExtensions
{
    /// <summary>
    /// Converts an <see cref="Option{T}"/> to a <see cref="Result{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="option">The optional value to convert.</param>
    /// <param name="failureProvider">A function that provides a <see cref="Failure"/> when the option is <c>None</c>.</param>
    /// <returns>
    /// A successful <see cref="Result{T}"/> when the option has a value, 
    /// or a failed result with the provided <see cref="Failure"/> if it is <c>None</c>.
    /// </returns>
    public static Result<T> ToResult<T>(this Option<T> option, Func<Failure> failureProvider)
    {
        return option.TryGetValue(out var value)
            ? Result.Success(value)
            : failureProvider();
    }

    /// <summary>
    /// Converts an <see cref="Option{T}"/> to a <see cref="Result{T}"/>, using an asynchronous failure provider.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="option">The optional value to convert.</param>
    /// <param name="failureProviderAsync">An async function that provides a <see cref="Failure"/> when the option is <c>None</c>.</param>
    /// <returns>
    /// A task that produces a successful <see cref="Result{T}"/> when the option has a value, 
    /// or a failed result with the provided <see cref="Failure"/> if it is <c>None</c>.
    /// </returns>
    public static async Task<Result<T>> ToResultAsync<T>(this Option<T> option, Func<Task<Failure>> failureProviderAsync)
    {
        return option.TryGetValue(out var value)
            ? Result.Success(value)
            : await failureProviderAsync().ConfigureAwait(false);
    }

    /// <summary>
    /// Converts an asynchronous <see cref="Option{T}"/> to a <see cref="Result{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="optionTask">The task that produces an optional value.</param>
    /// <param name="failureProvider">A function that provides a <see cref="Failure"/> when the option is <c>None</c>.</param>
    /// <returns>
    /// A task that produces a successful <see cref="Result{T}"/> when the option has a value, 
    /// or a failed result with the provided <see cref="Failure"/> if it is <c>None</c>.
    /// </returns>
    public static async Task<Result<T>> ToResult<T>(this Task<Option<T>> optionTask, Func<Failure> failureProvider)
    {
        var option = await optionTask.ConfigureAwait(false);
        return ToResult(option, failureProvider);
    }

    /// <summary>
    /// Converts an asynchronous <see cref="Option{T}"/> to a <see cref="Result{T}"/>, using an asynchronous failure provider.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="optionTask">The task that produces an optional value.</param>
    /// <param name="failureProviderAsync">An async function that provides a <see cref="Failure"/> when the option is <c>None</c>.</param>
    /// <returns>
    /// A task that produces a successful <see cref="Result{T}"/> when the option has a value, 
    /// or a failed result with the provided <see cref="Failure"/> if it is <c>None</c>.
    /// </returns>
    public static async Task<Result<T>> ToResultAsync<T>(this Task<Option<T>> optionTask, Func<Task<Failure>> failureProviderAsync)
    {
        var option = await optionTask.ConfigureAwait(false);
        return await ToResultAsync(option, failureProviderAsync).ConfigureAwait(false);
    }
}