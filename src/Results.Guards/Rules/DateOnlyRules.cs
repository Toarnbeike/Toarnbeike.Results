using Toarnbeike.Results.Guards.Attributes;
using Toarnbeike.Results.Guards.Core.Guards;

namespace Toarnbeike.Results.Guards.Rules;

public static class DateOnlyRules
{
    private const string RuleCategory = "Date";
    private const string QualifiedRuleCategory = "Date.DateOnly";

    extension(IGuardTarget<DateOnly> target)
    {
        private DateOnly Today => DateOnly.FromDateTime(target.GuardContext.TimeProvider.GetUtcNow().DateTime);

        /// <summary>
        /// Rule that the targeted date must be on or after the specified date.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<DateOnly> OnOrAfter(DateOnly min) =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    ComparisonGuards.AtLeast(value, min),
                    $"{RuleCategory}.{nameof(OnOrAfter)}",
                    ("Min", min)));

        /// <summary>
        /// Rule that the targeted date must be on or before the specified date.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<DateOnly> OnOrBefore(DateOnly max) =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    ComparisonGuards.AtMost(value, max),
                    $"{RuleCategory}.{nameof(OnOrBefore)}",
                    ("Max", max)));

        /// <summary>
        /// Rule that the targeted date must be between the specified dates.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<DateOnly> Between(DateOnly min, DateOnly max) =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    ComparisonGuards.AtLeast(value, min) && ComparisonGuards.AtMost(value, max),
                    $"{RuleCategory}.{nameof(Between)}",
                    ("Min", min),
                    ("Max", max)));

        /// <summary>
        /// Rule that the targeted date must be in the future.
        /// </summary>
        public IGuardRuleResult<DateOnly> Future() =>
            target.Evaluate(value =>
            {
                var today = DateOnly.FromDateTime(target.GuardContext.TimeProvider.GetUtcNow().DateTime);
                return new RuleEvaluation(
                    ComparisonGuards.AtLeast(value, today),
                    $"{RuleCategory}.{nameof(Future)}",
                    ("Now", today));
            });

        /// <summary>
        /// Rule that the targeted date must be in the past.
        /// </summary>
        public IGuardRuleResult<DateOnly> Past() =>
            target.Evaluate(value =>
            {
                var today = DateOnly.FromDateTime(target.GuardContext.TimeProvider.GetUtcNow().DateTime);
                return new RuleEvaluation(
                    ComparisonGuards.AtMost(value, today),
                    $"{RuleCategory}.{nameof(Past)}",
                    ("Now", today));
            });

        /// <summary>
        /// Rule that the targeted date must be in the future, but by no more than the provided time span.
        /// </summary>
        public IGuardRuleResult<DateOnly> WithinFuture(int days) =>
            target.Evaluate(value =>
            {
                var today = DateOnly.FromDateTime(target.GuardContext.TimeProvider.GetUtcNow().DateTime);
                return new RuleEvaluation(
                    ComparisonGuards.AtLeast(value, today) && ComparisonGuards.AtMost(value, today.AddDays(days)),
                    $"{QualifiedRuleCategory}.{nameof(WithinFuture)}",
                    ("Today", today),
                    ("Days", days));
            });

        /// <summary>
        /// Rule that the targeted date must be in the future, but by no more than the provided time span.
        /// </summary>
        public IGuardRuleResult<DateOnly> WithinPast(int days) =>
            target.Evaluate(value =>
            {
                var today = DateOnly.FromDateTime(target.GuardContext.TimeProvider.GetUtcNow().DateTime);
                return new RuleEvaluation(
                    ComparisonGuards.AtLeast(value, today.AddDays(-days)) && ComparisonGuards.AtMost(value, today),
                    $"{QualifiedRuleCategory}.{nameof(WithinPast)}",
                    ("Today", today),
                    ("Days", days));
            });

        /// <summary>
        /// Rule that the targeted date must be on the specified day of the week.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<DateOnly> OnDayOfWeek(DayOfWeek dayOfWeek) =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    DayOfWeekGuards.OnDayOfWeek(value, dayOfWeek),
                    $"{RuleCategory}.{nameof(OnDayOfWeek)}",
                    ("Actual", value.DayOfWeek),
                    ("ExpectedDay", dayOfWeek)));

        /// <summary>
        /// Rule that the targeted date must be on one of the specified days of the week.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<DateOnly> OnDaysOfWeek(params DayOfWeek[] allowed) =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    DayOfWeekGuards.OnDaysOfWeek(value, allowed),
                    $"{RuleCategory}.{nameof(OnDaysOfWeek)}",
                    ("Actual", value.DayOfWeek),
                    ("Allowed", allowed)));

        /// <summary>
        /// Rule that the targeted date must be on a weekday.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<DateOnly> OnWeekday() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    DayOfWeekGuards.OnDaysOfWeek(value, [DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday]),
                    $"{RuleCategory}.{nameof(OnWeekday)}",
                    ("Actual", value.DayOfWeek)));

        /// <summary>
        /// Rule that the targeted date must be on a weekend day.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<DateOnly> OnWeekend() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    DayOfWeekGuards.OnDaysOfWeek(value, [DayOfWeek.Saturday, DayOfWeek.Sunday]),
                    $"{RuleCategory}.{nameof(OnWeekend)}",
                    ("Actual", value.DayOfWeek)));
    }
}