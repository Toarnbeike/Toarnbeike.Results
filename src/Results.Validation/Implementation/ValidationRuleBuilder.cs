using Toarnbeike.Results.Failures;

namespace Toarnbeike.Results.Validation.Implementation;

public delegate IEnumerable<ValidationFailure> ValidatorExecutor<T, out TProperty>(
    T value, Func<TProperty, bool> rule, string message);

public sealed class ValidationRuleBuilder<T, TProperty>(
    Action<Func<T, IEnumerable<ValidationFailure>>> registerRule,
    ValidatorExecutor<T, TProperty> executor)
{
    internal ValidationRuleBuilder<T, TProperty> Add(Func<TProperty, bool> rule, string message)
    {
        registerRule(value => executor(value, rule, message));
        return this;
    }
}