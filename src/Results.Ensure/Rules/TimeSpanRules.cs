using Toarnbeike.Results.Ensure.Guards;

namespace Toarnbeike.Results.Ensure.Rules;

public static class TimeSpanRules
{
    private const string RuleCategory = "TimeSpan";

    extension(IGuardTarget<TimeSpan> target)
    {
        /// <summary>
        /// Rule that the targeted timespan must be at least the specified min.
        /// </summary>
        public IGuardRuleResult AtLeast(TimeSpan min) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, min),
                $"{RuleCategory}.{nameof(AtLeast)}", 
                ("Min", min));

        /// <summary>
        /// Rule that the targeted timespan must be at most the specified max.
        /// </summary>
        public IGuardRuleResult AtMost(TimeSpan max) =>
            target.Evaluate(ComparisonGuards.AtMost(target.Value, max),
                $"{RuleCategory}.{nameof(AtMost)}", 
                ("Max", max));

        /// <summary>
        /// Rule that the targeted timespan must be between the specified durations (inclusive).
        /// </summary>
        public IGuardRuleResult Between(TimeSpan min, TimeSpan max) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, min) &&
                            ComparisonGuards.AtMost(target.Value, max),
                $"{RuleCategory}.{nameof(Between)}", 
                ("Min", min),
                ("Max", max));

        /// <summary>
        /// Rule that the targeted timespan must be close to the specified timespan, within the provided time span.
        /// </summary>
        public IGuardRuleResult Around(TimeSpan expected, TimeSpan tolerance) =>
            target.Evaluate(ToleranceGuards.Equal(target.Value.Ticks, expected.Ticks, tolerance.Ticks),
                $"{RuleCategory}.{nameof(Around)}", 
                ("Expected", expected),
                ("Tolerance", tolerance));

        /// <summary>
        /// Rule that the targeted timespan must be at least zero.
        /// </summary>
        public IGuardRuleResult AtLeastZero() =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, TimeSpan.Zero),
                $"{RuleCategory}.{nameof(AtLeastZero)}");

        /// <summary>
        /// Rule that the targeted timespan must be at most zero.
        /// </summary>
        public IGuardRuleResult AtMostZero() =>
            target.Evaluate(ComparisonGuards.AtMost(target.Value, TimeSpan.Zero),
                $"{RuleCategory}.{nameof(AtMostZero)}");
    }
}