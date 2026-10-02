using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class GuidExtensions
{
    extension<T>(ValidationRuleBuilder<T, Guid> builder)
    {
        public ValidationRuleBuilder<T, Guid> NotEmpty(string? message = null) =>
            builder.Add(GuidRules.NotEmpty(message));

        public ValidationRuleBuilder<T, Guid> Version4(string? message = null) =>
            builder.Add(GuidRules.Version4(message));

        public ValidationRuleBuilder<T, Guid> Version7(string? message = null) =>
            builder.Add(GuidRules.Version7(message));
    }

    extension<T>(ValidationTarget<T, Guid> target)
    {
        public Result<T> NotEmpty(string? message = null) =>
            target.Apply(GuidRules.NotEmpty(message));

        public Result<T> Version4(string? message = null) =>
            target.Apply(GuidRules.Version4(message));

        public Result<T> Version7(string? message = null) =>
            target.Apply(GuidRules.Version7(message));
    }
}
