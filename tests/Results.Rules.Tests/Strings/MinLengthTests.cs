namespace Toarnbeike.Results.Rules.Tests.Strings;

public class MinLengthTests
{
    [Test]
    public void MinLength_Predicate_Fails_ForShortString()
    {
        var rule = StringRules.MinLength(5, null);
        var value = "abc";
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void MinLength_Predicate_Succeeds_ForLongEnoughString()
    {
        var rule = StringRules.MinLength(5, null);
        var value = "abcdef";
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void MinLength_Predicate_Succeeds_ForStringOfExactLength()
    {
        var rule = StringRules.MinLength(5, null);
        var value = "abcde";
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void MinLength_Predicate_Fails_ForNullString()
    {
        var rule = StringRules.MinLength(5, null);
        string? value = null;
        rule.Predicate(value!).ShouldBeFalse();
    }

    [Test]
    public void MinLength_DefaultMessage_IsCorrect()
    {
        var rule = StringRules.MinLength(5, null);
        rule.Message.ShouldBe("Value must be at least 5 characters long.");
    }

    [Test]
    public void MinLength_UsesCustomMessage()
    {
        var rule = StringRules.MinLength(5, "Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
