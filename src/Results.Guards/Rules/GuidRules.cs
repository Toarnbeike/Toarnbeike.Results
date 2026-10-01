using Toarnbeike.Results.Guards.Attributes;
using Toarnbeike.Results.Guards.Core.Guards;

namespace Toarnbeike.Results.Guards.Rules;

public static class GuidRules
{
    private const string RuleCategory = "Guid";

    extension(IGuardTarget<Guid> target)
    {
        /// <summary>
        /// Rule that the targeted guid must not be empty.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<Guid> NotEmpty() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    GuidGuards.NotEmpty(value),
                    $"{RuleCategory}.{nameof(NotEmpty)}"));

        /// <summary>
        /// Rule that the targeted guid is created as guid v4.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<Guid> Version4() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    GuidGuards.IsVersion(value, 4),
                    $"{RuleCategory}.{nameof(Version4)}",
                    ("Actual", value.Version)));

        /// <summary>
        /// Rule that the targeted guid is created as guid v7.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<Guid> Version7() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    GuidGuards.IsVersion(value, 7),
                    $"{RuleCategory}.{nameof(Version7)}",
                    ("Actual", value.Version)));
    }
}