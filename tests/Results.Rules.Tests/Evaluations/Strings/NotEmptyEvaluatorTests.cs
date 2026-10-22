using Toarnbeike.Results.Rules.Evaluations.Strings;

namespace Toarnbeike.Results.Rules.Tests.Evaluations.Strings;

public class NotEmptyEvaluatorTests
{
    [Test]
    public void Evaluate_NonEmptyString_ReturnsValidResult()
    {
        var evaluator = new NotEmptyEvaluator();
        var result = evaluator.Evaluate("Hello, World!");
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_WhitespaceString_ReturnsValidResult()
    {
        var evaluator = new NotEmptyEvaluator();
        var result = evaluator.Evaluate("   ");
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_EmptyString_ReturnsInvalidResult()
    {
        var evaluator = new NotEmptyEvaluator();
        var result = evaluator.Evaluate(string.Empty);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_NullString_ReturnsInvalidResult()
    {
        var evaluator = new NotEmptyEvaluator();
        var result = evaluator.Evaluate(null!);
        result.IsValid.ShouldBeFalse();
    }
}
