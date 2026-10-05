using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Rules;

namespace Toarnbeike.Results.Ensure;

public delegate Result EnsureExecutor<T, TProperty>(T value, Rule<TProperty> rule);

public sealed class EnsureTarget<T, TProperty>(
    Result<T> result, 
    EnsureExecutor<T, TProperty> executor)
{
    internal Result<T> Apply(Rule<TProperty> rule) =>
        result.BindTap(value => executor(value, rule));
}
