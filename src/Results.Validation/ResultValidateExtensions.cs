using Toarnbeike.Results.Extensions;

namespace Toarnbeike.Results.Validation;

/// <summary>
/// Extensions for validating properties of a <see cref="Result{T}"/> using a fluent API.
/// </summary>
public static class ResultValidateExtensions
{
    extension<T>(Result<T> result)
    {
        /// <summary>
        /// Validates the value contained in the <see cref="Result{T}"/> using the specified <see cref="IValidator{T}"/>.
        /// </summary>
        /// <param name="validator">The validator to use.</param>
        /// <returns>A <see cref="Result{T}"/> binding the original result to the outcome of the validation.</returns>
        public Result<T> ValidateUsing(IValidator<T> validator)
        {
            ArgumentNullException.ThrowIfNull(validator);
            return result.Bind(validator.Validate);
        }
    }
}
