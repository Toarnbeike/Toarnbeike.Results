using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Tests.Rules.Predicates;

public class MustTests
{
    [Test]
    public void Must_Predicate_Succeeds_WhenConditionIsTrue()
    {
        var rule = PredicateRules.Must<int>(value => value > 0, "message if failure");
        var value = 5;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void Must_Predicate_Fails_WhenConditionIsFalse()
    {
        var rule = PredicateRules.Must<int>(value => value > 0, "message if failure");
        var value = -1;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void Must_UsesCustomMessage()
    {
        var rule = PredicateRules.Must<int>(value => value > 0, "Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
