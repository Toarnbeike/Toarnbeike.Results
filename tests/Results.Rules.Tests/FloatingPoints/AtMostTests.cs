namespace Toarnbeike.Results.Rules.Tests.FloatingPoints;

public class AtMostTests
{
    [Test]
    public void AtMost_Predicate_Succeeds_WhenValueEqualsUpperBound()
    {
        var rule = FloatingPointRules.AtMost(5.0, null, null);
        var value = 5.0;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void AtMost_Predicate_Succeeds_WhenValueBelowUpperBound()
    {
        var rule = FloatingPointRules.AtMost(5.0, null, null);
        var value = 4.0;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void AtMost_Predicate_Fails_WhenValueExceedsUpperBound()
    {
        var rule = FloatingPointRules.AtMost(5.0, null, null);
        var value = 6.0;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void AtMost_Predicate_Succeeds_WhenValueIsWithinDefaultTolerance()
    {
        var rule = FloatingPointRules.AtMost(5.0, null, null);
        var value = 5 + 1e-9;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void AtMost_Predicate_Succeeds_WhenValueIsWithinCustomTolerance()
    {
        var rule = FloatingPointRules.AtMost(5.0, 0.1, null);
        var value = 5.05;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void AtMost_Predicate_Fails_WhenValueIsOutsideCustomTolerance()
    {
        var rule = FloatingPointRules.AtMost(5.0, 0.1, null);
        var value = 5.11;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void AtMost_Predicate_Fails_WhenValueIsOutsideDefaultTolerance()
    {
        var rule = FloatingPointRules.AtMost(5.0, null, null);
        var value = 5 + 1e-8;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void AtMost_Predicate_Fails_ForNaN()
    {
        var rule = FloatingPointRules.AtMost(5.0, null, null);
        var value = double.NaN;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void AtMost_ThrowsArgumentOutOfRangeException_ForNegativeTolerance()
    {
        Should.Throw<ArgumentOutOfRangeException>(() =>
        {
            var rule = FloatingPointRules.AtMost(5.0, -0.1, null);
        });
    }

    [Test]
    public void AtMost_DefaultMessage_UsesUpperBoundValue()
    {
        var rule = FloatingPointRules.AtMost(5.0, null, null);
        rule.Message.ShouldBe("Value must be at most 5.");
    }

    [Test]
    public void AtMost_UsesCustomMessage()
    {
        var rule = FloatingPointRules.AtMost(5.0, null, "Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
