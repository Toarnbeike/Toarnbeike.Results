using System.Numerics;
using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class NumericExtensions
{
    extension<T, TNumeric>(ValidationRuleBuilder<T, TNumeric> target)
    where TNumeric : struct, INumber<TNumeric>
    {
        public ValidationRuleBuilder<T, TNumeric> GreaterThan(TNumeric min, string? message = null) =>
            target.Add(value => ComparisonRules.GreaterThan(value, min),
                message ?? $"must be greater than {min}");

        public ValidationRuleBuilder<T, TNumeric> LessThan(TNumeric max, string? message = null) =>
            target.Add(value => ComparisonRules.LessThan(value, max),
                message ?? $"must be less than {max}");

        public ValidationRuleBuilder<T, TNumeric> Positive(string? message = null) =>
            target.Add(value => ComparisonRules.GreaterThan(value, TNumeric.Zero),
                message ?? "must be positive");
    }

    extension<T, TNumeric>(ValidationTarget<T, TNumeric> target)
        where TNumeric : struct, INumber<TNumeric>
    {
        public Result<T> GreaterThan(TNumeric min, string? message = null) =>
            target.Apply(value => ComparisonRules.GreaterThan(value, min),
                message ?? $"must be greater than {min}");

        public Result<T> LessThan(TNumeric max, string? message = null) =>
            target.Apply(value => ComparisonRules.LessThan(value, max),
                message ?? $"must be less than {max}");

        public Result<T> Positive(string? message = null) =>
            target.Apply(value => ComparisonRules.GreaterThan(value, TNumeric.Zero),
                message ?? "must be positive");
    }
}
