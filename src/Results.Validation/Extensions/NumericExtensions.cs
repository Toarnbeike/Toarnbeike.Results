using System.Numerics;
using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class NumericExtensions
{
    extension<T, TNumeric>(ValidationRuleBuilder<T, TNumeric> builder)
    where TNumeric : struct, INumber<TNumeric>
    {
        public ValidationRuleBuilder<T, TNumeric> GreaterThan(TNumeric min, string? message = null) =>
            builder.Add(NumericRules.GreaterThan(min, message));

        public ValidationRuleBuilder<T, TNumeric> LessThan(TNumeric max, string? message = null) =>
            builder.Add(NumericRules.LessThan(max, message));

        public ValidationRuleBuilder<T, TNumeric> Positive(string? message = null) =>
            builder.Add(NumericRules.Positive<TNumeric>(message));
    }

    extension<T, TNumeric>(ValidationTarget<T, TNumeric> target)
        where TNumeric : struct, INumber<TNumeric>
    {
        public Result<T> GreaterThan(TNumeric min, string? message = null) =>
            target.Apply(NumericRules.GreaterThan(min, message));

        public Result<T> LessThan(TNumeric max, string? message = null) =>
            target.Apply(NumericRules.LessThan(max, message));

        public Result<T> Positive(string? message = null) =>
            target.Apply(NumericRules.Positive<TNumeric>(message));
    }
}
