using Toarnbeike.Results.Rules.Evaluations.Numbers;

namespace Toarnbeike.Rules.Tests.Evaluations.Numbers;

public class MultipleOfEvaluatorTests
{
    [Test]
    public void Evaluate_MultipleOf_ReturnsValidResult()
    {
        var evaluator = new MultipleOfEvaluator<int>(3);
        var result = evaluator.Evaluate(9);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_NotMultipleOf_ReturnsInvalidResult()
    {
        var evaluator = new MultipleOfEvaluator<int>(4);
        var result = evaluator.Evaluate(10);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_Zero_ReturnsValidResult()
    {
        var evaluator = new MultipleOfEvaluator<int>(5);
        var result = evaluator.Evaluate(0);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_NegativeMultipleOf_ReturnsValidResult()
    {
        var evaluator = new MultipleOfEvaluator<int>(7);
        var result = evaluator.Evaluate(-14);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_LongMultipleOf_ReturnsValidResult()
    {
        var evaluator = new MultipleOfEvaluator<long>(1000000000L);
        var result = evaluator.Evaluate(3000000000L);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_Any_ReturnsCorrectEvaluationDetails()
    {
        var evaluator = new MultipleOfEvaluator<int>(5);
        var value = 12;
        var result = evaluator.Evaluate(value);
        result.Evaluator.ShouldBe("MultipleOfEvaluator");
        result.EvaluationDetails.ShouldContainKeyAndValue("attemptedValue", value);
        result.EvaluationDetails.ShouldContainKeyAndValue("type", typeof(int).Name);
        result.EvaluationDetails.ShouldContainKeyAndValue("factor", 5);
    }
}
