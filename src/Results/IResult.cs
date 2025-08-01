using System.Diagnostics.CodeAnalysis;

namespace Toarnbeike.Results;

/// <summary>
/// Represents the outcome of an operation, which can either be a success or a failure.
/// </summary>
/// <remarks>
/// If the operation failed, the failure reason can be retrieved via <see cref="TryGetFailure(out Failure)"/>.
/// </remarks>
public interface IResult
{
    /// <summary>
    /// Gets a value indicating whether the result represents a successful outcome.
    /// </summary>
    bool IsSuccess { get; }

    /// <summary>
    /// Gets a value indicating whether the result represents a failure.
    /// </summary>
    bool IsFailure { get; }

    /// <summary>
    /// Attempts to retrieve the reason why to operation failed.
    /// </summary>
    /// <param name="failure">When this method returns, contains the reason if the result is a failure; otherwise, <c>null</c>.</param>
    /// <returns><c>true</c> if the result is a failure; otherwise, <c>false</c>.</returns>
    bool TryGetFailure([MaybeNullWhen(false)] out Failure failure);
}
