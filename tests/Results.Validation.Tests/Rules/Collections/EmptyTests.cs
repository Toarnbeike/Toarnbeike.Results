using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Tests.Rules.Collections;

public class EmptyTests
{
    [Test]
    public void Empty_Predicate_Fails_ForNonEmpty()
    {
        var rule = CollectionRules.Empty<string[]>(null);
        string[] value = ["a"];
        rule.Predicate(value).ShouldBeFalse();
        rule.Message.ShouldBe("Collection must be empty.");
    }

    [Test]
    public void Empty_Predicate_Succeeds_ForEmpty()
    {
        var rule = CollectionRules.Empty<List<int>>(null);
        var value = new List<int>();
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void Empty_UsesCustomMessage()
    {
        var rule = CollectionRules.Empty<List<string>>("Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
