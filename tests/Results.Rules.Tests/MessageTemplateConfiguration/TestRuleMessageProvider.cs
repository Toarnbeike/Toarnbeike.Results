using Toarnbeike.Results.Rules.Messages;

namespace Toarnbeike.Results.Rules.Tests.MessageTemplateConfiguration;

internal sealed class TestRuleMessageProvider : IRuleMessageProvider
{
    public IReadOnlyDictionary<string, string> MessageTemplatesByRule => new Dictionary<string, string>();
}
