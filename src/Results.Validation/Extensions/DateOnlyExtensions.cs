using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class DateOnlyExtensions
{
    extension<T>(ValidationRuleBuilder<T, DateOnly> builder)
    {
        public ValidationRuleBuilder<T, DateOnly> OnOrAfter(DateOnly min, string? message = null) =>
            builder.Add(date => ComparisonRules.AtLeast(date, min),
                message ?? $"must be on or after {min}.");

        public ValidationRuleBuilder<T, DateOnly> OnOrBefore(DateOnly max, string? message = null) =>
            builder.Add(date => ComparisonRules.AtMost(date, max),
                message ?? $"must be on or before {max}.");

        public ValidationRuleBuilder<T, DateOnly> MustBeBetween(DateOnly min, DateOnly max, string? message = null) =>
            builder.Add(date => ComparisonRules.AtLeast(date, min) &&
                ComparisonRules.AtMost(date, max),
                message ?? $"must be between {min} and {max}.");
    }

    extension<T>(ValidationTarget<T, DateOnly> target)
    {
        public Result<T> OnOrAfter(DateOnly min, string? message = null) =>
            target.Apply(date => ComparisonRules.AtLeast(date, min),
                message ?? $"must be on or after {min}.");

        public Result<T> OnOrBefore(DateOnly max, string? message = null) =>
            target.Apply(date => ComparisonRules.AtMost(date, max),
                message ?? $"must be on or before {max}.");

        public Result<T> MustBeBetween(DateOnly min, DateOnly max, string? message = null) =>
            target.Apply(date => ComparisonRules.AtLeast(date, min) &&
                ComparisonRules.AtMost(date, max),
                message ?? $"must be between {min} and {max}.");
    }
}
