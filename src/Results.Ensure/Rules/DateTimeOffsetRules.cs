using Toarnbeike.Results.Ensure.Guards;

namespace Toarnbeike.Results.Ensure.Rules;

public static class DateTimeOffsetRules
{
    extension(IGuardTarget<DateTimeOffset> target)
    {
        private IGuardTarget<DateOnly> AsDateOnly() => 
            target.As(t => DateOnly.FromDateTime(t.DateTime));

        /// <summary>
        /// Rule that the targeted date must be on or after the specified date.
        /// </summary>
        public IGuardRuleResult OnOrAfter(DateTimeOffset other) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, other),
                $"{nameof(DateTimeOffsetRules)}.{nameof(OnOrAfter)}", other);

        /// <summary>
        /// Rule that the targeted date must be on or before the specified date.
        /// </summary>
        public IGuardRuleResult OnOrBefore(DateTimeOffset other) =>
            target.Evaluate(ComparisonGuards.AtMost(target.Value, other),
                $"{nameof(DateTimeOffsetRules)}.{nameof(OnOrBefore)}", other);

        /// <summary>
        /// Rule that the targeted date must be between the specified dates.
        /// </summary>
        public IGuardRuleResult Between(DateTimeOffset start, DateTimeOffset end) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, start) &&
                            ComparisonGuards.AtMost(target.Value, end),
                $"{nameof(DateTimeOffsetRules)}.{nameof(Between)}", (start, end));

        /// <summary>
        /// Rule that the targeted date must be in the future.
        /// </summary>
        public IGuardRuleResult Future() =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, DateTimeOffset.UtcNow),
                $"{nameof(DateTimeOffsetRules)}.{nameof(Future)}");

        /// <summary>
        /// Rule that the targeted date must be in the past.
        /// </summary>
        public IGuardRuleResult Past() =>
            target.Evaluate(ComparisonGuards.AtMost(target.Value, DateTimeOffset.UtcNow),
                $"{nameof(DateTimeOffsetRules)}.{nameof(Past)}");

        /// <summary>
        /// Rule that the targeted date must be in the future, but by no more than the provided time span.
        /// </summary>
        public IGuardRuleResult WithinFuture(TimeSpan timeSpan) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, DateTimeOffset.UtcNow) &&
                            ComparisonGuards.AtMost(target.Value, DateTimeOffset.UtcNow + timeSpan),
                $"{nameof(DateTimeOffsetRules)}.{nameof(WithinFuture)}", timeSpan);


        /// <summary>
        /// Rule that the targeted date must be in the past, but by no more than the provided time span.
        /// </summary>
        public IGuardRuleResult WithinPast(TimeSpan timeSpan) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, DateTimeOffset.UtcNow - timeSpan) &&
                            ComparisonGuards.AtMost(target.Value, DateTimeOffset.UtcNow),
                $"{nameof(DateTimeOffsetRules)}.{nameof(WithinPast)}", timeSpan);

        /// <summary>
        /// Rule that the targeted date must be close to the specified date, within the provided time span.
        /// </summary>
        public IGuardRuleResult CloseTo(DateTimeOffset expected, TimeSpan tolerance) =>
            target.Evaluate(ToleranceGuards.Equal(target.Value.Ticks, expected.Ticks, tolerance.Ticks),
                $"{nameof(DateTimeOffsetRules)}.{nameof(CloseTo)}", (expected, tolerance));

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