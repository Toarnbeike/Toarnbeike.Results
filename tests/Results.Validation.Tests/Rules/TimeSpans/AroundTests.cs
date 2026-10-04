using Toarnbeike.Results.Validation.Rules;

namespace Toarnbeike.Results.Validation.Tests.Rules.TimeSpans;

public class AroundTests
{
    [Test]
    public void Around_Predicate_Succeeds_WhenValueEqualsTarget()
    {
        var rule = TimeSpanRules.Around(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(1), null);
        var value = TimeSpan.FromSeconds(5);
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void Around_Predicate_Succeeds_WhenValueWithinTolerance()
    {
        var rule = TimeSpanRules.Around(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(1), null);
        var value = TimeSpan.FromSeconds(5.5);
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void Around_Predicate_Succeeds_EvenWhenValueAndToleranceIsAroundTimeSpanMinValue()
    {
        var around = TimeSpan.MinValue + TimeSpan.FromSeconds(1);

        var rule = TimeSpanRules.Around(around, TimeSpan.FromSeconds(2), null);
        var value = TimeSpan.MinValue;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void Around_Predicate_Succeeds_EvenWhenValueAndToleranceIsAroundTimeSpanMaxValue()
    {
        var around = TimeSpan.MaxValue - TimeSpan.FromSeconds(1);
        var rule = TimeSpanRules.Around(around, TimeSpan.FromSeconds(2), null);
        var value = TimeSpan.MaxValue;
        rule.Predicate(value).ShouldBeTrue();
    }

    [Test]
    public void Around_Predicate_Fails_WhenValueOutsideTolerance()
    {
        var rule = TimeSpanRules.Around(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(1), null);
        var value = TimeSpan.FromSeconds(7);
        rule.Predicate(value).ShouldBeFalse();
    }

    [Test]
    public void Around_ThrowsArgumentOutOfRangeException_WhenToleranceIsLessThanZero()
    {
        Should.Throw<ArgumentOutOfRangeException>(() =>
        {
            var rule = TimeSpanRules.Around(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(-1), null);
        });
    }

    [Test]
    public void Around_Message_UsesTargetAndTolerance()
    {
        var rule = TimeSpanRules.Around(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(1), null);
        rule.Message.ShouldBe("TimeSpan must be around 00:00:05 with a tolerance of 00:00:01.");
    }

    [Test]
    public void Around_UsesCustomMessage()
    {
        var rule = TimeSpanRules.Around(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(1), "Custom message");
        rule.Message.ShouldBe("Custom message");
    }
}
