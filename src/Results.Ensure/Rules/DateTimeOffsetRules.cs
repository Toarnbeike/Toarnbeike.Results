using Toarnbeike.Results.Ensure.Guards;

namespace Toarnbeike.Results.Ensure.Rules;

public static class DateTimeOffsetRules
{
    private const string RuleCategory = "Date";
    private const string QualifiedRuleCategory = "Date.DateTimeOffset";

    extension(IGuardTarget<DateTimeOffset> target)
    {
        private DateOnly DateOnlyValue => DateOnly.FromDateTime(target.Value.DateTime);

        /// <summary>
        /// Rule that the targeted date must be on or after the specified date.
        /// </summary>
        public IGuardRuleResult OnOrAfter(DateTimeOffset min) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, min),
                $"{RuleCategory}.{nameof(OnOrAfter)}",
                ("Min", min));

        /// <summary>
        /// Rule that the targeted date must be on or before the specified date.
        /// </summary>
        public IGuardRuleResult OnOrBefore(DateTimeOffset max) =>
            target.Evaluate(ComparisonGuards.AtMost(target.Value, max),
                $"{RuleCategory}.{nameof(OnOrBefore)}",
                ("Max", max));

        /// <summary>
        /// Rule that the targeted date must be between the specified dates.
        /// </summary>
        public IGuardRuleResult Between(DateTimeOffset min, DateTimeOffset max) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, min) &&
                            ComparisonGuards.AtMost(target.Value, max),
                $"{RuleCategory}.{nameof(Between)}",
                ("Min", min),
                ("Max", max));

        /// <summary>
        /// Rule that the targeted date must be in the future.
        /// </summary>
        public IGuardRuleResult Future()
        {
            var utcNow = target.TimeProvider.GetUtcNow();
            return target.Evaluate(ComparisonGuards.AtLeast(target.Value, utcNow),
                $"{RuleCategory}.{nameof(Future)}",
                ("Now", utcNow));
        }

        /// <summary>
        /// Rule that the targeted date must be in the past.
        /// </summary>
        public IGuardRuleResult Past()
        {
            var utcNow = target.TimeProvider.GetUtcNow();
            return target.Evaluate(ComparisonGuards.AtMost(target.Value, utcNow),
                $"{RuleCategory}.{nameof(Past)}",
                ("Now", utcNow));
        }

        /// <summary>
        /// Rule that the targeted date must be in the future, but by no more than the provided time span.
        /// </summary>
        public IGuardRuleResult WithinFuture(TimeSpan timeSpan)
        {
            var utcNow = target.TimeProvider.GetUtcNow();
            return target.Evaluate(ComparisonGuards.AtLeast(target.Value, utcNow) &&
                                   ComparisonGuards.AtMost(target.Value, utcNow + timeSpan),
                $"{QualifiedRuleCategory}.{nameof(WithinFuture)}",
                ("Now", utcNow),
                ("TimeSpan", timeSpan));
        }


        /// <summary>
        /// Rule that the targeted date must be in the past, but by no more than the provided time span.
        /// </summary>
        public IGuardRuleResult WithinPast(TimeSpan timeSpan)
        {
            var utcNow = target.TimeProvider.GetUtcNow();
            return target.Evaluate(ComparisonGuards.AtLeast(target.Value, utcNow - timeSpan) &&
                            ComparisonGuards.AtMost(target.Value, utcNow),
                $"{QualifiedRuleCategory}.{nameof(WithinPast)}",
                ("Now", utcNow),
                ("TimeSpan", timeSpan));
        }

        /// <summary>
        /// Rule that the targeted date must be close to the specified date, within the provided time span.
        /// </summary>
        public IGuardRuleResult Around(DateTimeOffset comparison, TimeSpan tolerance) =>
            target.Evaluate(ToleranceGuards.Equal(target.Value.Ticks, comparison.Ticks, tolerance.Ticks),
                $"{QualifiedRuleCategory}.{nameof(Around)}",
                ("Comparison", comparison),
                ("Tolerance", tolerance));

        /// <summary>
        /// Rule that the targeted date must be on the specified day of the week.
        /// </summary>
        public IGuardRuleResult OnDayOfWeek(DayOfWeek dayOfWeek) =>
            target.Evaluate(DayOfWeekGuards.OnDayOfWeek(target.DateOnlyValue, dayOfWeek),
                $"{RuleCategory}.{nameof(OnDayOfWeek)}",
                ("Actual", target.Value.DayOfWeek),
                ("ExpectedDay", dayOfWeek));

        /// <summary>
        /// Rule that the targeted date must be on one of the specified days of the week.
        /// </summary>
        public IGuardRuleResult OnDaysOfWeek(params DayOfWeek[] allowed) =>
            target.Evaluate(DayOfWeekGuards.OnDaysOfWeek(target.DateOnlyValue, allowed),
                $"{RuleCategory}.{nameof(OnDaysOfWeek)}",
                ("Actual", target.Value.DayOfWeek),
                ("Allowed", allowed));

        /// <summary>
        /// Rule that the targeted date must be on a weekday.
        /// </summary>
        public IGuardRuleResult OnWeekday() =>
            target.Evaluate(DayOfWeekGuards.OnDaysOfWeek(target.DateOnlyValue, [DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday]),
                $"{RuleCategory}.{nameof(OnWeekday)}",
                ("Actual", target.Value.DayOfWeek));

        /// <summary>
        /// Rule that the targeted date must be on a weekend day.
        /// </summary>
        public IGuardRuleResult OnWeekend() =>
            target.Evaluate(DayOfWeekGuards.OnDaysOfWeek(target.DateOnlyValue, [DayOfWeek.Saturday, DayOfWeek.Sunday]),
                $"{RuleCategory}.{nameof(OnWeekend)}",
                ("Actual", target.Value.DayOfWeek));
    }
}