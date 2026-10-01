using Toarnbeike.Results.Guards.Attributes;
using Toarnbeike.Results.Guards.Core.Guards;

namespace Toarnbeike.Results.Guards.Rules;

public static class TimeSpanRules
{
    private const string RuleCategory = "TimeSpan";

    extension(IGuardTarget<TimeSpan> target)
    {
        /// <summary>
        /// Rule that the targeted timespan must be at least the specified min.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TimeSpan> AtLeast(TimeSpan min) =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    ComparisonGuards.AtLeast(value, min),
                    $"{RuleCategory}.{nameof(AtLeast)}",
                    ("Min", min)));

        /// <summary>
        /// Rule that the targeted timespan must be at most the specified max.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TimeSpan> AtMost(TimeSpan max) =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    ComparisonGuards.AtMost(value, max),
                    $"{RuleCategory}.{nameof(AtMost)}",
                    ("Max", max)));

        /// <summary>
        /// Rule that the targeted timespan must be between the specified durations (inclusive).
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TimeSpan> Between(TimeSpan min, TimeSpan max) =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    ComparisonGuards.AtMost(value, max) && ComparisonGuards.AtLeast(value, min),
                    $"{RuleCategory}.{nameof(Between)}",
                    ("Min", min),
                    ("Max", max)));

        /// <summary>
        /// Rule that the targeted timespan must be close to the specified timespan, within the provided time span.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TimeSpan> Around(TimeSpan expected, TimeSpan tolerance) =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    ToleranceGuards.Equal(value.Ticks, expected.Ticks, tolerance.Ticks),
                    $"{RuleCategory}.{nameof(Around)}",
                    ("Expected", expected),
                    ("Tolerance", tolerance)));

        /// <summary>
        /// Rule that the targeted timespan must be at least zero.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TimeSpan> AtLeastZero() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    ComparisonGuards.AtLeast(value, TimeSpan.Zero),
                    $"{RuleCategory}.{nameof(AtLeastZero)}",
                    ("Min", TimeSpan.Zero)));

        /// <summary>
        /// Rule that the targeted timespan must be at most zero.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TimeSpan> AtMostZero() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    ComparisonGuards.AtMost(value, TimeSpan.Zero),
                    $"{RuleCategory}.{nameof(AtMostZero)}",
                    ("Max", TimeSpan.Zero)));
    }
}