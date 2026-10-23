using Toarnbeike.Results.Rules.Evaluations.Comparisons;

namespace Toarnbeike.Results.Rules.TypeRules;

public static class BoolRules
{
    /// <summary>
    /// Verifies that the boolean value is true.
    /// </summary>
    /// <returns>A RuleDelegate that evaluates the boolean value.</returns>
    public static RuleDelegate<bool> IsTrue() => value =>
        RuleResult.Create("Bool.IsTrue", value, new EqualToEvaluator<bool>(true));
    /// <summary>
    /// Verifies that the boolean value is false.
    /// </summary>
    /// <returns>A RuleDelegate that evaluates the boolean value.</returns>
    public static RuleDelegate<bool> IsFalse() => value =>
        RuleResult.Create("Bool.IsFalse", value, new EqualToEvaluator<bool>(false));
}
