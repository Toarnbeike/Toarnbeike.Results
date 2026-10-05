using System.Runtime.CompilerServices;
using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Rules.Helpers;
using Toarnbeike.Results.Validation.Implementation;

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
        /// Creates a validation target for  a property of the value contained in the <see cref="Result{T}"/> using the specified property selector.
        /// This validation target can then be used to specify the validation rule for the property.
        /// All validation rules are short-circuiting, meaning that if a rule fails, subsequent rules will not be evaluated.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property to validate.</typeparam>
        /// <param name="propertySelector">A function to select the property from the value.</param>
        /// <param name="propertyName">An optional name of the property.</param>
        /// <param name="expression">Auto filled: The expression used to select the property.</param>
        /// <returns>A <see cref="ValidationTarget{T, TProperty}"/> to specify the validation rule for the property.</returns>
        public ValidationTarget<T, TProperty> Validate<TProperty>(
        Func<T, TProperty> propertySelector,
        string? propertyName = null,
        [CallerArgumentExpression(nameof(propertySelector))] string? expression = null)
        {
            ArgumentNullException.ThrowIfNull(propertySelector);
            propertyName ??= ExpressionHelpers.ExtractPropertyName(expression);

            var executor = new ValidationExecutor<T, TProperty>((value, rule) =>
                rule.Predicate(propertySelector(value))
                    ? Result.Success()
                    : new ValidationFailure(propertyName, rule.Message));

            return new ValidationTarget<T, TProperty>(result, executor);
        }

        /// <summary>
        /// Creates a validation target for all items in a collection property of the value contained in the <see cref="Result{T}"/> using the specified collection selector.
        /// This validation target can then be used to specify the validation rule for the items in the collection.
        /// All validation rules are short-circuiting, meaning that if a rule fails, subsequent rules will not be evaluated.
        /// Also when a single item in the collection fails the validation, the validation will return with the first encountered failure.
        /// </summary>
        /// <typeparam name="TItem">The type of the items in the collection to validate.</typeparam>
        /// <param name="collectionSelector">A function to select the collection from the value.</param>
        /// <param name="propertyName">An optional name of the property.</param>
        /// <param name="expression">Auto filled: The expression used to select the collection.</param>
        /// <returns>A <see cref="ValidationTarget{T, TItem}"/> to specify the validation rule for the items in the collection.</returns>
        public ValidationTarget<T, TItem> ValidateAll<TItem>(
            Func<T, IEnumerable<TItem>> collectionSelector,
            string? propertyName = null,
            [CallerArgumentExpression(nameof(collectionSelector))] string? expression = null)
        {
            ArgumentNullException.ThrowIfNull(collectionSelector);
            propertyName ??= ExpressionHelpers.ExtractPropertyName(expression);

            var executor = new ValidationExecutor<T, TItem>((value, rule) =>
            {
                var collection = collectionSelector(value);
                var index = 0;
                foreach (var item in collection)
                {
                    if (!rule.Predicate(item))
                    {
                        return new ValidationFailure($"{propertyName}[{index}]", rule.Message);
                    }
                    index++;
                }
                return Result.Success();
            });

            return new ValidationTarget<T, TItem>(result, executor);
        }

        /// <summary>
        /// Creates a validation target for all items in a collection property of the value contained in the <see cref="Result{T}"/> using the specified collection selector.
        /// The validation target will determine if at least one item in the collection satisfies the specified validation rule.
        /// If no items in the collection satisfy the rule, the validation will return a failure with the specified message.
        /// </summary>
        /// <typeparam name="TItem">The type of the items in the collection to validate.</typeparam>
        /// <param name="collectionSelector">A function to select the collection from the value.</param>
        /// <param name="propertyName">An optional name of the property.</param>
        /// <param name="expression">Auto filled: The expression used to select the collection.</param>
        /// <returns>A <see cref="ValidationTarget{T, TItem}"/> representing the validation target for any item in the collection.</returns>
        public ValidationTarget<T, TItem> ValidateAny<TItem>(
            Func<T, IEnumerable<TItem>> collectionSelector,
            string? propertyName = null,
            [CallerArgumentExpression(nameof(collectionSelector))] string? expression = null)
        {
            ArgumentNullException.ThrowIfNull(collectionSelector);
            propertyName ??= ExpressionHelpers.ExtractPropertyName(expression);
            var executor = new ValidationExecutor<T, TItem>((value, rule) =>
            {
                var collection = collectionSelector(value);
                return collection.Any(item => rule.Predicate(item))
                    ? Result.Success()
                    : new ValidationFailure(propertyName, $"No items in the collection satisfied the rule: {rule.Message}");
            });
            return new ValidationTarget<T, TItem>(result, executor);
        }
    }
}
