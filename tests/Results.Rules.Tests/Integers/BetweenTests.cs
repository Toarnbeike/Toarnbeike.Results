namespace Toarnbeike.Results.Rules.Tests.Integers;

public class BetweenTests
{
    [Test]
    public void Between_Predicate_Succeeds_WhenValueEqualsLowerBound()
    {
        var rule = IntegerRules.Between(5, 10, null);
        var value = 5;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void Between_Predicate_Succeeds_WhenValueEqualsUpperBound()
    {
        var rule = IntegerRules.Between(5, 10, null);
        var value = 10;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void Between_Predicate_Succeeds_WhenValueIsBetweenBounds()
    {
        var rule = IntegerRules.Between(5, 10, null);
        var value = 7;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void Between_Predicate_Fails_WhenValueIsBelowLowerBound()
    {
        var rule = IntegerRules.Between(5, 10, null);
        var value = 4;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void Between_Predicate_Fails_WhenValueIsAboveUpperBound()
    {
        var rule = IntegerRules.Between(5, 10, null);
        var value = 11;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void Between_Message_UsesBounds()
    {
        var rule = IntegerRules.Between(5, 10, null);
        rule.Message.ShouldBe("Value must be between 5 and 10.");
    }

    [Test]
    public void Between_UsesCustomMessage()
    {
        var rule = IntegerRules.Between(5, 10, "Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
