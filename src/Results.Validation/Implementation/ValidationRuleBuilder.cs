using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.Validation.Implementation;

public delegate IEnumerable<ValidationFailure> ValidatorExecutor<T, TProperty>(
    T value, ValidationRule<TProperty> rule);

public sealed class ValidationRuleBuilder<T, TProperty>(
    Action<Func<T, IEnumerable<ValidationFailure>>> registerRule,
    ValidatorExecutor<T, TProperty> executor)
{
    internal ValidationRuleBuilder<T, TProperty> Add(ValidationRule<TProperty> rule)
    {
        registerRule(value => executor(value, rule));
        return this;
    }
}