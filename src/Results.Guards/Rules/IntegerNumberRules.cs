using System.Numerics;
using Toarnbeike.Results.Guards.Core.Guards;

namespace Toarnbeike.Results.Guards.Rules;

public static class IntegerNumberRules
{
    private const string RuleCategory = "Number";
    private const string QualifiedRuleCategory = "Number.Integer";

    extension<TInteger>(IGuardTarget<TInteger> target) where TInteger : struct, IBinaryInteger<TInteger>
    {
        /// <summary>
        /// Rule that the targeted number must be strictly greater than the specified minimum.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TInteger> GreaterThan(TInteger min) =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    ComparisonGuards.GreaterThan(value, min),
                    $"{RuleCategory}.{nameof(GreaterThan)}",
                    ("Min", min)));

        /// <summary>
        /// Rule that the targeted number must be less than the specified maximum.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TInteger> LessThan(TInteger max) =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    ComparisonGuards.LessThan(value, max),
                    $"{RuleCategory}.{nameof(LessThan)}",
                    ("Max", max)));

        /// <summary>
        /// Rule that the targeted number must be at least the specified minimum.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TInteger> AtLeast(TInteger min) =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    ComparisonGuards.AtLeast(value, min),
                    $"{QualifiedRuleCategory}.{nameof(AtLeast)}",
                    ("Min", min)));

        /// <summary>
        /// Rule that the targeted number must be at most the specified maximum.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TInteger> AtMost(TInteger max) =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    ComparisonGuards.AtMost(value, max),
                    $"{QualifiedRuleCategory}.{nameof(AtMost)}",
                    ("Max", max)));

        /// <summary>
        /// Rule that the targeted number must be between the specified minimum and maximum, inclusive.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TInteger> Between(TInteger min, TInteger max) =>
            target.Evaluate(value => new RuleEvaluation(
                ComparisonGuards.AtLeast(value, min) && ComparisonGuards.AtMost(value, max),
                $"{QualifiedRuleCategory}.{nameof(Between)}",
                ("Min", min),
                ("Max", max)));

        /// <summary>
        /// Rule that the targeted number must be positive, that is, strictly greater than 0.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TInteger> Positive() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    ComparisonGuards.GreaterThan(value, TInteger.Zero),
                    $"{RuleCategory}.{nameof(Positive)}"));

        /// <summary>
        /// Rule that the targeted number must be negative, that is, strictly less than 0.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TInteger> Negative() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    ComparisonGuards.LessThan(value, TInteger.Zero),
                    $"{RuleCategory}.{nameof(Negative)}"));

        /// <summary>
        /// Rule that the targeted number must be at least 0.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TInteger> AtLeastZero() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    ComparisonGuards.AtLeast(value, TInteger.Zero),
                    $"{QualifiedRuleCategory}.{nameof(AtLeastZero)}"));

        /// <summary>
        /// Rule that the targeted number must be at most 0.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TInteger> AtMostZero() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    ComparisonGuards.AtMost(value, TInteger.Zero),
                    $"{QualifiedRuleCategory}.{nameof(AtMostZero)}"));

        /// <summary>
        /// Rule that the targeted number must be negative, that is, strictly less than 0.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TInteger> NotZero() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    ComparisonGuards.NotEqual(value, TInteger.Zero),
                    $"{QualifiedRuleCategory}.{nameof(NotZero)}"));

        /// <summary>
        /// Rule that the targeted number must be multiple of the specified factor.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TInteger> MultipleOf(TInteger factor) =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    IntegerGuards.MultipleOf(value, factor),
                    $"{QualifiedRuleCategory}.{nameof(MultipleOf)}",
                    ("Factor", factor)));

        /// <summary>
        /// Rule that the targeted number must be an even number.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TInteger> Even() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    IntegerGuards.Even(value),
                    $"{QualifiedRuleCategory}.{nameof(Even)}"));

        /// <summary>
        /// Rule that the targeted number must be an odd number.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TInteger> Odd() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    IntegerGuards.Odd(value),
                    $"{QualifiedRuleCategory}.{nameof(Odd)}"));

        /// <summary>
        /// Rule that the targeted number must be a power of two.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TInteger> PowerOf2() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    IntegerGuards.PowerOf2(value),
                    $"{QualifiedRuleCategory}.{nameof(PowerOf2)}"));

        /// <summary>
        /// Rule that the targeted number must be a prime number.
        /// </summary>
        /// <remarks>
        /// Checking for primality is more expensive than other guards, so use this rule only when necessary.
        /// The implementation uses trial division, which is fine for small integers but may not be suitable for large values.
        /// </remarks>
        public IGuardRuleResult<TInteger> Prime() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    IntegerGuards.Prime(value),
                    $"{QualifiedRuleCategory}.{nameof(Prime)}"));
    }
}