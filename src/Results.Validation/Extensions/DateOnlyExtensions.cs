using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class DateOnlyExtensions
{
    extension<T>(ValidationRuleBuilder<T, DateOnly> builder)
    {
        public ValidationRuleBuilder<T, DateOnly> OnOrAfter(DateOnly min, string? message = null) =>
            builder.Add(DateRules.OnOrAfter(min, message));

        public ValidationRuleBuilder<T, DateOnly> OnOrBefore(DateOnly max, string? message = null) =>
            builder.Add(DateRules.OnOrBefore(max, message));

        public ValidationRuleBuilder<T, DateOnly> MustBeBetween(DateOnly min, DateOnly max, string? message = null) =>
            builder.Add(DateRules.MustBeBetween(min, max, message));
    }

    extension<T>(ValidationTarget<T, DateOnly> target)
    {
        public Result<T> OnOrAfter(DateOnly min, string? message = null) =>
            target.Apply(DateRules.OnOrAfter(min, message));

        public Result<T> OnOrBefore(DateOnly max, string? message = null) =>
            target.Apply(DateRules.OnOrBefore(max, message));

        public Result<T> MustBeBetween(DateOnly min, DateOnly max, string? message = null) =>
            target.Apply(DateRules.MustBeBetween(min, max, message));
    }
}