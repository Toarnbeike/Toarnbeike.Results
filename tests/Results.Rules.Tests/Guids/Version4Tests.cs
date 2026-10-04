namespace Toarnbeike.Results.Rules.Tests.Guids;

public class Version4Tests
{
    [Test]
    public void Version4_Predicate_Succeeds_ForVersion4Guid()
    {
        var rule = GuidRules.Version4(null);
        var value = Guid.NewGuid();
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void Version4_Predicate_Fails_ForNonVersion4Guid()
    {
        var rule = GuidRules.Version4(null);
        var value = new Guid("6ba7b810-9dad-11d1-80b4-00c04fd430c8"); // Version 1 GUID
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void Version4_Predicate_Fails_ForEmptyGuid()
    {
        var rule = GuidRules.Version4(null);
        var value = Guid.Empty;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void Version4_Predicate_Fails_ForVersion7Guid()
    {
        var rule = GuidRules.Version4(null);
        var value = Guid.CreateVersion7();
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void Version4_DefaultMessage_IsCorrect()
    {
        var rule = GuidRules.Version4(null);
        rule.Message.ShouldBe("Guid must be version 4.");
    }

    [Test]
    public void Version4_UsesCustomMessage()
    {
        var rule = GuidRules.Version4("Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
