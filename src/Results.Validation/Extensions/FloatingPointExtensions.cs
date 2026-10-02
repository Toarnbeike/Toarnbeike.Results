using System.Numerics;
using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class FloatingPointExtensions
{
    extension<T, TFloating>(ValidationRuleBuilder<T, TFloating> builder)
    where TFloating : struct, IFloatingPointIeee754<TFloating>
    {
        public ValidationRuleBuilder<T, TFloating> AtLeast(TFloating min, TFloating? tolerance = null, string? message = null) =>
            builder.Add(FloatingPointRules.AtLeast(min, tolerance, message));

        public ValidationRuleBuilder<T, TFloating> AtMost(TFloating max, TFloating? tolerance = null, string? message = null) =>
            builder.Add(FloatingPointRules.AtMost(max, tolerance, message));

        public ValidationRuleBuilder<T, TFloating> Between(TFloating min, TFloating max, TFloating? tolerance = null, string? message = null) =>
            builder.Add(FloatingPointRules.Between(min, max, tolerance, message));
        
        public ValidationRuleBuilder<T, TFloating> MultipleOf(TFloating factor, TFloating? tolerance = null, string? message = null) =>
            builder.Add(FloatingPointRules.MultipleOf(factor, tolerance, message));

        public ValidationRuleBuilder<T, TFloating> Finite(string? message = null) =>
            builder.Add(FloatingPointRules.Finite<TFloating>(message));

        public ValidationRuleBuilder<T, TFloating> NotNaN(string? message = null) =>
            builder.Add(FloatingPointRules.NotNaN<TFloating>(message));
    }

    extension<T, TFloating>(ValidationTarget<T, TFloating> target)
        where TFloating : struct, IFloatingPointIeee754<TFloating>
    {
        public Result<T> AtLeast(TFloating min, TFloating? tolerance = null, string? message = null) =>
            target.Apply(FloatingPointRules.AtLeast(min, tolerance, message));

        public Result<T> AtMost(TFloating max, TFloating? tolerance = null, string? message = null) =>
            target.Apply(FloatingPointRules.AtMost(max, tolerance, message));

        public Result<T> Between(TFloating min, TFloating max, TFloating? tolerance = null, string? message = null) =>
            target.Apply(FloatingPointRules.Between(min, max, tolerance, message));

        public Result<T> MultipleOf(TFloating factor, TFloating? tolerance = null, string? message = null) =>
            target.Apply(FloatingPointRules.MultipleOf(factor, tolerance, message));

        public Result<T> Finite(string? message = null) =>
            target.Apply(FloatingPointRules.Finite<TFloating>(message));

        public Result<T> NotNaN(string? message = null) =>
            target.Apply(FloatingPointRules.NotNaN<TFloating>(message));
    }
}
