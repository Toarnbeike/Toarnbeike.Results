namespace Toarnbeike.Results.Rules.Tests.Integers;

public class AtLeastTests
{
    [Test]
    public void AtLeast_Predicate_Succeeds_WhenValueEqualsLowerBound()
    {
        var rule = IntegerRules.AtLeast(5, null);
        var value = 5;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void AtLeast_Predicate_Succeeds_WhenValueExceedsLowerBound()
    {
        var rule = IntegerRules.AtLeast(5, null);
        var value = 6;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void AtLeast_Predicate_Fails_WhenValueBelowLowerBound()
    {
        var rule = IntegerRules.AtLeast(5, null);
        var value = 4;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void AtLeast_Message_UsesLowerBound()
    {
        var rule = IntegerRules.AtLeast(5, null);
        rule.Message.ShouldBe("Value must be at least 5.");
    }

    [Test]
    public void AtLeast_UsesCustomMessage()
    {
        var rule = IntegerRules.AtLeast(5, "Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
