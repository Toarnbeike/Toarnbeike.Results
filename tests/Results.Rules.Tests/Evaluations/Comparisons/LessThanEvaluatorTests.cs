using Toarnbeike.Results.Rules.Evaluations.Comparisons;

namespace Toarnbeike.Results.Rules.Tests.Evaluations.Comparisons;

public class LessThanEvaluatorTests
{
    [Test]
    public void Evaluate_ValueLessThanMax_ReturnsValidResult()
    {
        var evaluator = new LessThanEvaluator<int>(10);
        var result = evaluator.Evaluate(5);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ValueEqualToMax_ReturnsInvalidResult()
    {
        var evaluator = new LessThanEvaluator<double>(10d);
        var result = evaluator.Evaluate(10);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_ValueGreaterThanMax_ReturnsInvalidResult()
    {
        var evaluator = new LessThanEvaluator<TimeSpan>(TimeSpan.FromMinutes(5));
        var result = evaluator.Evaluate(TimeSpan.FromMinutes(10));
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_Any_ReturnsCorrectEvaluationDetails()
    {
        var max = new DateOnly(2023, 1, 1);
        var evaluator = new LessThanEvaluator<DateOnly>(max);
        var value = new DateOnly(2023, 1, 2);
        var result = evaluator.Evaluate(value);
        result.Evaluator.ShouldBe("LessThanEvaluator");
        result.EvaluationDetails.ShouldContainKeyAndValue("attemptedValue", value);
        result.EvaluationDetails.ShouldContainKeyAndValue("type", typeof(DateOnly).Name);
        result.EvaluationDetails.ShouldContainKeyAndValue("max", max);
    }
}
