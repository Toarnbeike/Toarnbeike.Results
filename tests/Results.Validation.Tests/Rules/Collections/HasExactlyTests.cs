using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Tests.Rules.Collections;

public class HasExactlyTests
{
    [Test]
    public void HasExactly_Predicate_Succeeds_WhenCountEqualsExpected()
    {
        var rule = CollectionRules.HasExactly<List<string>>(2, null);
        var v2 = new List<string> { "a", "b" };
        rule.Predicate(v2).ShouldBeTrue();
    }

    [Test]
    public void HasExactly_Predicate_Fails_WhenCountBelowExpected()
    {
        var rule = CollectionRules.HasExactly<List<string>>(2, null);
        var v1 = new List<string> { "a" };
        rule.Predicate(v1).ShouldBeFalse();
    }

    [Test]
    public void HasExactly_Predicate_Fails_WhenCountExceedsExpected()
    {
        var rule = CollectionRules.HasExactly<List<string>>(2, null);
        var v3 = new List<string> { "a", "b", "c" };
        rule.Predicate(v3).ShouldBeFalse();
    }

    [Test]
    public void HasExactly_Predicate_Fails_ForEmptyCollection()
    {
        var rule = CollectionRules.HasExactly<List<string>>(2, null);
        var value = new List<string>();
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void HasExactly_Predicate_Fails_ForNullCollection()
    {
        var rule = CollectionRules.HasExactly<List<string>>(2, null);
        List<string>? value = null;
        rule.Predicate(value!).ShouldBeFalse();
    }

    [Test]
    public void HasExactly_UsesCustomMessage()
    {
        var rule = CollectionRules.HasExactly<List<string>>(2, "Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
