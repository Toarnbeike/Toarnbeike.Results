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
            builder.Add(value => ComparisonRules.AtLeast(value, min),
                message ?? $"must be at least {min}.)");
        public ValidationRuleBuilder<T, TInteger> AtMost(TInteger max, string? message = null) =>
            builder.Add(value => ComparisonRules.AtMost(value, max),
                message ?? $"must be at most {max}.)");
        
        public ValidationRuleBuilder<T, TInteger> Between(TInteger min, TInteger max, string? message = null) =>
            builder.Add(value =>
                ComparisonRules.AtLeast(value, min) && ComparisonRules.AtMost(value, max),
                message ?? $"must be between {min} and {max}.");

        public ValidationRuleBuilder<T, TInteger> MultipleOf(TInteger factor, string? message = null) =>
            builder.Add(value => IntegerRules.MultipleOf(value, factor),
                message ?? $"must be a multiple of {factor}.");
    }

    extension<T, TInteger>(ValidationTarget<T, TInteger> target)
        where TInteger : struct, IBinaryInteger<TInteger>
    {
        public Result<T> AtLeast(TInteger min, string? message = null)
        {
            return target.Apply(value => ComparisonRules.AtLeast(value, min),
                message ?? $"must be at least {min}.)");
        }

        public Result<T> AtMost(TInteger max, string? message = null)
        {
            return target.Apply(value => ComparisonRules.AtMost(value, max),
                message ?? $"must be at most {max}.)");
        }

        public Result<T> Between(TInteger min, TInteger max, string? message = null)
        {
            return target.Apply(value =>
                ComparisonRules.AtLeast(value, min) && ComparisonRules.AtMost(value, max),
                message ?? $"must be between {min} and {max}.");
        }

        public Result<T> MultipleOf(TInteger factor, string? message = null)
        {
            return target.Apply(value => IntegerRules.MultipleOf(value, factor),
                message ?? $"must be a multiple of {factor}.");
        }
    }
}
