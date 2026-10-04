namespace Toarnbeike.Results.Rules.Tests.Collections;

public class HasAtMostTests
{
    [Test]
    public void HasAtMost_Predicate_Succeeds_WhenCountEqualsUpperBound()
    {
        var rule = CollectionRules.HasAtMost<List<string>>(2, null);
        var v2 = new List<string> { "a", "b" };
        rule.Predicate(v2).ShouldBeTrue();
    }

    [Test]
    public void HasAtMost_Predicate_Succeeds_WhenCountBelowUpperBound()
    {
        var rule = CollectionRules.HasAtMost<List<string>>(2, null);
        var v1 = new List<string> { "a" };
        rule.Predicate(v1).ShouldBeTrue();
    }

    [Test]
    public void HasAtMost_Predicate_Fails_WhenCountExceedsUpperBound()
    {
        var rule = CollectionRules.HasAtMost<List<string>>(2, null);
        var v3 = new List<string> { "a", "b", "c" };
        rule.Predicate(v3).ShouldBeFalse();
    }

    [Test]
    public void HasAtMost_Predicate_Succeeds_ForEmptyCollection()
    {
        var rule = CollectionRules.HasAtMost<List<string>>(2, null);
        var value = new List<string>();
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void HasAtMost_Predicate_Fails_ForNullCollection()
    {
        var rule = CollectionRules.HasAtMost<List<string>>(2, null);
        List<string>? value = null;
        rule.Predicate(value!).ShouldBeFalse();
    }

    [Test]
    public void HasAtMost_UsesCustomMessage()
    {
        var rule = CollectionRules.HasAtMost<List<string>>(2, "Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
