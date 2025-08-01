namespace Toarnbeike.Results.Extensions;

/// <summary>
/// Provides extension methods for binding a non-generic <see cref="Result"/>
/// to a new <see cref="Result{TOut}"/> using a function.
/// </summary>
public static class BindResultExtensions
{
    /// <summary>
    /// Projects a successful <see cref="Result"/> into a new <see cref="Result{TOut}"/> using the specified function.
    /// </summary>
    /// <typeparam name="TOut">The result type of the projection.</typeparam>
    /// <param name="result">The original result to project from.</param>
    /// <param name="bindFunc">
    /// A function to apply if the original result is successful.
    /// Should return a new <see cref="Result{TOut}"/>.
    /// </param>
    /// <returns>
    /// If the original result is a failure, that failure is returned.
    /// Otherwise, the result of <paramref name="bindFunc"/> is returned.
    /// </returns>
    public static Result<TOut> Bind<TOut>(this Result result, Func<Result<TOut>> bindFunc)
    {
        if (result.TryGetFailure(out var failure))
        {
            return Result<TOut>.Failure(failure);
        }

        return bindFunc();
    }

    /// <summary>
    /// Asynchronously projects a successful <see cref="Result"/> into a new <see cref="Result{TOut}"/>
    /// using the specified asynchronous function.
    /// </summary>
    /// <typeparam name="TOut">The result type of the projection.</typeparam>
    /// <param name="result">The original result to project from.</param>
    /// <param name="bindTaskFunc">
    /// An asynchronous function to apply if the original result is successful.
    /// Should return a new <see cref="Result{TOut}"/>.
    /// </param>
    /// <returns>
    /// A task that resolves to:
    /// - The original failure if <paramref name="result"/> is a failure,
    /// - Otherwise, the result of <paramref name="bindTaskFunc"/>.
    /// </returns>
    public static async Task<Result<TOut>> BindAsync<TOut>(this Result result, Func<Task<Result<TOut>>> bindTaskFunc)
    {
        if (result.TryGetFailure(out var failure))
        {
            return Result<TOut>.Failure(failure);
        }

        return await bindTaskFunc();
    }

    /// <summary>
    /// Projects a successful <see cref="Result"/> into a new <see cref="Result{TOut}"/> using the specified function.
    /// </summary>
    /// <typeparam name="TOut">The result type of the projection.</typeparam>
    /// <param name="resultTask">The original result task to project from.</param>
    /// <param name="bindFunc">
    /// A function to apply if the original result is successful.
    /// Should return a new <see cref="Result{TOut}"/>.
    /// </param>
    /// <returns>
    /// If the original result is a failure, that failure is returned.
    /// Otherwise, the result of <paramref name="bindFunc"/> is returned.
    /// </returns>
    public static async Task<Result<TOut>> Bind<TOut>(this Task<Result> resultTask, Func<Result<TOut>> bindFunc)
    {
        var result = await resultTask;
        return Bind(result, bindFunc);
    }

    /// <summary>
    /// Asynchronously projects a successful <see cref="Result"/> into a new <see cref="Result{TOut}"/>
    /// using the specified asynchronous function.
    /// </summary>
    /// <typeparam name="TOut">The result type of the projection.</typeparam>
    /// <param name="resultTask">The original result task to project from.</param>
    /// <param name="bindTaskFunc">
    /// An asynchronous function to apply if the original result is successful.
    /// Should return a new <see cref="Result{TOut}"/>.
    /// </param>
    /// <returns>
    /// A task that resolves to:
    /// - The original failure if <paramref name="result"/> is a failure,
    /// - Otherwise, the result of <paramref name="bindTaskFunc"/>.
    /// </returns>
    public static async Task<Result<TOut>> BindAsync<TOut>(this Task<Result> resultTask, Func<Task<Result<TOut>>> bindTaskFunc)
    {
        var result = await resultTask;
        return await BindAsync(result, bindTaskFunc);
    }
}
