using System.Numerics;
using Toarnbeike.Results.Ensure.Guards;

namespace Toarnbeike.Results.Ensure.Rules;

public static class IntegerNumberRules
{
    private const string RuleCategory = "Number";
    private const string QualifiedRuleCategory = "Number.Integer";

    extension<TInteger>(IGuardTarget<TInteger> target) where TInteger : struct, IBinaryInteger<TInteger>
    {
        /// <summary>
        /// Rule that the targeted number must be strictly greater than the specified minimum.
        /// </summary>
        public IGuardRuleResult GreaterThan(TInteger min) =>
            target.Evaluate(ComparisonGuards.GreaterThan(target.Value, min),
                $"{RuleCategory}.{nameof(GreaterThan)}",
                ("Min", min));

        /// <summary>
        /// Rule that the targeted number must be less than the specified maximum.
        /// </summary>
        public IGuardRuleResult LessThan(TInteger max) =>
            target.Evaluate(ComparisonGuards.LessThan(target.Value, max),
                $"{RuleCategory}.{nameof(LessThan)}",
                ("Max", max));

        /// <summary>
        /// Rule that the targeted number must be at least the specified minimum.
        /// </summary>
        public IGuardRuleResult AtLeast(TInteger min) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, min),
                $"{QualifiedRuleCategory}.{nameof(AtLeast)}", 
                ("Min", min));

        /// <summary>
        /// Rule that the targeted number must be at most the specified maximum.
        /// </summary>
        public IGuardRuleResult AtMost(TInteger max) =>
            target.Evaluate(ComparisonGuards.AtMost(target.Value, max),
                $"{QualifiedRuleCategory}.{nameof(AtMost)}", 
                ("Max", max));

        /// <summary>
        /// Rule that the targeted number must be between the specified minimum and maximum, inclusive.
        /// </summary>
        public IGuardRuleResult Between(TInteger min, TInteger max) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, min) &&
                            ComparisonGuards.AtMost(target.Value, max),
                $"{QualifiedRuleCategory}.{nameof(Between)}",
                ("Min", min),
                ("Max", max));

        /// <summary>
        /// Rule that the targeted number must be positive, that is, strictly greater than 0.
        /// </summary>
        public IGuardRuleResult Positive() =>
            target.Evaluate(ComparisonGuards.GreaterThan(target.Value, TInteger.Zero),
                $"{RuleCategory}.{nameof(Positive)}");

        /// <summary>
        /// Rule that the targeted number must be negative, that is, strictly less than 0.
        /// </summary>
        public IGuardRuleResult Negative() =>
            target.Evaluate(ComparisonGuards.LessThan(target.Value, TInteger.Zero),
                $"{RuleCategory}.{nameof(Negative)}");

        /// <summary>
        /// Rule that the targeted number must be at least 0.
        /// </summary>
        public IGuardRuleResult AtLeastZero() =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, TInteger.Zero),
                $"{QualifiedRuleCategory}.{nameof(AtLeastZero)}");

        /// <summary>
        /// Rule that the targeted number must be at most 0.
        /// </summary>
        public IGuardRuleResult AtMostZero() =>
            target.Evaluate(ComparisonGuards.AtMost(target.Value, TInteger.Zero),
                $"{QualifiedRuleCategory}.{nameof(AtMostZero)}");

        /// <summary>
        /// Rule that the targeted number must be negative, that is, strictly less than 0.
        /// </summary>
        public IGuardRuleResult NotZero() =>
            target.Evaluate(ComparisonGuards.NotEqual(target.Value, TInteger.Zero),
                $"{QualifiedRuleCategory}.{nameof(NotZero)}");

        /// <summary>
        /// Rule that the targeted number must be multiple of the specified factor.
        /// </summary>
        public IGuardRuleResult MultipleOf(TInteger factor) =>
            target.Evaluate(IntegerGuards.MultipleOf(target.Value, factor),
                $"{QualifiedRuleCategory}.{nameof(MultipleOf)}", 
                ("Factor", factor));

        /// <summary>
        /// Rule that the targeted number must be an even number.
        /// </summary>
        public IGuardRuleResult Even() =>
            target.Evaluate(IntegerGuards.Even(target.Value),
                $"{QualifiedRuleCategory}.{nameof(Even)}");

        /// <summary>
        /// Rule that the targeted number must be an odd number.
        /// </summary>
        public IGuardRuleResult Odd() =>
            target.Evaluate(IntegerGuards.Odd(target.Value),
                $"{QualifiedRuleCategory}.{nameof(Odd)}");

        /// <summary>
        /// Rule that the targeted number must be a power of two.
        /// </summary>
        public IGuardRuleResult PowerOf2() =>
            target.Evaluate(IntegerGuards.PowerOf2(target.Value),
                $"{QualifiedRuleCategory}.{nameof(PowerOf2)}");

        /// <summary>
        /// Rule that the targeted number must be a prime number.
        /// </summary>
        /// <remarks>
        /// Checking for primality is more expensive than other guards, so use this rule only when necessary.
        /// The implementation uses trial division, which is fine for small integers but may not be suitable for large values.
        /// </remarks>
        public IGuardRuleResult Prime() =>
            target.Evaluate(IntegerGuards.Prime(target.Value),
                $"{QualifiedRuleCategory}.{nameof(Prime)}");
    }
}