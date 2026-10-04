namespace Toarnbeike.Results.Rules.Tests.FloatingPoints;

public class BetweenTests
{
    [Test]
    public void Between_Predicate_Succeeds_WhenValueEqualsLowerBound()
    {
        var rule = FloatingPointRules.Between(5.0, 10.0, null, null);
        var value = 5.0;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void Between_Predicate_Succeeds_WhenValueEqualsUpperBound()
    {
        var rule = FloatingPointRules.Between(5.0, 10.0, null, null);
        var value = 10.0;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void Between_Predicate_Succeeds_WhenValueIsWithinBounds()
    {
        var rule = FloatingPointRules.Between(5.0, 10.0, null, null);
        var value = 7.5;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void Between_Predicate_Fails_WhenValueIsBelowLowerBound()
    {
        var rule = FloatingPointRules.Between(5.0, 10.0, null, null);
        var value = 4.9;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void Between_Predicate_Fails_WhenValueIsAboveUpperBound()
    {
        var rule = FloatingPointRules.Between(5.0, 10.0, null, null);
        var value = 10.1;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void Between_Predicate_Succeeds_WhenValue_IsWithinDefaultToleranceOfLowerBound()
    {
        var rule = FloatingPointRules.Between(5.0, 10.0, null, null);
        var value = 5 - 1e-9;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void Between_Predicate_Succeeds_WhenValue_IsWithinDefaultToleranceOfUpperBound()
    {
        var rule = FloatingPointRules.Between(5.0, 10.0, null, null);
        var value = 10 + 1e-9;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void Between_Predicate_Fails_WhenValue_IsOutsideDefaultToleranceOfLowerBound()
    {
        var rule = FloatingPointRules.Between(5.0, 10.0, null, null);
        var value = 5 - 1e-8;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void Between_Predicate_Fails_WhenValue_IsOutsideDefaultToleranceOfUpperBound()
    {
        var rule = FloatingPointRules.Between(5.0, 10.0, null, null);
        var value = 10 + 1e-8;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void Between_Predicate_Succeeds_WhenValue_IsWithinCustomToleranceOfLowerBound()
    {
        var rule = FloatingPointRules.Between(5.0, 10.0, 0.1, null);
        var value = 4.95;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void Between_Predicate_Succeeds_WhenValue_IsWithinCustomToleranceOfUpperBound()
    {
        var rule = FloatingPointRules.Between(5.0, 10.0, 0.1, null);
        var value = 10.05;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void Between_Predicate_Fails_WhenValue_IsOutsideCustomToleranceOfLowerBound()
    {
        var rule = FloatingPointRules.Between(5.0, 10.0, 0.1, null);
        var value = 4.89;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void Between_Predicate_Fails_WhenValue_IsOutsideCustomToleranceOfUpperBound()
    {
        var rule = FloatingPointRules.Between(5.0, 10.0, 0.1, null);
        var value = 10.11;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void Between_Predicate_Fails_ForNaN()
    {
        var rule = FloatingPointRules.Between(5.0, 10.0, null, null);
        var value = double.NaN;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void Between_ThrowsArgumentOutOfRangeException_ForNegativeTolerance()
    {
        Should.Throw<ArgumentOutOfRangeException>(() =>
        {
            var rule = FloatingPointRules.Between(5.0, 10.0, -0.1, null);
        });
    }

    [Test]
    public void Between_DefaultMessage_UsesLowerAndUpperBoundValues()
    {
        var rule = FloatingPointRules.Between(5.0, 10.0, null, null);
        rule.Message.ShouldBe("Value must be between 5 and 10.");
    }

    [Test]
    public void Between_UsesCustomMessage()
    {
        var rule = FloatingPointRules.Between(5.0, 10.0, null, "Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
