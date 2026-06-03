using System.Numerics;
using Toarnbeike.Results.Guards.Implementations.Guards;

namespace Toarnbeike.Results.Guards.Rules;

public static class FloatingPointNumberRules
{
    private const string RuleCategory = "Number";
    private const string QualifiedRuleCategory = "Number.Floating";

    extension<TFloatingPoint>(IGuardTarget<TFloatingPoint> target) where TFloatingPoint : struct, IFloatingPointIeee754<TFloatingPoint>
    {
        private TFloatingPoint GetTolerance(TFloatingPoint? providedTolerance) =>
            providedTolerance ?? target.GuardContext.ToleranceProvider.GetTolerance<TFloatingPoint>();

        /// <summary>
        /// Rule that the targeted number must be strictly greater than the specified minimum.
        /// </summary>
        public IGuardRuleResult<TFloatingPoint> GreaterThan(TFloatingPoint min)
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ComparisonGuards.GreaterThan(target.Value, min),
                    $"{RuleCategory}.{nameof(GreaterThan)}",
                    ("Min", min));
        }

        /// <summary>
        /// Rule that the targeted number must be less than the specified maximum.
        /// </summary>
        public IGuardRuleResult<TFloatingPoint> LessThan(TFloatingPoint max)
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ComparisonGuards.LessThan(target.Value, max),
                    $"{RuleCategory}.{nameof(LessThan)}",
                    ("Max", max));
        }

        /// <summary>
        /// Rule that the targeted number must be at least the specified minimum.
        /// </summary>
        public IGuardRuleResult<TFloatingPoint> AtLeast(TFloatingPoint min, TFloatingPoint? tolerance = null)
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ToleranceGuards.AtLeast(target.Value, min, target.GetTolerance(tolerance)),
                    $"{QualifiedRuleCategory}.{nameof(AtLeast)}", 
                    ("Min", min),
                    ("Tolerance", tolerance));
        }

        /// <summary>
        /// Rule that the targeted number must be at most the specified maximum.
        /// </summary>
        public IGuardRuleResult<TFloatingPoint> AtMost(TFloatingPoint max, TFloatingPoint? tolerance = null)
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ToleranceGuards.AtMost(target.Value, max, target.GetTolerance(tolerance)),
                    $"{QualifiedRuleCategory}.{nameof(AtMost)}", 
                    ("Max", max),
                    ("Tolerance", tolerance));
        }

        /// <summary>
        /// Rule that the targeted number must be between the specified minimum and maximum, inclusive.
        /// </summary>
        public IGuardRuleResult<TFloatingPoint> Between(TFloatingPoint min, TFloatingPoint max, TFloatingPoint? tolerance = null)
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ToleranceGuards.AtLeast(target.Value, min, target.GetTolerance(tolerance)) &&
                                ToleranceGuards.AtMost(target.Value, max, target.GetTolerance(tolerance)),
                    $"{QualifiedRuleCategory}.{nameof(Between)}", 
                    ("Min", min),
                    ("Max", max),
                    ("Tolerance", tolerance));
        }

        /// <summary>
        /// Rule that the targeted number must be positive, that is, strictly greater than 0.
        /// </summary>
        public IGuardRuleResult<TFloatingPoint> Positive()
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ComparisonGuards.GreaterThan(target.Value, TFloatingPoint.Zero),
                    $"{RuleCategory}.{nameof(Positive)}");
        }

        /// <summary>
        /// Rule that the targeted number must be negative, that is, strictly less than 0.
        /// </summary>
        public IGuardRuleResult<TFloatingPoint> Negative()
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ComparisonGuards.LessThan(target.Value, TFloatingPoint.Zero),
                    $"{RuleCategory}.{nameof(Negative)}");
        }

        /// <summary>
        /// Rule that the targeted number must be at least 0.
        /// </summary>
        public IGuardRuleResult<TFloatingPoint> AtLeastZero(TFloatingPoint? tolerance = null)
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ToleranceGuards.AtLeast(target.Value, TFloatingPoint.Zero, target.GetTolerance(tolerance)),
                    $"{QualifiedRuleCategory}.{nameof(AtLeastZero)}", 
                    ("Tolerance", tolerance));
        }

        /// <summary>
        /// Rule that the targeted number must be at most 0.
        /// </summary>
        public IGuardRuleResult<TFloatingPoint> AtMostZero(TFloatingPoint? tolerance = null)
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ToleranceGuards.AtMost(target.Value, TFloatingPoint.Zero, target.GetTolerance(tolerance)),
                    $"{QualifiedRuleCategory}.{nameof(AtMostZero)}", 
                    ("Tolerance", tolerance));
        }

        /// <summary>
        /// Rule that the targeted number must be exactly equal to 0.
        /// </summary>
        public IGuardRuleResult<TFloatingPoint> Zero(TFloatingPoint? tolerance = null)
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ToleranceGuards.Equal(target.Value, TFloatingPoint.Zero, target.GetTolerance(tolerance)),
                    $"{QualifiedRuleCategory}.{nameof(Zero)}", 
                    ("Tolerance", tolerance));
        }

        /// <summary>
        /// Rule that the targeted number must be negative, that is, strictly less than 0.
        /// </summary>
        public IGuardRuleResult<TFloatingPoint> NotZero(TFloatingPoint? tolerance = null)
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ToleranceGuards.NotEqual(target.Value, TFloatingPoint.Zero, target.GetTolerance(tolerance)),
                    $"{QualifiedRuleCategory}.{nameof(NotZero)}", 
                    ("Tolerance", tolerance));
        }

        /// <summary>
        /// Rule that the targeted number must be multiple of the specified factor.
        /// </summary>
        public IGuardRuleResult<TFloatingPoint> MultipleOf(TFloatingPoint factor, TFloatingPoint? tolerance = null)
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(FloatingPointGuards.MultipleOf(target.Value, factor, target.GetTolerance(tolerance)),
                    $"{QualifiedRuleCategory}.{nameof(MultipleOf)}", 
                    ("Factor", factor),
                    ("Tolerance", tolerance));
        }

        /// <summary>
        /// Rule that the targeted number must be a whole number, that is, have no fractional part.
        /// </summary>
        public IGuardRuleResult<TFloatingPoint> WholeNumber(TFloatingPoint? tolerance = null)
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(FloatingPointGuards.MultipleOf(target.Value, TFloatingPoint.One, target.GetTolerance(tolerance)),
                    $"{QualifiedRuleCategory}.{nameof(WholeNumber)}", 
                    ("Tolerance", tolerance));
        }

        /// <summary>
        /// Rule that the targeted number must have at most the specified number of decimal places.
        /// </summary>
        public IGuardRuleResult<TFloatingPoint> MaxDecimalPlaces(int maxPlaces, TFloatingPoint? tolerance = null)
        {
            var factor = TFloatingPoint.One / Power10<TFloatingPoint>(maxPlaces);
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(FloatingPointGuards.MultipleOf(target.Value, factor, target.GetTolerance(tolerance)),
                    $"{QualifiedRuleCategory}.{nameof(MaxDecimalPlaces)}", 
                    ("MaxPlaces", maxPlaces),
                    ("Tolerance", tolerance));
        }

        /// <summary>
        /// Rule that the targeted number must be Finite, that is, not NaN and not infinity.
        /// </summary>
        public IGuardRuleResult<TFloatingPoint> Finite()
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(FloatingPointGuards.Finite(target.Value),
                    $"{QualifiedRuleCategory}.{nameof(Finite)}");
        }

        /// <summary>
        /// Rule that the targeted number must be a defined value, that is, not NaN.
        /// </summary>
        public IGuardRuleResult<TFloatingPoint> NotNaN()
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(FloatingPointGuards.NotNaN(target.Value),
                    $"{QualifiedRuleCategory}.{nameof(NotNaN)}");
        }
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