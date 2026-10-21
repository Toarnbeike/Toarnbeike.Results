using Toarnbeike.Results.Rules.Evaluations.Strings;

namespace Toarnbeike.Rules.Tests.Evaluations.Strings;

public class IsUriEvaluatorTests
{
    [Test]
    public void Evaluate_ValidUri_ReturnsValidResult()
    {
        var evaluator = new IsUriEvaluator();
        var result = evaluator.Evaluate("https://www.example.com");
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_InvalidUri_ReturnsInvalidResult()
    {
        var evaluator = new IsUriEvaluator();
        var result = evaluator.Evaluate("http://:bad-port/path");
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_Null_ReturnsInvalidResult()
    {
        var evaluator = new IsUriEvaluator();
        var resultNull = evaluator.Evaluate(null!);
        resultNull.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_EmptyString_ReturnsValidResult()
    {
        var evaluator = new IsUriEvaluator();
        var resultEmpty = evaluator.Evaluate(string.Empty);
        resultEmpty.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ValidLocalUri_ReturnsValidResult()
    {
        var evaluator = new IsUriEvaluator();
        var result = evaluator.Evaluate("file:///C:/path/to/file.txt");
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ValidUriWithQueryParameters_ReturnsValidResult()
    {
        var evaluator = new IsUriEvaluator();
        var result = evaluator.Evaluate("https://www.example.com/search?q=test");
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_ValidAbsoluteUri_ReturnsInvalidResult_WhenKindIsRelative()
    {
        var evaluator = new IsUriEvaluator(UriKind.Relative);
        var result = evaluator.Evaluate("https://www.example.com");
        result.IsValid.ShouldBeFalse();
    }
}
