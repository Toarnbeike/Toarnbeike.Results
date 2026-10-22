using Toarnbeike.Results.Rules.Evaluations.Numbers;

namespace Toarnbeike.Results.Rules.Tests.Evaluations.Numbers;

public class PowerOfTwoEvaluatorTests
{
    [Test]
    public void Evaluate_PowerOfTwo_ReturnsValidResult()
    {
        var evaluator = new PowerOf2Evaluator<int>();
        var result = evaluator.Evaluate(8);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_NotPowerOfTwo_ReturnsInvalidResult()
    {
        var evaluator = new PowerOf2Evaluator<int>();
        var result = evaluator.Evaluate(10);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_Zero_ReturnsInvalidResult()
    {
        var evaluator = new PowerOf2Evaluator<int>();
        var result = evaluator.Evaluate(0);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_NegativeNumber_ReturnsInvalidResult()
    {
        var evaluator = new PowerOf2Evaluator<int>();
        var result = evaluator.Evaluate(-4);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_LongPowerOfTwo_ReturnsValidResult()
    {
        var evaluator = new PowerOf2Evaluator<long>();
        var result = evaluator.Evaluate(1024L);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_Any_ReturnsCorrectEvaluationDetails()
    {
        var evaluator = new PowerOf2Evaluator<int>();
        var value = 10;
        var result = evaluator.Evaluate(value);
        result.Evaluator.ShouldBe("PowerOf2Evaluator");
        result.EvaluationDetails.ShouldContainKeyAndValue("attemptedValue", value);
        result.EvaluationDetails.ShouldContainKeyAndValue("type", typeof(int).Name);
    }
}
