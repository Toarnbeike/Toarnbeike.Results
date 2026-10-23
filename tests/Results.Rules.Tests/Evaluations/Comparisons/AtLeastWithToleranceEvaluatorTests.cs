using Toarnbeike.Results.Rules.Evaluations.Comparisons;

namespace Toarnbeike.Results.Rules.Tests.Evaluations.Comparisons;

public class AtLeastWithToleranceEvaluatorTests
{
    [Test]
    public void Evaluate_ValueGreaterThanMinWithTolerance_ReturnsValidResult()
    {
        var evaluator = new AtLeastWithToleranceEvaluator<float>(5f, 0.2f);
        var result = evaluator.Evaluate(6f);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ValueEqualToMinWithTolerance_ReturnsValidResult()
    {
        var evaluator = new AtLeastWithToleranceEvaluator<double>(5d, 0.5);
        var result = evaluator.Evaluate(5.5);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ValueEqualToMaxWithTolerance_ReturnsValidResult()
    {
        var evaluator = new AtLeastWithToleranceEvaluator<float>(5f, 0.2f);
        var result = evaluator.Evaluate(5.0f);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ValueLessThanMinWithTolerance_ReturnsInvalidResult()
    {
        var evaluator = new AtLeastWithToleranceEvaluator<decimal>(5m, 0.4m);
        var result = evaluator.Evaluate(4.5m);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_WithToleranceOfZero_ReturnsValidResultForEqualValue()
    {
        var evaluator = new AtLeastWithToleranceEvaluator<int>(5, 0);
        var result = evaluator.Evaluate(5);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_WithToleranceOfZero_ReturnsInvalidResultForLessValue()
    {
        var evaluator = new AtLeastWithToleranceEvaluator<int>(5, 0);
        var result = evaluator.Evaluate(4);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_WithNegativeTolerance_ThrowsArgumentOutOfRangeException()
    {
        var min = 5d;
        var tolerance = -0.5;
        var evaluator = new AtLeastWithToleranceEvaluator<double>(min, tolerance);
        Should.Throw<ArgumentOutOfRangeException>(() => evaluator.Evaluate(5d));
    }

    [Test]
    public void Evaluate_WithToleranceAndMinBelowMinimumValue_ReturnsValidResult()
    {
        var min = float.MinValue + 1e-7f;
        var tolerance = 0.5f; // without overflow check, this would cause an overflow when calculating min - tolerance
        var evaluator = new AtLeastWithToleranceEvaluator<float>(min, tolerance);
        var value = float.MinValue;
        var result = evaluator.Evaluate(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_WithoutToleranceProvided_ReturnValidResultForTolerancesBelowDefaultTolerance()
    {
        var min = 5f;
        var evaluator = new AtLeastWithToleranceEvaluator<float>(min, null);
        var value = 5f - 1e-7f; // default tolerance is 1e-6 for floats
        var result = evaluator.Evaluate(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_WithoutToleranceProvided_ReturnInvalidResultForTolerancesAboveDefaultTolerance()
    {
        var min = 5f;
        var evaluator = new AtLeastWithToleranceEvaluator<float>(min, null);
        var value = 5f - 1e-5f; // default tolerance is 1e-6 for floats
        var result = evaluator.Evaluate(value);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_Any_ReturnsCorrectEvaluationDetails()
    {
        var min = 5d;
        var tolerance = 0.5;
        var evaluator = new AtLeastWithToleranceEvaluator<double>(min, tolerance);
        var value = 4.5d;
        var result = evaluator.Evaluate(value);
        result.Evaluator.ShouldBe("AtLeastWithToleranceEvaluator");
        result.EvaluationDetails.ShouldContainKeyAndValue("attemptedValue", value);
        result.EvaluationDetails.ShouldContainKeyAndValue("type", "Double");
        result.EvaluationDetails.ShouldContainKeyAndValue("min", min);
        result.EvaluationDetails.ShouldContainKeyAndValue("tolerance", tolerance);
    }
}