using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class PredicateExtensions
{
    extension<T, TProperty>(ValidationRuleBuilder<T, TProperty> builder)
    {
        public ValidationRuleBuilder<T, TProperty> MustSatisfy(Func<TProperty, bool> predicate, string message) =>
            builder.Add(PredicateRules.MustSatisfy(predicate, message));

        public ValidationRuleBuilder<T, TProperty> MustNotSatisfy(Func<TProperty, bool> predicate, string message) =>
            builder.Add(PredicateRules.MustNotSatisfy(predicate, message));
    }

    extension<T, TProperty>(ValidationTarget<T, TProperty> target)
    {
        public Result<T> MustSatisfy(Func<TProperty, bool> predicate, string message) =>
            target.Apply(PredicateRules.MustSatisfy(predicate, message));

        public Result<T> MustNotSatisfy(Func<TProperty, bool> predicate, string message) =>
            target.Apply(PredicateRules.MustNotSatisfy(predicate, message));
    }
}