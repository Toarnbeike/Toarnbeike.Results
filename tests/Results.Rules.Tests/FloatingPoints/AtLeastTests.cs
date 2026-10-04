namespace Toarnbeike.Results.Rules.Tests.FloatingPoints;

/// <summary>
/// This test used decimals instead of doubles to prove that `FloatingPointRules` now properly supports `decimal` values.
/// </summary>
public class AtLeastTests
{
    [Test]
    public void AtLeast_Predicate_Succeeds_WhenValueEqualsLowerBound()
    {
        var rule = FloatingPointRules.AtLeast(5.0m, null, null);
        var value = 5.0m;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void AtLeast_Predicate_Succeeds_WhenValueExceedsLowerBound()
    {
        var rule = FloatingPointRules.AtLeast(5.0m, null, null);
        var value = 6.0m;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void AtLeast_Predicate_Fails_WhenValueBelowLowerBound()
    {
        var rule = FloatingPointRules.AtLeast(5.0m, null, null);
        var value = 4.0m;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void AtLeast_Predicate_Succeeds_WhenValueIsWithinDefaultTolerance()
    {
        var rule = FloatingPointRules.AtLeast(5.0m, null, null);
        var value = 5.0m - 1e-9m;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void AtLeast_Predicate_Succeeds_WhenValueIsWithinCustomTolerance()
    {
        var rule = FloatingPointRules.AtLeast(5.0m, 0.1m, null);
        var value = 4.95m;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void AtLeast_Predicate_Fails_WhenValueIsOutsideCustomTolerance()
    {
        var rule = FloatingPointRules.AtLeast(5.0m, 0.1m, null);
        var value = 4.89m;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void AtLeast_Predicate_Fails_WhenValueIsOutsideDefaultTolerance()
    {
        var rule = FloatingPointRules.AtLeast(5.0m, null, null);
        var value = 5.0m - 1e-8m;
        rule.Predicate(value).ShouldBeFalse();
    }

    /// <summary>
    /// NaN is not defined for decimal, therefore this test uses double.
    /// </summary>
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
            var rule = FloatingPointRules.AtLeast(5.0m, -0.1m, null);
        });
    }

    [Test]
    public void AtLeast_DefaultMessage_UsesLowerBoundValue()
    {
        var rule = FloatingPointRules.AtLeast(5.0m, null, null);
        rule.Message.ShouldBe($"Value must be at least {5.0m}.");
    }

    [Test]
    public void AtLeast_UsesCustomMessage()
    {
        var rule = FloatingPointRules.AtLeast(5.0m, null, "Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
