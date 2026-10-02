using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class TimeSpanExtensions
{
    extension<T>(ValidationRuleBuilder<T, TimeSpan> builder)
    {
        public ValidationRuleBuilder<T, TimeSpan> AtLeast(TimeSpan min, string? message = null) =>
            builder.Add(TimeSpanRules.AtLeast(min, message));

        public ValidationRuleBuilder<T, TimeSpan> AtMost(TimeSpan max, string? message = null) =>
            builder.Add(TimeSpanRules.AtMost(max, message));

        public ValidationRuleBuilder<T, TimeSpan> Between(TimeSpan min, TimeSpan max, string? message = null) =>
            builder.Add(TimeSpanRules.MustBeBetween(min, max, message));

        public ValidationRuleBuilder<T, TimeSpan> Around(TimeSpan expected, TimeSpan tolerance, string? message = null) =>
            builder.Add(TimeSpanRules.Around(expected, tolerance, message));
    }

    extension<T>(ValidationTarget<T, TimeSpan> target)
    {
        public Result<T> AtLeast(TimeSpan min, string? message = null) =>
            target.Apply(TimeSpanRules.AtLeast(min, message));

        public Result<T> AtMost(TimeSpan max, string? message = null) =>
            target.Apply(TimeSpanRules.AtMost(max, message));

        public Result<T> Between(TimeSpan min, TimeSpan max, string? message = null) =>
            target.Apply(TimeSpanRules.MustBeBetween(min, max, message));

        public Result<T> Around(TimeSpan expected, TimeSpan tolerance, string? message = null) =>
            target.Apply(TimeSpanRules.Around(expected, tolerance, message));
    }
}