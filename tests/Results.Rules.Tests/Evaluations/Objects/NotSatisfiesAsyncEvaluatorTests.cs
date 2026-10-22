using Toarnbeike.Results.Rules.Evaluations.Objects;

namespace Toarnbeike.Results.Rules.Tests.Evaluations.Objects;

public class NotSatisfiesAsyncEvaluatorTests
{
    [Test]
    public async Task Evaluate_ValueSatisfiesPredicate_ReturnsInvalidResult()
    {
        var evaluator = new NotSatisfiesAsyncEvaluator<int>(async x => await Task.FromResult(x > 0));
        var result = await evaluator.EvaluateAsync(5);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public async Task Evaluate_ValueDoesNotSatisfyPredicate_ReturnsValidResult()
    {
        var evaluator = new NotSatisfiesAsyncEvaluator<int>(async x => await Task.FromResult(x > 0));
        var result = await evaluator.EvaluateAsync(-5);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public async Task Evaluate_NullValue_ReturnsValidResult()
    {
        var evaluator = new NotSatisfiesAsyncEvaluator<string>(async x => await Task.FromResult(x != null));
        var result = await evaluator.EvaluateAsync(null!);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public async Task Evaluate_Any_ReturnsCorrectEvaluationDetails()
    {
        var evaluator = new NotSatisfiesAsyncEvaluator<string>(async x => await Task.FromResult(x.Contains("test")));
        var value = "example";
        var result = await evaluator.EvaluateAsync(value);
        result.Evaluator.ShouldBe("NotSatisfiesAsyncEvaluator");
        result.EvaluationDetails.ShouldContainKeyAndValue("attemptedValue", value);
        result.EvaluationDetails.ShouldContainKeyAndValue("type", typeof(string).Name);
    }
}
