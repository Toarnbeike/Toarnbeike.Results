using Toarnbeike.Results.Guards.Implementations.Guards;

namespace Toarnbeike.Results.Guards.Rules;

public static class EnumRules
{
    private const string RuleCategory = "Enum";

    extension<TEnum>(IGuardTarget<TEnum> target) where TEnum : struct, Enum
    {
        /// <summary>
        /// Rule that the targeted enum value must be a defined value of the enum type.
        /// </summary>
        public IGuardRuleResult<TEnum> IsDefined()
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(EnumGuards.IsDefined(target.Value),
                    $"{RuleCategory}.{nameof(IsDefined)}",
                    ("EnumType", typeof(TEnum)));
        }

        /// <summary>
        /// Rule that the targeted enum value must be one of the specified values.
        /// </summary>
        public IGuardRuleResult<TEnum> OneOf(params TEnum[] validValues)
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(EnumGuards.OneOf(target.Value, validValues),
                    $"{RuleCategory}.{nameof(OneOf)}", 
                    ("ValidValues", validValues.Select(x => x.ToString())));
        }

        /// <summary>
        /// Rule that the targeted enum value must not be one of the specified values.
        /// </summary>
        public IGuardRuleResult<TEnum> NotOneOf(params TEnum[] invalidValues)
        {
            return !target.GuardContext.ShouldContinueExecution
                ? target.SkipEvaluation()
                : target.Evaluate(EnumGuards.NotOneOf(target.Value, invalidValues),
                    $"{RuleCategory}.{nameof(NotOneOf)}", 
                    ("InvalidValues", invalidValues.Select(x => x.ToString())));
        }
    }
}