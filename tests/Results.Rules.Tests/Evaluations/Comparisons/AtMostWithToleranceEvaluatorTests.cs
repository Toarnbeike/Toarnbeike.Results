using Toarnbeike.Results.Rules.Evaluations.Comparisons;

namespace Toarnbeike.Rules.Tests.Evaluations.Comparisons;

public class AtMostWithToleranceEvaluatorTests
{
    [Test]
    public void Evaluate_ValueLessThanMaxWithTolerance_ReturnsValidResult()
    {
        var evaluator = new AtMostWithToleranceEvaluator<float>(5f, 0.2f);
        var result = evaluator.Evaluate(4f);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ValueEqualToMaxWithTolerance_ReturnsValidResult()
    {
        var evaluator = new AtMostWithToleranceEvaluator<double>(5d, 0.5);
        var result = evaluator.Evaluate(5.5);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ValueEqualToMinWithTolerance_ReturnsValidResult()
    {
        var evaluator = new AtMostWithToleranceEvaluator<float>(5f, 0.2f);
        var result = evaluator.Evaluate(5.0f);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ValueGreaterThanMaxWithTolerance_ReturnsInvalidResult()
    {
        var evaluator = new AtMostWithToleranceEvaluator<decimal>(5m, 0.4m);
        var result = evaluator.Evaluate(5.5m);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_WithToleranceOfZero_ReturnsValidResultForEqualValue()
    {
        var evaluator = new AtMostWithToleranceEvaluator<int>(5, 0);
        var result = evaluator.Evaluate(5);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_WithToleranceOfZero_ReturnsInvalidResultForGreaterValue()
    {
        var evaluator = new AtMostWithToleranceEvaluator<int>(5, 0);
        var result = evaluator.Evaluate(6);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_WithNegativeTolerance_ThrowsArgumentOutOfRangeException()
    {
        var max = 5d;
        var tolerance = -0.5;
        var evaluator = new AtMostWithToleranceEvaluator<double>(max, tolerance);
        Should.Throw<ArgumentOutOfRangeException>(() => evaluator.Evaluate(5d));
    }

    [Test]
    public void Evaluate_WithToleranceAndMaxAboveMaximumValue_ReturnsValidResult()
    {
        var max = float.MaxValue - 1e-7f;
        var tolerance = 0.5f; // without overflow check, this would cause an overflow when calculating max + tolerance
        var evaluator = new AtMostWithToleranceEvaluator<float>(max, tolerance);
        var value = float.MaxValue;
        var result = evaluator.Evaluate(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_WithoutToleranceProvided_ReturnValidResultForTolerancesBelowDefaultTolerance()
    {
        var max = 5f;
        var evaluator = new AtMostWithToleranceEvaluator<float>(max, null);
        var value = 5f + 1e-7f; // default tolerance is 1e-6 for floats
        var result = evaluator.Evaluate(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_WithoutToleranceProvided_ReturnInvalidResultForTolerancesAboveDefaultTolerance()
    {
        var max = 5f;
        var evaluator = new AtMostWithToleranceEvaluator<float>(max, null);
        var value = 5f + 1e-5f; // default tolerance is 1e-6 for floats
        var result = evaluator.Evaluate(value);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_Any_ReturnsCorrectEvaluationDetails()
    {
        var max = 5d;
        var tolerance = 0.5;
        var evaluator = new AtMostWithToleranceEvaluator<double>(max, tolerance);
        var value = 5.5d;
        var result = evaluator.Evaluate(value);
        result.Evaluator.ShouldBe("AtMostWithToleranceEvaluator");
        result.EvaluationDetails.ShouldContainKeyAndValue("attemptedValue", value);
        result.EvaluationDetails.ShouldContainKeyAndValue("type", "Double");
        result.EvaluationDetails.ShouldContainKeyAndValue("max", max);
        result.EvaluationDetails.ShouldContainKeyAndValue("tolerance", tolerance);
    }
}
