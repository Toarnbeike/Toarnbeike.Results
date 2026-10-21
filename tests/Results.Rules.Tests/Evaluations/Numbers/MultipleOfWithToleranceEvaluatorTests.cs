using Toarnbeike.Results.Rules.Evaluations.Numbers;

namespace Toarnbeike.Rules.Tests.Evaluations.Numbers;

public class MultipleOfWithToleranceEvaluatorTests
{
    [Test]
    public void Evaluate_ValueIsMultipleOfFactor_ReturnsValidResult()
    {
        var evaluator = new MultipleOfWithToleranceEvaluator<float>(3f, 0.1f);
        var result = evaluator.Evaluate(9f);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ValueIsNotMultipleOfFactorButWithinTolerance_ReturnsValidResult()
    {
        var evaluator = new MultipleOfWithToleranceEvaluator<float>(4f, 0.2f);
        var result = evaluator.Evaluate(8.1f); // 8.1 is not a multiple of 4, but within tolerance of 0.2
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ValueIsNotMultipleOfFactorAndOutsideTolerance_ReturnsInvalidResult()
    {
        var evaluator = new MultipleOfWithToleranceEvaluator<decimal>(5m, 0.1m);
        var result = evaluator.Evaluate(12m); // 12 is not a multiple of 5 and outside tolerance of 0.1
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_FactorIsZero_ThrowsArgumentOutOfRangeException()
    {
        var evaluator = new MultipleOfWithToleranceEvaluator<decimal>(0m, 0.1m);
        Should.Throw<ArgumentOutOfRangeException>(() => evaluator.Evaluate(10m));
    }

    [Test]
    public void Evaluate_ToleranceIsNegative_ThrowsArgumentOutOfRangeException()
    {
        var evaluator = new MultipleOfWithToleranceEvaluator<double>(5d, -0.1);
        Should.Throw<ArgumentOutOfRangeException>(() => evaluator.Evaluate(10d));
    }

    [Test]
    public void Evaluate_WithoutTolerance_UsesDefaultTolerance()
    {
        var evaluator = new MultipleOfWithToleranceEvaluator<double>(2, null);
        var result = evaluator.Evaluate(4 + 1e-13); // 4 + 1e-13 is within the default tolerance of 4, which is a multiple of 2
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_WithoutTolerance_UsesDefaultToleranceAndFails()
    {
        var evaluator = new MultipleOfWithToleranceEvaluator<double>(2, null);
        var result = evaluator.Evaluate(4 + 1e-5); // 4 + 1e-5 is outside the default tolerance of 4, which is a multiple of 2
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_Any_ReturnsCorrectEvaluationDetails()
    {
        var factor = 3d;
        var tolerance = 0.5d;
        var evaluator = new MultipleOfWithToleranceEvaluator<double>(factor, tolerance);
        var value = 7.2d;
        var result = evaluator.Evaluate(value);
        result.Evaluator.ShouldBe("MultipleOfWithToleranceEvaluator");
        result.EvaluationDetails.ShouldContainKeyAndValue("attemptedValue", value);
        result.EvaluationDetails.ShouldContainKeyAndValue("type", typeof(double).Name);
        result.EvaluationDetails.ShouldContainKeyAndValue("factor", factor);
        result.EvaluationDetails.ShouldContainKeyAndValue("tolerance", tolerance);
    }
}
