namespace Toarnbeike.Results.Rules.Tests.Strings;

public class NotWhitespaceTests
{
    [Test]
    public void NotWhitespace_Predicate_Fails_ForWhitespaceString()
    {
        var rule = StringRules.NotWhiteSpace(null);
        var value = "   ";
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void NotWhitespace_Predicate_Succeeds_ForNonWhitespaceString()
    {
        var rule = StringRules.NotWhiteSpace(null);
        var value = "Hello";
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void NotWhitespace_Predicate_Fails_ForNullString()
    {
        var rule = StringRules.NotWhiteSpace(null);
        string? value = null;
        rule.Predicate(value!).ShouldBeFalse();
    }

    [Test]
    public void NotWhitespace_DefaultMessage_IsCorrect()
    {
        var rule = StringRules.NotWhiteSpace(null);
        rule.Message.ShouldBe("Value must not be whitespace.");
    }

    [Test]
    public void NotWhitespace_UsesCustomMessage()
    {
        var rule = StringRules.NotWhiteSpace("Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
