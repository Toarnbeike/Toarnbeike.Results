using System.Net.Mail;
using Toarnbeike.Results.Rules.Evaluations.Strings;

namespace Toarnbeike.Results.Rules.Tests.Evaluations.Strings;

public class IsEmailAddressEvaluatorTests
{
    [Test]
    public void Evaluate_ValidEmailAddress_ReturnsValidResult()
    {
        var evaluator = new IsEmailAddressEvaluator();
        var result = evaluator.Evaluate("test@example.com");
        result.IsValid.ShouldBeTrue();
        result.EvaluationDetails.ShouldContainKey("formatted");
        var formatted = result.EvaluationDetails["formatted"].ShouldBeOfType<MailAddress>();
        formatted.Address.ShouldBe("test@example.com");
    }

    [Test]
    public void Evaluate_InvalidEmailAddress_ReturnsInvalidResult()
    {
        var evaluator = new IsEmailAddressEvaluator();
        var result = evaluator.Evaluate("invalid-email");
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_Null_ReturnsInvalidResult()
    {
        var evaluator = new IsEmailAddressEvaluator();
        var resultNull = evaluator.Evaluate(null!);
        resultNull.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_EmptyString_ReturnsInvalidResult()
    {
        var evaluator = new IsEmailAddressEvaluator();
        var resultEmpty = evaluator.Evaluate(string.Empty);
        resultEmpty.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Evaluate_TaggedEmailAddress_ReturnsValidResult()
    {
        var evaluator = new IsEmailAddressEvaluator();
        var result = evaluator.Evaluate("user+tag@example.com");
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_SpecialCharactersInLocalPart_ReturnsValidResult()
    {
        var evaluator = new IsEmailAddressEvaluator();
        var result = evaluator.Evaluate("test!#$%&'*+-/=?^_{|}~@example.com");
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_EmailWithSubdomain_ReturnsValidResult()
    {
        var evaluator = new IsEmailAddressEvaluator();
        var result = evaluator.Evaluate("user@example.subdomain.com");
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evaluate_EmailWithQuotedLocalPart_ReturnsValidResult()
    {
        var evaluator = new IsEmailAddressEvaluator();
        var result = evaluator.Evaluate("\"quoted.local.part\"@example.com");
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Evalute_EmailWithIPDomain_ReturnsValidResult()
    {
        var evaluator = new IsEmailAddressEvaluator();
        var result = evaluator.Evaluate("user@[192.168.1.1]");
        result.IsValid.ShouldBeTrue();
    }
}
