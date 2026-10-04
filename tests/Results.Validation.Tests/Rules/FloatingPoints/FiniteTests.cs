using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Tests.Rules.FloatingPoints;

public class FiniteTests
{
    [Test]
    public void Finite_Predicate_Succeeds_ForFiniteValue()
    {
        var rule = FloatingPointRules.Finite<double>(null);
        var value = 5.0;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void Finite_Predicate_Fails_ForPositiveInfinity()
    {
        var rule = FloatingPointRules.Finite<double>(null);
        var value = double.PositiveInfinity;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void Finite_Predicate_Fails_ForNegativeInfinity()
    {
        var rule = FloatingPointRules.Finite<double>(null);
        var value = double.NegativeInfinity;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void Finite_Predicate_Fails_ForNaN()
    {
        var rule = FloatingPointRules.Finite<double>(null);
        var value = double.NaN;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void Finite_DefaultMessage_UsesTypeName()
    {
        var rule = FloatingPointRules.Finite<double>(null);
        rule.Message.ShouldBe("Value must be a finite Double.");
    }

    [Test]
    public void Finite_UsesCustomMessage()
    {
        var rule = FloatingPointRules.Finite<double>("Custom message");
        rule.Message.ShouldBe("Custom message");
    }

}
