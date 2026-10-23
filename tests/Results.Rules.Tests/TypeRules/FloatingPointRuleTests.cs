using Toarnbeike.Results.Rules.Tests.MessageTemplateConfiguration;
using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Rules.Tests.TypeRules;

[ParallelGroup(nameof(TestMessageTemplateGroup))]
public class FloatingPointRuleTests : TestMessageTemplateGroup
{
    [Test]
    public void AtLeast_ShouldReturnValidResult_ForValueWithinTolerance()
    {
        var value = 5.1;
        var result = FloatingPointRules.AtLeast(5.0, 0.2)(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void AtLeast_ShouldReturnInvalidResult_ForValueOutsideTolerance()
    {
        var value = 4.7;
        var result = FloatingPointRules.AtLeast(5.0, 0.2)(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("AtLeastWithToleranceEvaluator");
        result.Message.ShouldContain("| Attempted Value: 4.7");
        result.Message.ShouldContain("type: Double");
        result.Message.ShouldContain("min: 5");
        result.Message.ShouldContain("tolerance: 0.2");
    }

    [Test]
    public void AtMost_ShouldReturnValidResult_ForValueWithinTolerance()
    {
        var value = 4.9;
        var result = FloatingPointRules.AtMost(5.0, 0.2)(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void AtMost_ShouldReturnInvalidResult_ForValueOutsideTolerance()
    {
        var value = 5.3;
        var result = FloatingPointRules.AtMost(5.0, 0.2)(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("AtMostWithToleranceEvaluator");
        result.Message.ShouldContain("| Attempted Value: 5.3");
        result.Message.ShouldContain("type: Double");
        result.Message.ShouldContain("max: 5");
        result.Message.ShouldContain("tolerance: 0.2");
    }

    [Test]
    public void Between_ShouldReturnValidResult_ForValueWithinTolerance()
    {
        var value = 5.1;
        var result = FloatingPointRules.Between(5.0, 6.0, 0.2)(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Between_ShouldReturnInvalidResult_ForValueOutsideTolerance()
    {
        var value = 4.7;
        var result = FloatingPointRules.Between(5.0, 6.0, 0.2)(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("BetweenWithToleranceEvaluator");
        result.Message.ShouldContain("| Attempted Value: 4.7");
        result.Message.ShouldContain("type: Double");
        result.Message.ShouldContain("min: 5");
        result.Message.ShouldContain("max: 6");
        result.Message.ShouldContain("tolerance: 0.2");
    }

    [Test]
    public void EqualTo_ShouldReturnValidResult_ForValueWithinTolerance()
    {
        var value = 5.1;
        var result = FloatingPointRules.EqualTo(5.0, 0.2)(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void EqualTo_ShouldReturnInvalidResult_ForValueOutsideTolerance()
    {
        var value = 4.7;
        var result = FloatingPointRules.EqualTo(5.0, 0.2)(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("EqualToWithToleranceEvaluator");
        result.Message.ShouldContain("| Attempted Value: 4.7");
        result.Message.ShouldContain("type: Double");
        result.Message.ShouldContain("target: 5");
        result.Message.ShouldContain("tolerance: 0.2");
    }

    [Test]
    public void NotEqualTo_ShouldReturnValidResult_ForValueNotEqual()
    {
        var value = 5.1;
        var result = FloatingPointRules.NotEqualTo(5.0, 0.05)(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void NotEqualTo_ShouldReturnInvalidResult_ForValueEqual()
    {
        var value = 5.0;
        var result = FloatingPointRules.NotEqualTo(5.0, 0.2)(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("NotEqualToWithToleranceEvaluator");
        result.Message.ShouldContain("| Attempted Value: 5");
        result.Message.ShouldContain("type: Double");
        result.Message.ShouldContain("target: 5");
        result.Message.ShouldContain("tolerance: 0.2");
    }

    [Test]
    public void MultipleOf_ShouldReturnValidResult_ForValueThatIsMultiple()
    {
        var value = 10.01;
        var result = FloatingPointRules.MultipleOf(2.0, 0.01)(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void MultipleOf_ShouldReturnInvalidResult_ForValueThatIsNotMultiple()
    {
        var value = 10.1;
        var result = FloatingPointRules.MultipleOf(2.0, 0.05)(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("MultipleOfWithToleranceEvaluator");
        result.Message.ShouldContain("| Attempted Value: 10.1");
        result.Message.ShouldContain("type: Double");
        result.Message.ShouldContain("factor: 2");
        result.Message.ShouldContain("tolerance: 0.05");
    }

    [Test]
    public void Finite_ShouldReturnValidResult_ForFiniteValue()
    {
        var value = 5.0;
        var result = FloatingPointRules.Finite<double>()(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Finite_ShouldReturnInvalidResult_ForInfinityValue()
    {
        var value = double.PositiveInfinity;
        var result = FloatingPointRules.Finite<double>()(value);
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("FiniteEvaluator");
        result.Message.ShouldContain("| Attempted Value: Infinity");
        result.Message.ShouldContain("type: Double");
    }
}
