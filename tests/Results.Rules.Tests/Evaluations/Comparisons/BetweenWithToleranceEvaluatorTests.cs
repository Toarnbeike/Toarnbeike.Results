using Toarnbeike.Results.Rules.Evaluations.Comparisons;

namespace Toarnbeike.Rules.Tests.Evaluations.Comparisons;

public class BetweenWithToleranceEvaluatorTests
{
    [Test]
    public void Evaluate_ValueWithinRangeWithTolerance_ReturnsValidResult()
    {
        var evaluator = new BetweenWithToleranceEvaluator<float>(5f, 10f, 0.2f);
        var result = evaluator.Evaluate(7f);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ValueEqualToMinWithTolerance_ReturnsValidResult()
    {
        var evaluator = new BetweenWithToleranceEvaluator<double>(5d, 10d, 0.5);
        var result = evaluator.Evaluate(5.5);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ValueEqualToMaxWithTolerance_ReturnsValidResult()
    {
        var evaluator = new BetweenWithToleranceEvaluator<float>(5f, 10f, 0.2f);
        var result = evaluator.Evaluate(10.0f);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ValueLessThanMinWithTolerance_ReturnsInvalidResult()
    {
        var evaluator = new BetweenWithToleranceEvaluator<decimal>(5m, 10m, 0.4m);
        var result = evaluator.Evaluate(4.5m);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_ValueGreaterThanMaxWithTolerance_ReturnsInvalidResult()
    {
        var evaluator = new BetweenWithToleranceEvaluator<decimal>(5m, 10m, 0.4m);
        var result = evaluator.Evaluate(10.5m);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_ZeroTolerance_ReturnsValidResultForEqualMinValue()
    {
        var evaluator = new BetweenWithToleranceEvaluator<int>(5, 10, 0);
        var result = evaluator.Evaluate(5);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ZeroTolerance_ReturnsValidResultForEqualMaxValue()
    {
        var evaluator = new BetweenWithToleranceEvaluator<int>(5, 10, 0);
        var result = evaluator.Evaluate(10);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ZeroTolerance_ReturnsInvalidResultForLessValue()
    {
        var evaluator = new BetweenWithToleranceEvaluator<int>(5, 10, 0);
        var result = evaluator.Evaluate(4);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_ZeroTolerance_ReturnsInvalidResultForGreaterValue()
    {
        var evaluator = new BetweenWithToleranceEvaluator<int>(5, 10, 0);
        var result = evaluator.Evaluate(11);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_NegativeTolerance_ThrowsArgumentOutOfRangeException()
    {
        var min = 5d;
        var max = 10d;
        var tolerance = -0.5;
        var evaluator = new BetweenWithToleranceEvaluator<double>(min, max, tolerance);
        Should.Throw<ArgumentOutOfRangeException>(() => evaluator.Evaluate(7d));
    }

    [Test]
    public void Evaluate_ToleranceAroundMinValue_ReturnsValidIfMinValue()
    {
        var min = float.MinValue + 1e-5f;
        var max = 0;
        var tolerance = 0.5f;
        var evaluator = new BetweenWithToleranceEvaluator<float>(min, max, tolerance);
        var result = evaluator.Evaluate(float.MinValue);
        result.IsValid.ShouldBeTrue();
        result.EvaluationDetails.ShouldContainKeyAndValue("adjustedMin", float.MinValue);
    }

    [Test]
    public void Evaluate_ToleranceAroundMaxValue_ReturnsValidIfMaxValue()
    {
        var min = 0;
        var max = float.MaxValue - 1e-5f;
        var tolerance = 0.5f;
        var evaluator = new BetweenWithToleranceEvaluator<float>(min, max, tolerance);
        var result = evaluator.Evaluate(float.MaxValue);
        result.IsValid.ShouldBeTrue();
        result.EvaluationDetails.ShouldContainKeyAndValue("adjustedMax", float.MaxValue);
    }

    [Test]
    public void Evaluate_WithoutToleranceProvided_ReturnsValidResultForTolerancesBelowDefaultToleranceBottom()
    {
        var min = 5f;
        var max = 10f;
        var value = min - 1e-7f; // default tolerance = 1e-6f
        var evaluator = new BetweenWithToleranceEvaluator<float>(min, max, null);
        var result = evaluator.Evaluate(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_WithoutToleranceProvided_ReturnsValidResultForTolerancesBelowDefaultToleranceTop()
    {
        var min = 5f;
        var max = 10f;
        var value = max + 1e-7f; // default tolerance = 1e-6f
        var evaluator = new BetweenWithToleranceEvaluator<float>(min, max, null);
        var result = evaluator.Evaluate(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_WithoutToleranceProvided_ReturnsInvalidResultForTolerancesAboveDefaultToleranceBottom()
    {
        var min = 5f;
        var max = 10f;
        var value = min - 1e-5f; // default tolerance = 1e-6f
        var evaluator = new BetweenWithToleranceEvaluator<float>(min, max, null);
        var result = evaluator.Evaluate(value);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_WithoutToleranceProvided_ReturnsInvalidResultForTolerancesAboveDefaultToleranceTop()
    {
        var min = 5f;
        var max = 10f;
        var value = max + 1e-5f; // default tolerance = 1e-6f
        var evaluator = new BetweenWithToleranceEvaluator<float>(min, max, null);
        var result = evaluator.Evaluate(value);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_Any_ReturnsCorrectEvaluationDetails()
    {
        var min = 5d;
        var max = 10d;
        var tolerance = 0.5;
        var evaluator = new BetweenWithToleranceEvaluator<double>(min, max, tolerance);
        var value = 4.5d;
        var result = evaluator.Evaluate(value);
        result.Evaluator.ShouldBe("BetweenWithToleranceEvaluator");
        result.EvaluationDetails.ShouldContainKeyAndValue("attemptedValue", value);
        result.EvaluationDetails.ShouldContainKeyAndValue("type", "Double");
        result.EvaluationDetails.ShouldContainKeyAndValue("min", min);
        result.EvaluationDetails.ShouldContainKeyAndValue("max", max);
        result.EvaluationDetails.ShouldContainKeyAndValue("tolerance", tolerance);
    }
}