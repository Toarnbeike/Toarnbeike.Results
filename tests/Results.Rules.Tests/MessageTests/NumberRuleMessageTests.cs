using Toarnbeike.Results.Rules;
using Toarnbeike.Rules.Tests.MessageTemplateConfiguration;

namespace Toarnbeike.Rules.Tests.MessageTests;

[ParallelGroup(nameof(DefaultMessageTemplateGroup))]
public class NumberRuleMessageTests : DefaultMessageTemplateGroup
{
    [Test]
    public void GreaterThan_ShouldReturnCorrectMessage_ForValueBelowMin()
    {
        var value = 3;
        var min = 5;
        var result = NumberRules.GreaterThan(min)(value);
        result.Message.ShouldBe($"'3' is not a valid Int32, must be strictly greater than '5'.");
    }

    [Test]
    public void AtLeast_ShouldReturnCorrectMessage_ForValueBelowMin()
    {
        var value = 2;
        var min = 5;
        var result = NumberRules.AtLeast(min)(value);
        result.Message.ShouldBe($"'2' is not a valid Int32, must be at least '5'.");
    }

    [Test]
    public void AtMost_ShouldReturnCorrectMessage_ForValueAboveMax()
    {
        var value = 10;
        var max = 5;
        var result = NumberRules.AtMost(max)(value);
        result.Message.ShouldBe($"'10' is not a valid Int32, must be at most '5'.");
    }

    [Test]
    public void LessThan_ShouldReturnCorrectMessage_ForValueAboveMax()
    {
        var value = 10;
        var max = 5;
        var result = NumberRules.LessThan(max)(value);
        result.Message.ShouldBe($"'10' is not a valid Int32, must be strictly less than '5'.");
    }

    [Test]
    public void Between_ShouldReturnCorrectMessage_ForValueOutsideRange()
    {
        var value = 7;
        var min = 1;
        var max = 5;
        var result = NumberRules.Between(min, max)(value);
        result.Message.ShouldBe($"'7' is not a valid Int32, must be between '1' and '5'.");
    }

    [Test]
    public void EqualTo_ShouldReturnCorrectMessage_ForNonEqualValue()
    {
        var value = 3;
        var other = 5;
        var result = NumberRules.EqualTo(other)(value);
        result.Message.ShouldBe($"'3' is not a valid Int32, must be equal to '5'.");
    }

    [Test]
    public void NotEqualTo_ShouldReturnCorrectMessage_ForEqualValue()
    {
        var value = 3;
        var other = 3;
        var result = NumberRules.NotEqualTo(other)(value);
        result.Message.ShouldBe($"'3' is not a valid Int32, must not be equal to '3'.");
    }

    [Test]
    public void MultipleOf_ShouldReturnCorrectMessage_ForNonMultiple()
    {
        var value = 7;
        var factor = 3;
        var result = NumberRules.MultipleOf(factor)(value);
        result.Message.ShouldBe($"'7' is not valid, must be a multiple of '3'.");
    }

    [Test]
    public void Even_ShouldReturnCorrectMessage_ForOddValue()
    {
        var value = 5;
        var result = NumberRules.Even<int>()(value);
        result.Message.ShouldBe($"'5' is not valid, must be an even number.");
    }

    [Test]
    public void Odd_ShouldReturnCorrectMessage_ForEvenValue()
    {
        var value = 4;
        var result = NumberRules.Odd<int>()(value);
        result.Message.ShouldBe($"'4' is not valid, must be an odd number.");
    }

    [Test]
    public void PowerOf2_ShouldReturnCorrectMessage_ForNonPowerOf2Value()
    {
        var value = 10;
        var result = NumberRules.PowerOf2<int>()(value);
        result.Message.ShouldBe($"'10' is not valid, must be a power of 2.");
    }
}
