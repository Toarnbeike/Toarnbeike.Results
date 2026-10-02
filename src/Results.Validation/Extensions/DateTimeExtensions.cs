using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class DateTimeExtensions
{
    extension<T>(ValidationRuleBuilder<T, DateTime> builder)
    {
        public ValidationRuleBuilder<T, DateTime> OnOrAfter(DateTime min, string? message = null) =>
            builder.Add(DateRules.OnOrAfter(min, message));

        public ValidationRuleBuilder<T, DateTime> OnOrBefore(DateTime max, string? message = null) =>
            builder.Add(DateRules.OnOrBefore(max, message));

        public ValidationRuleBuilder<T, DateTime> MustBeBetween(DateTime min, DateTime max, string? message = null) =>
            builder.Add(DateRules.MustBeBetween(min, max, message));
    }

    extension<T>(ValidationTarget<T, DateTime> target)
    {
        public Result<T> OnOrAfter(DateTime min, string? message = null) =>
            target.Apply(DateRules.OnOrAfter(min, message));

        public Result<T> OnOrBefore(DateTime max, string? message = null) =>
            target.Apply(DateRules.OnOrBefore(max, message));

        public Result<T> MustBeBetween(DateTime min, DateTime max, string? message = null) =>
            target.Apply(DateRules.MustBeBetween(min, max, message));
    }
}