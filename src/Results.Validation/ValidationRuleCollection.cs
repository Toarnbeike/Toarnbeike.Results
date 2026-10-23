using System.Runtime.CompilerServices;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Rules;
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
            executor: (value, rule, message) =>
            {
                var ruleResult = rule(propertySelector(value));
                return ruleResult.IsValid
                    ? []
                    : [new ValidationFailure(propertyName, message ?? ruleResult.Message)];
            },
            propertySelector,
            propertyName,
            validator => _rules.Add(value => NestedValidation.Validate(
                validator,
                propertySelector(value),
                propertyName!)));
    }

    public ValidationRuleBuilder<T, IEnumerable<TItem>> ThatCollection<TItem>(
        Func<T, IEnumerable<TItem>> propertySelector,
        string? propertyName = null,
        [CallerArgumentExpression(nameof(propertySelector))] string? expression = null)
    {
        ArgumentNullException.ThrowIfNull(propertySelector);
        propertyName ??= ExpressionHelpers.ExtractPropertyName(expression);
        return new ValidationRuleBuilder<T, IEnumerable<TItem>>(
            registerRule: _rules.Add,
            executor: (value, rule, message) =>
            {
                var ruleResult = rule(propertySelector(value));
                return ruleResult.IsValid
                    ? []
                    : [new ValidationFailure(propertyName, message ?? ruleResult.Message)];
            },
            propertySelector,
            propertyName,
            validator => _rules.Add(value => NestedValidation.Validate(
                validator,
                propertySelector(value),
                propertyName!)));
    }

}