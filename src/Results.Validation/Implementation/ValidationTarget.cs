using Toarnbeike.Results.Extensions;

namespace Toarnbeike.Results.Validation.Implementation;

public delegate Result ValidationExecutor<T, out TProperty>(
    T value, Func<TProperty, bool> rule, string message);

public sealed class ValidationTarget<T, TProperty>(
    Result<T> result,
    ValidationExecutor<T, TProperty> executor)
{
    internal Result<T> Apply(Func<TProperty, bool> rule, string message) =>
        result.BindTap(value => executor(value, rule, message));
}