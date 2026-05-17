using Toarnbeike.Results.Ensure.Guards;
using Toarnbeike.Results.Ensure.Implementation.RuleResults;

namespace Toarnbeike.Results.Ensure.Rules;

public static class DateTimeRules
{
    extension(IGuardTarget<DateTime> target)
    { 
        private IGuardTarget<DateOnly> AsDateOnly() => target.As(DateOnly.FromDateTime);

        /// <summary>
        /// Rule that the targeted date must be on or after the specified date.
        /// </summary>
        public IGuardRuleResult OnOrAfter(DateTime other) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, other),
                $"{nameof(DateTimeRules)}.{nameof(OnOrAfter)}", other);

        /// <summary>
        /// Rule that the targeted date must be on or before the specified date.
        /// </summary>
        public IGuardRuleResult OnOrBefore(DateTime other) =>
            target.Evaluate(ComparisonGuards.AtMost(target.Value, other),
                $"{nameof(DateTimeRules)}.{nameof(OnOrBefore)}", other);

        /// <summary>
        /// Rule that the targeted date must be between the specified dates.
        /// </summary>
        public IGuardRuleResult Between(DateTime start, DateTime end) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, start) &&
                            ComparisonGuards.AtMost(target.Value, end),
                $"{nameof(DateTimeRules)}.{nameof(Between)}", (start, end));

        /// <summary>
        /// Rule that the targeted date must be in the future.
        /// </summary>
        public IGuardRuleResult Future() =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, DateTime.UtcNow),
                $"{nameof(DateTimeRules)}.{nameof(Future)}");

        /// <summary>
        /// Rule that the targeted date must be in the past.
        /// </summary>
        public IGuardRuleResult Past() =>
            target.Evaluate(ComparisonGuards.AtMost(target.Value, DateTime.UtcNow),
                $"{nameof(DateTimeRules)}.{nameof(Past)}");

        /// <summary>
        /// Rule that the targeted date must be in the future, but by no more than the provided time span.
        /// </summary>
        public IGuardRuleResult WithinFuture(TimeSpan timeSpan) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, DateTime.UtcNow) &&
                            ComparisonGuards.AtMost(target.Value, DateTime.UtcNow + timeSpan),
                $"{nameof(DateTimeRules)}.{nameof(WithinFuture)}", timeSpan);


        /// <summary>
        /// Rule that the targeted date must be in the past, but by no more than the provided time span.
        /// </summary>
        public IGuardRuleResult WithinPast(TimeSpan timeSpan) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, DateTime.UtcNow - timeSpan) &&
                            ComparisonGuards.AtMost(target.Value, DateTime.UtcNow),
                $"{nameof(DateTimeRules)}.{nameof(WithinPast)}", timeSpan);

        /// <summary>
        /// Rule that the targeted date must be close to the specified date, within the provided time span.
        /// </summary>
        public IGuardRuleResult CloseTo(DateTime expected, TimeSpan tolerance) =>
            target.Evaluate(ToleranceGuards.Equal(target.Value.Ticks, expected.Ticks, tolerance.Ticks),
                $"{nameof(DateTimeRules)}.{nameof(CloseTo)}", (expected, tolerance));

        /// <summary>
        /// Rule that the targeted date must be on the specified day of the week.
        /// </summary>
        public IGuardRuleResult OnDayOfWeek(DayOfWeek dayOfWeek) =>
            target.AsDateOnly().OnDayOfWeek(dayOfWeek);

        /// <summary>
        /// Rule that the targeted date must be on one of the specified days of the week.
        /// </summary>
        public IGuardRuleResult OnDaysOfWeek(params DayOfWeek[] allowed) =>
            target.AsDateOnly().OnDaysOfWeek(allowed);

        /// <summary>
        /// Rule that the targeted date must be on a weekday.
        /// </summary>
        public IGuardRuleResult OnWeekday() => target.AsDateOnly().OnWeekday();

        /// <summary>
        /// Rule that the targeted date must be on a weekend day.
        /// </summary>
        public IGuardRuleResult OnWeekend() => target.AsDateOnly().OnWeekend();
    }
}