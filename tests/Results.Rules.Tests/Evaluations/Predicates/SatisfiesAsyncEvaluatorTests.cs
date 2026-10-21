using Toarnbeike.Results.Rules.Evaluations.Predicates;

namespace Toarnbeike.Rules.Tests.Evaluations.Predicates;

public class SatisfiesAsyncEvaluatorTests
{
    [Test]
    public async Task Evaluate_ValueSatisfiesPredicate_ReturnsValidResult()
    {
        var evaluator = new SatisfiesAsyncEvaluator<int>(async x => await Task.FromResult(x > 0));
        var result = await evaluator.EvaluateAsync(5);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public async Task Evaluate_ValueDoesNotSatisfyPredicate_ReturnsInvalidResult()
    {
        var evaluator = new SatisfiesAsyncEvaluator<int>(async x => await Task.FromResult(x > 0));
        var result = await evaluator.EvaluateAsync(-5);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public async Task Evaluate_NullValue_ReturnsInvalidResult()
    {
        var evaluator = new SatisfiesAsyncEvaluator<string>(async x => await Task.FromResult(x != null));
        var result = await evaluator.EvaluateAsync(null!);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public async Task Evaluate_Any_ReturnsCorrectEvaluationDetails()
    {
        var evaluator = new SatisfiesAsyncEvaluator<string>(async x => await Task.FromResult(x.Contains("test")));
        var value = "example";
        var result = await evaluator.EvaluateAsync(value);
        result.Evaluator.ShouldBe("SatisfiesAsyncEvaluator");
        result.EvaluationDetails.ShouldContainKeyAndValue("attemptedValue", value);
        result.EvaluationDetails.ShouldContainKeyAndValue("type", typeof(string).Name);
    }
}
