using Toarnbeike.Results.Ensure.Guards;
using Toarnbeike.Results.Ensure.Implementation.RuleResults;

namespace Toarnbeike.Results.Ensure.Rules;

public static class EnumRules
{
    extension<TEnum>(IGuardTarget<TEnum> target) where TEnum : struct, Enum
    {
        /// <summary>
        /// Rule that the targeted enum value must be a defined value of the enum type.
        /// </summary>
        public IGuardRuleResult IsDefined() =>
            target.Evaluate(EnumGuards.IsDefined(target.Value),
                $"{nameof(EnumRules)}.{nameof(IsDefined)}");

        /// <summary>
        /// Rule that the targeted enum value must be one of the specified values.
        /// </summary>
        public IGuardRuleResult OneOf(params TEnum[] validValues) =>
            target.Evaluate(EnumGuards.OneOf(target.Value, validValues),
                $"{nameof(EnumRules)}.{nameof(OneOf)}", validValues);

        /// <summary>
        /// Rule that the targeted enum value must not be one of the specified values.
        /// </summary>
        public IGuardRuleResult NotOneOf(params TEnum[] invalidValues) =>
            target.Evaluate(EnumGuards.NotOneOf(target.Value, invalidValues),
                $"{nameof(EnumRules)}.{nameof(NotOneOf)}", invalidValues);
    }
}