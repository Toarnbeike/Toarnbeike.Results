namespace Toarnbeike.Results.Rules.Tests.TimeSpans;

public class AtLeastTests
{
    [Test]
    public void AtLeast_Predicate_Succeeds_WhenValueEqualsLowerBound()
    {
        var rule = TimeSpanRules.AtLeast(TimeSpan.FromSeconds(5), null);
        var value = TimeSpan.FromSeconds(5);
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void AtLeast_Predicate_Succeeds_WhenValueExceedsLowerBound()
    {
        var rule = TimeSpanRules.AtLeast(TimeSpan.FromSeconds(5), null);
        var value = TimeSpan.FromSeconds(6);
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void AtLeast_Predicate_Fails_WhenValueBelowLowerBound()
    {
        var rule = TimeSpanRules.AtLeast(TimeSpan.FromSeconds(5), null);
        var value = TimeSpan.FromSeconds(4);
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void AtLeast_Message_UsesLowerBound()
    {
        var rule = TimeSpanRules.AtLeast(TimeSpan.FromSeconds(5), null);
        rule.Message.ShouldBe("TimeSpan must be at least 00:00:05.");
    }

    [Test]
    public void AtLeast_UsesCustomMessage()
    {
        var rule = TimeSpanRules.AtLeast(TimeSpan.FromSeconds(5), "Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
