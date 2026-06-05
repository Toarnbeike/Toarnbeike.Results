using Toarnbeike.Results.Guards.Core.Guards;

namespace Toarnbeike.Results.Guards.Rules;

public static class DateTimeRules
{
    private const string RuleCategory = "Date";
    private const string QualifiedRuleCategory = "Date.DateTime";

    extension(IGuardTarget<DateTime> target)
    {
        /// <summary>
        /// Rule that the targeted date must be on or after the specified date.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<DateTime> OnOrAfter(DateTime min) =>
            target.Evaluate(value => 
                new RuleEvaluation(
                    ComparisonGuards.AtLeast(value, min),
                    $"{RuleCategory}.{nameof(OnOrAfter)}",
                    ("Min", min)));

        /// <summary>
        /// Rule that the targeted date must be on or before the specified date.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<DateTime> OnOrBefore(DateTime max) =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    ComparisonGuards.AtMost(value, max),
                    $"{RuleCategory}.{nameof(OnOrBefore)}",
                    ("Max", max)));

        /// <summary>
        /// Rule that the targeted date must be between the specified dates.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<DateTime> Between(DateTime min, DateTime max) =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    ComparisonGuards.AtLeast(value, min) && ComparisonGuards.AtMost(value, max),
                    $"{RuleCategory}.{nameof(Between)}",
                    ("Min", min),
                    ("Max", max)));

        /// <summary>
        /// Rule that the targeted date must be in the future.
        /// </summary>
        public IGuardRuleResult<DateTime> Future() =>
            target.Evaluate(value =>
            {
                var utcNow = target.GuardContext.TimeProvider.GetUtcNow();
                return new RuleEvaluation(
                    ComparisonGuards.AtLeast(value, utcNow),
                    $"{RuleCategory}.{nameof(Future)}",
                    ("Now", utcNow.DateTime));
            });

        /// <summary>
        /// Rule that the targeted date must be in the past.
        /// </summary>
        public IGuardRuleResult<DateTime> Past() =>
            target.Evaluate(value =>
            {
                var utcNow = target.GuardContext.TimeProvider.GetUtcNow();
                return new RuleEvaluation(
                    ComparisonGuards.AtMost(value, utcNow),
                    $"{RuleCategory}.{nameof(Past)}",
                    ("Now", utcNow.DateTime));
            });

        /// <summary>
        /// Rule that the targeted date must be in the future, but by no more than the provided time span.
        /// </summary>
        public IGuardRuleResult<DateTime> WithinFuture(TimeSpan timeSpan) =>
            target.Evaluate(value =>
            {
                var utcNow = target.GuardContext.TimeProvider.GetUtcNow();
                return new RuleEvaluation(
                    ComparisonGuards.AtLeast(value, utcNow) && ComparisonGuards.AtMost(value, utcNow + timeSpan),
                    $"{QualifiedRuleCategory}.{nameof(WithinFuture)}",
                    ("Now", utcNow.DateTime),
                    ("TimeSpan", timeSpan));
            });

        /// <summary>
        /// Rule that the targeted date must be in the past, but by no more than the provided time span.
        /// </summary>
        public IGuardRuleResult<DateTime> WithinPast(TimeSpan timeSpan) =>
            target.Evaluate(value =>
            {
                var utcNow = target.GuardContext.TimeProvider.GetUtcNow();
                return new RuleEvaluation(
                    ComparisonGuards.AtLeast(value, utcNow - timeSpan) && ComparisonGuards.AtMost(value, utcNow),
                    $"{QualifiedRuleCategory}.{nameof(WithinPast)}",
                    ("Now", utcNow.DateTime),
                    ("TimeSpan", timeSpan));
            });

        /// <summary>
        /// Rule that the targeted date must be close to the specified date, within the provided time span.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<DateTime> Around(DateTime comparison, TimeSpan tolerance) =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    ToleranceGuards.Equal(value.Ticks, comparison.Ticks, tolerance.Ticks),
                    $"{QualifiedRuleCategory}.{nameof(Around)}",
                    ("Comparison", comparison),
                    ("Tolerance", tolerance)));

        /// <summary>
        /// Rule that the targeted date must be on the specified day of the week.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<DateTime> OnDayOfWeek(DayOfWeek dayOfWeek) =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    DayOfWeekGuards.OnDayOfWeek(DateOnly.FromDateTime(value), dayOfWeek),
                    $"{RuleCategory}.{nameof(OnDayOfWeek)}",
                    ("Actual", value.DayOfWeek),
                    ("ExpectedDay", dayOfWeek)));

        /// <summary>
        /// Rule that the targeted date must be on one of the specified days of the week.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<DateTime> OnDaysOfWeek(params DayOfWeek[] allowed) =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    DayOfWeekGuards.OnDaysOfWeek(DateOnly.FromDateTime(value), allowed),
                    $"{RuleCategory}.{nameof(OnDaysOfWeek)}",
                    ("Actual", value.DayOfWeek),
                    ("Allowed", allowed)));

        /// <summary>
        /// Rule that the targeted date must be on a weekday.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<DateTime> OnWeekday() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    DayOfWeekGuards.OnDaysOfWeek(DateOnly.FromDateTime(value), [DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday]),
                    $"{RuleCategory}.{nameof(OnWeekday)}",
                    ("Actual", value.DayOfWeek)));

        /// <summary>
        /// Rule that the targeted date must be on a weekend day.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<DateTime> OnWeekend() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    DayOfWeekGuards.OnDaysOfWeek(DateOnly.FromDateTime(value), [DayOfWeek.Saturday, DayOfWeek.Sunday]),
                    $"{RuleCategory}.{nameof(OnWeekend)}",
                    ("Actual", value.DayOfWeek)));
    }
}