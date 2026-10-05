using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Validation.Implementation;

public delegate IEnumerable<ValidationFailure> ValidatorExecutor<T, TProperty>(
    T value, Rule<TProperty> rule);

public sealed class ValidationRuleBuilder<T, TProperty>(
    Action<Func<T, IEnumerable<ValidationFailure>>> registerRule,
    ValidatorExecutor<T, TProperty> executor)
{
    internal ValidationRuleBuilder<T, TProperty> Add(Rule<TProperty> rule)
    {
        registerRule(value => executor(value, rule));
        return this;
    }
}