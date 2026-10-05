using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Validation.Implementation;

public delegate Result ValidationExecutor<T, TProperty>(
    T value, Rule<TProperty> rule);

public sealed class ValidationTarget<T, TProperty>(
    Result<T> result,
    ValidationExecutor<T, TProperty> executor)
{
    internal Result<T> Apply(Rule<TProperty> rule) =>
        result.BindTap(value => executor(value, rule));
}