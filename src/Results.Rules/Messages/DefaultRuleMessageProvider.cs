namespace Toarnbeike.Results.Rules.Messages;

/// <summary>
/// Provides default message templates (in English) for validation rules.
/// </summary>
internal sealed class DefaultRuleMessageProvider : IRuleMessageProvider
{
    /// <inheritdoc />
    public IReadOnlyDictionary<string, string> MessageTemplatesByRule { get; } = new Dictionary<string, string>
    {
        { "Collection.Contains", "Collection is not valid, must contain '{expected}'." },
        { "Collection.NotContains", "Collection is not valid, must not contain '{unexpected}'." },
        { "Collection.HasExactly", "Collection is not valid, must have exactly '{target}' items, but has '{attemptedValue}'." },
        { "Collection.HasAtLeast", "Collection is not valid, must have at least '{min}' items, but has '{attemptedValue}'." },
        { "Collection.HasAtMost", "Collection is not valid, must have at most '{max}' items, but has '{attemptedValue}'." },
        { "Collection.HasBetween", "Collection is not valid, must have between '{min}' and '{max}' items, but has '{attemptedValue}'." },

        { "Comparison.GreaterThan", "'{attemptedValue}' is not a valid {type}, must be strictly greater than '{min}'."  },
        { "Comparison.AtLeast", "'{attemptedValue}' is not a valid {type}, must be at least '{min}'."  },
        { "Comparison.AtMost", "'{attemptedValue}' is not a valid {type}, must be at most '{max}'." },
        { "Comparison.LessThan", "'{attemptedValue}' is not a valid {type}, must be strictly less than '{max}'." },
        { "Comparison.Between", "'{attemptedValue}' is not a valid {type}, must be between '{min}' and '{max}'." },
        { "Comparison.EqualTo", "'{attemptedValue}' is not a valid {type}, must be equal to '{target}'." },
        { "Comparison.NotEqualTo", "'{attemptedValue}' is not a valid {type}, must not be equal to '{target}'." },

        { "Comparison.AtLeastWithTolerance", "'{attemptedValue}' is not valid, must be at least '{min} (-{tolerance})'." },
        { "Comparison.AtMostWithTolerance", "'{attemptedValue}' is not valid, must be at most '{max} (+{tolerance})'." },
        { "Comparison.BetweenWithTolerance", "'{attemptedValue}' is not valid, must be between '{min}' and '{max}' '(+-{tolerance})'." },
        { "Comparison.EqualToWithTolerance", "'{attemptedValue}' is not valid, must be equal to '{target} (+-{tolerance})'." },
        { "Comparison.NotEqualToWithTolerance", "'{attemptedValue}' is not valid, must not be equal to '{target} (+-{tolerance})'." },

        { "Date.OnOrAfter", "'{attemptedValue}' is not valid, must be on or after '{min}'." },
        { "Date.OnOrBefore", "'{attemptedValue}' is not valid, must be on or before '{max}'." },
        { "Date.Between", "'{attemptedValue}' is not valid, must be between '{min}' and '{max}'." },
        { "Date.Future", "'{attemptedValue}' is not valid, must be a future {type}."  },
        { "Date.Past", "'{attemptedValue}' is not valid, must be a past {type}." },
        { "Date.WithinPast", "'{attemptedValue}' is not valid, must be a past {type} after '{min}'." },
        { "Date.WithinFuture", "'{attemptedValue}' is not valid, must be a future {type} before '{max}'." },

        { "Enum.IsDefined", "'{attemptedValue}' is not valid, must be a defined {enumType}." },
        { "Enum.OneOf", "'{attemptedValue}' is not valid, must be one of the accepted {enumType} values: [{acceptedValues}]." },
        { "Enum.NotOneOf", "'{attemptedValue}' is not valid, must not be one of the rejected {enumType} values: [{rejectedValues}]." },

        { "Float.MultipleOfWithTolerance", "'{attemptedValue}' is not valid, must be a multiple of '{factor} (+-{tolerance})'." },
        { "Float.Finite", "'{attemptedValue}' is not valid, must be a finite number." },

        { "Guid.NotEmpty", "Value is not valid, must not be an empty Guid." },
        { "Guid.IsVersion", "'{originalValue}' is not valid, must be a version {target} Guid, but is a version {attemptedValue} Guid." },

        { "Int.MultipleOf", "'{attemptedValue}' is not valid, must be a multiple of '{factor}'." },
        { "Int.Even", "'{attemptedValue}' is not valid, must be an even number."  },
        { "Int.Odd", "'{attemptedValue}' is not valid, must be an odd number." },
        { "Int.PowerOf2", "'{attemptedValue}' is not valid, must be a power of 2." },

        { "String.NotEmpty", "'{attemptedValue}' is not valid, must not be empty." },
        { "String.NotWhitespace", "'{attemptedValue}' is not valid, must not be whitespace." },
        { "String.MinLength", "'{originalValue}' is not valid, must be at least {min} characters long, but is {attemptedValue}." },
        { "String.MaxLength", "'{originalValue}' is not valid, must be at most {max} characters long, but is {attemptedValue}." },
        { "String.LengthBetween", "'{originalValue}' is not valid, must be between {min} and {max} characters long, but is {attemptedValue}." },
        { "String.IsEmailAddress", "'{attemptedValue}' is not valid, must be a valid email address." },
        { "String.IsUri", "'{attemptedValue}' is not valid, must be a valid URI." },
        { "String.Matches", "'{attemptedValue}' is not valid, must match the pattern '{description}'." },

        { "TimeSpan.Around", "'{attemptedValue}' is not a valid {type}, must be around '{target}' within a tolerance of '{tolerance}'." },
    };
}
