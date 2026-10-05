namespace Toarnbeike.Results.Rules.Tests.Strings;

public class NotEmptyTests
{
    [Test]
    public void NotEmpty_Predicate_Fails_ForEmptyString()
    {
        var rule = StringRules.NotEmpty(null);
        var value = "";
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void NotEmpty_Predicate_Succeeds_ForNonEmptyString()
    {
        var rule = StringRules.NotEmpty(null);
        var value = "Hello";
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void NotEmpty_Predicate_Fails_ForNullString()
    {
        var rule = StringRules.NotEmpty(null);
        string? value = null;
        rule.Predicate(value!).ShouldBeFalse();
    }

    [Test]
    public void NotEmpty_DefaultMessage_IsCorrect()
    {
        var rule = StringRules.NotEmpty(null);
        rule.Message.ShouldBe("Value must not be empty.");
    }

    [Test]
    public void NotEmpty_UsesCustomMessage()
    {
        var rule = StringRules.NotEmpty("Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
