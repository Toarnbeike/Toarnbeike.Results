namespace Toarnbeike.Results.Extensions;

/// <summary>
/// Match: extract a value from a <see cref="Result"> or a <see cref="Result{TValue}"/>
/// by matching on success or failure.
/// </summary>
public static class MatchExtensions
{
    /// <summary>
    /// Projects the result into a value of type <typeparamref name="TOut"/> by applying one of the two provided functions,
    /// depending on whether the result is a success or a failure.
    /// </summary>
    /// <typeparam name="TOut">The type of the value returned by the match.</typeparam>
    /// <param name="result">The result to match on.</param>
    /// <param name="onSuccess">The function to apply if the result is a success.</param>
    /// <param name="onFailure">The function to apply if the result is a failure.</param>
    /// <returns>The value returned by either <paramref name="onSuccess"/> or <paramref name="onFailure"/>.</returns>
    /// <remarks>
    /// This method is useful when you want to transform a result into another value based on whether it succeeded or failed.
    /// Both match functions are required and must be non-null.
    /// </remarks>
    public static TOut Match<TOut>(this Result result, Func<TOut> onSuccess, Func<Failure, TOut> onFailure) =>
        result.TryGetFailure(out var failure)
            ? onFailure(failure)
            : onSuccess();

    /// <summary>
    /// Projects the result into a value of type <typeparamref name="TOut"/> by applying one of the two provided async functions,
    /// depending on whether the result is a success or a failure.
    /// </summary>
    /// <typeparam name="TOut">The type of the value returned by the match.</typeparam>
    /// <param name="result">The result to match on.</param>
    /// <param name="onSuccess">The async function to apply if the result is a success.</param>
    /// <param name="onFailure">The async function to apply if the result is a failure.</param>
    /// <returns>The value returned by either <paramref name="onSuccess"/> or <paramref name="onFailure"/>.</returns>
    /// <remarks>
    /// This method is useful when you want to transform a result into another value based on whether it succeeded or failed.
    /// Both match functions are required and must be non-null.
    /// </remarks>
    public static async Task<TOut> MatchAsync<TOut>(this Result result, Func<Task<TOut>> onSuccess, Func<Failure, Task<TOut>> onFailure) =>
        result.TryGetFailure(out var failure)
            ? await onFailure(failure).ConfigureAwait(false)
            : await onSuccess().ConfigureAwait(false);

    /// <summary>
    /// Projects the result into a value of type <typeparamref name="TOut"/> by applying one of the two provided functions,
    /// depending on whether the result is a success or a failure.
    /// </summary>
    /// <typeparam name="TOut">The type of the value returned by the match.</typeparam>
    /// <param name="resultTask">The async result to match on.</param>
    /// <param name="onSuccess">The function to apply if the result is a success.</param>
    /// <param name="onFailure">The function to apply if the result is a failure.</param>
    /// <returns>The value returned by either <paramref name="onSuccess"/> or <paramref name="onFailure"/>.</returns>
    /// <remarks>
    /// This method is useful when you want to transform a result into another value based on whether it succeeded or failed.
    /// Both match functions are required and must be non-null.
    /// </remarks>
    public static async Task<TOut> Match<TOut>(this Task<Result> resultTask, Func<TOut> onSuccess, Func<Failure, TOut> onFailure)
    {
        var result = await resultTask.ConfigureAwait(false);
        return Match(result, onSuccess, onFailure);
    }

    /// <summary>
    /// Projects the result into a value of type <typeparamref name="TOut"/> by applying one of the two provided async functions,
    /// depending on whether the result is a success or a failure.
    /// </summary>
    /// <typeparam name="TOut">The type of the value returned by the match.</typeparam>
    /// <param name="resultTask">The async result to match on.</param>
    /// <param name="onSuccess">The async function to apply if the result is a success.</param>
    /// <param name="onFailure">The async function to apply if the result is a failure.</param>
    /// <returns>The value returned by either <paramref name="onSuccess"/> or <paramref name="onFailure"/>.</returns>
    /// <remarks>
    /// This method is useful when you want to transform a result into another value based on whether it succeeded or failed.
    /// Both match functions are required and must be non-null.
    /// </remarks>
    public static async Task<TOut> MatchAsync<TOut>(this Task<Result> resultTask, Func<Task<TOut>> onSuccess, Func<Failure, Task<TOut>> onFailure)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await MatchAsync(result, onSuccess, onFailure).ConfigureAwait(false);
    }

    /// <summary>
    /// Projects the result into a value of type <typeparamref name="TOut"/> by applying one of the two provided functions,
    /// depending on whether the result is a success or a failure.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    /// <typeparam name="TOut">The type of the value returned by the match.</typeparam>
    /// <param name="result">The result to match on.</param>
    /// <param name="onSuccess">The function to apply if the result is a success.</param>
    /// <param name="onFailure">The function to apply if the result is a failure.</param>
    /// <returns>The value returned by either <paramref name="onSuccess"/> or <paramref name="onFailure"/>.</returns>
    /// <remarks>
    /// This method is useful when you want to transform a result into another value based on whether it succeeded or failed.
    /// Both match functions are required and must be non-null.
    /// </remarks>
    public static TOut Match<TValue, TOut>(this Result<TValue> result, Func<TValue, TOut> onSuccess, Func<Failure, TOut> onFailure) =>
        result.TryGetValue(out var value, out var failure)
            ? onSuccess(value)
            : onFailure(failure);

    /// <summary>
    /// Projects the result into a value of type <typeparamref name="TOut"/> by applying one of the two provided async functions,
    /// depending on whether the result is a success or a failure.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    /// <typeparam name="TOut">The type of the value returned by the match.</typeparam>
    /// <param name="result">The result to match on.</param>
    /// <param name="onSuccess">The async function to apply if the result is a success.</param>
    /// <param name="onFailure">The async function to apply if the result is a failure.</param>
    /// <returns>The value returned by either <paramref name="onSuccess"/> or <paramref name="onFailure"/>.</returns>
    /// <remarks>
    /// This method is useful when you want to transform a result into another value based on whether it succeeded or failed.
    /// Both match functions are required and must be non-null.
    /// </remarks>
    public static async Task<TOut> MatchAsync<TValue, TOut>(this Result<TValue> result, Func<TValue, Task<TOut>> onSuccess, Func<Failure, Task<TOut>> onFailure) =>
        result.TryGetValue(out var value, out var failure)
            ? await onSuccess(value).ConfigureAwait(false)
            : await onFailure(failure).ConfigureAwait(false);

    /// <summary>
    /// Projects the result into a value of type <typeparamref name="TOut"/> by applying one of the two provided functions,
    /// depending on whether the result is a success or a failure.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    /// <typeparam name="TOut">The type of the value returned by the match.</typeparam>
    /// <param name="resultTask">The async result to match on.</param>
    /// <param name="onSuccess">The function to apply if the result is a success.</param>
    /// <param name="onFailure">The function to apply if the result is a failure.</param>
    /// <returns>The value returned by either <paramref name="onSuccess"/> or <paramref name="onFailure"/>.</returns>
    /// <remarks>
    /// This method is useful when you want to transform a result into another value based on whether it succeeded or failed.
    /// Both match functions are required and must be non-null.
    /// </remarks>
    public static async Task<TOut> Match<TValue, TOut>(this Task<Result<TValue>> resultTask, Func<TValue, TOut> onSuccess, Func<Failure, TOut> onFailure)
    {
        var result = await resultTask.ConfigureAwait(false);
        return Match(result, onSuccess, onFailure);
    }

    /// <summary>
    /// Projects the result into a value of type <typeparamref name="TOut"/> by applying one of the two provided async functions,
    /// depending on whether the result is a success or a failure.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    /// <typeparam name="TOut">The type of the value returned by the match.</typeparam>
    /// <param name="resultTask">The async result to match on.</param>
    /// <param name="onSuccess">The async function to apply if the result is a success.</param>
    /// <param name="onFailure">The async function to apply if the result is a failure.</param>
    /// <returns>The value returned by either <paramref name="onSuccess"/> or <paramref name="onFailure"/>.</returns>
    /// <remarks>
    /// This method is useful when you want to transform a result into another value based on whether it succeeded or failed.
    /// Both match functions are required and must be non-null.
    /// </remarks>
    public static async Task<TOut> MatchAsync<TValue, TOut>(this Task<Result<TValue>> resultTask, Func<TValue, Task<TOut>> onSuccess, Func<Failure, Task<TOut>> onFailure)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await MatchAsync(result, onSuccess, onFailure).ConfigureAwait(false);
    }
}
