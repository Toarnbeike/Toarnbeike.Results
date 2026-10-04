using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Tests.Rules.Strings;

public class MatchesTests
{
    [Test]
    public void Matches_Predicate_Succeeds_ForMatchingString()
    {
        var rule = StringRules.Matches(@"^\d{3}-\d{2}-\d{4}$", null);
        var value = "123-45-6789";
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void Matches_Predicate_Fails_ForNonMatchingString()
    {
        var rule = StringRules.Matches(@"^\d{3}-\d{2}-\d{4}$", null);
        var value = "abc-de-fghi";
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void Matches_Predicate_Fails_ForNullString()
    {
        var rule = StringRules.Matches(@"^\d{3}-\d{2}-\d{4}$", null);
        string? value = null;
        rule.Predicate(value!).ShouldBeFalse();
    }

    [Test]
    public void Matches_DefaultMessage_IsCorrect()
    {
        var rule = StringRules.Matches(@"^\d{3}-\d{2}-\d{4}$", null);
        rule.Message.ShouldBe(@"Value must match the pattern '^\d{3}-\d{2}-\d{4}$'.");
    }

    [Test]
    public void Matches_UsesCustomMessage()
    {
        var rule = StringRules.Matches(@"^\d{3}-\d{2}-\d{4}$", "Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
