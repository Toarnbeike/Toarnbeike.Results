using Toarnbeike.Results.Guards.Implementations.Guards;

namespace Toarnbeike.Results.Guards.Rules;

public static class GuidRules
{
    private const string RuleCategory = "Guid";

    extension(IGuardTarget<Guid> target)
    {
        /// <summary>
        /// Rule that the targeted guid must not be empty.
        /// </summary>
        public IGuardRuleResult<Guid> NotEmpty()
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(GuidGuards.NotEmpty(target.Value),
                    $"{RuleCategory}.{nameof(NotEmpty)}");
        }

        /// <summary>
        /// Rule that the targeted guid is created as guid v4.
        /// </summary>
        public IGuardRuleResult<Guid> Version4()
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(GuidGuards.IsVersion(target.Value, 4),
                    $"{RuleCategory}.{nameof(Version4)}", 
                    ("Actual", target.Value.Version));
        }

        /// <summary>
        /// Rule that the targeted guid is created as guid v7.
        /// </summary>
        public IGuardRuleResult<Guid> Version7()
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(GuidGuards.IsVersion(target.Value, 7), 
                    $"{RuleCategory}.{nameof(Version7)}",
                    ("Actual", target.Value.Version));
        }
    }
}