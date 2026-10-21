using Toarnbeike.Results.Rules.Evaluations.Numbers;

namespace Toarnbeike.Rules.Tests.Evaluations.Numbers;

public class FiniteEvaluatorTests
{
    [Test]
    public void Evaluate_FiniteNumber_ReturnsValidResult()
    {
        var evaluator = new FiniteEvaluator<double>();
        var result = evaluator.Evaluate(3.14);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_NaN_ReturnsInvalidResult()
    {
        var evaluator = new FiniteEvaluator<double>();
        var result = evaluator.Evaluate(double.NaN);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_PositiveInfinity_ReturnsInvalidResult()
    {
        var evaluator = new FiniteEvaluator<float>();
        var result = evaluator.Evaluate(float.PositiveInfinity);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_NegativeInfinity_ReturnsInvalidResult()
    {
        var evaluator = new FiniteEvaluator<double>();
        var result = evaluator.Evaluate(double.NegativeInfinity);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_MaxValue_ReturnsValidResult()
    {
        var evaluator = new FiniteEvaluator<double>();
        var result = evaluator.Evaluate(double.MaxValue);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_MinValue_ReturnsValidResult()
    {
        var evaluator = new FiniteEvaluator<decimal>();
        var result = evaluator.Evaluate(decimal.MinValue);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_Zero_ReturnsValidResult()
    {
        var evaluator = new FiniteEvaluator<double>();
        var result = evaluator.Evaluate(0.0);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_Any_ReturnsCorrectEvaluationDetails()
    {
        var evaluator = new FiniteEvaluator<double>();
        var value = double.NaN;
        var result = evaluator.Evaluate(value);
        result.Evaluator.ShouldBe("FiniteEvaluator");
        result.EvaluationDetails.ShouldContainKeyAndValue("attemptedValue", value);
        result.EvaluationDetails.ShouldContainKeyAndValue("type", "Double");
    }
}
