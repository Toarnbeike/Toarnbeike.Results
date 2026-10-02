using System.Numerics;
using Toarnbeike.Results.Validation.Implementation;
using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Extensions;

public static class FloatingPointExtensions
{
    extension<T, TFloating>(ValidationRuleBuilder<T, TFloating> builder)
    where TFloating : struct, IFloatingPointIeee754<TFloating>
    {
        public ValidationRuleBuilder<T, TFloating> AtLeast(TFloating min, TFloating? tolerance = null, string? message = null)
        {
            var tol = GetTolerance(tolerance);
            return builder.Add(value => ToleranceRules.AtLeast(value, min, tol),
                message ?? $"must be at least {min} (tolerance: {tol}).");
        }

        public ValidationRuleBuilder<T, TFloating> AtMost(TFloating max, TFloating? tolerance = null, string? message = null)
        {
            var tol = GetTolerance(tolerance);
            return builder.Add(value => ToleranceRules.AtMost(value, max, tol),
                message ?? $"must be at most {max} (tolerance: {tol}).");
        }

        public ValidationRuleBuilder<T, TFloating> Between(TFloating min, TFloating max, TFloating? tolerance = null, string? message = null)
        {
            var tol = GetTolerance(tolerance);
            return builder.Add(value =>
                ToleranceRules.AtLeast(value, min, tol) && ToleranceRules.AtMost(value, max, tol),
                message ?? $"must be between {min} and {max} (tolerance: {tol}).");
        }

        public ValidationRuleBuilder<T, TFloating> MultipleOf(TFloating factor, TFloating? tolerance = null, string? message = null)
        {
            var tol = GetTolerance(tolerance);
            return builder.Add(value => FloatingPointRules.MultipleOf(value, factor, tol),
                message ?? $"must be a multiple of {factor} (tolerance: {tol}).");
        }

        public ValidationRuleBuilder<T, TFloating> Finite(string? message = null) =>
            builder.Add(value => FloatingPointRules.Finite(value), message ?? "must be a finite number.");

        public ValidationRuleBuilder<T, TFloating> NotNaN(string? message = null) =>
            builder.Add(value => FloatingPointRules.NotNaN(value), message ?? "must be defined.");
    }

    extension<T, TFloating>(ValidationTarget<T, TFloating> target)
        where TFloating : struct, IFloatingPointIeee754<TFloating>
    {
        public Result<T> AtLeast(TFloating min, TFloating? tolerance = null, string? message = null)
        {
            var tol = GetTolerance(tolerance);
            return target.Apply(value => ToleranceRules.AtLeast(value, min, tol),
                message ?? $"must be at least {min} (tolerance: {tol}).");
        }

        public Result<T> AtMost(TFloating max, TFloating? tolerance = null, string? message = null)
        {
            var tol = GetTolerance(tolerance);
            return target.Apply(value => ToleranceRules.AtMost(value, max, tol),
                message ?? $"must be at most {max} (tolerance: {tol}).");
        }

        public Result<T> Between(TFloating min, TFloating max, TFloating? tolerance = null, string? message = null)
        {
            var tol = GetTolerance(tolerance);
            return target.Apply(value =>
                ToleranceRules.AtLeast(value, min, tol) && ToleranceRules.AtMost(value, max, tol),
                message ?? $"must be between {min} and {max} (tolerance: {tol}).");
        }

        public Result<T> MultipleOf(TFloating factor, TFloating? tolerance = null, string? message = null)
        {
            var tol = GetTolerance(tolerance);
            return target.Apply(value => FloatingPointRules.MultipleOf(value, factor, tol),
                message ?? $"must be a multiple of {factor} (tolerance: {tol}).");
        }

        public Result<T> Finite(string? message = null) =>
            target.Apply(value => FloatingPointRules.Finite(value), message ?? "must be a finite number.");

        public Result<T> NotNaN(string? message = null) =>
            target.Apply(value => FloatingPointRules.NotNaN(value), message ?? "must be defined.");
    }

    private static TFloating GetTolerance<TFloating>(TFloating? provided)
    where TFloating : struct, IFloatingPointIeee754<TFloating> =>
    provided ?? ToleranceHelper.Default<TFloating>();
}
