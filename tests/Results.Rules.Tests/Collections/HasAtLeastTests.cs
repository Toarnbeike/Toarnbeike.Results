namespace Toarnbeike.Results.Rules.Tests.Collections;

public class HasAtLeastTests
{
    [Test]
    public void HasAtLeast_Predicate_Succeeds_WhenCountEqualsLowerBound()
    {
        var rule = CollectionRules.HasAtLeast<List<string>>(2, null);
        var v2 = new List<string> { "a", "b" };
        rule.Predicate(v2).ShouldBeTrue();
    }

    [Test]
    public void HasAtLeast_Predicate_Succeeds_WhenCountExceedsLowerBound()
    {
        var rule = CollectionRules.HasAtLeast<List<string>>(2, null);
        var v3 = new List<string> { "a", "b", "c" };
        rule.Predicate(v3).ShouldBeTrue();
    }

    [Test]
    public void HasAtLeast_Predicate_Fails_WhenCountBelowLowerBound()
    {
        var rule = CollectionRules.HasAtLeast<List<string>>(2, null);
        var v1 = new List<string> { "a" };
        rule.Predicate(v1).ShouldBeFalse();
    }

    [Test]
    public void HasAtLeast_Predicate_Fails_ForEmptyCollection()
    {
        var rule = CollectionRules.HasAtLeast<List<string>>(2, null);
        var value = new List<string>();
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void HasAtLeast_Predicate_Fails_ForNullCollection()
    {
        var rule = CollectionRules.HasAtLeast<List<string>>(2, null);
        List<string>? value = null;
        rule.Predicate(value!).ShouldBeFalse();
    }

    [Test]
    public void HasAtLeast_UsesCustomMessage()
    {
        var rule = CollectionRules.HasAtLeast<List<string>>(2, "Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
