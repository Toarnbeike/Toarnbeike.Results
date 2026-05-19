using Toarnbeike.Results.Ensure.Guards;

namespace Toarnbeike.Results.Ensure.Rules;

public static class DateOnlyRules
{
    private static DateOnly Today => DateOnly.FromDateTime(DateTime.UtcNow);

    extension(IGuardTarget<DateOnly> target)
    {
        /// <summary>
        /// Rule that the targeted date must be on or after the specified date.
        /// </summary>
        public IGuardRuleResult OnOrAfter(DateOnly min) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, min),
                $"{nameof(DateRules)}.{nameof(OnOrAfter)}", 
                ("Min", min));

        /// <summary>
        /// Rule that the targeted date must be on or before the specified date.
        /// </summary>
        public IGuardRuleResult OnOrBefore(DateOnly max) =>
            target.Evaluate(ComparisonGuards.AtMost(target.Value, max),
                $"{nameof(DateRules)}.{nameof(OnOrBefore)}", 
                ("Max", max));

        /// <summary>
        /// Rule that the targeted date must be between the specified dates.
        /// </summary>
        public IGuardRuleResult Between(DateOnly min, DateOnly max) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, min) &&
                            ComparisonGuards.AtMost(target.Value, max),
                $"{nameof(DateRules)}.{nameof(Between)}", 
                ("Min", min),
                ("Max", max));

        /// <summary>
        /// Rule that the targeted date must be in the future.
        /// </summary>
        public IGuardRuleResult Future() =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, Today),
                $"{nameof(DateRules)}.{nameof(Future)}",
                ("Now", Today));

        /// <summary>
        /// Rule that the targeted date must be in the past.
        /// </summary>
        public IGuardRuleResult Past() =>
            target.Evaluate(ComparisonGuards.AtMost(target.Value, Today),
                $"{nameof(DateRules)}.{nameof(Past)}",
                ("Now", Today));

        /// <summary>
        /// Rule that the targeted date must be in the future, but by no more than the provided number of days.
        /// </summary>
        public IGuardRuleResult WithinFuture(int days) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, Today) &&
                            ComparisonGuards.AtMost(target.Value, Today.AddDays(days)),
                $"{nameof(DateOnlyRules)}.{nameof(WithinFuture)}", 
                ("Today", Today),
                ("Days", days));

        /// <summary>
        /// Rule that the targeted date must be in the past, but by no more than the provided number of days.
        /// </summary>
        public IGuardRuleResult WithinPast(int days) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, Today.AddDays(-days)) &&
                            ComparisonGuards.AtMost(target.Value, Today),
                $"{nameof(DateOnlyRules)}.{nameof(WithinPast)}", 
                ("Today", Today), 
                ("Days", days));

        /// <summary>
        /// Rule that the targeted date must be on the specified day of the week.
        /// </summary>
        public IGuardRuleResult OnDayOfWeek(DayOfWeek dayOfWeek) =>
            target.Evaluate(DayOfWeekGuards.OnDayOfWeek(target.Value, dayOfWeek),
                $"{nameof(DateRules)}.{nameof(OnDayOfWeek)}",
                ("Actual", target.Value.DayOfWeek), 
                ("ExpectedDay", dayOfWeek));

        /// <summary>
        /// Rule that the targeted date must be on one of the specified days of the week.
        /// </summary>
        public IGuardRuleResult OnDaysOfWeek(params DayOfWeek[] allowed) =>
            target.Evaluate(DayOfWeekGuards.OnDaysOfWeek(target.Value, allowed),
                $"{nameof(DateRules)}.{nameof(OnDaysOfWeek)}",
                ("Actual", target.Value.DayOfWeek),
                ("Allowed", allowed));

        /// <summary>
        /// Rule that the targeted date must be on a weekday.
        /// </summary>
        public IGuardRuleResult OnWeekday() =>
            target.Evaluate(DayOfWeekGuards.OnDaysOfWeek(target.Value, [DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday]),
                $"{nameof(DateRules)}.{nameof(OnWeekday)}", 
                ("Actual", target.Value.DayOfWeek));

        /// <summary>
        /// Rule that the targeted date must be on a weekend day.
        /// </summary>
        public IGuardRuleResult OnWeekend() =>
            target.Evaluate(DayOfWeekGuards.OnDaysOfWeek(target.Value, [DayOfWeek.Saturday, DayOfWeek.Sunday]),
                $"{nameof(DateRules)}.{nameof(OnWeekend)}",
                ("Actual", target.Value.DayOfWeek));
    }
}