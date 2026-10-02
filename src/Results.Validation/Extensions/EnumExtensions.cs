using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class EnumExtensions
{
    extension<T, TEnum>(ValidationRuleBuilder<T, TEnum> builder)
    where TEnum : struct, Enum
    {
        public ValidationRuleBuilder<T, TEnum> IsDefined(string? message = null) =>
            builder.Add(value => EnumRules.IsDefined(value),
                message ?? $"must be a defined {typeof(TEnum).Name}.");

        public ValidationRuleBuilder<T, TEnum> OneOf(TEnum[] acceptedValues, string? message = null) =>
            builder.Add(value => EnumRules.OneOf(value, acceptedValues),
                message ?? $"must be one of [{string.Join(", ", acceptedValues.Select(v => v.ToString()))}].");

        public ValidationRuleBuilder<T, TEnum> NotOneOf(TEnum[] rejectedValues, string? message = null) =>
            builder.Add(value => EnumRules.NotOneOf(value, rejectedValues),
                message ?? $"must not be one of [{string.Join(", ", rejectedValues.Select(v => v.ToString()))}].");
    }

    extension<T, TEnum>(ValidationTarget<T, TEnum> target)
        where TEnum : struct, Enum
    {
        public Result<T> IsDefined(string? message = null) =>
            target.Apply(value => EnumRules.IsDefined(value),
                message ?? $"must be a defined {typeof(TEnum).Name}.");

        public Result<T> OneOf(TEnum[] acceptedValues, string? message = null) =>
            target.Apply(value => EnumRules.OneOf(value, acceptedValues),
                message ?? $"must be one of [{string.Join(", ", acceptedValues.Select(v => v.ToString()))}].");

        public Result<T> NotOneOf(TEnum[] rejectedValues, string? message = null) =>
            target.Apply(value => EnumRules.NotOneOf(value, rejectedValues),
                message ?? $"must not be one of [{string.Join(", ", rejectedValues.Select(v => v.ToString()))}].");
    }
}
