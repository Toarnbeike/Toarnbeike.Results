using Toarnbeike.Results.Rules.Evaluations.Comparisons;

namespace Toarnbeike.Results.Rules.Tests.Evaluations.Comparisons;

public class BetweenEvaluatorTests
{
    [Test]
    public void Evaluate_ValueWithinRange_ReturnsValidResult()
    {
        var evaluator = new BetweenEvaluator<int>(5, 10);
        var result = evaluator.Evaluate(7);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ValueEqualToMin_ReturnsValidResult()
    {
        var evaluator = new BetweenEvaluator<double>(5d, 10d);
        var result = evaluator.Evaluate(5);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ValueEqualToMax_ReturnsValidResult()
    {
        var evaluator = new BetweenEvaluator<TimeSpan>(TimeSpan.FromMinutes(3), TimeSpan.FromMinutes(5));
        var result = evaluator.Evaluate(TimeSpan.FromMinutes(5));
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ValueOutsideRange_ReturnsInvalidResult()
    {
        var evaluator = new BetweenEvaluator<DateOnly>(new DateOnly(2023, 1, 1), new DateOnly(2023, 12, 31));
        var result = evaluator.Evaluate(new DateOnly(2024, 1, 1));
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_Any_ReturnsCorrectEvaluationDetails()
    {
        var min = new DateOnly(2023, 1, 1);
        var max = new DateOnly(2023, 12, 31);
        var evaluator = new BetweenEvaluator<DateOnly>(min, max);
        var value = new DateOnly(2024, 1, 1);
        var result = evaluator.Evaluate(value);
        result.Evaluator.ShouldBe("BetweenEvaluator");
        result.EvaluationDetails.ShouldContainKeyAndValue("attemptedValue", value);
        result.EvaluationDetails.ShouldContainKeyAndValue("type", typeof(DateOnly).Name);
        result.EvaluationDetails.ShouldContainKeyAndValue("min", min);
        result.EvaluationDetails.ShouldContainKeyAndValue("max", max);
    }
}
