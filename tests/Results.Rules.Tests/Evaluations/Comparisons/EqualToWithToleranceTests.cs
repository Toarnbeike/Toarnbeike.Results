using Toarnbeike.Results.Rules.Evaluations.Comparisons;

namespace Toarnbeike.Results.Rules.Tests.Evaluations.Comparisons;

public class EqualToWithToleranceTests
{
    [Test]
    public void Evaluate_ValueEqualToMinWithTolerance_ReturnsValidResult()
    {
        var evaluator = new EqualToWithToleranceEvaluator<double>(5d, 0.5);
        var result = evaluator.Evaluate(4.5);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ValueEqualToMaxWithTolerance_ReturnsValidResult()
    {
        var evaluator = new EqualToWithToleranceEvaluator<float>(5f, 0.2f);
        var result = evaluator.Evaluate(5.2f);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ValueLessThanMinWithTolerance_ReturnsInvalidResult()
    {
        var evaluator = new EqualToWithToleranceEvaluator<decimal>(5m, 0.4m);
        var result = evaluator.Evaluate(4.5m);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_ValueGreaterThanMaxWithTolerance_ReturnsInvalidResult()
    {
        var evaluator = new EqualToWithToleranceEvaluator<decimal>(5m, 0.4m);
        var result = evaluator.Evaluate(5.5m);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_WithToleranceOfZero_ReturnsValidResultForEqualValue()
    {
        var evaluator = new EqualToWithToleranceEvaluator<int>(5, 0);
        var result = evaluator.Evaluate(5);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_WithToleranceOfZero_ReturnsInvalidResultForDifferentValue()
    {
        var evaluator = new EqualToWithToleranceEvaluator<int>(5, 0);
        var result = evaluator.Evaluate(4);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_WithNegativeTolerance_ThrowsArgumentOutOfRangeException()
    {
        var target = 5d;
        var tolerance = -0.5;
        var evaluator = new EqualToWithToleranceEvaluator<double>(target, tolerance);
        Should.Throw<ArgumentOutOfRangeException>(() => evaluator.Evaluate(5d));
    }

    [Test]
    public void Evaluate_WithoutToleranceProvided_ReturnValidResultForTolerancesBelowDefaultTolerance()
    {
        var target = 5f;
        var evaluator = new EqualToWithToleranceEvaluator<float>(target, null);
        var value = 5f - 1e-7f; // default tolerance is 1e-6 for floats
        var result = evaluator.Evaluate(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_WithoutToleranceProvided_ReturnInvalidResultForTolerancesAboveDefaultTolerance()
    {
        var target = 5f;
        var evaluator = new EqualToWithToleranceEvaluator<float>(target, null);
        var value = 5f - 1e-5f; // default tolerance is 1e-6 for floats
        var result = evaluator.Evaluate(value);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_Any_ReturnsCorrectEvaluationDetails()
    {
        var target = 5d;
        var tolerance = 0.5;
        var evaluator = new EqualToWithToleranceEvaluator<double>(target, tolerance);
        var value = 4.5d;
        var result = evaluator.Evaluate(value);
        result.Evaluator.ShouldBe("EqualToWithToleranceEvaluator");
        result.EvaluationDetails.ShouldContainKeyAndValue("attemptedValue", value);
        result.EvaluationDetails.ShouldContainKeyAndValue("type", "Double");
        result.EvaluationDetails.ShouldContainKeyAndValue("target", target);
        result.EvaluationDetails.ShouldContainKeyAndValue("tolerance", tolerance);
    }
}
