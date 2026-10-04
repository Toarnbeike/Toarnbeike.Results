using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Tests.Rules.TimeSpans;

public class MustBeBetweenTests
{
    [Test]
    public void MustBeBetween_Predicate_Succeeds_WhenValueIsWithinBounds()
    {
        var rule = TimeSpanRules.MustBeBetween(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10), null);
        var value = TimeSpan.FromSeconds(7);
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void MustBeBetween_Predicate_Succeeds_WhenValueEqualsLowerBound()
    {
        var rule = TimeSpanRules.MustBeBetween(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10), null);
        var value = TimeSpan.FromSeconds(5);
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void MustBeBetween_Predicate_Succeeds_WhenValueEqualsUpperBound()
    {
        var rule = TimeSpanRules.MustBeBetween(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10), null);
        var value = TimeSpan.FromSeconds(10);
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void MustBeBetween_Predicate_Fails_WhenValueIsBelowLowerBound()
    {
        var rule = TimeSpanRules.MustBeBetween(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10), null);
        var value = TimeSpan.FromSeconds(4);
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void MustBeBetween_Predicate_Fails_WhenValueIsAboveUpperBound()
    {
        var rule = TimeSpanRules.MustBeBetween(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10), null);
        var value = TimeSpan.FromSeconds(11);
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void MustBeBetween_Message_UsesBounds()
    {
        var rule = TimeSpanRules.MustBeBetween(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10), null);
        rule.Message.ShouldBe("TimeSpan must be between 00:00:05 and 00:00:10.");
    }

    [Test]
    public void MustBeBetween_UsesCustomMessage()
    {
        var rule = TimeSpanRules.MustBeBetween(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10), "Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
