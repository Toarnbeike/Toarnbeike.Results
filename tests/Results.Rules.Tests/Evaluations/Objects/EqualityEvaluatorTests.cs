using Toarnbeike.Results.Rules.Evaluations.Objects;

namespace Toarnbeike.Results.Rules.Tests.Evaluations.Objects;

public class EqualityEvaluatorTests
{
    [Test]
    public void Evaluate_EqualValues_ReturnsValidResult()
    {
        var evaluator = new EqualityEvaluator<int>(5);
        var result = evaluator.Evaluate(5);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_NotEqualValues_ReturnsInvalidResult()
    {
        var evaluator = new EqualityEvaluator<int>(5);
        var result = evaluator.Evaluate(10);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_NullValue_ReturnsInvalidResult()
    {
        var evaluator = new EqualityEvaluator<string>("test");
        var result = evaluator.Evaluate(null!);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_Any_ReturnsCorrectEvaluationDetails()
    {
        var evaluator = new EqualityEvaluator<string>("test");
        var value = "example";
        var result = evaluator.Evaluate(value);
        result.Evaluator.ShouldBe("EqualityEvaluator");
        result.EvaluationDetails.ShouldContainKeyAndValue("expected", "test");
        result.EvaluationDetails.ShouldContainKeyAndValue("attemptedValue", value);
        result.EvaluationDetails.ShouldContainKeyAndValue("type", typeof(string).Name);
    }
}
