using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Tests.Rules.FloatingPoints;

public class NotNaNTests
{
    [Test]
    public void NotNaN_Predicate_Succeeds_ForNonNaNValue()
    {
        var rule = FloatingPointRules.NotNaN<double>(null);
        var value = 5.0;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void NotNaN_Predicate_Fails_ForNaN()
    {
        var rule = FloatingPointRules.NotNaN<double>(null);
        var value = double.NaN;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void NotNaN_DefaultMessage()
    {
        var rule = FloatingPointRules.NotNaN<double>(null);
        rule.Message.ShouldBe("Value must not be NaN.");
    }

    [Test]
    public void NotNaN_UsesCustomMessage()
    {
        var rule = FloatingPointRules.NotNaN<double>("Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
