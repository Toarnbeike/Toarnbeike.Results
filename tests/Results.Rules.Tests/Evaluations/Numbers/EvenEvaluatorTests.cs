using Toarnbeike.Results.Rules.Evaluations.Numbers;

namespace Toarnbeike.Results.Rules.Tests.Evaluations.Numbers;

public class EvenEvaluatorTests
{
    [Test]
    public void Evaluate_EvenNumber_ReturnsValidResult()
    {
        var evaluator = new EvenEvaluator<int>();
        var result = evaluator.Evaluate(4);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_OddNumber_ReturnsInvalidResult()
    {
        var evaluator = new EvenEvaluator<int>();
        var result = evaluator.Evaluate(5);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_Zero_ReturnsValidResult()
    {
        var evaluator = new EvenEvaluator<int>();
        var result = evaluator.Evaluate(0);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_NegativeEvenNumber_ReturnsValidResult()
    {
        var evaluator = new EvenEvaluator<int>();
        var result = evaluator.Evaluate(-2);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_LongEvenNumber_ReturnsValidResult()
    {
        var evaluator = new EvenEvaluator<long>();
        var result = evaluator.Evaluate(10000000000L);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_Any_ReturnsCorrectEvaluationDetails()
    {
        var evaluator = new EvenEvaluator<int>();
        var value = 5;
        var result = evaluator.Evaluate(value);
        result.Evaluator.ShouldBe("EvenEvaluator");
        result.EvaluationDetails.ShouldContainKeyAndValue("attemptedValue", value);
        result.EvaluationDetails.ShouldContainKeyAndValue("type", typeof(int).Name);
    }
}
