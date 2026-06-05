using Toarnbeike.Results.Guards.Core.Guards;

namespace Toarnbeike.Results.Guards.Rules;

public static class EnumRules
{
    private const string RuleCategory = "Enum";

    extension<TEnum>(IGuardTarget<TEnum> target) where TEnum : struct, Enum
    {
        /// <summary>
        /// Rule that the targeted enum value must be a defined value of the enum type.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TEnum> IsDefined() =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    EnumGuards.IsDefined(value),
                    $"{RuleCategory}.{nameof(IsDefined)}",
                    ("EnumType", typeof(TEnum))));

        /// <summary>
        /// Rule that the targeted enum value must be one of the specified values.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TEnum> OneOf(params TEnum[] validValues) =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    EnumGuards.OneOf(value, validValues),
                    $"{RuleCategory}.{nameof(OneOf)}",
                    ("EnumType", typeof(TEnum)),
                    ("ValidValues", validValues.Select(x => x.ToString()))));


        /// <summary>
        /// Rule that the targeted enum value must not be one of the specified values.
        /// </summary>
        [GeneratePrimitiveOverload]
        public IGuardRuleResult<TEnum> NotOneOf(params TEnum[] invalidValues) =>
            target.Evaluate(value =>
                new RuleEvaluation(
                    EnumGuards.NotOneOf(value, invalidValues),
                    $"{RuleCategory}.{nameof(NotOneOf)}",
                    ("EnumType", typeof(TEnum)),
                    ("InvalidValues", invalidValues.Select(x => x.ToString()))));
    }
}