using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class EnumExtensions
{
    extension<T, TEnum>(ValidationRuleBuilder<T, TEnum> builder)
    where TEnum : struct, Enum
    {
        public ValidationRuleBuilder<T, TEnum> IsDefined(string? message = null) =>
            builder.Add(EnumRules.IsDefined<TEnum>(message));

        public ValidationRuleBuilder<T, TEnum> OneOf(TEnum[] acceptedValues, string? message = null) =>
            builder.Add(EnumRules.OneOf(acceptedValues, message));

        public ValidationRuleBuilder<T, TEnum> NotOneOf(TEnum[] rejectedValues, string? message = null) =>
            builder.Add(EnumRules.NotOneOf(rejectedValues, message));
    }

    extension<T, TEnum>(ValidationTarget<T, TEnum> target)
        where TEnum : struct, Enum
    {
        public Result<T> IsDefined(string? message = null) =>
            target.Apply(EnumRules.IsDefined<TEnum>(message));

        public Result<T> OneOf(TEnum[] acceptedValues, string? message = null) =>
            target.Apply(EnumRules.OneOf(acceptedValues, message));

        public Result<T> NotOneOf(TEnum[] rejectedValues, string? message = null) =>
            target.Apply(EnumRules.NotOneOf(rejectedValues, message));
    }
}
