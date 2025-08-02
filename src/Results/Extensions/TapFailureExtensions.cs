namespace Toarnbeike.Results.Extensions;

/// <summary>
/// TapFailure: Extension method for executing side-effects on a failing <see cref="Result"/> or <see cref="Result{TValue}"/>, 
/// without modifying the result.
/// </summary>
public static class TapFailureExtensions
{
    /// <summary>
    /// Executes the specified <paramref name="onFailure"/> action if the result is a failure.
    /// </summary>
    /// <param name="result">The result to inspect.</param>
    /// <param name="onFailure">The side-effect to perform if the result is a failure.</param>
    /// <returns>The original result instance.</returns>
    /// <remarks>
    /// Use this method to perform a side-effect (e.g., logging) when the result is a failure.
    /// The error of the result is not modified.
    /// </remarks>
    public static Result TapFailure(this Result result, Action<Failure> onFailure)
    {
        if (result.IsFailure && result.TryGetFailure(out var failure))
        {
            onFailure(failure);
        }

        return result;
    }

    /// <summary>
    /// Executes the specified <paramref name="onFailure"/> action if the result is a failure.
    /// </summary>
    /// <param name="result">The result to inspect.</param>
    /// <param name="onFailure">The async side-effect to perform if the result is a failure.</param>
    /// <returns>The original result instance.</returns>
    /// <remarks>
    /// Use this method to perform a side-effect (e.g., logging) when the result is a failure.
    /// The error of the result is not modified.
    /// </remarks>
    public static async Task<Result> TapFailureAsync(this Result result, Func<Failure, Task> onFailure)
    {
        if (result.IsFailure && result.TryGetFailure(out var failure))
        {
            await onFailure(failure);
        }

        return result;
    }

    /// <summary>
    /// Executes the specified <paramref name="onFailure"/> action if the result is a failure.
    /// </summary>
    /// <param name="resultTask">The async result to inspect.</param>
    /// <param name="onFailure">The side-effect to perform if the result is a failure.</param>
    /// <returns>The original result instance.</returns>
    /// <remarks>
    /// Use this method to perform a side-effect (e.g., logging) when the result is a failure.
    /// The error of the result is not modified.
    /// </remarks>
    public static async Task<Result> TapFailure(this Task<Result> resultTask, Action<Failure> onFailure)
    {
        var result = await resultTask;
        return TapFailure(result, onFailure);
    }

    /// <summary>
    /// Executes the specified <paramref name="onFailure"/> action if the result is a failure.
    /// </summary>
    /// <param name="resultTask">The async result to inspect.</param>
    /// <param name="onFailure">The async side-effect to perform if the result is a failure.</param>
    /// <returns>The original result instance.</returns>
    /// <remarks>
    /// Use this method to perform a side-effect (e.g., logging) when the result is a failure.
    /// The error of the result is not modified.
    /// </remarks>
    public static async Task<Result> TapFailureAsync(this Task<Result> resultTask, Func<Failure, Task> onFailure)
    {
        var result = await resultTask;
        return await TapFailureAsync(result, onFailure);
    }

    /// <summary>
    /// Executes the specified <paramref name="onFailure"/> action if the result is a failure.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    /// <param name="result">The result to inspect.</param>
    /// <param name="onFailure">The side-effect to perform if the result is a failure.</param>
    /// <returns>The original result instance.</returns>
    /// <remarks>
    /// Use this method to perform a side-effect (e.g., logging) when the result is a failure.
    /// The error of the result is not modified.
    /// </remarks>
    public static Result<TValue> TapFailure<TValue>(this Result<TValue> result, Action<Failure> onFailure)
    {
        if (result.IsFailure && result.TryGetFailure(out var failure))
        {
            onFailure(failure);
        }

        return result;
    }

    /// <summary>
    /// Executes the specified <paramref name="onFailure"/> action if the result is a failure.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    /// <param name="result">The result to inspect.</param>
    /// <param name="onFailure">The async side-effect to perform if the result is a failure.</param>
    /// <returns>The original result instance.</returns>
    /// <remarks>
    /// Use this method to perform a side-effect (e.g., logging) when the result is a failure.
    /// The error of the result is not modified.
    /// </remarks>
    public static async Task<Result<TValue>> TapFailureAsync<TValue>(this Result<TValue> result, Func<Failure, Task> onFailure)
    {
        if (result.IsFailure && result.TryGetFailure(out var failure))
        {
            await onFailure(failure);
        }

        return result;
    }

    /// <summary>
    /// Executes the specified <paramref name="onFailure"/> action if the result is a failure.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    /// <param name="resultTask">The async result to inspect.</param>
    /// <param name="onFailure">The side-effect to perform if the result is a failure.</param>
    /// <returns>The original result instance.</returns>
    /// <remarks>
    /// Use this method to perform a side-effect (e.g., logging) when the result is a failure.
    /// The error of the result is not modified.
    /// </remarks>
    public static async Task<Result<TValue>> TapFailure<TValue>(this Task<Result<TValue>> resultTask, Action<Failure> onFailure)
    {
        var result = await resultTask;
        return TapFailure(result, onFailure);
    }

    /// <summary>
    /// Executes the specified <paramref name="onFailure"/> action if the result is a failure.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    /// <param name="resultTask">The async result to inspect.</param>
    /// <param name="onFailure">The async side-effect to perform if the result is a failure.</param>
    /// <returns>The original result instance.</returns>
    /// <remarks>
    /// Use this method to perform a side-effect (e.g., logging) when the result is a failure.
    /// The error of the result is not modified.
    /// </remarks>
    public static async Task<Result<TValue>> TapFailureAsync<TValue>(this Task<Result<TValue>> resultTask, Func<Failure, Task> onFailure)
    {
        var result = await resultTask;
        return await TapFailureAsync(result, onFailure);
    }
}