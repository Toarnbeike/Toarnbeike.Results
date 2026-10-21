using Toarnbeike.Results.Rules;
using Toarnbeike.Rules.Tests.MessageTemplateConfiguration;

namespace Toarnbeike.Rules.Tests;

[ParallelGroup(nameof(TestMessageTemplateGroup))]
public class NumberRuleTests : TestMessageTemplateGroup
{
    [Test]
    public void GreaterThan_ShouldReturnValidResult_ForValueGreaterThanMin()
    {
        var value = 10;
        var result = NumberRules.GreaterThan(5)(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void GreaterThan_ShouldReturnInvalidResult_ForValueEqualToMin()
    {
        var value = 5;
        var result = NumberRules.GreaterThan(5)(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("GreaterThanEvaluator");
        result.Message.ShouldContain("| Attempted Value: 5");
        result.Message.ShouldContain("type: Int32");
        result.Message.ShouldContain("min: 5");
    }

    [Test]
    public void AtLeast_ShouldReturnValidResult_ForValueGreaterThanMin()
    {
        var value = 10;
        var result = NumberRules.AtLeast(5)(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void AtLeast_ShouldReturnInvalidResult_ForValueLessThanMin()
    {
        var value = 4;
        var result = NumberRules.AtLeast(5)(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("AtLeastEvaluator");
        result.Message.ShouldContain("| Attempted Value: 4");
        result.Message.ShouldContain("type: Int32");
        result.Message.ShouldContain("min: 5");
    }

    [Test]
    public void AtMost_ShouldReturnValidResult_ForValueLessThanMax()
    {
        var value = 4;
        var result = NumberRules.AtMost(5)(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void AtMost_ShouldReturnInvalidResult_ForValueGreaterThanMax()
    {
        var value = 6;
        var result = NumberRules.AtMost(5)(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("AtMostEvaluator");
        result.Message.ShouldContain("| Attempted Value: 6");
        result.Message.ShouldContain("type: Int32");
        result.Message.ShouldContain("max: 5");
    }

    [Test]
    public void LessThan_ShouldReturnValidResult_ForValueLessThanMax()
    {
        var value = 4;
        var result = NumberRules.LessThan(5)(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void LessThan_ShouldReturnInvalidResult_ForValueEqualToMax()
    {
        var value = 5;
        var result = NumberRules.LessThan(5)(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("LessThanEvaluator");
        result.Message.ShouldContain("| Attempted Value: 5");
        result.Message.ShouldContain("type: Int32");
        result.Message.ShouldContain("max: 5");
    }

    [Test]
    public void Between_ShouldReturnValidResult_ForValueWithinRange()
    {
        var value = 5;
        var result = NumberRules.Between(4, 6)(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Between_ShouldReturnInvalidResult_ForValueOutsideRange()
    {
        var value = 3;
        var result = NumberRules.Between(4, 6)(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("BetweenEvaluator");
        result.Message.ShouldContain("| Attempted Value: 3");
        result.Message.ShouldContain("type: Int32");
        result.Message.ShouldContain("min: 4");
        result.Message.ShouldContain("max: 6");
    }

    [Test]
    public void EqualTo_ShouldReturnValidResult_ForValueEqualToTarget()
    {
        var value = 5;
        var result = NumberRules.EqualTo(5)(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void EqualTo_ShouldReturnInvalidResult_ForValueNotEqualToTarget()
    {
        var value = 4;
        var result = NumberRules.EqualTo(5)(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("EqualToEvaluator");
        result.Message.ShouldContain("| Attempted Value: 4");
        result.Message.ShouldContain("type: Int32");
        result.Message.ShouldContain("target: 5");
    }

    [Test]
    public void NotEqualTo_ShouldReturnValidResult_ForValueNotEqualToTarget()
    {
        var value = 4;
        var result = NumberRules.NotEqualTo(5)(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void NotEqualTo_ShouldReturnInvalidResult_ForValueEqualToTarget()
    {
        var value = 5;
        var result = NumberRules.NotEqualTo(5)(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("NotEqualToEvaluator");
        result.Message.ShouldContain("| Attempted Value: 5");
        result.Message.ShouldContain("type: Int32");
        result.Message.ShouldContain("target: 5");
    }

    [Test]
    public void MultipleOf_ShouldReturnValidResult_ForValueMultipleOfFactor()
    {
        var value = 10;
        var result = NumberRules.MultipleOf(5)(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void MultipleOf_ShouldReturnInvalidResult_ForValueNotMultipleOfFactor()
    {
        var value = 7;
        var result = NumberRules.MultipleOf(5)(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("MultipleOfEvaluator");
        result.Message.ShouldContain("| Attempted Value: 7");
        result.Message.ShouldContain("type: Int32");
        result.Message.ShouldContain("factor: 5");
    }

    [Test]
    public void Even_ShouldReturnValidResult_ForEvenValue()
    {
        var value = 4;
        var result = NumberRules.Even<int>()(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Even_ShouldReturnInvalidResult_ForOddValue()
    {
        var value = 5;
        var result = NumberRules.Even<int>()(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("EvenEvaluator");
        result.Message.ShouldContain("| Attempted Value: 5");
        result.Message.ShouldContain("type: Int32");
    }

    [Test]
    public void Odd_ShouldReturnValidResult_ForOddValue()
    {
        var value = 5;
        var result = NumberRules.Odd<int>()(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Odd_ShouldReturnInvalidResult_ForEvenValue()
    {
        var value = 4;
        var result = NumberRules.Odd<int>()(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("OddEvaluator");
        result.Message.ShouldContain("| Attempted Value: 4");
        result.Message.ShouldContain("type: Int32");
    }

    [Test]
    public void PowerOf2_ShouldReturnValidResult_ForPowerOf2Value()
    {
        var value = 8;
        var result = NumberRules.PowerOf2<int>()(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void PowerOf2_ShouldReturnInvalidResult_ForNonPowerOf2Value()
    {
        var value = 10;
        var result = NumberRules.PowerOf2<int>()(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("PowerOf2Evaluator");
        result.Message.ShouldContain("| Attempted Value: 10");
        result.Message.ShouldContain("type: Int32");
    }
}
