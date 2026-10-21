using Toarnbeike.Results.Rules.Evaluations.Strings;

namespace Toarnbeike.Rules.Tests.Evaluations.Strings;

public class NotWhitespaceEvaluatorTests
{
    [Test]
    public void Evaluate_NonWhitespaceString_ReturnsValidResult()
    {
        var evaluator = new NotWhitespaceEvaluator();
        var result = evaluator.Evaluate("Hello, World!");
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_WhitespaceString_ReturnsInvalidResult()
    {
        var evaluator = new NotWhitespaceEvaluator();
        var result = evaluator.Evaluate("   ");
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_EmptyString_ReturnsInvalidResult()
    {
        var evaluator = new NotWhitespaceEvaluator();
        var result = evaluator.Evaluate(string.Empty);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_NullString_ReturnsInvalidResult()
    {
        var evaluator = new NotWhitespaceEvaluator();
        var result = evaluator.Evaluate(null!);
        result.IsValid.ShouldBeFalse();
    }
}
