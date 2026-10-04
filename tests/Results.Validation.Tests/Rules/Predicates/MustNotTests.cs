using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Tests.Rules.Predicates;

public class MustNotTests
{
    [Test]
    public void MustNot_Predicate_Succeeds_WhenConditionIsFalse()
    {
        var rule = PredicateRules.MustNot<int>(value => value < 0, "message if failure");
        var value = 5;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void MustNot_Predicate_Fails_WhenConditionIsTrue()
    {
        var rule = PredicateRules.MustNot<int>(value => value < 0, "message if failure");
        var value = -1;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void MustNot_UsesCustomMessage()
    {
        var rule = PredicateRules.MustNot<int>(value => value < 0, "Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
