using System.Collections;
using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class CollectionExtensions
{
    extension<T, TEnumerable>(ValidationRuleBuilder<T, TEnumerable> builder)
        where TEnumerable : IEnumerable
    {
        public ValidationRuleBuilder<T, TEnumerable> NotEmpty(string? message = null) =>
            builder.Add(CollectionRules.NotEmpty<TEnumerable>(message));

        public ValidationRuleBuilder<T, TEnumerable> Empty(string? message = null) =>
            builder.Add(CollectionRules.Empty<TEnumerable>(message));

        public ValidationRuleBuilder<T, TEnumerable> HasAtLeast(int min, string? message = null) =>
            builder.Add(CollectionRules.HasAtLeast<TEnumerable>(min, message));

        public ValidationRuleBuilder<T, TEnumerable> HasAtMost(int max, string? message = null) =>
            builder.Add(CollectionRules.HasAtMost<TEnumerable>(max, message));

        public ValidationRuleBuilder<T, TEnumerable> HasBetween(int min, int max, string? message = null) =>
            builder.Add(CollectionRules.HasBetween<TEnumerable>(min, max, message));

        public ValidationRuleBuilder<T, TEnumerable> HasExactly(int expected, string? message = null) =>
            builder.Add(CollectionRules.HasExactly<TEnumerable>(expected, message));
    }

    extension<T, TEnumerable>(ValidationTarget<T, TEnumerable> target)
        where TEnumerable : IEnumerable
    {
        public Result<T> NotEmpty(string? message = null) =>
            target.Apply(CollectionRules.NotEmpty<TEnumerable>(message));

        public Result<T> Empty(string? message = null) =>
            target.Apply(CollectionRules.Empty<TEnumerable>(message));

        public Result<T> HasAtLeast(int min, string? message = null) =>
            target.Apply(CollectionRules.HasAtLeast<TEnumerable>(min, message));

        public Result<T> HasAtMost(int max, string? message = null) =>
            target.Apply(CollectionRules.HasAtMost<TEnumerable>(max, message));

        public Result<T> HasBetween(int min, int max, string? message = null) =>
            target.Apply(CollectionRules.HasBetween<TEnumerable>(min, max, message));

        public Result<T> HasExactly(int expected, string? message = null) =>
            target.Apply(CollectionRules.HasExactly<TEnumerable>(expected, message));
    }
}