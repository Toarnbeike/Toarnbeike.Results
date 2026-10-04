namespace Toarnbeike.Results.Rules.Tests.Integers;

public class MultipleOfTests
{
    [Test]
    public void MultipleOf_Predicate_Succeeds_ForMultiple()
    {
        var rule = IntegerRules.MultipleOf(3, null);
        var value = 9;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void MultipleOf_Predicate_Fails_ForNonMultiple()
    {
        var rule = IntegerRules.MultipleOf(3, null);
        var value = 10;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void MultipleOf_Predicate_Succeeds_ForZero()
    {
        var rule = IntegerRules.MultipleOf(3, null);
        var value = 0;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void MultipleOf_Throws_ForZeroFactor()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => IntegerRules.MultipleOf(0, null));
    }

    [Test]
    public void MultipleOf_Message_UsesFactor()
    {
        var rule = IntegerRules.MultipleOf(3, null);
        rule.Message.ShouldBe("Value must be a multiple of 3.");
    }

    [Test]
    public void MultipleOf_UsesCustomMessage()
    {
        var rule = IntegerRules.MultipleOf(3, "Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
