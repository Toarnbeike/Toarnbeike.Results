using Toarnbeike.Results.Ensure.Guards;
using Toarnbeike.Results.Ensure.Implementation.RuleResults;

namespace Toarnbeike.Results.Ensure.Rules;

public static class GuidRules
{
    extension(IGuardTarget<Guid> target)
    {
        /// <summary>
        /// Rule that the targeted guid must not be empty.
        /// </summary>
        public IGuardRuleResult NotEmpty() =>
            target.Evaluate(GuidGuards.NotEmpty(target.Value),
                $"{nameof(GuidRules)}.{nameof(NotEmpty)}");

        /// <summary>
        /// Rule that the targeted guid is created as guid v4.
        /// </summary>
        public IGuardRuleResult Version4() =>
            target.Evaluate(GuidGuards.IsVersion(target.Value, 4),
                $"{nameof(GuidRules)}.{nameof(Version4)}", 4);

        /// <summary>
        /// Rule that the targeted guid is created as guid v7.
        /// </summary>
        public IGuardRuleResult Version7() =>
            target.Evaluate(GuidGuards.IsVersion(target.Value, 7), 
                $"{nameof(GuidRules)}.{nameof(Version7)}", 7);
    }
}