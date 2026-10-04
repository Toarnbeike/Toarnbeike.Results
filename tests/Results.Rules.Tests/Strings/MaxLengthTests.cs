namespace Toarnbeike.Results.Rules.Tests.Strings;

public class MaxLengthTests
{
    [Test]
    public void MaxLength_Predicate_Fails_ForLongString()
    {
        var rule = StringRules.MaxLength(5, null);
        var value = "abcdef";
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void MaxLength_Predicate_Succeeds_ForShortEnoughString()
    {
        var rule = StringRules.MaxLength(5, null);
        var value = "abc";
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void MaxLength_Predicate_Succeeds_ForStringOfExactLength()
    {
        var rule = StringRules.MaxLength(5, null);
        var value = "abcde";
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void MaxLength_Predicate_Fails_ForNullString()
    {
        var rule = StringRules.MaxLength(5, null);
        string? value = null;
        rule.Predicate(value!).ShouldBeFalse();
    }

    [Test]
    public void MaxLength_DefaultMessage_IsCorrect()
    {
        var rule = StringRules.MaxLength(5, null);
        rule.Message.ShouldBe("Value must be at most 5 characters long.");
    }

    [Test]
    public void MaxLength_UsesCustomMessage()
    {
        var rule = StringRules.MaxLength(5, "Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
