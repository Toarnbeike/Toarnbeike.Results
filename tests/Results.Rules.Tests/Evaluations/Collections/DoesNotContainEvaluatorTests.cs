using Toarnbeike.Results.Rules.Evaluations.Collections;

namespace Toarnbeike.Rules.Tests.Evaluations.Collections;

public class DoesNotContainEvaluatorTests
{
    [Test]
    public void Evaluate_CollectionDoesNotContainValue_ReturnsValidResult()
    {
        var evaluator = new DoesNotContainEvaluator<int>(6);
        var collection = new List<int> { 1, 2, 3, 4, 5 };
        var result = evaluator.Evaluate(collection);
        result.IsValid.ShouldBeTrue();
    }
    [Test]
    public void Evaluate_CollectionContainsValue_ReturnsInvalidResult()
    {
        var evaluator = new DoesNotContainEvaluator<string>("test");
        var collection = new List<string> { "a", "b", "c", "test" };
        var result = evaluator.Evaluate(collection);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_EmptyCollection_ReturnsValidResult()
    {
        var evaluator = new DoesNotContainEvaluator<double>(1.0);
        var collection = new List<double>();
        var result = evaluator.Evaluate(collection);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_CollectionWithNullValue_ThrowsArgumentNullException()
    {
        var evaluator = new DoesNotContainEvaluator<string>(null!);
        var collection = new List<string> { "a", null!, "c" };
        Should.Throw<ArgumentNullException>(() => evaluator.Evaluate(collection));
    }

    [Test]
    public void Evaluate_CollectionWithNullCollection_ReturnsValidResult()
    {
        var evaluator = new DoesNotContainEvaluator<int>(1);
        List<int> collection = null!;
        var result = evaluator.Evaluate(collection);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_CollectionWithDifferentTypes_ReturnsValidResult()
    {
        // Shows that there is casting in DoesNotContainEvaluator, and it will return false if the types don't match.
        var evaluator = new DoesNotContainEvaluator<object>(1);     // check for int with value 1
        var collection = new List<object> { "a", 2.0, true, 1m };   // find decimal with value 1 -- no match.
        var result = evaluator.Evaluate(collection);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_Any_ReturnsCorrectEvaluationDetails()
    {
        var evaluator = new DoesNotContainEvaluator<int>(5);
        var collection = new List<int> { 1, 2, 3 };
        var result = evaluator.Evaluate(collection);
        result.Evaluator.ShouldBe("DoesNotContainEvaluator");
        result.EvaluationDetails.ShouldContainKeyAndValue("attemptedValue", collection);
        result.EvaluationDetails.ShouldContainKeyAndValue("type", typeof(int).Name);
    }
}
