namespace Toarnbeike.Results.Rules.Tests.Integers;

public class AtMostTests
{
    [Test]
    public void AtMost_Predicate_Succeeds_WhenValueEqualsUpperBound()
    {
        var rule = IntegerRules.AtMost(5, null);
        var value = 5;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void AtMost_Predicate_Succeeds_WhenValueBelowUpperBound()
    {
        var rule = IntegerRules.AtMost(5, null);
        var value = 4;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void AtMost_Predicate_Fails_WhenValueExceedsUpperBound()
    {
        var rule = IntegerRules.AtMost(5, null);
        var value = 6;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void AtMost_Message_UsesUpperBound()
    {
        var rule = IntegerRules.AtMost(5, null);
        rule.Message.ShouldBe("Value must be at most 5.");
    }

    [Test]
    public void AtMost_UsesCustomMessage()
    {
        var rule = IntegerRules.AtMost(5, "Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
