namespace Toarnbeike.Results.Rules.Tests.Guids;

public class NotEmptyTests
{
    [Test]
    public void NotEmpty_Predicate_Fails_ForEmptyGuid()
    {
        var rule = GuidRules.NotEmpty(null);
        var value = Guid.Empty;
        rule.Predicate(value).ShouldBeFalse();
        rule.Message.ShouldBe("Guid must not be empty.");
    }

    [Test]
    public void NotEmpty_Predicate_Succeeds_ForNonEmptyGuid()
    {
        var rule = GuidRules.NotEmpty(null);
        var value = Guid.NewGuid();
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void NotEmpty_Predicate_Fails_ForDefaultGuid()
    {
        var rule = GuidRules.NotEmpty(null);
#pragma warning disable S4581
        var value = default(Guid);
#pragma warning restore S4581
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void NotEmpty_DefaultMessage_IsCorrect()
    {
        var rule = GuidRules.NotEmpty(null);
        rule.Message.ShouldBe("Guid must not be empty.");
    }

    [Test]
    public void NotEmpty_UsesCustomMessage()
    {
        var rule = GuidRules.NotEmpty("Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
