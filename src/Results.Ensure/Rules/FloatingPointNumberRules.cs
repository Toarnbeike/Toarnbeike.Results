using System.Numerics;
using Toarnbeike.Results.Ensure.Guards;

namespace Toarnbeike.Results.Ensure.Rules;

public static class FloatingPointNumberRules
{
    extension<TFloatingPoint>(IGuardTarget<TFloatingPoint> target) where TFloatingPoint : struct, IFloatingPointIeee754<TFloatingPoint>
    {
        private TFloatingPoint GetTolerance(TFloatingPoint? providedTolerance) =>
            providedTolerance ?? target.ToleranceProvider.GetTolerance<TFloatingPoint>();

        /// <summary>
        /// Rule that the targeted number must be strictly greater than the specified minimum.
        /// </summary>
        public IGuardRuleResult GreaterThan(TFloatingPoint min) =>
            target.Evaluate(ComparisonGuards.GreaterThan(target.Value, min),
                $"{nameof(FloatingPointNumberRules)}.{nameof(GreaterThan)}", min);

        /// <summary>
        /// Rule that the targeted number must be at least the specified minimum.
        /// </summary>
        public IGuardRuleResult AtLeast(TFloatingPoint min, TFloatingPoint? tolerance = null) =>
            target.Evaluate(ToleranceGuards.AtLeast(target.Value, min, target.GetTolerance(tolerance)),
                $"{nameof(FloatingPointNumberRules)}.{nameof(AtLeast)}", min);

        /// <summary>
        /// Rule that the targeted number must be at most the specified maximum.
        /// </summary>
        public IGuardRuleResult AtMost(TFloatingPoint max, TFloatingPoint? tolerance = null) =>
            target.Evaluate(ToleranceGuards.AtMost(target.Value, max, target.GetTolerance(tolerance)),
                $"{nameof(FloatingPointNumberRules)}.{nameof(AtMost)}", max);

        /// <summary>
        /// Rule that the targeted number must be less than the specified maximum.
        /// </summary>
        public IGuardRuleResult LessThan(TFloatingPoint max) =>
            target.Evaluate(ComparisonGuards.LessThan(target.Value, max),
                $"{nameof(FloatingPointNumberRules)}.{nameof(LessThan)}", max);

        /// <summary>
        /// Rule that the targeted number must be between the specified minimum and maximum, inclusive.
        /// </summary>
        public IGuardRuleResult Between(TFloatingPoint min, TFloatingPoint max, TFloatingPoint? tolerance = null) =>
            target.Evaluate(ToleranceGuards.AtLeast(target.Value, min, target.GetTolerance(tolerance)) &&
                            ToleranceGuards.AtMost(target.Value, max, target.GetTolerance(tolerance)),
                $"{nameof(FloatingPointNumberRules)}.{nameof(Between)}", (min, max));

        /// <summary>
        /// Rule that the targeted number must be positive, that is, strictly greater than 0.
        /// </summary>
        public IGuardRuleResult Positive() =>
            target.Evaluate(ComparisonGuards.GreaterThan(target.Value, TFloatingPoint.Zero),
                $"{nameof(FloatingPointNumberRules)}.{nameof(Positive)}");

        /// <summary>
        /// Rule that the targeted number must be at least 0.
        /// </summary>
        public IGuardRuleResult AtLeastZero(TFloatingPoint? tolerance = null) =>
            target.Evaluate(ToleranceGuards.AtLeast(target.Value, TFloatingPoint.Zero, target.GetTolerance(tolerance)),
                $"{nameof(FloatingPointNumberRules)}.{nameof(AtLeastZero)}");

        /// <summary>
        /// Rule that the targeted number must be at most 0.
        /// </summary>
        public IGuardRuleResult AtMostZero(TFloatingPoint? tolerance = null) =>
            target.Evaluate(ToleranceGuards.AtMost(target.Value, TFloatingPoint.Zero, target.GetTolerance(tolerance)),
                $"{nameof(FloatingPointNumberRules)}.{nameof(AtMostZero)}");

        /// <summary>
        /// Rule that the targeted number must be negative, that is, strictly less than 0.
        /// </summary>
        public IGuardRuleResult Negative() =>
            target.Evaluate(ComparisonGuards.LessThan(target.Value, TFloatingPoint.Zero),
                $"{nameof(FloatingPointNumberRules)}.{nameof(Negative)}");

        /// <summary>
        /// Rule that the targeted number must be exactly equal to 0.
        /// </summary>
        public IGuardRuleResult Zero(TFloatingPoint? tolerance = null) =>
            target.Evaluate(ToleranceGuards.Equal(target.Value, TFloatingPoint.Zero, target.GetTolerance(tolerance)),
                $"{nameof(FloatingPointNumberRules)}.{nameof(Zero)}");

        /// <summary>
        /// Rule that the targeted number must be negative, that is, strictly less than 0.
        /// </summary>
        public IGuardRuleResult NotZero(TFloatingPoint? tolerance = null) =>
            target.Evaluate(ToleranceGuards.NotEqual(target.Value, TFloatingPoint.Zero, target.GetTolerance(tolerance)),
                $"{nameof(FloatingPointNumberRules)}.{nameof(NotZero)}");

        /// <summary>
        /// Rule that the targeted number must be multiple of the specified factor.
        /// </summary>
        public IGuardRuleResult MultipleOf(TFloatingPoint factor, TFloatingPoint? tolerance = null) =>
            target.Evaluate(ToleranceGuards.MultipleOf(target.Value, factor, target.GetTolerance(tolerance)),
                $"{nameof(FloatingPointNumberRules)}.{nameof(MultipleOf)}");

        /// <summary>
        /// Rule that the targeted number must be a whole number, that is, have no fractional part.
        /// </summary>
        public IGuardRuleResult WholeNumber(TFloatingPoint? tolerance = null) =>
            target.Evaluate(ToleranceGuards.MultipleOf(target.Value, TFloatingPoint.One, target.GetTolerance(tolerance)),
                $"{nameof(FloatingPointNumberRules)}.{nameof(WholeNumber)}");

        /// <summary>
        /// Rule that the targeted number must have at most the specified number of decimal places.
        /// </summary>
        public IGuardRuleResult MaxDecimalPlaces(int maxPlaces, TFloatingPoint? tolerance = null)
        {
            var factor = TFloatingPoint.One / Power10<TFloatingPoint>(maxPlaces);
            return target.Evaluate(ToleranceGuards.MultipleOf(target.Value, factor, target.GetTolerance(tolerance)),
                $"{nameof(FloatingPointNumberRules)}.{nameof(MaxDecimalPlaces)}");
        }

        /// <summary>
        /// Rule that the targeted number must be Finite, that is, not NaN and not infinity.
        /// </summary>
        public IGuardRuleResult Finite() =>
            target.Evaluate(FloatingPointGuards.Finite(target.Value),
                $"{nameof(FloatingPointNumberRules)}.{nameof(Finite)}");

        /// <summary>
        /// Rule that the targeted number must be a defined value, that is, not NaN.
        /// </summary>
        public IGuardRuleResult NotNaN() =>
            target.Evaluate(FloatingPointGuards.NotNaN(target.Value),
                $"{nameof(FloatingPointNumberRules)}.{nameof(NotNaN)}");
    }

    /// <summary>
    /// Create a generic <see cref="INumber{TSelf}"/> instance by raising 10 to the provided power.
    /// </summary>
    internal static TNumber Power10<TNumber>(int power) where TNumber : INumber<TNumber>
    {
        var result = TNumber.One;
        var ten = TNumber.CreateChecked(10);
        for (var i = 0; i < power; i++)
        {
            result *= ten;
        }

        return result;
    }
}