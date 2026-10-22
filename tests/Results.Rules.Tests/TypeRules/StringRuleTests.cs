using Toarnbeike.Results.Rules.Tests.MessageTemplateConfiguration;
using Toarnbeike.Results.Rules.TypeRules;

namespace Toarnbeike.Results.Rules.Tests.TypeRules;

[ParallelGroup(nameof(TestMessageTemplateGroup))]
public class StringRuleTests : TestMessageTemplateGroup
{
    [Test]
    public void NotEmpty_ShouldReturnTrue_WhenStringIsNotEmpty()
    {
        var rule = StringRules.NotEmpty();
        var result = rule("Hello");
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void NotEmpty_ShouldReturnFalse_WhenStringIsEmpty()
    {
        var rule = StringRules.NotEmpty();
        var result = rule("");
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("NotEmptyEvaluator");
        result.Message.ShouldContain("| Attempted Value: ");
    }

    [Test]
    public void NotWhitespace_ShouldReturnTrue_WhenStringIsNotWhitespace()
    {
        var rule = StringRules.NotWhitespace();
        var result = rule("Hello");
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void NotWhitespace_ShouldReturnFalse_WhenStringIsWhitespace()
    {
        var rule = StringRules.NotWhitespace();
        var result = rule("   ");
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("NotWhitespaceEvaluator");
        result.Message.ShouldContain("| Attempted Value:    ");
    }

    [Test]
    public void MinLength_ShouldReturnTrue_WhenStringLengthIsGreaterThanOrEqualToMinLength()
    {
        var rule = StringRules.MinLength(5);
        var result = rule("Hello");
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void MinLength_ShouldReturnFalse_WhenStringLengthIsLessThanMinLength()
    {
        var rule = StringRules.MinLength(5);
        var result = rule("Hi");
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("AtLeastEvaluator");
        result.Message.ShouldContain("| Attempted Value: 2");
        result.Message.ShouldContain("originalValue: Hi");
        result.Message.ShouldContain("min: 5");
    }

    [Test]
    public void MaxLength_ShouldReturnTrue_WhenStringLengthIsLessThanOrEqualToMaxLength()
    {
        var rule = StringRules.MaxLength(5);
        var result = rule("Hi");
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void MaxLength_ShouldReturnFalse_WhenStringLengthIsGreaterThanMaxLength()
    {
        var rule = StringRules.MaxLength(5);
        var result = rule("Hello, World!");
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("AtMostEvaluator");
        result.Message.ShouldContain("| Attempted Value: 13");
        result.Message.ShouldContain("originalValue: Hello, World!");
        result.Message.ShouldContain("max: 5");
    }

    [Test]
    public void LengthBetween_ShouldReturnTrue_WhenStringLengthIsWithinRange()
    {
        var rule = StringRules.LengthBetween(5, 10);
        var result = rule("Hello");
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void LengthBetween_ShouldReturnFalse_WhenStringLengthIsOutsideRange()
    {
        var rule = StringRules.LengthBetween(5, 10);
        var result = rule("Hi");
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("BetweenEvaluator");
        result.Message.ShouldContain("| Attempted Value: 2");
        result.Message.ShouldContain("originalValue: Hi");
        result.Message.ShouldContain("min: 5");
        result.Message.ShouldContain("max: 10");
    }

    [Test]
    public void IsEmailAddress_ShouldReturnTrue_WhenStringIsValidEmail()
    {
        var rule = StringRules.IsEmailAddress();
        var result = rule("test@example.com");
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void IsEmailAddress_ShouldReturnFalse_WhenStringIsInvalidEmail()
    {
        var rule = StringRules.IsEmailAddress();
        var result = rule("invalid-email");
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("IsEmailAddressEvaluator");
        result.Message.ShouldContain("| Attempted Value: invalid-email");
    }

    [Test]
    public void IsUri_ShouldReturnTrue_WhenStringIsValidUri()
    {
        var rule = StringRules.IsUri();
        var result = rule("https://www.example.com");
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void IsUri_ShouldReturnFalse_WhenStringIsInvalidUri()
    {
        var rule = StringRules.IsUri();
        var result = rule("http://:bad-port/path");
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("IsUriEvaluator");
        result.Message.ShouldContain("| Attempted Value: http://:bad-port/path");
    }

    [Test]
    public void IsUri_ShouldReturnTrue_WhenStringIsRelativeAndRelativeParameterIsProvided()
    {
        var rule = StringRules.IsUri(UriKind.Relative);
        var result = rule("/relative/path");
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Matches__ShouldReturnTrue_WhenStringMatchesPattern()
    {
        var rule = StringRules.Matches(@"^\d{3}-\d{2}-\d{4}$");
        var result = rule("123-45-6789");
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Matches_ShouldReturnFalse_WhenStringDoesNotMatchPattern()
    {
        var rule = StringRules.Matches(@"^\d{3}-\d{2}-\d{4}$");
        var result = rule("invalid-pattern");
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("MatchesEvaluator");
        result.Message.ShouldContain("| Attempted Value: invalid-pattern");
        result.Message.ShouldContain("pattern: ^\\d{3}-\\d{2}-\\d{4}$");
    }

    [Test]
    public void Matches_ShouldReturnFalse_WhenStringDoesNotMatchPattern_WithDescription()
    {
        var rule = StringRules.Matches(@"^\d{3}-\d{2}-\d{4}$", "SSN Pattern");
        var result = rule("invalid-pattern");
        result.IsValid.ShouldBeFalse();
        result.EvaluatorName.ShouldBe("MatchesEvaluator");
        result.Message.ShouldContain("| Attempted Value: invalid-pattern");
        result.Message.ShouldContain("pattern: ^\\d{3}-\\d{2}-\\d{4}$");
        result.Message.ShouldContain("description: SSN Pattern");
    }
}
