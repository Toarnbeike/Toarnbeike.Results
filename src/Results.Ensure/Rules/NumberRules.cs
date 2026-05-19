using System.Numerics;
using Toarnbeike.Results.Ensure.Guards;

namespace Toarnbeike.Results.Ensure.Rules;

public static class NumberRules
{
    extension<TNumber>(IGuardTarget<TNumber> target) where TNumber : struct, INumber<TNumber>
    {
        /// <summary>
        /// Rule that the targeted number must be strictly greater than the specified minimum.
        /// </summary>
        public IGuardRuleResult GreaterThan(TNumber min) =>
            target.Evaluate(ComparisonGuards.GreaterThan(target.Value, min),
                $"{nameof(NumberRules)}.{nameof(GreaterThan)}",
                ("Min", min));

        /// <summary>
        /// Rule that the targeted number must be less than the specified maximum.
        /// </summary>
        public IGuardRuleResult LessThan(TNumber max) =>
            target.Evaluate(ComparisonGuards.LessThan(target.Value, max),
                $"{nameof(NumberRules)}.{nameof(LessThan)}",
                ("Max", max));

        /// <summary>
        /// Rule that the targeted number must be positive, that is, strictly greater than 0.
        /// </summary>
        public IGuardRuleResult Positive() =>
            target.Evaluate(ComparisonGuards.GreaterThan(target.Value, TNumber.Zero),
                $"{nameof(NumberRules)}.{nameof(Positive)}");

        /// <summary>
        /// Rule that the targeted number must be negative, that is, strictly less than 0.
        /// </summary>
        public IGuardRuleResult Negative() =>
            target.Evaluate(ComparisonGuards.LessThan(target.Value, TNumber.Zero),
                $"{nameof(NumberRules)}.{nameof(Negative)}");
    }
}