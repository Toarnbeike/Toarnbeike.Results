using Toarnbeike.Results.Rules;
using Toarnbeike.Rules.Tests.MessageTemplateConfiguration;

namespace Toarnbeike.Rules.Tests;

[ParallelGroup(nameof(TestMessageTemplateGroup))]
public class TimeSpanRuleTests : TestMessageTemplateGroup
{
    [Test]
    public void AtLeast_ShouldReturnValidResult_ForTimeSpanGreaterThanOrEqualToMin()
    {
        var value = TimeSpan.FromHours(2);
        var result = TimeSpanRules.AtLeast(TimeSpan.FromHours(1))(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void AtLeast_ShouldReturnInvalidResult_ForTimeSpanLessThanMin()
    {
        var value = TimeSpan.FromHours(0.5);
        var result = TimeSpanRules.AtLeast(TimeSpan.FromHours(1))(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("AtLeastEvaluator");
        result.Message.ShouldContain("| Attempted Value: 00:30:00");
        result.Message.ShouldContain("type: TimeSpan");
        result.Message.ShouldContain("min: 01:00:00");
    }

    [Test]
    public void AtMost_ShouldReturnValidResult_ForTimeSpanLessThanOrEqualToMax()
    {
        var value = TimeSpan.FromHours(1);
        var result = TimeSpanRules.AtMost(TimeSpan.FromHours(2))(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void AtMost_ShouldReturnInvalidResult_ForTimeSpanGreaterThanMax()
    {
        var value = TimeSpan.FromHours(3);
        var result = TimeSpanRules.AtMost(TimeSpan.FromHours(2))(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("AtMostEvaluator");
        result.Message.ShouldContain("| Attempted Value: 03:00:00");
        result.Message.ShouldContain("type: TimeSpan");
        result.Message.ShouldContain("max: 02:00:00");
    }

    [Test]
    public void Between_ShouldReturnValidResult_ForTimeSpanWithinRange()
    {
        var value = TimeSpan.FromHours(1.5);
        var result = TimeSpanRules.Between(TimeSpan.FromHours(1), TimeSpan.FromHours(2))(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Between_ShouldReturnInvalidResult_ForTimeSpanOutsideRange()
    {
        var value = TimeSpan.FromHours(0.5);
        var result = TimeSpanRules.Between(TimeSpan.FromHours(1), TimeSpan.FromHours(2))(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("BetweenEvaluator");
        result.Message.ShouldContain("| Attempted Value: 00:30:00");
        result.Message.ShouldContain("type: TimeSpan");
        result.Message.ShouldContain("min: 01:00:00");
        result.Message.ShouldContain("max: 02:00:00");
    }

    [Test]
    public void Around_ShouldReturnValidResult_ForTimeSpanWithinTolerance()
    {
        var value = TimeSpan.FromHours(1.5);
        var result = TimeSpanRules.Around(TimeSpan.FromHours(1), TimeSpan.FromHours(0.5))(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Around_ShouldReturnInvalidResult_ForTimeSpanOutsideTolerance()
    {
        var value = TimeSpan.FromHours(2);
        var result = TimeSpanRules.Around(TimeSpan.FromHours(1), TimeSpan.FromHours(0.5))(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("BetweenEvaluator");
        result.Message.ShouldContain("| Attempted Value: 02:00:00");
        result.Message.ShouldContain("type: TimeSpan");
        result.Message.ShouldContain("min: 00:30:00");
        result.Message.ShouldContain("max: 01:30:00");
        result.Message.ShouldContain("tolerance: 00:30:00");
    }

    [Test]
    public void AtLeastZero_ShouldReturnValidResult_ForTimeSpanGreaterThanOrEqualToZero()
    {
        var value = TimeSpan.FromHours(0);
        var result = TimeSpanRules.AtLeastZero()(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void AtLeastZero_ShouldReturnInvalidResult_ForTimeSpanLessThanZero()
    {
        var value = TimeSpan.FromHours(-1);
        var result = TimeSpanRules.AtLeastZero()(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("AtLeastEvaluator");
        result.Message.ShouldContain("| Attempted Value: -01:00:00");
        result.Message.ShouldContain("type: TimeSpan");
        result.Message.ShouldContain("min: 00:00:00");
    }
}
