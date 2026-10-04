using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Tests.Rules.TimeSpans;

public class AtMostTests
{
    [Test]
    public void AtMost_Predicate_Succeeds_WhenValueEqualsUpperBound()
    {
        var rule = TimeSpanRules.AtMost(TimeSpan.FromSeconds(5), null);
        var value = TimeSpan.FromSeconds(5);
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void AtMost_Predicate_Succeeds_WhenValueBelowUpperBound()
    {
        var rule = TimeSpanRules.AtMost(TimeSpan.FromSeconds(5), null);
        var value = TimeSpan.FromSeconds(4);
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void AtMost_Predicate_Fails_WhenValueExceedsUpperBound()
    {
        var rule = TimeSpanRules.AtMost(TimeSpan.FromSeconds(5), null);
        var value = TimeSpan.FromSeconds(6);
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void AtMost_Message_UsesUpperBound()
    {
        var rule = TimeSpanRules.AtMost(TimeSpan.FromSeconds(5), null);
        rule.Message.ShouldBe("TimeSpan must be at most 00:00:05.");
    }

    [Test]
    public void AtMost_UsesCustomMessage()
    {
        var rule = TimeSpanRules.AtMost(TimeSpan.FromSeconds(5), "Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
