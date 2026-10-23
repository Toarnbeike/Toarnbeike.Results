using Toarnbeike.Results.Rules.Evaluations.Numbers;

namespace Toarnbeike.Results.Rules.Tests.Evaluations.Numbers;

public class OddEvaluatorTests
{
    [Test]
    public void Evaluate_OddNumber_ReturnsValidResult()
    {
        var evaluator = new OddEvaluator<int>();
        var result = evaluator.Evaluate(5);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_EvenNumber_ReturnsInvalidResult()
    {
        var evaluator = new OddEvaluator<int>();
        var result = evaluator.Evaluate(4);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_Zero_ReturnsInvalidResult()
    {
        var evaluator = new OddEvaluator<int>();
        var result = evaluator.Evaluate(0);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_NegativeOddNumber_ReturnsValidResult()
    {
        var evaluator = new OddEvaluator<int>();
        var result = evaluator.Evaluate(-3);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_LongOddNumber_ReturnsValidResult()
    {
        var evaluator = new OddEvaluator<long>();
        var result = evaluator.Evaluate(10000000001L);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_Any_ReturnsCorrectEvaluationDetails()
    {
        var evaluator = new OddEvaluator<int>();
        var value = 4;
        var result = evaluator.Evaluate(value);
        result.Evaluator.ShouldBe("OddEvaluator");
        result.EvaluationDetails.ShouldContainKeyAndValue("attemptedValue", value);
        result.EvaluationDetails.ShouldContainKeyAndValue("type", typeof(int).Name);
    }
}
