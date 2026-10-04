using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Tests.Rules.FloatingPoints;

public class AtLeastTests
{
    [Test]
    public void AtLeast_Predicate_Succeeds_WhenValueEqualsLowerBound()
    {
        var rule = FloatingPointRules.AtLeast(5.0, null, null);
        var value = 5.0;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void AtLeast_Predicate_Succeeds_WhenValueExceedsLowerBound()
    {
        var rule = FloatingPointRules.AtLeast(5.0, null, null);
        var value = 6.0;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void AtLeast_Predicate_Fails_WhenValueBelowLowerBound()
    {
        var rule = FloatingPointRules.AtLeast(5.0, null, null);
        var value = 4.0;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void AtLeast_Predicate_Succeeds_WhenValueIsWithinDefaultTolerance()
    {
        var rule = FloatingPointRules.AtLeast(5.0, null, null);
        var value = 5 - 1e-9;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void AtLeast_Predicate_Succeeds_WhenValueIsWithinCustomTolerance()
    {
        var rule = FloatingPointRules.AtLeast(5.0, 0.1, null);
        var value = 4.95;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void AtLeast_Predicate_Fails_WhenValueIsOutsideCustomTolerance()
    {
        var rule = FloatingPointRules.AtLeast(5.0, 0.1, null);
        var value = 4.89;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void AtLeast_Predicate_Fails_WhenValueIsOutsideDefaultTolerance()
    {
        var rule = FloatingPointRules.AtLeast(5.0, null, null);
        var value = 5 - 1e-8;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void AtLeast_Predicate_Fails_ForNaN()
    {
        var rule = FloatingPointRules.AtLeast(5.0, null, null);
        var value = double.NaN;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void AtLeast_ThrowsArgumentOutOfRangeException_ForNegativeTolerance()
    {
        Should.Throw<ArgumentOutOfRangeException>(() =>
        {
            var rule = FloatingPointRules.AtLeast(5.0, -0.1, null);
        });
    }

    [Test]
    public void AtLeast_DefaultMessage_UsesLowerBoundValue()
    {
        var rule = FloatingPointRules.AtLeast(5.0, null, null);
        rule.Message.ShouldBe("Value must be at least 5.");
    }

    [Test]
    public void AtLeast_UsesCustomMessage()
    {
        var rule = FloatingPointRules.AtLeast(5.0, null, "Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
