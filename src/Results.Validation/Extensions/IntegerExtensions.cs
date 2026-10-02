using System.Numerics;
using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class IntegerExtensions
{
    extension<T, TInteger>(ValidationRuleBuilder<T, TInteger> builder)
    where TInteger : struct, IBinaryInteger<TInteger>
    {
        public ValidationRuleBuilder<T, TInteger> AtLeast(TInteger min, string? message = null) =>
            builder.Add(IntegerRules.AtLeast(min, message));
        public ValidationRuleBuilder<T, TInteger> AtMost(TInteger max, string? message = null) =>
            builder.Add(IntegerRules.AtMost(max, message));

        public ValidationRuleBuilder<T, TInteger> Between(TInteger min, TInteger max, string? message = null) =>
            builder.Add(IntegerRules.Between(min, max, message));

        public ValidationRuleBuilder<T, TInteger> MultipleOf(TInteger factor, string? message = null) =>
            builder.Add(IntegerRules.MultipleOf(factor, message));
    }

    extension<T, TInteger>(ValidationTarget<T, TInteger> target)
        where TInteger : struct, IBinaryInteger<TInteger>
    {
        public Result<T> AtLeast(TInteger min, string? message = null) =>
            target.Apply(IntegerRules.AtLeast(min, message));

        public Result<T> AtMost(TInteger max, string? message = null) =>
            target.Apply(IntegerRules.AtMost(max, message));
        public Result<T> Between(TInteger min, TInteger max, string? message = null) =>
            target.Apply(IntegerRules.Between(min, max, message));

        public Result<T> MultipleOf(TInteger factor, string? message = null) =>
            target.Apply(IntegerRules.MultipleOf(factor, message));
    }
}