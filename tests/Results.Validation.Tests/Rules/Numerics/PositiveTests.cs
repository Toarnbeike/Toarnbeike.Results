using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Tests.Rules.Numerics;

public class PositiveTests
{
    [Test]
    public void Positive_Predicate_Succeeds_ForPositiveValue()
    {
        var rule = NumericRules.Positive<int>(null);
        rule.Predicate(5).ShouldBeTrue();
    }

    [Test]
    public void Positive_Predicate_Fails_ForZero()
    {
        var rule = NumericRules.Positive<int>(null);
        rule.Predicate(0).ShouldBeFalse();
    }

    [Test]
    public void Positive_Predicate_Fails_ForNegativeValue()
    {
        var rule = NumericRules.Positive<int>(null);
        rule.Predicate(-5).ShouldBeFalse();
    }

    [Test]
    public void Positive_Message_UsesDefaultMessage()
    {
        var rule = NumericRules.Positive<int>(null);
        rule.Message.ShouldBe("Value must be positive.");
    }

    [Test]
    public void Positive_UsesCustomMessage()
    {
        var rule = NumericRules.Positive<int>("Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
