using System.Collections.Immutable;
using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Tests.Rules.Collections;

public class NotEmptyTests
{
    [Test]
    public void NotEmpty_Predicate_Fails_ForEmptyArray()
    {
        var rule = CollectionRules.NotEmpty<string[]>(null);
        string[] value = [];
        rule.Predicate(value).ShouldBeFalse();
        rule.Message.ShouldBe("Collection must not be empty.");
    }

    [Test]
    public void NotEmpty_Predicate_Fails_ForEmptyImmutableList()
    {
        var rule = CollectionRules.NotEmpty<ImmutableList<int>>(null);
        var value = ImmutableList<int>.Empty;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void NotEmpty_Predicate_Succeeds_ForNonEmptyList()
    {
        var rule = CollectionRules.NotEmpty<List<string>>(null);
        var value = new List<string> { "a" };
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void NotEmpty_UsesCustomMessage()
    {
        var rule = CollectionRules.NotEmpty<List<string>>("Custom message");
        rule.Message.ShouldBe("Custom message");
    }

    [Test]
    public void NotEmpty_Fails_ForNullCollection()
    {
        var rule = CollectionRules.NotEmpty<List<string>>(null);
        List<string>? value = null;
        rule.Predicate(value!).ShouldBeFalse();
    }
}
