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
        public IGuardRuleResult OnOrAfter(DateTimeOffset min) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, min),
                $"{nameof(DateRules)}.{nameof(OnOrAfter)}", 
                ("Min", min));

        /// <summary>
        /// Rule that the targeted date must be on or before the specified date.
        /// </summary>
        public IGuardRuleResult OnOrBefore(DateTimeOffset max) =>
            target.Evaluate(ComparisonGuards.AtMost(target.Value, max),
                $"{nameof(DateRules)}.{nameof(OnOrBefore)}", 
                ("Max", max));

        /// <summary>
        /// Rule that the targeted date must be between the specified dates.
        /// </summary>
        public IGuardRuleResult Between(DateTimeOffset min, DateTimeOffset max) =>
            target.Evaluate(ComparisonGuards.AtLeast(target.Value, min) &&
                            ComparisonGuards.AtMost(target.Value, max),
                $"{nameof(DateRules)}.{nameof(Between)}", 
                ("Min", min), 
                ("Max", max));

        /// <summary>
        /// Rule that the targeted date must be in the future.
        /// </summary>
        public IGuardRuleResult Future()
        {
            var utcNow = DateTimeOffset.UtcNow;
            return target.Evaluate(ComparisonGuards.AtLeast(target.Value, utcNow),
                $"{nameof(DateRules)}.{nameof(Future)}",
                ("Now", utcNow));
        }

        /// <summary>
        /// Rule that the targeted date must be in the past.
        /// </summary>
        public IGuardRuleResult Past()
        {
            var utcNow = DateTimeOffset.UtcNow;
            return target.Evaluate(ComparisonGuards.AtMost(target.Value, utcNow),
                $"{nameof(DateRules)}.{nameof(Past)}",
                ("Now", utcNow));
        }

        /// <summary>
        /// Rule that the targeted date must be in the future, but by no more than the provided time span.
        /// </summary>
        public IGuardRuleResult WithinFuture(TimeSpan timeSpan)
        {
            var utcNow = DateTimeOffset.UtcNow;
            return target.Evaluate(ComparisonGuards.AtLeast(target.Value, utcNow) &&
                                   ComparisonGuards.AtMost(target.Value, utcNow + timeSpan),
                $"{nameof(DateTimeOffsetRules)}.{nameof(WithinFuture)}",
                ("Now", utcNow),
                ("TimeSpan", timeSpan));
        }


        /// <summary>
        /// Rule that the targeted date must be in the past, but by no more than the provided time span.
        /// </summary>
        public IGuardRuleResult WithinPast(TimeSpan timeSpan)
        {
            var utcNow = DateTimeOffset.UtcNow;
            return target.Evaluate(ComparisonGuards.AtLeast(target.Value, utcNow - timeSpan) &&
                                   ComparisonGuards.AtMost(target.Value, utcNow),
                $"{nameof(DateTimeOffsetRules)}.{nameof(WithinPast)}",
                ("Now", utcNow),
                ("TimeSpan", timeSpan));
        }

        /// <summary>
        /// Rule that the targeted date must be close to the specified date, within the provided time span.
        /// </summary>
        public IGuardRuleResult Around(DateTimeOffset comparison, TimeSpan tolerance) =>
            target.Evaluate(ToleranceGuards.Equal(target.Value.Ticks, comparison.Ticks, tolerance.Ticks),
                $"{nameof(DateTimeOffsetRules)}.{nameof(Around)}", 
                ("Comparison", comparison), 
                ("Tolerance", tolerance));

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