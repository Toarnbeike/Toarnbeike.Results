using System.Runtime.CompilerServices;
using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation;

public abstract class Validator<T> : IValidator<T>
{
    private readonly List<Func<T, IEnumerable<ValidationFailure>>> _rules = [];

    public Result<T> Validate(T value)
    {
        var failures = IterateRules(value).ToList();
        return failures.Count == 0 
            ? value 
            : new ValidationFailureSummary(failures);
    }

    protected ValidationRuleBuilder<T, TProperty> That<TProperty>(
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

    protected ValidationRuleBuilder<T, TProperty> ThatAll<TProperty>(
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

    private IEnumerable<ValidationFailure> IterateRules(T value)
    {
        foreach (var rule in _rules)
        {
            foreach (var failure in rule(value))
            {
                yield return failure;
            }
        }
    }
}