namespace Toarnbeike.Results.Rules.Tests.FloatingPoints;

public class MultipleOfTests
{
    [Test]
    public void MultipleOf_Predicate_Succeeds_ForExactMultiple()
    {
        var rule = FloatingPointRules.MultipleOf(2.0, null, null);
        var value = 4.0;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void MultipleOf_Predicate_Succeeds_ForMultipleWithinDefaultTolerance_Upper()
    {
        var rule = FloatingPointRules.MultipleOf(2.0, null, null);
        var value = 4.0 + 1e-10;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void MultipleOf_Predicate_Succeeds_ForMultipleWithinDefaultTolerance_Lower()
    {
        var rule = FloatingPointRules.MultipleOf(2.0, null, null);
        var value = 4.0 - 1e-10;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void MultipleOf_Predicate_Succeeds_ForMultipleWithinCustomTolerance_Upper()
    {
        var rule = FloatingPointRules.MultipleOf(2.0, 0.1, null);
        var value = 4.05;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void MultipleOf_Predicate_Succeeds_ForMultipleWithinCustomTolerance_Lower()
    {
        var rule = FloatingPointRules.MultipleOf(2.0, 0.1, null);
        var value = 3.95;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void MultipleOf_Predicate_Fails_ForNonMultiple()
    {
        var rule = FloatingPointRules.MultipleOf(2.0, null, null);
        var value = 5.0;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void MultipleOf_Predicate_Fails_ForNonMultipleOutsideDefaultTolerance_Upper()
    {
        var rule = FloatingPointRules.MultipleOf(2.0, null, null);
        var value = 4.0 + 1e-8;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void MultipleOf_Predicate_Fails_ForNonMultipleOutsideDefaultTolerance_Lower()
    {
        var rule = FloatingPointRules.MultipleOf(2.0, null, null);
        var value = 4.0 - 1e-8;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void MultipleOf_Predicate_Fails_ForNonMultipleOutsideCustomTolerance_Upper()
    {
        var rule = FloatingPointRules.MultipleOf(2.0, 0.1, null);
        var value = 4.11;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void MultipleOf_Predicate_Fails_ForNonMultipleOutsideCustomTolerance_Lower()
    {
        var rule = FloatingPointRules.MultipleOf(2.0, 0.1, null);
        var value = 3.89;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void MultipleOf_Predicate_Throws_ForZeroFactor()
    {
        Should.Throw<ArgumentOutOfRangeException>(() =>
        {
            var rule = FloatingPointRules.MultipleOf(0.0, null, null);
        });
    }

    [Test]
    public void MultipleOf_Predicate_Throws_ForNegativeTolerance()
    {
        Should.Throw<ArgumentOutOfRangeException>(() =>
        {
            var rule = FloatingPointRules.MultipleOf(2.0, -0.1, null);
        });
    }

    [Test]
    public void MultipleOf_Predicate_Fails_ForNaN()
    {
        var rule = FloatingPointRules.MultipleOf(2.0, null, null);
        var value = double.NaN;
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void MultipleOf_DefaultMessage_UsesFactorValue()
    {
        var rule = FloatingPointRules.MultipleOf(2.0, null, null);
        rule.Message.ShouldBe("Value must be a multiple of 2.");
    }

    [Test]
    public void MultipleOf_UsesCustomMessage()
    {
        var rule = FloatingPointRules.MultipleOf(2.0, null, "Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
