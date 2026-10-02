using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Extensions;

public static class PredicateExtensions
{
    extension<T, TProperty>(ValidationRuleBuilder<T, TProperty> builder)
    {
        public ValidationRuleBuilder<T, TProperty> MustSatisfy(Func<TProperty, bool> predicate, string message)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            return builder.Add(predicate, message);
        }

        public ValidationRuleBuilder<T, TProperty> MustNotSatisfy(Func<TProperty, bool> predicate, string message)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            return builder.Add(value => !predicate(value), message);
        }
    }

    extension<T, TProperty>(ValidationTarget<T, TProperty> target)
    {
        public Result<T> MustSatisfy(Func<TProperty, bool> predicate, string message)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            return target.Apply(predicate, message);
        }

        public Result<T> MustNotSatisfy(Func<TProperty, bool> predicate, string message)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            return target.Apply(value => !predicate(value), message);
        }
    }
}
