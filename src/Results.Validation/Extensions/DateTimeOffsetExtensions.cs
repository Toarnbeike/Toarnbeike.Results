using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class DateTimeOffsetExtensions
{
    extension<T>(ValidationRuleBuilder<T, DateTimeOffset> builder)
    {
        public ValidationRuleBuilder<T, DateTimeOffset> OnOrAfter(DateTimeOffset min, string? message = null) =>
            builder.Add(date => ComparisonRules.AtLeast(date, min),
                message ?? $"must be on or after {min}.");

        public ValidationRuleBuilder<T, DateTimeOffset> OnOrBefore(DateTimeOffset max, string? message = null) =>
            builder.Add(date => ComparisonRules.AtMost(date, max),
                message ?? $"must be on or before {max}.");

        public ValidationRuleBuilder<T, DateTimeOffset> MustBeBetween(DateTimeOffset min, DateTimeOffset max, string? message = null) =>
            builder.Add(date => ComparisonRules.AtLeast(date, min) &&
                ComparisonRules.AtMost(date, max),
                message ?? $"must be between {min} and {max}.");
    }

    extension<T>(ValidationTarget<T, DateTimeOffset> target)
    {
        public Result<T> OnOrAfter(DateTimeOffset min, string? message = null) =>
            target.Apply(date => ComparisonRules.AtLeast(date, min),
                message ?? $"must be on or after {min}.");

        public Result<T> OnOrBefore(DateTimeOffset max, string? message = null) =>
            target.Apply(date => ComparisonRules.AtMost(date, max),
                message ?? $"must be on or before {max}.");

        public Result<T> MustBeBetween(DateTimeOffset min, DateTimeOffset max, string? message = null) =>
            target.Apply(date => ComparisonRules.AtLeast(date, min) &&
                ComparisonRules.AtMost(date, max),
                message ?? $"must be between {min} and {max}.");
    }
}
