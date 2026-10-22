using System.Runtime.CompilerServices;
using Toarnbeike.Results.Ensure;
using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Rules;

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

        /// <summary>
        /// Creates a validation target that evaluates rules for the current value and associates failures with the
        /// specified property name.
        /// </summary>
        /// <remarks>If a custom message is not provided, the message from the rule result is used.</remarks>
        /// <param name="propertyName">The name of the property to associate with validation failures.</param>
        /// <returns>A validation target that executes rules against the current value and returns success or a validation
        /// failure.</returns>
        public RuleTarget<T, T> Validate(string propertyName)
        {
            var executor = new RuleExecutor<T, T>((value, rule, message) =>
            {
                var validationResult = rule(value);
                return validationResult.IsValid
                    ? Result.Success()
                    : new ValidationFailure(propertyName, message ?? validationResult.Message);
            });

            return new RuleTarget<T, T>(result, executor);
        }

        /// <summary>
        /// Creates a validation target for  a property of the value contained in the <see cref="Result{T}"/> using the specified property selector.
        /// This validation target can then be used to specify the validation rule for the property.
        /// All validation rules are short-circuiting, meaning that if a rule fails, subsequent rules will not be evaluated.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property to validate.</typeparam>
        /// <param name="propertySelector">A function to select the property from the value.</param>
        /// <param name="propertyName">An optional name of the property.</param>
        /// <param name="expression">Auto filled: The expression used to select the property.</param>
        /// <returns>A <see cref="RuleTarget{T, TProperty}"/> to specify the validation rule for the property.</returns>
        public RuleTarget<T, TProperty> Validate<TProperty>(Func<T, TProperty> propertySelector,
            string? propertyName = null, [CallerArgumentExpression(nameof(propertySelector))] string? expression = null)
        {
            ArgumentNullException.ThrowIfNull(propertySelector);
            propertyName ??= ExpressionHelpers.ExtractPropertyName(expression);

            var executor = new RuleExecutor<T, TProperty>((value, rule, message) =>
            {
                var validationResult = rule(propertySelector(value));
                return validationResult.IsValid
                    ? Result.Success()
                    : new ValidationFailure(propertyName, message ?? validationResult.Message);
            });

            return new RuleTarget<T, TProperty>(result, executor);
        }

        /// <summary>
        /// Creates a validation target for a collection selected from the current value.
        /// </summary>
        /// <typeparam name="TItem">The type of elements in the selected collection.</typeparam>
        /// <param name="collectionSelector">Selects the collection to validate.</param>
        /// <param name="propertyName">The property name used for validation failures. If null, the name is inferred from the selector expression.</param>
        /// <param name="expression">The captured argument expression for inferring the property name when no property name is provided.</param>
        /// <returns>A <see cref="RuleTarget{T, IEnumerable{TItem}}"/> used to define validation rules for the selected collection.</returns>
        public RuleTarget<T, IEnumerable<TItem>> ValidateCollection<TItem>(
            Func<T, IEnumerable<TItem>> collectionSelector,
            string? propertyName = null,
            [CallerArgumentExpression(nameof(collectionSelector))] string? expression = null)
        {
            ArgumentNullException.ThrowIfNull(collectionSelector);
            propertyName ??= ExpressionHelpers.ExtractPropertyName(expression);

            var executor = new RuleExecutor<T, IEnumerable<TItem>>((value, rule, message) =>
            {
                var validationResult = rule(collectionSelector(value));
                return validationResult.IsValid
                    ? Result.Success()
                    : new ValidationFailure(propertyName, message ?? validationResult.Message);
            });

            return new RuleTarget<T, IEnumerable<TItem>>(result, executor);
        }
    }
}
