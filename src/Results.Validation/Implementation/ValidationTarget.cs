using Toarnbeike.Results.Extensions;

namespace Toarnbeike.Results.Validation.Implementation;

public delegate Result ValidationExecutor<T, TProperty>(
    T value, ValidationRule<TProperty> rule);

public sealed class ValidationTarget<T, TProperty>(
    Result<T> result,
    ValidationExecutor<T, TProperty> executor)
{
    internal Result<T> Apply(ValidationRule<TProperty> rule) =>
        result.BindTap(value => executor(value, rule));
}