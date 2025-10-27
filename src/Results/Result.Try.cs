using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results;

/// <summary>
/// Provides factory methods for safely executing actions and functions that might throw,
/// wrapping the outcome in a <see cref="Result"/> or <see cref="Result{TValue}"/>.
/// </summary>
public partial class Result
{
    /// <summary>
    /// Executes the specified <paramref name="action"/>, returning a successful result if no exception is thrown.
    /// If an exception occurs, it is captured as a <see cref="ExceptionFailure"/>.
    /// </summary>
    /// <param name="action">The action to execute.</param>
    public static Result Try(Action action)
    {
        try
        {
            action();
            return Success();
        }
        catch (Exception ex)
        {
            return new ExceptionFailure(ex);
        }
    }

    /// <summary>
    /// Executes the specified asynchronous <paramref name="task"/>, returning a successful result if no exception is thrown.
    /// If an exception occurs, it is captured as a <see cref="ExceptionFailure"/>.
    /// </summary>
    /// <param name="task">The asynchronous operation to execute.</param>
    public static async Task<Result> TryAsync(Func<Task> task)
    {
        try
        {
            await task().ConfigureAwait(false);
            return Success();
        }
        catch (Exception ex)
        {
            return new ExceptionFailure(ex);
        }
    }

    /// <summary>
    /// Executes the specified asynchronous <paramref name="valueTask"/>, returning a successful result if no exception is thrown.
    /// If an exception occurs, it is captured as a <see cref="ExceptionFailure"/>.
    /// </summary>
    /// <param name="valueTask">The asynchronous operation to execute.</param>
    public static async Task<Result> TryValueAsync(Func<ValueTask> valueTask) => 
        await TryAsync(() => valueTask().AsTask());

    /// <summary>
    /// Executes the specified <paramref name="func"/>, returning a successful result with its value if no exception is thrown.
    /// If an exception occurs, it is captured as a <see cref="ExceptionFailure"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value returned by the function.</typeparam>
    /// <param name="func">The function to execute.</param>
    public static Result<TValue> Try<TValue>(Func<TValue> func)
    {
        try
        {
            return Success(func());
        }
        catch (Exception ex)
        {
            return new ExceptionFailure(ex);
        }
    }

    /// <summary>
    /// Executes the specified asynchronous <paramref name="func"/>, returning a successful result with its value if no exception is thrown.
    /// If an exception occurs, it is captured as a <see cref="ExceptionFailure"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value returned by the asynchronous function.</typeparam>
    /// <param name="func">The asynchronous function to execute.</param>
    public static async Task<Result<TValue>> TryAsync<TValue>(Func<Task<TValue>> func)
    {
        try
        {
            var value = await func().ConfigureAwait(false);
            return Success(value);
        }
        catch (Exception ex)
        {
            return new ExceptionFailure(ex);
        }
    }

    /// <summary>
    /// Executes the specified asynchronous <paramref name="func"/>, returning a successful result with its value if no exception is thrown.
    /// If an exception occurs, it is captured as a <see cref="ExceptionFailure"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value returned by the asynchronous function.</typeparam>
    /// <param name="func">The asynchronous function to execute.</param>
    public static async Task<Result<TValue>> TryValueAsync<TValue>(Func<ValueTask<TValue>> func) =>
        await TryAsync(() => func().AsTask());
}
