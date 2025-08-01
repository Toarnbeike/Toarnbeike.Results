namespace Toarnbeike.Results.Extensions;

/// <summary>
/// Bind: Provides extension methods for binding a <see cref="Result{TIn}"/>
/// to a new <see cref="Result{TOut}"/> using a function.
/// </summary>
public static class BindResultTValueExtensions
{
    /// <summary>
    /// Projects the value of a successful <see cref="Result{TIn}"/> into a new <see cref="Result{TOut}"/>
    /// using the specified bind function.
    /// </summary>
    /// <typeparam name="TIn">The type of the original result value.</typeparam>
    /// <typeparam name="TOut">The result type of the projection.</typeparam>
    /// <param name="result">The original result to project from.</param>
    /// <param name="bindFunc">
    /// A function that takes the original value and returns a new <see cref="Result{TOut}"/>.
    /// </param>
    /// <returns>
    /// If <paramref name="result"/> is a failure, that failure is returned.
    /// Otherwise, the result of <paramref name="bindFunc"/> is returned.
    /// </returns>
    public static Result<TOut> Bind<TIn, TOut>(this Result<TIn> result, Func<TIn, Result<TOut>> bindFunc)
    {
        if (!result.TryGetValue(out var value, out var failure))
        {
            return Result<TOut>.Failure(failure);
        }

        return bindFunc(value);
    }

    /// <summary>
    /// Asynchronously projects the value of a successful <see cref="Result{TIn}"/> into a new <see cref="Result{TOut}"/>
    /// using the specified asynchronous bind function.
    /// </summary>
    /// <typeparam name="TIn">The type of the original result value.</typeparam>
    /// <typeparam name="TOut">The result type of the projection.</typeparam>
    /// <param name="result">The original result to project from.</param>
    /// <param name="bindTaskFunc">
    /// An asynchronous function that takes the original value and returns a new <see cref="Result{TOut}"/>.
    /// </param>
    /// <returns>
    /// A task that resolves to:
    /// - The original failure if <paramref name="result"/> is a failure,
    /// - Otherwise, the result of <paramref name="bindTaskFunc"/>.
    /// </returns>
    public static async Task<Result<TOut>> BindAsync<TIn, TOut>(this Result<TIn> result, Func<TIn, Task<Result<TOut>>> bindTaskFunc)
    {
        if (!result.TryGetValue(out var value, out var failure))
        {
            return Result<TOut>.Failure(failure);
        }

        return await bindTaskFunc(value);
    }

    /// <summary>
    /// Projects the value of a successful <see cref="Result{TIn}"/> into a new <see cref="Result{TOut}"/>
    /// using the specified bind function.
    /// </summary>
    /// <typeparam name="TIn">The type of the original result value.</typeparam>
    /// <typeparam name="TOut">The result type of the projection.</typeparam>
    /// <param name="resultTask">The original result task to project from.</param>
    /// <param name="bindFunc">
    /// A function that takes the original value and returns a new <see cref="Result{TOut}"/>.
    /// </param>
    /// <returns>
    /// If <paramref name="result"/> is a failure, that failure is returned.
    /// Otherwise, the result of <paramref name="bindFunc"/> is returned.
    /// </returns>
    public static async Task<Result<TOut>> Bind<TIn, TOut>(this Task<Result<TIn>> resultTask, Func<TIn, Result<TOut>> bindFunc)
    {
        var result = await resultTask;
        return Bind(result, bindFunc);
    }

    /// <summary>
    /// Asynchronously projects the value of a successful <see cref="Result{TIn}"/> into a new <see cref="Result{TOut}"/>
    /// using the specified asynchronous bind function.
    /// </summary>
    /// <typeparam name="TIn">The type of the original result value.</typeparam>
    /// <typeparam name="TOut">The result type of the projection.</typeparam>
    /// <param name="resultTask">The original result task to project from.</param>
    /// <param name="bindTaskFunc">
    /// An asynchronous function that takes the original value and returns a new <see cref="Result{TOut}"/>.
    /// </param>
    /// <returns>
    /// A task that resolves to:
    /// - The original failure if <paramref name="result"/> is a failure,
    /// - Otherwise, the result of <paramref name="bindTaskFunc"/>.
    /// </returns>
    public static async Task<Result<TOut>> BindAsync<TIn, TOut>(this Task<Result<TIn>> resultTask, Func<TIn, Task<Result<TOut>>> bindTaskFunc)
    {
        var result = await resultTask;
        return await BindAsync(result, bindTaskFunc);
    }
}
