using Toarnbeike.Results.Guards.Implementations.Guards;

namespace Toarnbeike.Results.Guards.Rules;

public static class DateTimeRules
{
    private const string RuleCategory = "Date";
    private const string QualifiedRuleCategory = "Date.DateTime";

    extension(IGuardTarget<DateTime> target)
    { 
        private DateOnly DateOnlyValue => DateOnly.FromDateTime(target.Value);

        /// <summary>
        /// Rule that the targeted date must be on or after the specified date.
        /// </summary>
        public IGuardRuleResult<DateTime> OnOrAfter(DateTime min)
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ComparisonGuards.AtLeast(target.Value, min),
                    $"{RuleCategory}.{nameof(OnOrAfter)}", 
                    ("Min", min));
        }

        /// <summary>
        /// Rule that the targeted date must be on or before the specified date.
        /// </summary>
        public IGuardRuleResult<DateTime> OnOrBefore(DateTime max)
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ComparisonGuards.AtMost(target.Value, max),
                    $"{RuleCategory}.{nameof(OnOrBefore)}", 
                    ("Max", max));
        }

        /// <summary>
        /// Rule that the targeted date must be between the specified dates.
        /// </summary>
        public IGuardRuleResult<DateTime> Between(DateTime min, DateTime max)
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ComparisonGuards.AtLeast(target.Value, min) &&
                                ComparisonGuards.AtMost(target.Value, max),
                    $"{RuleCategory}.{nameof(Between)}", 
                    ("Min", min), 
                    ("Max", max));
        }

        /// <summary>
        /// Rule that the targeted date must be in the future.
        /// </summary>
        public IGuardRuleResult<DateTime> Future()
        {
            var utcNow = target.GuardContext.TimeProvider.GetUtcNow();
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ComparisonGuards.AtLeast(target.Value, utcNow),
                    $"{RuleCategory}.{nameof(Future)}",
                    ("Now", utcNow.DateTime));
        }

        /// <summary>
        /// Rule that the targeted date must be in the past.
        /// </summary>
        public IGuardRuleResult<DateTime> Past()
        {
            var utcNow = target.GuardContext.TimeProvider.GetUtcNow();
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ComparisonGuards.AtMost(target.Value, utcNow),
                    $"{RuleCategory}.{nameof(Past)}",
                    ("Now", utcNow.DateTime));
        }

        /// <summary>
        /// Rule that the targeted date must be in the future, but by no more than the provided time span.
        /// </summary>
        public IGuardRuleResult<DateTime> WithinFuture(TimeSpan timeSpan)
        {
            var utcNow = target.GuardContext.TimeProvider.GetUtcNow();
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ComparisonGuards.AtLeast(target.Value, utcNow) &&
                                ComparisonGuards.AtMost(target.Value, utcNow + timeSpan),
                    $"{QualifiedRuleCategory}.{nameof(WithinFuture)}",
                    ("Now", utcNow.DateTime),
                    ("TimeSpan", timeSpan));
        }


        /// <summary>
        /// Rule that the targeted date must be in the past, but by no more than the provided time span.
        /// </summary>
        public IGuardRuleResult<DateTime> WithinPast(TimeSpan timeSpan)
        {
            var utcNow = target.GuardContext.TimeProvider.GetUtcNow();
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ComparisonGuards.AtLeast(target.Value, utcNow - timeSpan) &&
                                ComparisonGuards.AtMost(target.Value, utcNow),
                    $"{QualifiedRuleCategory}.{nameof(WithinPast)}", 
                    ("Now", utcNow.DateTime),
                    ("TimeSpan", timeSpan));
        }

        /// <summary>
        /// Rule that the targeted date must be close to the specified date, within the provided time span.
        /// </summary>
        public IGuardRuleResult<DateTime> Around(DateTime comparison, TimeSpan tolerance)
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ToleranceGuards.Equal(target.Value.Ticks, comparison.Ticks, tolerance.Ticks),
                    $"{QualifiedRuleCategory}.{nameof(Around)}", 
                    ("Comparison", comparison),
                    ("Tolerance", tolerance));
        }

        /// <summary>
        /// Rule that the targeted date must be on the specified day of the week.
        /// </summary>
        public IGuardRuleResult<DateTime> OnDayOfWeek(DayOfWeek dayOfWeek)
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(DayOfWeekGuards.OnDayOfWeek(target.DateOnlyValue, dayOfWeek),
                    $"{RuleCategory}.{nameof(OnDayOfWeek)}",
                    ("Actual", target.Value.DayOfWeek),
                    ("ExpectedDay", dayOfWeek));
        }

        /// <summary>
        /// Rule that the targeted date must be on one of the specified days of the week.
        /// </summary>
        public IGuardRuleResult<DateTime> OnDaysOfWeek(params DayOfWeek[] allowed)
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(DayOfWeekGuards.OnDaysOfWeek(target.DateOnlyValue, allowed),
                    $"{RuleCategory}.{nameof(OnDaysOfWeek)}",
                    ("Actual", target.Value.DayOfWeek),
                    ("Allowed", allowed));
        }

        /// <summary>
        /// Rule that the targeted date must be on a weekday.
        /// </summary>
        public IGuardRuleResult<DateTime> OnWeekday()
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(DayOfWeekGuards.OnDaysOfWeek(target.DateOnlyValue, [DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday]),
                    $"{RuleCategory}.{nameof(OnWeekday)}",
                    ("Actual", target.Value.DayOfWeek));
        }

        /// <summary>
        /// Rule that the targeted date must be on a weekend day.
        /// </summary>
        public IGuardRuleResult<DateTime> OnWeekend()
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(DayOfWeekGuards.OnDaysOfWeek(target.DateOnlyValue, [DayOfWeek.Saturday, DayOfWeek.Sunday]),
                    $"{RuleCategory}.{nameof(OnWeekend)}",
                    ("Actual", target.Value.DayOfWeek));
        }
    }
}