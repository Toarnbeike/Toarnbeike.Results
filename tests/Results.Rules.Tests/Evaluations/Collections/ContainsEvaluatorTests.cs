using Toarnbeike.Results.Rules.Evaluations.Collections;

namespace Toarnbeike.Results.Rules.Tests.Evaluations.Collections;

public class ContainsEvaluatorTests
{
    [Test]
    public void Evaluate_CollectionContainsValue_ReturnsValidResult()
    {
        var evaluator = new ContainsEvaluator<int>(3);
        var collection = new List<int> { 1, 2, 3, 4, 5 };
        var result = evaluator.Evaluate(collection);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_CollectionDoesNotContainValue_ReturnsInvalidResult()
    {
        var evaluator = new ContainsEvaluator<string>("test");
        var collection = new List<string> { "a", "b", "c" };
        var result = evaluator.Evaluate(collection);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_EmptyCollection_ReturnsInvalidResult()
    {
        var evaluator = new ContainsEvaluator<double>(1.0);
        var collection = new List<double>();
        var result = evaluator.Evaluate(collection);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_CollectionWithNullValue_ThrowsArgumentNullException()
    {
        var evaluator = new ContainsEvaluator<string>(null!);
        var collection = new List<string> { "a", null!, "c" };
        Should.Throw<ArgumentNullException>(() => evaluator.Evaluate(collection));
    }

    [Test]
    public void Evaluate_CollectionWithNullCollection_ReturnsInvalidResult()
    {
        var evaluator = new ContainsEvaluator<int>(1);
        List<int> collection = null!;
        var result = evaluator.Evaluate(collection);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_CollectionWithDifferentTypes_ReturnsInvalidResult()
    {
        // Shows that there is casting in ContainsEvaluator, and it will return false if the types don't match.
        var evaluator = new ContainsEvaluator<object>(1);               // check for int with value 1
        var collection = new List<object> { "a", 2.0, true, 1m };       // has decimal with value 1 -- no match.
        var result = evaluator.Evaluate(collection);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_Any_ReturnsCorrectEvaluationDetails()
    {
        var evaluator = new ContainsEvaluator<int>(5);
        var collection = new List<int> { 1, 2, 3 };
        var result = evaluator.Evaluate(collection);
        result.Evaluator.ShouldBe("ContainsEvaluator");
        result.EvaluationDetails.ShouldContainKeyAndValue("attemptedValue", collection);
        result.EvaluationDetails.ShouldContainKeyAndValue("type", typeof(int).Name);
    }
}