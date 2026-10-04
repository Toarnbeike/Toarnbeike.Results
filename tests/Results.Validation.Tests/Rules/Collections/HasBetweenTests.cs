using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Tests.Rules.Collections;

public class HasBetweenTests
{
    [Test]
    public void HasBetween_Predicate_Succeeds_WhenCountEqualsLowerBound()
    {
        var rule = CollectionRules.HasBetween<List<string>>(2, 4, null);
        var v2 = new List<string> { "a", "b" };
        rule.Predicate(v2).ShouldBeTrue();
    }

    [Test]
    public void HasBetween_Predicate_Succeeds_WhenCountEqualsUpperBound()
    {
        var rule = CollectionRules.HasBetween<List<string>>(2, 4, null);
        var v4 = new List<string> { "a", "b", "c", "d" };
        rule.Predicate(v4).ShouldBeTrue();
    }

    [Test]
    public void HasBetween_Predicate_Succeeds_WhenCountIsBetweenBounds()
    {
        var rule = CollectionRules.HasBetween<List<string>>(2, 4, null);
        var v3 = new List<string> { "a", "b", "c" };
        rule.Predicate(v3).ShouldBeTrue();
    }

    [Test]
    public void HasBetween_Predicate_Fails_WhenCountBelowLowerBound()
    {
        var rule = CollectionRules.HasBetween<List<string>>(2, 4, null);
        var v1 = new List<string> { "a" };
        rule.Predicate(v1).ShouldBeFalse();
    }

    [Test]
    public void HasBetween_Predicate_Fails_WhenCountExceedsUpperBound()
    {
        var rule = CollectionRules.HasBetween<List<string>>(2, 4, null);
        var v5 = new List<string> { "a", "b", "c", "d", "e" };
        rule.Predicate(v5).ShouldBeFalse();
    }

    [Test]
    public void HasBetween_Predicate_Fails_ForEmptyCollection()
    {
        var rule = CollectionRules.HasBetween<List<string>>(2, 4, null);
        var value = new List<string>();
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void HasBetween_Predicate_Fails_ForNullCollection()
    {
        var rule = CollectionRules.HasBetween<List<string>>(2, 4, null);
        List<string>? value = null;
        rule.Predicate(value!).ShouldBeFalse();
    }

    [Test]
    public void HasBetween_UsesCustomMessage()
    {
        var rule = CollectionRules.HasBetween<List<string>>(2, 4, "Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
