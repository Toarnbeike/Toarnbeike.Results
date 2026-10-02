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
            builder.Add(collection => ComparisonRules.AtLeast(GetCount(collection), 1),
                message ?? "must not be empty.");

        public ValidationRuleBuilder<T, TEnumerable> Empty(string? message = null) =>
            builder.Add(collection => ComparisonRules.Equal(GetCount(collection), 0),
                message ?? "must be empty.");

        public ValidationRuleBuilder<T, TEnumerable> HasAtLeast(int min, string? message = null) =>
            builder.Add(collection => ComparisonRules.AtLeast(GetCount(collection), min),
                message ?? $"must contain at least {min} element(s).");

        public ValidationRuleBuilder<T, TEnumerable> HasAtMost(int max, string? message = null) =>
            builder.Add(collection => ComparisonRules.AtMost(GetCount(collection), max),
                message ?? $"must contain at most {max} element(s).");

        public ValidationRuleBuilder<T, TEnumerable> HasBetween(int min, int max, string? message = null) =>
            builder.Add(collection =>
            {
                var actualCount = GetCount(collection);
                return ComparisonRules.AtLeast(actualCount, min) &&
                    ComparisonRules.AtMost(actualCount, max);
            }, message ?? $"must contain between {min} and {max} elements.");

        public ValidationRuleBuilder<T, TEnumerable> HasExactly(int expected, string? message = null) =>
            builder.Add(collection => ComparisonRules.Equal(GetCount(collection), expected),
                message ?? $"collection must contain exactly {expected} element(s).");
    }

    extension<T, TEnumerable>(ValidationTarget<T, TEnumerable> target)
        where TEnumerable : IEnumerable
    {
        public Result<T> NotEmpty(string? message = null) =>
            target.Apply(collection => ComparisonRules.AtLeast(GetCount(collection), 1), 
                message ?? "must not be empty.");

        public Result<T> Empty(string? message = null) =>
            target.Apply(collection => ComparisonRules.Equal(GetCount(collection), 0), 
                message ?? "must be empty.");

        public Result<T> HasAtLeast(int min, string? message = null) =>
            target.Apply(collection => ComparisonRules.AtLeast(GetCount(collection), min),
                message ?? $"must contain at least {min} element(s).");

        public Result<T> HasAtMost(int max, string? message = null) =>
            target.Apply(collection => ComparisonRules.AtMost(GetCount(collection), max), 
                message ?? $"must contain at most {max} element(s).");

        public Result<T> HasBetween(int min, int max, string? message = null) =>
            target.Apply(collection =>
            {
                var actualCount = GetCount(collection);
                return ComparisonRules.AtLeast(actualCount, min) &&
                    ComparisonRules.AtMost(actualCount, max);
            }, message ?? $"must contain between {min} and {max} elements.");

        public Result<T> HasExactly(int expected, string? message = null) =>
            target.Apply(collection => ComparisonRules.Equal(GetCount(collection), expected),
                message ?? $"collection must contain exactly {expected} element(s).");
    }

    private static int GetCount(IEnumerable target)
    {
        if (target is ICollection collection)
        {
            return collection.Count;
        }

        return target.Cast<object?>().ToArray().Length;
    }
}