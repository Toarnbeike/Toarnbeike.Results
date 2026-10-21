using Toarnbeike.Results.Rules.Evaluations.Comparisons;

namespace Toarnbeike.Rules.Tests.Evaluations.Comparisons;

public class EqualToEvaluatorTests
{
    [Test]
    public void Evaluate_ValueEqualToTarget_ReturnsValidResult()
    {
        var evaluator = new EqualToEvaluator<int>(5);
        var result = evaluator.Evaluate(5);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ValueSmallerThanTarget_ReturnsInvalidResult()
    {
        var evaluator = new EqualToEvaluator<double>(5d);
        var result = evaluator.Evaluate(10);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_ValueGreaterThanTarget_ReturnsInvalidResult()
    {
        var evaluator = new EqualToEvaluator<TimeSpan>(TimeSpan.FromMinutes(3));
        var result = evaluator.Evaluate(TimeSpan.FromMinutes(5));
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_Any_ReturnsCorrectEvaluationDetails()
    {
        var target = new DateOnly(2023, 1, 1);
        var evaluator = new EqualToEvaluator<DateOnly>(target);
        var value = new DateOnly(2022, 12, 31);
        var result = evaluator.Evaluate(value);
        result.Evaluator.ShouldBe("EqualToEvaluator");
        result.EvaluationDetails.ShouldContainKeyAndValue("attemptedValue", value);
        result.EvaluationDetails.ShouldContainKeyAndValue("type", typeof(DateOnly).Name);
        result.EvaluationDetails.ShouldContainKeyAndValue("target", target);
    }
}
