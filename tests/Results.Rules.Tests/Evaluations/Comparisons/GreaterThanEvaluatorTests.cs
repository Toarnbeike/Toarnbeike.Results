using Toarnbeike.Results.Rules.Evaluations.Comparisons;

namespace Toarnbeike.Results.Rules.Tests.Evaluations.Comparisons;

public class GreaterThanEvaluatorTests
{
    [Test]
    public void Evaluate_ValueGreaterThanMin_ReturnsValidResult()
    {
        var evaluator = new GreaterThanEvaluator<int>(5);
        var result = evaluator.Evaluate(10);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ValueEqualToMin_ReturnsInvalidResult()
    {
        var evaluator = new GreaterThanEvaluator<double>(5d);
        var result = evaluator.Evaluate(5);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_ValueLessThanMin_ReturnsInvalidResult()
    {
        var evaluator = new GreaterThanEvaluator<TimeSpan>(TimeSpan.FromMinutes(3));
        var result = evaluator.Evaluate(TimeSpan.FromSeconds(150));
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_Any_ReturnsCorrectEvaluationDetails()
    {
        var min = new DateOnly(2023, 1, 1);
        var evaluator = new GreaterThanEvaluator<DateOnly>(min);
        var value = new DateOnly(2022, 12, 31);
        var result = evaluator.Evaluate(value);
        result.Evaluator.ShouldBe("GreaterThanEvaluator");
        result.EvaluationDetails.ShouldContainKeyAndValue("attemptedValue", value);
        result.EvaluationDetails.ShouldContainKeyAndValue("type", typeof(DateOnly).Name);
        result.EvaluationDetails.ShouldContainKeyAndValue("min", min);
    }
}
