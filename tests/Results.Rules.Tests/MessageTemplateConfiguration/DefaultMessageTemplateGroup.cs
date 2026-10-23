namespace Toarnbeike.Results.Rules.Tests.MessageTemplateConfiguration;

public abstract class DefaultMessageTemplateGroup
{
    private static int _initialized = 0;

    [Before(Class)]
    public static void InitializeDefaultMessageTemplateGroup()
    {
        if (Interlocked.Exchange(ref _initialized, 1) == 0)
        {
            RuleContext.UseDefaultMessageProvider();
            RuleContext.UseFixedUtcNow(new DateTimeOffset(2023, 1, 1, 12, 0, 0, TimeSpan.Zero));
            RuleContext.UseInvariantCulture();
        }
    }
}
