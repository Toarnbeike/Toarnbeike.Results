using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class DateTimeOffsetExtensions
{
    extension<T>(ValidationRuleBuilder<T, DateTimeOffset> builder)
    {
        public ValidationRuleBuilder<T, DateTimeOffset> OnOrAfter(DateTimeOffset min, string? message = null) =>
            builder.Add(DateRules.OnOrAfter(min, message));

        public ValidationRuleBuilder<T, DateTimeOffset> OnOrBefore(DateTimeOffset max, string? message = null) =>
            builder.Add(DateRules.OnOrBefore(max, message));

        public ValidationRuleBuilder<T, DateTimeOffset> MustBeBetween(DateTimeOffset min, DateTimeOffset max, string? message = null) =>
            builder.Add(DateRules.MustBeBetween(min, max, message));
    }

    extension<T>(ValidationTarget<T, DateTimeOffset> target)
    {
        public Result<T> OnOrAfter(DateTimeOffset min, string? message = null) =>
            target.Apply(DateRules.OnOrAfter(min, message));

        public Result<T> OnOrBefore(DateTimeOffset max, string? message = null) =>
            target.Apply(DateRules.OnOrBefore(max, message));

        public Result<T> MustBeBetween(DateTimeOffset min, DateTimeOffset max, string? message = null) =>
            target.Apply(DateRules.MustBeBetween(min, max, message));
    }
}