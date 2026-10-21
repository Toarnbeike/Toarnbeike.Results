using Toarnbeike.Results.Rules.Evaluations.Strings;

namespace Toarnbeike.Rules.Tests.Evaluations.Strings;

public class MatchesEvaluatorTests
{
    [Test]
    public void Evaluate_ValidRegexMatch_ReturnsValidResult()
    {
        var evaluator = new MatchesEvaluator(@"^\d{3}-\d{2}-\d{4}$");
        var result = evaluator.Evaluate("123-45-6789");
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_InvalidRegexMatch_ReturnsInvalidResult()
    {
        var evaluator = new MatchesEvaluator(@"^\d{3}-\d{2}-\d{4}$");
        var result = evaluator.Evaluate("invalid-ssn");
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_EmptyStringRegexMatch_ReturnsInvalidResult()
    {
        var evaluator = new MatchesEvaluator(@"^\d{3}-\d{2}-\d{4}$");
        var result = evaluator.Evaluate(string.Empty);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_RegexWithSpecialCharacters_ReturnsValidResult()
    {
        var evaluator = new MatchesEvaluator(@"^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$");
        var result = evaluator.Evaluate("test@example.com");
        result.IsValid.ShouldBeTrue();
    }
}
