using Toarnbeike.Results.Rules.Evaluations.Objects;

namespace Toarnbeike.Results.Rules.Tests.Evaluations.Objects;

public class SatisfiesEvaluatorTests
{
    [Test]
    public void Evaluate_ValueSatisfiesPredicate_ReturnsValidResult()
    {
        var evaluator = new SatisfiesEvaluator<int>(x => x > 0);
        var result = evaluator.Evaluate(5);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ValueDoesNotSatisfyPredicate_ReturnsInvalidResult()
    {
        var evaluator = new SatisfiesEvaluator<int>(x => x > 0);
        var result = evaluator.Evaluate(-5);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_NullValue_ReturnsInvalidResult()
    {
        var evaluator = new SatisfiesEvaluator<string>(x => x != null);
        var result = evaluator.Evaluate(null!);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_Any_ReturnsCorrectEvaluationDetails()
    {
        var evaluator = new SatisfiesEvaluator<string>(x => x.Contains("test"));
        var value = "example";
        var result = evaluator.Evaluate(value);
        result.Evaluator.ShouldBe("SatisfiesEvaluator");
        result.EvaluationDetails.ShouldContainKeyAndValue("attemptedValue", value);
        result.EvaluationDetails.ShouldContainKeyAndValue("type", typeof(string).Name);
    }
}
