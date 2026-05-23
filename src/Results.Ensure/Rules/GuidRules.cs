using Toarnbeike.Results.Ensure.Guards;

namespace Toarnbeike.Results.Ensure.Rules;

public static class GuidRules
{
    private const string RuleCategory = "Guid";

    extension(IGuardTarget<Guid> target)
    {
        /// <summary>
        /// Rule that the targeted guid must not be empty.
        /// </summary>
        public IGuardRuleResult NotEmpty() =>
            target.Evaluate(GuidGuards.NotEmpty(target.Value),
                $"{RuleCategory}.{nameof(NotEmpty)}");

        /// <summary>
        /// Rule that the targeted guid is created as guid v4.
        /// </summary>
        public IGuardRuleResult Version4() =>
            target.Evaluate(GuidGuards.IsVersion(target.Value, 4),
                $"{RuleCategory}.{nameof(Version4)}", 
                ("Actual", target.Value.Version));

        /// <summary>
        /// Rule that the targeted guid is created as guid v7.
        /// </summary>
        public IGuardRuleResult Version7() =>
            target.Evaluate(GuidGuards.IsVersion(target.Value, 7), 
                $"{RuleCategory}.{nameof(Version7)}",
                ("Actual", target.Value.Version));
    }
}