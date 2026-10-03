using System.Runtime.CompilerServices;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation;

public sealed class ValidationRuleCollection<T>
{
    private readonly List<Func<T, IEnumerable<ValidationFailure>>> _rules = [];
    public IReadOnlyList<Func<T, IEnumerable<ValidationFailure>>> Build() => _rules;

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