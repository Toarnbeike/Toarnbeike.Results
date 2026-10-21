using Toarnbeike.Results.Rules.Evaluations.Enums;

namespace Toarnbeike.Results.Rules;

/// <summary>
/// Set of Rules for Enum types.
/// </summary>
public static class EnumRules
{
    /// <summary>
    /// Verifies that the specified enum value is defined in the enum type.
    /// </summary>
    /// <typeparam name="TEnum">The type of the enum.</typeparam>
    /// <returns>A rule delegate that verifies the enum value.</returns>
    public static RuleDelegate<TEnum> IsDefined<TEnum>() 
        where TEnum : struct, Enum => value =>
        RuleResult.Create("Enum.IsDefined", value, new IsDefinedEvaluator<TEnum>());

    /// <summary>
    /// Verifies that the specified enum value is one of the accepted values.
    /// </summary>
    /// <typeparam name="TEnum">The type of the enum.</typeparam>
    /// <param name="acceptedValues">The accepted values.</param>
    /// <returns>A rule delegate that verifies the enum value.</returns>
    public static RuleDelegate<TEnum> OneOf<TEnum>(params TEnum[] acceptedValues)
        where TEnum : struct, Enum => value =>
        RuleResult.Create("Enum.OneOf", value, new OneOfEvaluator<TEnum>(acceptedValues));

    /// <summary>
    /// Verifies that the specified enum value is not one of the rejected values.
    /// </summary>
    /// <typeparam name="TEnum">The type of the enum.</typeparam>
    /// <param name="rejectedValues">The rejected values.</param>
    /// <returns>A rule delegate that verifies the enum value.</returns>
    public static RuleDelegate<TEnum> NotOneOf<TEnum>(params TEnum[] rejectedValues)
        where TEnum : struct, Enum => value =>
        RuleResult.Create("Enum.NotOneOf", value, new NotOneOfEvaluator<TEnum>(rejectedValues));
}
