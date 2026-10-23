using Toarnbeike.Results.Rules.Evaluations.Objects;

namespace Toarnbeike.Results.Rules.Tests.Evaluations.Objects;

public class NotEqualityEvaluatorTests
{
    [Test]
    public void Evaluate_EqualValues_ReturnsInvalidResult()
    {
        var evaluator = new NotEqualityEvaluator<int>(5);
        var result = evaluator.Evaluate(5);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_NotEqualValues_ReturnsValidResult()
    {
        var evaluator = new NotEqualityEvaluator<int>(5);
        var result = evaluator.Evaluate(10);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_NullValue_ReturnsValidResult()
    {
        var evaluator = new NotEqualityEvaluator<string>("test");
        var result = evaluator.Evaluate(null!);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_Any_ReturnsCorrectEvaluationDetails()
    {
        var evaluator = new NotEqualityEvaluator<string>("test");
        var value = "test";
        var result = evaluator.Evaluate(value);
        result.Evaluator.ShouldBe("NotEqualityEvaluator");
        result.EvaluationDetails.ShouldContainKeyAndValue("expected", "test");
        result.EvaluationDetails.ShouldContainKeyAndValue("attemptedValue", value);
        result.EvaluationDetails.ShouldContainKeyAndValue("type", typeof(string).Name);
    }
}
