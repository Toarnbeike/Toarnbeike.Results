namespace Toarnbeike.Results.Rules.Messages;

/// <summary>
/// Provides a mechanism to retrieve message templates for validation rules.
/// </summary>
public interface IRuleMessageProvider
{
    /// <summary>
    /// Collection of message templates associated with rule names. 
    /// Each entry maps a rule name to its corresponding message template.
    /// </summary>
    public IReadOnlyDictionary<string, string> MessageTemplatesByRule { get; }
}
