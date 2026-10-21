using Toarnbeike.Results.Rules;

namespace Toarnbeike.Rules.Tests.MessageTemplateConfiguration;

public abstract class TestMessageTemplateGroup
{
    private static int _initialized = 0;

    [Before(Class)]
    public static void InitializeTestMessageTemplateGroup()
    {
        if (Interlocked.Exchange(ref _initialized, 1) == 0)
        {
            RuleContext.UseCustomMessageProvider(new TestRuleMessageProvider());
            RuleContext.UseFixedUtcNow(new DateTimeOffset(2024, 1, 1, 12, 0, 0, TimeSpan.Zero));
        }
    }
}
