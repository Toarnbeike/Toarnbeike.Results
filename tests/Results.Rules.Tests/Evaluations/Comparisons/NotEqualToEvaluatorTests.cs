using Toarnbeike.Results.Rules.Evaluations.Comparisons;

namespace Toarnbeike.Rules.Tests.Evaluations.Comparisons;

public class NotEqualToEvaluatorTests
{
    [Test]
    public void Evaluate_ValueNotEqualToTarget_ReturnsValidResult()
    {
        var evaluator = new NotEqualToEvaluator<int>(5);
        var result = evaluator.Evaluate(10);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ValueEqualToTarget_ReturnsInvalidResult()
    {
        var evaluator = new NotEqualToEvaluator<string>("test");
        var result = evaluator.Evaluate("test");
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_Any_ReturnsCorrectEvaluationDetails()
    {
        var target = 42;
        var evaluator = new NotEqualToEvaluator<int>(target);
        var value = 100;
        var result = evaluator.Evaluate(value);
        result.Evaluator.ShouldBe("NotEqualToEvaluator");
        result.EvaluationDetails.ShouldContainKeyAndValue("attemptedValue", value);
        result.EvaluationDetails.ShouldContainKeyAndValue("type", typeof(int).Name);
        result.EvaluationDetails.ShouldContainKeyAndValue("target", target);
    }
}
