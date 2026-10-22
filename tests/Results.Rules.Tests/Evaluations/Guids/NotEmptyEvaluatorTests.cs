using Toarnbeike.Results.Rules.Evaluations.Guids;

namespace Toarnbeike.Results.Rules.Tests.Evaluations.Guids;

public class NotEmptyEvaluatorTests
{
    [Test]
    public void Evaluate_ShouldReturnValidResult_ForNewGuid()
    {
        var evaluator = new NotEmptyEvaluator();
        var result = evaluator.Evaluate(Guid.NewGuid());
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ShouldReturnValidResult_ForNonEmptyGuid()
    {
        var evaluator = new NotEmptyEvaluator();
        var result = evaluator.Evaluate(Guid.Parse("12345678-1234-1234-1234-123456789abc"));
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ShouldReturnValidResult_ForGuidV7()
    {
        var evaluator = new NotEmptyEvaluator();
        var result = evaluator.Evaluate(Guid.CreateVersion7());
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ShouldReturnInvalidResult_ForEmptyGuid()
    {
        var evaluator = new NotEmptyEvaluator();
        var result = evaluator.Evaluate(Guid.Empty);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_ShouldReturnInvalidResult_ForDefaultGuid()
    {
        var evaluator = new NotEmptyEvaluator();
#pragma warning disable S4581 // Suppress warning for using default(Guid) for testing purposes
        var result = evaluator.Evaluate(default);
#pragma warning restore S4581
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_ShouldReturnInvalidResult_ForGuidWithAllZeros()
    {
        var evaluator = new NotEmptyEvaluator();
        var result = evaluator.Evaluate(Guid.Parse("00000000-0000-0000-0000-000000000000"));
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_ShouldReturnInvalidResult_ForGuidWithAllOnes()
    {
        var evaluator = new NotEmptyEvaluator();
        var result = evaluator.Evaluate(Guid.AllBitsSet);
        result.IsValid.ShouldBeFalse(); // This is an valid GUID that is explicitly rejected by the NotEmptyEvaluator, as it is not considered a valid non-empty GUID.
    }

    [Test]
    public void Evaluate_ShouldReturnCorrectEvaluationDetails()
    {
        var evaluator = new NotEmptyEvaluator();
        var value = Guid.NewGuid();
        var result = evaluator.Evaluate(value);
        result.Evaluator.ShouldBe("NotEmptyEvaluator");
        result.EvaluationDetails.ShouldContainKeyAndValue("attemptedValue", value);
    }
}
