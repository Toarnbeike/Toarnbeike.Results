using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Tests.Rules.Numerics;

public class GreaterThanTests
{
    [Test]
    public void GreaterThan_Predicate_Succeeds_WhenValueExceedsMin()
    {
        var rule = NumericRules.GreaterThan(5, null);
        rule.Predicate(6).ShouldBeTrue();
    }

    [Test]
    public void GreaterThan_Predicate_Fails_WhenValueEqualsMin()
    {
        var rule = NumericRules.GreaterThan(5, null);
        rule.Predicate(5).ShouldBeFalse();
    }

    [Test]
    public void GreaterThan_Predicate_Fails_WhenValueBelowMin()
    {
        var rule = NumericRules.GreaterThan(5, null);
        rule.Predicate(4).ShouldBeFalse();
    }

    [Test]
    public void GreaterThan_Message_UsesMin()
    {
        var rule = NumericRules.GreaterThan(5, null);
        rule.Message.ShouldBe("Value must be greater than 5.");
    }

    [Test]
    public void GreaterThan_UsesCustomMessage()
    {
        var rule = NumericRules.GreaterThan(5, "Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
