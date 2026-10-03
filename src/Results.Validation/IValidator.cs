using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.Validation;

/// <summary>
/// Defines a validator that can validate a value of type <typeparamref name="T"/> and return a <see cref="Result{T}"/> indicating whether the validation was successful or not.
/// </summary>
/// <typeparam name="T">The type of value to validate.</typeparam>
public interface IValidator<T>
{
    /// <summary>
    /// Validates the specified value.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> indicating whether the validation was successful or not.
    /// Uses a <see cref="ValidationFailureSummary"/> to aggregate all failures.
    /// </returns>
    Result<T> Validate(T value);
}
