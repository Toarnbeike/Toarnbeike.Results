using Toarnbeike.Results.Ensure.Guards;
using Toarnbeike.Results.Ensure.Implementation.RuleResults;

namespace Toarnbeike.Results.Ensure.Rules;

public static class DateOnlyRules
{
    private static DateOnly Today => DateOnly.FromDateTime(DateTime.UtcNow);

    extension(IGuardTarget<DateOnly> target)
    {
        /// <summary>
        /// Rule that the targeted date must be on or after the specified date.
        /// </summary>
        public IGuardRuleResult OnOrAfter(DateOnly other) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, other),
                $"{nameof(DateOnlyRules)}.{nameof(OnOrAfter)}", other);

        /// <summary>
        /// Rule that the targeted date must be on or before the specified date.
        /// </summary>
        public IGuardRuleResult OnOrBefore(DateOnly other) =>
            target.Evaluate(ComparisonGuards.AtMost(target.Value, other),
                $"{nameof(DateOnlyRules)}.{nameof(OnOrBefore)}", other);

        /// <summary>
        /// Rule that the targeted date must be between the specified dates.
        /// </summary>
        public IGuardRuleResult Between(DateOnly start, DateOnly end) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, start) &&
                            ComparisonGuards.AtMost(target.Value, end),
                $"{nameof(DateOnlyRules)}.{nameof(Between)}", (start, end));

        /// <summary>
        /// Rule that the targeted date must be in the future.
        /// </summary>
        public IGuardRuleResult Future() =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, Today),
                $"{nameof(DateOnlyRules)}.{nameof(Future)}");

        /// <summary>
        /// Rule that the targeted date must be in the past.
        /// </summary>
        public IGuardRuleResult Past() =>
            target.Evaluate(ComparisonGuards.AtMost(target.Value, Today),
                $"{nameof(DateOnlyRules)}.{nameof(Past)}");

        /// <summary>
        /// Rule that the targeted date must be in the future, but by no more than the provided number of days.
        /// </summary>
        public IGuardRuleResult WithinFuture(int days) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, Today) &&
                            ComparisonGuards.AtMost(target.Value, Today.AddDays(days)),
                $"{nameof(DateOnlyRules)}.{nameof(WithinFuture)}", days);

        /// <summary>
        /// Rule that the targeted date must be in the past, but by no more than the provided number of days.
        /// </summary>
        public IGuardRuleResult WithinPast(int days) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, Today.AddDays(-days)) &&
                            ComparisonGuards.AtMost(target.Value, Today),
                $"{nameof(DateOnlyRules)}.{nameof(WithinPast)}", days);

        /// <summary>
        /// Rule that the targeted date must be on the specified day of the week.
        /// </summary>
        public IGuardRuleResult OnDayOfWeek(DayOfWeek dayOfWeek) =>
            target.Evaluate(DayOfWeekGuards.OnDayOfWeek(target.Value, dayOfWeek),
                $"{nameof(DateOnlyRules)}.{nameof(OnDayOfWeek)}", dayOfWeek);

        /// <summary>
        /// Rule that the targeted date must be on one of the specified days of the week.
        /// </summary>
        public IGuardRuleResult OnDaysOfWeek(params DayOfWeek[] allowed) =>
            target.Evaluate(DayOfWeekGuards.OnDaysOfWeek(target.Value, allowed),
                $"{nameof(DateOnlyRules)}.{nameof(OnDaysOfWeek)}", allowed);

        /// <summary>
        /// Rule that the targeted date must be on a weekday.
        /// </summary>
        public IGuardRuleResult OnWeekday() =>
            target.Evaluate(DayOfWeekGuards.OnDaysOfWeek(target.Value, [DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday]),
                $"{nameof(DateOnlyRules)}.{nameof(OnWeekday)}");

        /// <summary>
        /// Rule that the targeted date must be on a weekend day.
        /// </summary>
        public IGuardRuleResult OnWeekend() =>
            target.Evaluate(DayOfWeekGuards.OnDaysOfWeek(target.Value, [DayOfWeek.Saturday, DayOfWeek.Sunday]),
                $"{nameof(DateOnlyRules)}.{nameof(OnWeekend)}");
    }
}