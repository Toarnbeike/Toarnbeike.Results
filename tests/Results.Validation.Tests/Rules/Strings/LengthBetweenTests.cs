using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Tests.Rules.Strings;

public class LengthBetweenTests
{
    [Test]
    public void LengthBetween_Predicate_Fails_ForShortString()
    {
        var rule = StringRules.LengthBetween(5, 10, null);
        var value = "abc";
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void LengthBetween_Predicate_Fails_ForLongString()
    {
        var rule = StringRules.LengthBetween(5, 10, null);
        var value = "abcdefghijk";
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void LengthBetween_Predicate_Succeeds_ForStringOfExactMinLength()
    {
        var rule = StringRules.LengthBetween(5, 10, null);
        var value = "abcde";
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void LengthBetween_Predicate_Succeeds_ForStringOfExactMaxLength()
    {
        var rule = StringRules.LengthBetween(5, 10, null);
        var value = "abcdefghij";
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void LengthBetween_Predicate_Succeeds_ForStringWithinRange()
    {
        var rule = StringRules.LengthBetween(5, 10, null);
        var value = "abcdefg";
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void LengthBetween_Predicate_Fails_ForNullString()
    {
        var rule = StringRules.LengthBetween(5, 10, null);
        string? value = null;
        rule.Predicate(value!).ShouldBeFalse();
    }

    [Test]
    public void LengthBetween_DefaultMessage_IsCorrect()
    {
        var rule = StringRules.LengthBetween(5, 10, null);
        rule.Message.ShouldBe("Value must be between 5 and 10 characters long.");
    }

    [Test]
    public void LengthBetween_UsesCustomMessage()
    {
        var rule = StringRules.LengthBetween(5, 10, "Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
