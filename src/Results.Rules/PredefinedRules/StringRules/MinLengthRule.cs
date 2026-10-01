using System.Globalization;
using Toarnbeike.Results.Rules.Attributes;

namespace Toarnbeike.Results.Rules.PredefinedRules.StringRules;

/// <summary>
/// Rule that a string must have at least a specified number of characters.
/// </summary>
/// <param name="minLength">The minimum length that the string must have.</param>
[GenerateTargetExtension]
[GeneratePrimitiveExtension]
[GenerateCollectionTargetExtension]
internal class MinLengthRule(int minLength) : IRule<string>
{
    public bool IsValid(string providedValue, ValidationContext context)
    {
        return providedValue.Length >= minLength;
    }

    public string GetFailureMessage(string attemptedValue, ValidationMessageContext context)
    {
        return context.Culture.DisplayName switch
        {
            "nl-NL" => $"{context.PropertyName} moet minimaal {minLength} tekens bevatten, maar heeft {attemptedValue.Length} tekens.",
            _ => $"{context.PropertyName} must contain at least {minLength} characters, but has {attemptedValue.Length} tekens."
        };
    }

    public string GetConditionMessage(CultureInfo culture)
    {
        return culture.DisplayName switch
        {
            "nl-NL" => $"Minimaal {minLength} tekens.",
            _ => $"At least {minLength} characters."
        };
    }
}