using Toarnbeike.Results.Guards.Implementations.Guards;

namespace Toarnbeike.Results.Guards.Rules;

public static class TimeSpanRules
{
    private const string RuleCategory = "TimeSpan";

    extension(IGuardTarget<TimeSpan> target)
    {
        /// <summary>
        /// Rule that the targeted timespan must be at least the specified min.
        /// </summary>
        public IGuardRuleResult<TimeSpan> AtLeast(TimeSpan min)
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ComparisonGuards.AtLeast(target.Value, min),
                    $"{RuleCategory}.{nameof(AtLeast)}", 
                    ("Min", min));
        }

        /// <summary>
        /// Rule that the targeted timespan must be at most the specified max.
        /// </summary>
        public IGuardRuleResult<TimeSpan> AtMost(TimeSpan max)
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ComparisonGuards.AtMost(target.Value, max),
                    $"{RuleCategory}.{nameof(AtMost)}", 
                    ("Max", max));
        }

        /// <summary>
        /// Rule that the targeted timespan must be between the specified durations (inclusive).
        /// </summary>
        public IGuardRuleResult<TimeSpan> Between(TimeSpan min, TimeSpan max)
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
        /// Rule that the targeted timespan must be close to the specified timespan, within the provided time span.
        /// </summary>
        public IGuardRuleResult<TimeSpan> Around(TimeSpan expected, TimeSpan tolerance)
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ToleranceGuards.Equal(target.Value.Ticks, expected.Ticks, tolerance.Ticks),
                    $"{RuleCategory}.{nameof(Around)}", 
                    ("Expected", expected),
                    ("Tolerance", tolerance));
        }

        /// <summary>
        /// Rule that the targeted timespan must be at least zero.
        /// </summary>
        public IGuardRuleResult<TimeSpan> AtLeastZero()
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ComparisonGuards.AtLeast(target.Value, TimeSpan.Zero),
                    $"{RuleCategory}.{nameof(AtLeastZero)}");
        }

        /// <summary>
        /// Rule that the targeted timespan must be at most zero.
        /// </summary>
        public IGuardRuleResult<TimeSpan> AtMostZero()
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(ComparisonGuards.AtMost(target.Value, TimeSpan.Zero),
                    $"{RuleCategory}.{nameof(AtMostZero)}");
        }
    }
}