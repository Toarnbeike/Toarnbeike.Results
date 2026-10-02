using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Validation.Rules.Numbers;

namespace Toarnbeike.Results.Validation.Extensions;

public static class ComparisonExtensions
{
    public static Result<T> AtLeast<T, TProperty>(
        this ValidationTarget<T, TProperty> target, TProperty minimum)
        where TProperty : IComparable<TProperty> =>
        target.Apply(new AtLeastRule<TProperty>(minimum));
}