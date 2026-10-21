using Toarnbeike.Results.Rules;
using Toarnbeike.Rules.Tests.MessageTemplateConfiguration;

namespace Toarnbeike.Rules.Tests.MessageTests;

[ParallelGroup(nameof(DefaultMessageTemplateGroup))]
public class FloatingPointRuleMessageTests : DefaultMessageTemplateGroup
{
    [Test]
    public void AtLeast_ShouldReturnCorrectMessage_ForValueBelowMin()
    {
        var value = 2.0;
        var min = 5.0;
        var result = FloatingPointRules.AtLeast(min, 0.2)(value);
        result.Message.ShouldBe($"'2' is not valid, must be at least '5 (-0.2)'.");
    }

    [Test]
    public void AtMost_ShouldReturnCorrectMessage_ForValueAboveMax()
    {
        var value = 10.0;
        var max = 5.0;
        var result = FloatingPointRules.AtMost(max, 0.2)(value);
        result.Message.ShouldBe($"'10' is not valid, must be at most '5 (+0.2)'.");
    }

    [Test]
    public void Between_ShouldReturnCorrectMessage_ForValueOutsideRange()
    {
        var value = 5.5;
        var min = 1.0;
        var max = 5.0;
        var result = FloatingPointRules.Between(min, max, 0.2)(value);
        result.Message.ShouldBe($"'5.5' is not valid, must be between '1' and '5' '(+-0.2)'.");
    }

    [Test]
    public void EqualTo_ShouldReturnCorrectMessage_ForNonEqualValue()
    {
        var value = 3.14;
        var other = 2.71;
        var result = FloatingPointRules.EqualTo(other, 0.2)(value);
        result.Message.ShouldBe($"'3.14' is not valid, must be equal to '2.71 (+-0.2)'.");
    }

    [Test]
    public void NotEqualTo_ShouldReturnCorrectMessage_ForEqualValue()
    {
        var value = 3.14;
        var other = 3.14;
        var result = FloatingPointRules.NotEqualTo(other, 0.2)(value);
        result.Message.ShouldBe($"'3.14' is not valid, must not be equal to '3.14 (+-0.2)'.");
    }

    [Test]
    public void MultipleOf_ShouldReturnCorrectMessage_ForNonMultiple()
    {
        var value = 7.5;
        var factor = 2.0;
        var result = FloatingPointRules.MultipleOf(factor, 0.2)(value);
        result.Message.ShouldBe($"'7.5' is not valid, must be a multiple of '2 (+-0.2)'.");
    }

    [Test]
    public void Finite_ShouldReturnCorrectMessage_ForNaN()
    {
        var value = double.NaN;
        var result = FloatingPointRules.Finite<double>()(value);
        result.Message.ShouldBe("'NaN' is not valid, must be a finite number.");
    }
}
