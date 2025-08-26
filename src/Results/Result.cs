using System.Diagnostics.CodeAnalysis;

namespace Toarnbeike.Results;

/// <summary>
/// Represents the outcome of an operation that either succeeded without a value or failed with an <see cref="Results.Failure"/>.
/// </summary>
/// <remarks>
/// Use <see cref="Success"/> to create a successful result without a value,
/// or <see cref="Failure(Failure)"/> to create a failed result.
/// </remarks>
public partial class Result : IResult
{
    private readonly Failure? _failure;

    /// <inheritdoc />
    public bool IsSuccess { get; }

    /// <inheritdoc />
    public bool IsFailure => !IsSuccess;

    /// <inheritdoc />
    public bool TryGetFailure([MaybeNullWhen(false)] out Failure failure)
    {
        failure = _failure;
        return IsFailure;
    }

    /// <summary>
    /// Creates a new <see cref="Result"/> representing a successful operation.
    /// </summary>
    /// <returns>A success <see cref="Result"/> instance.</returns>
    public static Result Success() => new(true, null);

    /// <summary>
    /// Creates a new <see cref="Result"/> representing a successful operation.
    /// </summary>
    /// <returns>A success <see cref="Result"/> instance, wrapped in a task.</returns>
    public static Task<Result> SuccessTask() => Task.FromResult(Success());

    /// <summary>
    /// Creates a new <see cref="Result"/> representing a failure with the specified <paramref name="failure"/>.
    /// </summary>
    /// <param name="failure">The reason why the operation failed.</param>
    /// <returns>A failure <see cref="Result"/> instance containing the specified <paramref name="failure"/>.</returns>
    public static Result Failure(Failure failure) => new(false, failure);

    /// <summary>
    /// Creates a new successful <see cref="Result{TValue}"/> with the specified <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="value">The value to encapsulate.</param>
    /// <returns>A success <see cref="Result{TValue}"/> instance containing the specified value.</returns>
    public static Result<TValue> Success<TValue>(TValue value) => Result<TValue>.Success(value);

    /// <summary>
    /// Creates a new successful <see cref="Result{TValue}"/> with the specified <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="value">The value to encapsulate.</param>
    /// <returns>A success <see cref="Result{TValue}"/> instance containing the specified value.</returns>
    public static Task<Result<TValue>> SuccessTask<TValue>(TValue value) => Task.FromResult(Success(value));
    
    /// <summary>
    /// Implicitly converts an <see cref="Results.Failure"/> to a failed <see cref="Result"/>.
    /// </summary>
    /// <param name="failure">The failure to convert.</param>
    public static implicit operator Result(Failure failure) => Failure(failure);

    private Result(bool isSuccess, Failure? failure) => (IsSuccess, _failure) = (isSuccess, failure);
}