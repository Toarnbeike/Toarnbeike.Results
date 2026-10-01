using System.Numerics;
using Toarnbeike.Results.Guards.Attributes;
using Toarnbeike.Results.Guards.Core.Guards;

namespace Toarnbeike.Results.Guards.Rules;

public static class FloatingPointNumberRules
{
    private const string RuleCategory = "Number";
    private const string QualifiedRuleCategory = "Number.Floating";

    extension<TFloatingPoint>(IGuardTarget<TFloatingPoint> target)
        where TFloatingPoint : struct, IFloatingPointIeee754<TFloatingPoint>
    {
        private TFloatingPoint GetTolerance(TFloatingPoint? providedTolerance) =>
            providedTolerance ?? target.GuardContext.ToleranceProvider.GetTolerance<TFloatingPoint>();

        /// <summary>
        /// Rule that the targeted number must be strictly greater than the specified minimum.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TFloatingPoint> GreaterThan(TFloatingPoint min) =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    ComparisonGuards.GreaterThan(value, min),
                    $"{RuleCategory}.{nameof(GreaterThan)}",
                    ("Min", min)));

        /// <summary>
        /// Rule that the targeted number must be less than the specified maximum.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TFloatingPoint> LessThan(TFloatingPoint max) =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    ComparisonGuards.LessThan(value, max),
                    $"{RuleCategory}.{nameof(LessThan)}",
                    ("Max", max)));

        /// <summary>
        /// Rule that the targeted number must be at least the specified minimum.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TFloatingPoint> AtLeast(TFloatingPoint min, TFloatingPoint? tolerance = null) =>
            target.Evaluate(value =>
            {
                var actualTolerance = target.GetTolerance(tolerance);
                return new RuleEvaluation(
                    ToleranceGuards.AtLeast(value, min, actualTolerance),
                    $"{QualifiedRuleCategory}.{nameof(AtLeast)}",
                    ("Min", min),
                    ("Tolerance", actualTolerance));
            });

        /// <summary>
        /// Rule that the targeted number must be at most the specified maximum.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TFloatingPoint> AtMost(TFloatingPoint max, TFloatingPoint? tolerance = null) =>
            target.Evaluate(value =>
            {
                var actualTolerance = target.GetTolerance(tolerance);
                return new RuleEvaluation(
                    ToleranceGuards.AtMost(value, max, actualTolerance),
                    $"{QualifiedRuleCategory}.{nameof(AtMost)}",
                    ("Max", max),
                    ("Tolerance", actualTolerance));
            });

        /// <summary>
        /// Rule that the targeted number must be between the specified minimum and maximum, inclusive.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TFloatingPoint> Between(TFloatingPoint min, TFloatingPoint max,
            TFloatingPoint? tolerance = null) =>
            target.Evaluate(value =>
            {
                var actualTolerance = target.GetTolerance(tolerance);
                return new RuleEvaluation(
                    ToleranceGuards.AtLeast(value, min, actualTolerance) &&
                    ToleranceGuards.AtMost(value, max, actualTolerance),
                    $"{QualifiedRuleCategory}.{nameof(Between)}", 
                    ("Min", min),
                    ("Max", max),
                    ("Tolerance", actualTolerance));
            });

        /// <summary>
        /// Rule that the targeted number must be positive, that is, strictly greater than 0.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TFloatingPoint> Positive() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    ComparisonGuards.GreaterThan(value, TFloatingPoint.Zero),
                    $"{RuleCategory}.{nameof(Positive)}"));

        /// <summary>
        /// Rule that the targeted number must be negative, that is, strictly less than 0.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TFloatingPoint> Negative() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    ComparisonGuards.LessThan(value, TFloatingPoint.Zero),
                    $"{RuleCategory}.{nameof(Negative)}"));

        /// <summary>
        /// Rule that the targeted number must be at least 0.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TFloatingPoint> AtLeastZero(TFloatingPoint? tolerance = null) =>
            target.Evaluate(value =>
            {
                var actualTolerance = target.GetTolerance(tolerance);
                return new RuleEvaluation(
                    ToleranceGuards.AtLeast(value, TFloatingPoint.Zero, actualTolerance),
                    $"{QualifiedRuleCategory}.{nameof(AtLeastZero)}",
                    ("Tolerance", actualTolerance));
            });

        /// <summary>
        /// Rule that the targeted number must be at most 0.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TFloatingPoint> AtMostZero(TFloatingPoint? tolerance = null) =>
            target.Evaluate(value =>
            {
                var actualTolerance = target.GetTolerance(tolerance);
                return new RuleEvaluation(
                    ToleranceGuards.AtMost(value, TFloatingPoint.Zero, actualTolerance),
                    $"{QualifiedRuleCategory}.{nameof(AtMostZero)}",
                    ("Tolerance", actualTolerance));
            });

        /// <summary>
        /// Rule that the targeted number must be exactly equal to 0.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TFloatingPoint> Zero(TFloatingPoint? tolerance = null) =>
            target.Evaluate(value =>
            {
                var actualTolerance = target.GetTolerance(tolerance);
                return new RuleEvaluation(
                    ToleranceGuards.Equal(value, TFloatingPoint.Zero, actualTolerance),
                    $"{QualifiedRuleCategory}.{nameof(Zero)}",
                    ("Tolerance", actualTolerance));
            });

        /// <summary>
        /// Rule that the targeted number must be negative, that is, strictly less than 0.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TFloatingPoint> NotZero(TFloatingPoint? tolerance = null) =>
            target.Evaluate(value =>
            {
                var actualTolerance = target.GetTolerance(tolerance);
                return new RuleEvaluation(
                    ToleranceGuards.NotEqual(value, TFloatingPoint.Zero, actualTolerance),
                    $"{QualifiedRuleCategory}.{nameof(NotZero)}",
                    ("Tolerance", actualTolerance));
            });

        /// <summary>
        /// Rule that the targeted number must be multiple of the specified factor.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TFloatingPoint> MultipleOf(TFloatingPoint factor, TFloatingPoint? tolerance = null) =>
            target.Evaluate(value =>
            {
                var actualTolerance = target.GetTolerance(tolerance);
                return new RuleEvaluation(
                    FloatingPointGuards.MultipleOf(value, factor, actualTolerance),
                    $"{QualifiedRuleCategory}.{nameof(MultipleOf)}",
                    ("Factor", factor),
                    ("Tolerance", actualTolerance));
            });

        /// <summary>
        /// Rule that the targeted number must be a whole number, that is, have no fractional part.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TFloatingPoint> WholeNumber(TFloatingPoint? tolerance = null) =>
            target.Evaluate(value =>
            {
                var actualTolerance = target.GetTolerance(tolerance);
                return new RuleEvaluation(
                    FloatingPointGuards.MultipleOf(value, TFloatingPoint.One, actualTolerance),
                    $"{QualifiedRuleCategory}.{nameof(WholeNumber)}",
                    ("Tolerance", actualTolerance));
            });

        /// <summary>
        /// Rule that the targeted number must have at most the specified number of decimal places.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TFloatingPoint> MaxDecimalPlaces(int maxPlaces, TFloatingPoint? tolerance = null) =>
            target.Evaluate(value =>
            {
                var factor = TFloatingPoint.One / Power10<TFloatingPoint>(maxPlaces);
                var actualTolerance = target.GetTolerance(tolerance);
                return new RuleEvaluation(
                    FloatingPointGuards.MultipleOf(value, factor, actualTolerance),
                    $"{QualifiedRuleCategory}.{nameof(MaxDecimalPlaces)}",
                    ("MaxPlaces", maxPlaces),
                    ("Tolerance", actualTolerance));
            });

        /// <summary>
        /// Rule that the targeted number must be Finite, that is, not NaN and not infinity.
        /// </summary>
        public IGuardRuleResult<TFloatingPoint> Finite() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    FloatingPointGuards.Finite(value),
                    $"{QualifiedRuleCategory}.{nameof(Finite)}"));

        /// <summary>
        /// Rule that the targeted number must be a defined value, that is, not NaN.
        /// </summary>
        public IGuardRuleResult<TFloatingPoint> NotNaN() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    FloatingPointGuards.NotNaN(value),
                    $"{QualifiedRuleCategory}.{nameof(NotNaN)}"));
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