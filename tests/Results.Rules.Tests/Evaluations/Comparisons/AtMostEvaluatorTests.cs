using Toarnbeike.Results.Rules.Evaluations.Comparisons;

namespace Toarnbeike.Rules.Tests.Evaluations.Comparisons;

public class AtMostEvaluatorTests
{
    [Test]
    public void Evaluate_ValueLessThanMax_ReturnsValidResult()
    {
        var evaluator = new AtMostEvaluator<int>(10);
        var result = evaluator.Evaluate(5);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ValueEqualToMax_ReturnsValidResult()
    {
        var evaluator = new AtMostEvaluator<double>(10d);
        var result = evaluator.Evaluate(10);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ValueGreaterThanMax_ReturnsInvalidResult()
    {
        var evaluator = new AtMostEvaluator<TimeSpan>(TimeSpan.FromMinutes(3));
        var result = evaluator.Evaluate(TimeSpan.FromSeconds(200));
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_Any_ReturnsCorrectEvaluationDetails()
    {
        var max = new DateOnly(2023, 1, 1);
        var evaluator = new AtMostEvaluator<DateOnly>(max);
        var value = new DateOnly(2023, 1, 2);
        var result = evaluator.Evaluate(value);
        result.Evaluator.ShouldBe("AtMostEvaluator");
        result.EvaluationDetails.ShouldContainKeyAndValue("attemptedValue", value);
        result.EvaluationDetails.ShouldContainKeyAndValue("type", typeof(DateOnly).Name);
        result.EvaluationDetails.ShouldContainKeyAndValue("max", max);
    }
}
