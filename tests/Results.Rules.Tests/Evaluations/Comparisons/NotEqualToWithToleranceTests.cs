using Toarnbeike.Results.Rules.Evaluations.Comparisons;

namespace Toarnbeike.Results.Rules.Tests.Evaluations.Comparisons;

public class NotEqualToWithToleranceTests
{
    [Test]
    public void Evaluate_ValueNotEqualToTargetWithTolerance_ReturnsValidResult()
    {
        var evaluator = new NotEqualToWithToleranceEvaluator<int>(5, 1);
        var result = evaluator.Evaluate(7);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ValueEqualToTargetWithTolerance_ReturnsInvalidResult()
    {
        var evaluator = new NotEqualToWithToleranceEvaluator<int>(5, 1);
        var result = evaluator.Evaluate(5);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_ValueWithinToleranceOfTarget_ReturnsInvalidResult()
    {
        var evaluator = new NotEqualToWithToleranceEvaluator<double>(5.0, 0.5);
        var result = evaluator.Evaluate(5.3);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_ZeroTolerance_ReturnsValidResultForDifferentValue()
    {
        var evaluator = new NotEqualToWithToleranceEvaluator<float>(5f, 0f);
        var result = evaluator.Evaluate(6f);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ZeroTolerance_ReturnsInvalidResultForSameValue()
    {
        var evaluator = new NotEqualToWithToleranceEvaluator<float>(5f, 0f);
        var result = evaluator.Evaluate(5f);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_NegativeTolerance_ThrowsArgumentOutOfRangeException()
    {
        var target = 5m;
        var tolerance = -0.5m;
        var evaluator = new NotEqualToWithToleranceEvaluator<decimal>(target, tolerance);
        Should.Throw<ArgumentOutOfRangeException>(() => evaluator.Evaluate(5m));
    }

    [Test]
    public void Evaluate_WithoutToleranceProvided_ReturnsValidResultForValuesOutsideDefaultTolerance()
    {
        var target = 5f;
        var evaluator = new NotEqualToWithToleranceEvaluator<float>(target, null);
        var value = 5f + 1e-5f; // default tolerance is 1e-6 for floats
        var result = evaluator.Evaluate(value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_WithoutToleranceProvided_ReturnsInvalidResultForValuesWithinDefaultTolerance()
    {
        var target = 5f;
        var evaluator = new NotEqualToWithToleranceEvaluator<float>(target, null);
        var value = 5f + 1e-7f; // default tolerance is 1e-6 for floats
        var result = evaluator.Evaluate(value);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_Any_ReturnsCorrectEvaluationDetails()
    {
        var target = 5d;
        var tolerance = 0.5;
        var evaluator = new NotEqualToWithToleranceEvaluator<double>(target, tolerance);
        var value = 5.3d;
        var result = evaluator.Evaluate(value);
        result.Evaluator.ShouldBe("NotEqualToWithToleranceEvaluator");
        result.EvaluationDetails.ShouldContainKeyAndValue("attemptedValue", value);
        result.EvaluationDetails.ShouldContainKeyAndValue("type", "Double");
        result.EvaluationDetails.ShouldContainKeyAndValue("target", target);
        result.EvaluationDetails.ShouldContainKeyAndValue("tolerance", tolerance);
    }
}