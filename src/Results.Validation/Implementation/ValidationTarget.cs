using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Implementation;

public delegate Result ValidationExecutor<T, out TProperty>(T value, IValidationRule<TProperty> rule);

public sealed class ValidationTarget<T, TProperty>(
    Result<T> result,
    ValidationExecutor<T, TProperty> executor)
{
    private readonly Result<T> _result = result;
    private readonly ValidationExecutor<T, TProperty> _executor = executor;

    internal Result<T> Apply(IValidationRule<TProperty> rule) =>
        _result.BindTap(value => _executor(value, rule));
}