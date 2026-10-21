using Toarnbeike.Results.Rules.Evaluations.Predicates;

namespace Toarnbeike.Rules.Tests.Evaluations.Predicates;

public class NotSatisfiesEvaluatorTests
{
    [Test]
    public void Evaluate_ValueSatisfiesPredicate_ReturnsInvalidResult()
    {
        var evaluator = new NotSatisfiesEvaluator<int>(x => x > 0);
        var result = evaluator.Evaluate(5);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_ValueDoesNotSatisfyPredicate_ReturnsValidResult()
    {
        var evaluator = new NotSatisfiesEvaluator<int>(x => x > 0);
        var result = evaluator.Evaluate(-5);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_NullValue_ReturnsValidResult()
    {
        var evaluator = new NotSatisfiesEvaluator<string>(x => x != null);
        var result = evaluator.Evaluate(null!);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_Any_ReturnsCorrectEvaluationDetails()
    {
        var evaluator = new NotSatisfiesEvaluator<string>(x => x.Contains("test"));
        var value = "example";
        var result = evaluator.Evaluate(value);
        result.Evaluator.ShouldBe("NotSatisfiesEvaluator");
        result.EvaluationDetails.ShouldContainKeyAndValue("attemptedValue", value);
        result.EvaluationDetails.ShouldContainKeyAndValue("type", typeof(string).Name);
    }
}
