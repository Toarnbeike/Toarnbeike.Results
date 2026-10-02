using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class DateTimeExtensions
{
    extension<T>(ValidationRuleBuilder<T, DateTime> builder)
    {
        public ValidationRuleBuilder<T, DateTime> OnOrAfter(DateTime min, string? message = null) =>
            builder.Add(date => ComparisonRules.AtLeast(date, min),
                message ?? $"must be on or after {min}.");

        public ValidationRuleBuilder<T, DateTime> OnOrBefore(DateTime max, string? message = null) =>
            builder.Add(date => ComparisonRules.AtMost(date, max),
                message ?? $"must be on or before {max}.");

        public ValidationRuleBuilder<T, DateTime> MustBeBetween(DateTime min, DateTime max, string? message = null) =>
            builder.Add(date => ComparisonRules.AtLeast(date, min) &&
                ComparisonRules.AtMost(date, max),
                message ?? $"must be between {min} and {max}.");
    }

    extension<T>(ValidationTarget<T, DateTime> target)
    {
        public Result<T> OnOrAfter(DateTime min, string? message = null) =>
            target.Apply(date => ComparisonRules.AtLeast(date, min),
                message ?? $"must be on or after {min}.");

        public Result<T> OnOrBefore(DateTime max, string? message = null) =>
            target.Apply(date => ComparisonRules.AtMost(date, max),
                message ?? $"must be on or before {max}.");

        public Result<T> MustBeBetween(DateTime min, DateTime max, string? message = null) =>
            target.Apply(date => ComparisonRules.AtLeast(date, min) &&
                ComparisonRules.AtMost(date, max),
                message ?? $"must be between {min} and {max}.");
    }
}
