using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class TimeSpanValidationTargetExtensions
{
    extension<T>(ValidationRuleBuilder<T, TimeSpan> builder)
    {
        public ValidationRuleBuilder<T, TimeSpan> AtLeast(TimeSpan min, string? message = null) =>
            builder.Add(value => ComparisonRules.AtLeast(value, min),
                message ?? $"must be at least {min}.");

        public ValidationRuleBuilder<T, TimeSpan> AtMost(TimeSpan max, string? message = null) =>
            builder.Add(value => ComparisonRules.AtMost(value, max),
                message ?? $"must be at most {max}.");

        public ValidationRuleBuilder<T, TimeSpan> Between(TimeSpan min, TimeSpan max, string? message = null) =>
            builder.Add(value => ComparisonRules.AtLeast(value, min) &&
                ComparisonRules.AtMost(value, max),
                message ?? $"must be between {min} and {max}.");

        public ValidationRuleBuilder<T, TimeSpan> Around(TimeSpan expected, TimeSpan tolerance, string? message = null) =>
            builder.Add(value => ComparisonRules.AtLeast(value, expected - tolerance) &&
                ComparisonRules.AtMost(value, expected + tolerance),
                message ?? $"must be around {expected} with a tolerance of {tolerance}.");
    }

    extension<T>(ValidationTarget<T, TimeSpan> target)
    {
        public Result<T> AtLeast(TimeSpan min, string? message = null) =>
            target.Apply(value => ComparisonRules.AtLeast(value, min),
                message ?? $"must be at least {min}.");

        public Result<T> AtMost(TimeSpan max, string? message = null) =>
            target.Apply(value => ComparisonRules.AtMost(value, max),
                message ?? $"must be at most {max}.");

        public Result<T> Between(TimeSpan min, TimeSpan max, string? message = null) =>
            target.Apply(value => ComparisonRules.AtLeast(value, min) &&
                ComparisonRules.AtMost(value, max),
                message ?? $"must be between {min} and {max}.");

        public Result<T> Around(TimeSpan expected, TimeSpan tolerance, string? message = null) =>
            target.Apply(value => ComparisonRules.AtLeast(value, expected - tolerance) &&
                ComparisonRules.AtMost(value, expected + tolerance),
                message ?? $"must be around {expected} with a tolerance of {tolerance}.");
    }
}