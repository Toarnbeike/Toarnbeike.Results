using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Tests.Rules.Numerics;

public class LessThanTests
{
    [Test]
    public void LessThan_Predicate_Succeeds_WhenValueBelowMax()
    {
        var rule = NumericRules.LessThan(5, null);
        rule.Predicate(4).ShouldBeTrue();
    }

    [Test]
    public void LessThan_Predicate_Fails_WhenValueEqualsMax()
    {
        var rule = NumericRules.LessThan(5, null);
        rule.Predicate(5).ShouldBeFalse();
    }

    [Test]
    public void LessThan_Predicate_Fails_WhenValueExceedsMax()
    {
        var rule = NumericRules.LessThan(5, null);
        rule.Predicate(6).ShouldBeFalse();
    }

    [Test]
    public void LessThan_Message_UsesMax()
    {
        var rule = NumericRules.LessThan(5, null);
        rule.Message.ShouldBe("Value must be less than 5.");
    }

    [Test]
    public void LessThan_UsesCustomMessage()
    {
        var rule = NumericRules.LessThan(5, "Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
