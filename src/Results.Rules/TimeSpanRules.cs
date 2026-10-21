using Toarnbeike.Results.Rules.Evaluations.Comparisons;

namespace Toarnbeike.Results.Rules;

/// <summary>
/// Set of Rules for TimeSpan types.
/// </summary>
public static class TimeSpanRules
{
    /// <summary>
    /// Verifies that the TimeSpan value is at least the specified minimum value.
    /// </summary>
    /// <param name="min">The minimum TimeSpan value.</param>
    /// <returns>A RuleDelegate that evaluates the TimeSpan value.</returns>
    public static RuleDelegate<TimeSpan> AtLeast(TimeSpan min) => value =>
        RuleResult.Create("Comparison.AtLeast", value, new AtLeastEvaluator<TimeSpan>(min));

    /// <summary>
    /// Verifies that the TimeSpan value is at most the specified maximum value.
    /// </summary>
    /// <param name="max">The maximum TimeSpan value.</param>
    /// <returns>A RuleDelegate that evaluates the TimeSpan value.</returns>
    public static RuleDelegate<TimeSpan> AtMost(TimeSpan max) => value =>
        RuleResult.Create("Comparison.AtMost", value, new AtMostEvaluator<TimeSpan>(max));

    /// <summary>
    /// Verifies that the TimeSpan value is between the specified minimum and maximum values.
    /// </summary>
    /// <param name="min">The minimum TimeSpan value.</param>
    /// <param name="max">The maximum TimeSpan value.</param>
    /// <returns>A RuleDelegate that evaluates the TimeSpan value.</returns>
    public static RuleDelegate<TimeSpan> Between(TimeSpan min, TimeSpan max) => value =>
        RuleResult.Create("Comparison.Between", value, new BetweenEvaluator<TimeSpan>(min, max));

    /// <summary>
    /// Verifies that the TimeSpan value is at least zero.
    /// </summary>
    /// <returns>A RuleDelegate that evaluates the TimeSpan value.</returns>
    public static RuleDelegate<TimeSpan> AtLeastZero() => AtLeast(TimeSpan.Zero);

    /// <summary>
    /// Verifies that the TimeSpan value is around the specified target value within the given tolerance.
    /// </summary>
    /// <param name="target">The target TimeSpan value.</param>
    /// <param name="tolerance">The tolerance TimeSpan value.</param>
    /// <returns>A RuleDelegate that evaluates the TimeSpan value.</returns>
    public static RuleDelegate<TimeSpan> Around(TimeSpan target, TimeSpan tolerance) => value =>
        RuleResult.Create("TimeSpan.Around", value, new BetweenEvaluator<TimeSpan>(target - tolerance, target + tolerance), ("tolerance", tolerance));

}
