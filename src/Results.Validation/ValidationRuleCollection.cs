using System.Runtime.CompilerServices;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation;

/// <summary>
/// Represents a collection of validation rules for a specific type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class ValidationRuleCollection<T>
{
    private readonly List<Func<T, IEnumerable<ValidationFailure>>> _rules = [];
    public IReadOnlyList<Func<T, IEnumerable<ValidationFailure>>> Build() => _rules;

    /// <summary>
    /// Creates a validation rule builder for a specific property of type <typeparamref name="TProperty"/> of the type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property to validate.</typeparam>
    /// <param name="propertySelector">A function to select the property from the value.</param>
    /// <param name="propertyName">An optional name of the property.</param>
    /// <param name="expression">Auto filled: The expression used to select the property.</param>
    /// <returns>A <see cref="ValidationRuleBuilder{T, TProperty}"/> to specify the validation rules for the property.</returns>
    public ValidationRuleBuilder<T, TProperty> That<TProperty>(
    Func<T, TProperty> propertySelector,
    string? propertyName = null,
    [CallerArgumentExpression(nameof(propertySelector))] string? expression = null)
    {
        ArgumentNullException.ThrowIfNull(propertySelector);
        propertyName ??= ExpressionHelpers.ExtractPropertyName(expression);

        return new ValidationRuleBuilder<T, TProperty>(
            registerRule: _rules.Add,
            executor: (value, rule) =>
            {
                var propertyValue = propertySelector(value);
                return rule.Predicate(propertyValue)
                    ? []
                    : [new ValidationFailure(propertyName, rule.Message)];
            });
    }

    /// <summary>
    /// Creates a validation rule builder for all items in a collection property of type <typeparamref name="TProperty"/> of the type <typeparamref name="T"/>.
    /// The validation rule builder will determine if all items in the collection satisfy the specified validation rule.
    /// A failure for each item that does not satisfy the rule will be returned.
    /// </summary>
    /// <typeparam name="TProperty">The type of the items in the collection to validate.</typeparam>
    /// <param name="propertySelector">A function to select the collection from the value.</param>
    /// <param name="propertyName">An optional name of the property.</param>
    /// <param name="expression">Auto filled: The expression used to select the collection.</param>
    /// <returns>A <see cref="ValidationRuleBuilder{T, TProperty}"/> to specify the validation rules for the items in the collection.</returns>
    public ValidationRuleBuilder<T, TProperty> ThatAll<TProperty>(
        Func<T, IEnumerable<TProperty>> propertySelector,
        string? propertyName = null,
        [CallerArgumentExpression(nameof(propertySelector))] string? expression = null)
    {
        ArgumentNullException.ThrowIfNull(propertySelector);
        propertyName ??= ExpressionHelpers.ExtractPropertyName(expression);

        return new ValidationRuleBuilder<T, TProperty>(
            registerRule: _rules.Add,
            executor: (value, rule) =>
            {
                var failures = new List<ValidationFailure>();
                var index = 0;
                foreach (var item in propertySelector(value))
                {
                    if (!rule.Predicate(item))
                    {
                        failures.Add(new ValidationFailure($"{propertyName}[{index}]", rule.Message));
                    }

                    index++;
                }

                return failures;
            });
    }
}