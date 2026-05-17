using Toarnbeike.Results.Ensure.Guards;
using Toarnbeike.Results.Ensure.Implementation.RuleResults;

namespace Toarnbeike.Results.Ensure.Rules;

public static class TimeSpanRules
{
    extension(IGuardTarget<TimeSpan> target)
    {
        /// <summary>
        /// Rule that the targeted timespan must be at least the specified duration.
        /// </summary>
        public IGuardRuleResult AtLeast(TimeSpan duration) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, duration),
                $"{nameof(TimeSpanRules)}.{nameof(AtLeast)}", duration);

        /// <summary>
        /// Rule that the targeted timespan must be at most the specified duration.
        /// </summary>
        public IGuardRuleResult AtMost(TimeSpan duration) =>
            target.Evaluate(ComparisonGuards.AtMost(target.Value, duration),
                $"{nameof(TimeSpanRules)}.{nameof(AtMost)}", duration);

        /// <summary>
        /// Rule that the targeted timespan must be between the specified durations (inclusive).
        /// </summary>
        public IGuardRuleResult Between(TimeSpan min, TimeSpan max) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, min) &&
                            ComparisonGuards.AtMost(target.Value, max),
                $"{nameof(TimeSpanRules)}.{nameof(Between)}", (min, max));

        /// <summary>
        /// Rule that the targeted timespan must be close to the specified timespan, within the provided time span.
        /// </summary>
        public IGuardRuleResult CloseTo(TimeSpan expected, TimeSpan tolerance) =>
            target.Evaluate(ToleranceGuards.Equal(target.Value.Ticks, expected.Ticks, tolerance.Ticks),
                $"{nameof(TimeSpanRules)}.{nameof(CloseTo)}", (expected, tolerance));

        /// <summary>
        /// Rule that the targeted timespan must be at least zero.
        /// </summary>
        public IGuardRuleResult AtLeastZero() =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, TimeSpan.Zero),
                $"{nameof(TimeSpanRules)}.{nameof(AtLeastZero)}");

        /// <summary>
        /// Rule that the targeted timespan must be at most zero.
        /// </summary>
        public IGuardRuleResult AtMostZero() =>
            target.Evaluate(ComparisonGuards.AtMost(target.Value, TimeSpan.Zero),
                $"{nameof(TimeSpanRules)}.{nameof(AtMostZero)}");

        /// <summary>
        /// Rule that the targeted timespan must not be exactly zero.
        /// </summary>
        public IGuardRuleResult NotZero() =>
            target.Evaluate(ComparisonGuards.NotEqual(target.Value, TimeSpan.Zero),
                $"{nameof(TimeSpanRules)}.{nameof(NotZero)}");
    }
}