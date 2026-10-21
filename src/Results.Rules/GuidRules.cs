using Toarnbeike.Results.Rules.Evaluations.Comparisons;
using Toarnbeike.Results.Rules.Evaluations.Guids;

namespace Toarnbeike.Results.Rules;

/// <summary>
/// Set of Rules for Guid type.
/// </summary>
public static class GuidRules
{
    /// <summary>
    /// Verifies that the Guid value is not empty (i.e., not equal to Guid.Empty).
    /// </summary>
    /// <returns>A rule delegate that verifies the Guid value.</returns>
    public static RuleDelegate<Guid> NotEmpty() => value =>
        RuleResult.Create("Guid.NotEmpty", value, new NotEmptyEvaluator());

    /// <summary>
    /// Verifies that the Guid value is a version 4 UUID.
    /// </summary>
    /// <returns>A rule delegate that verifies the Guid value.</returns>
    public static RuleDelegate<Guid> IsVersion4() => IsVersion(4);

    /// <summary>
    /// Verifies that the Guid value is a version 7 UUID.
    /// </summary>
    /// <returns>A rule delegate that verifies the Guid value.</returns>
    public static RuleDelegate<Guid> IsVersion7() => IsVersion(7);

    private static RuleDelegate<Guid> IsVersion(int version) => value =>
        RuleResult.Create("Guid.IsVersion", value, value => value.Version, new EqualToEvaluator<int>(version));
}
