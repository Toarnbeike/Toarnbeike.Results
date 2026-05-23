using Toarnbeike.Results.Ensure.Guards;

namespace Toarnbeike.Results.Ensure.Rules;

public static class DateOnlyRules
{
    private const string RuleCategory = "Date";
    private const string QualifiedRuleCategory = "Date.DateOnly";

    extension(IGuardTarget<DateOnly> target)
    {
        private DateOnly Today => DateOnly.FromDateTime(target.TimeProvider.GetUtcNow().DateTime);

        /// <summary>
        /// Rule that the targeted date must be on or after the specified date.
        /// </summary>
        public IGuardRuleResult OnOrAfter(DateOnly min) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, min),
                $"{RuleCategory}.{nameof(OnOrAfter)}", 
                ("Min", min));

        /// <summary>
        /// Rule that the targeted date must be on or before the specified date.
        /// </summary>
        public IGuardRuleResult OnOrBefore(DateOnly max) =>
            target.Evaluate(ComparisonGuards.AtMost(target.Value, max),
                $"{RuleCategory}.{nameof(OnOrBefore)}", 
                ("Max", max));

        /// <summary>
        /// Rule that the targeted date must be between the specified dates.
        /// </summary>
        public IGuardRuleResult Between(DateOnly min, DateOnly max) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, min) &&
                            ComparisonGuards.AtMost(target.Value, max),
                $"{RuleCategory}.{nameof(Between)}", 
                ("Min", min),
                ("Max", max));

        /// <summary>
        /// Rule that the targeted date must be in the future.
        /// </summary>
        public IGuardRuleResult Future() =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, target.Today),
                $"{RuleCategory}.{nameof(Future)}",
                ("Now", target.Today));

        /// <summary>
        /// Rule that the targeted date must be in the past.
        /// </summary>
        public IGuardRuleResult Past() =>
            target.Evaluate(ComparisonGuards.AtMost(target.Value, target.Today),
                $"{RuleCategory}.{nameof(Past)}",
                ("Now", target.Today));

        /// <summary>
        /// Rule that the targeted date must be in the future, but by no more than the provided number of days.
        /// </summary>
        public IGuardRuleResult WithinFuture(int days) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, target.Today) &&
                            ComparisonGuards.AtMost(target.Value, target.Today.AddDays(days)),
                $"{QualifiedRuleCategory}.{nameof(WithinFuture)}", 
                ("Today", target.Today),
                ("Days", days));

        /// <summary>
        /// Rule that the targeted date must be in the past, but by no more than the provided number of days.
        /// </summary>
        public IGuardRuleResult WithinPast(int days) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, target.Today.AddDays(-days)) &&
                            ComparisonGuards.AtMost(target.Value, target.Today),
                $"{QualifiedRuleCategory}.{nameof(WithinPast)}", 
                ("Today", target.Today), 
                ("Days", days));

        /// <summary>
        /// Rule that the targeted date must be on the specified day of the week.
        /// </summary>
        public IGuardRuleResult OnDayOfWeek(DayOfWeek dayOfWeek) =>
            target.Evaluate(DayOfWeekGuards.OnDayOfWeek(target.Value, dayOfWeek),
                $"{RuleCategory}.{nameof(OnDayOfWeek)}",
                ("Actual", target.Value.DayOfWeek), 
                ("ExpectedDay", dayOfWeek));

        /// <summary>
        /// Rule that the targeted date must be on one of the specified days of the week.
        /// </summary>
        public IGuardRuleResult OnDaysOfWeek(params DayOfWeek[] allowed) =>
            target.Evaluate(DayOfWeekGuards.OnDaysOfWeek(target.Value, allowed),
                $"{RuleCategory}.{nameof(OnDaysOfWeek)}",
                ("Actual", target.Value.DayOfWeek),
                ("Allowed", allowed));

        /// <summary>
        /// Rule that the targeted date must be on a weekday.
        /// </summary>
        public IGuardRuleResult OnWeekday() =>
            target.Evaluate(DayOfWeekGuards.OnDaysOfWeek(target.Value, [DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday]),
                $"{RuleCategory}.{nameof(OnWeekday)}", 
                ("Actual", target.Value.DayOfWeek));

        /// <summary>
        /// Rule that the targeted date must be on a weekend day.
        /// </summary>
        public IGuardRuleResult OnWeekend() =>
            target.Evaluate(DayOfWeekGuards.OnDaysOfWeek(target.Value, [DayOfWeek.Saturday, DayOfWeek.Sunday]),
                $"{RuleCategory}.{nameof(OnWeekend)}",
                ("Actual", target.Value.DayOfWeek));
    }
}