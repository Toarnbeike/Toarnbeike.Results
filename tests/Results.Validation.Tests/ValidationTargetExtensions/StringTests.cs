using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Rules.Extensions;

namespace Toarnbeike.Results.Validation.Tests.ValidationTargetExtensions;

public class StringTests
{
    private readonly Result<string> _result = "Hello";
    private readonly Result<string> _empty = string.Empty;

    [Test]
    public void NotEmpty_ShouldReturnSuccess_WhenStringIsNotEmpty()
    {
        var validationResult = _result.Validate("String").NotEmpty();
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void NotEmpty_ShouldReturnFailure_WhenStringIsEmpty()
    {
        var validationResult = _empty.Validate("String").NotEmpty();
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void NotEmpty_ShouldReturnFailureWithCustomMessage_WhenStringIsEmpty()
    {
        var validationResult = _empty.Validate("String").NotEmpty("Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }

    [Test]
    public void NotWhiteSpace_ShouldReturnSuccess_WhenStringIsNotWhiteSpace()
    {
        var validationResult = _result.Validate("String").NotWhiteSpace();
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void NotWhiteSpace_ShouldReturnFailure_WhenStringIsWhiteSpace()
    {
        var validationResult = _empty.Validate("String").NotWhiteSpace();
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void NotWhiteSpace_ShouldReturnFailureWithCustomMessage_WhenStringIsWhiteSpace()
    {
        var validationResult = _empty.Validate("String").NotWhiteSpace("Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }

    [Test]
    public void MinLength_ShouldReturnSuccess_WhenStringLengthIsGreaterThanOrEqualToMin()
    {
        var validationResult = _result.Validate("String").MinLength(5);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void MinLength_ShouldReturnFailure_WhenStringLengthIsLessThanMin()
    {
        var validationResult = _result.Validate("String").MinLength(10);
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void MinLength_ShouldReturnFailureWithCustomMessage_WhenStringLengthIsLessThanMin()
    {
        var validationResult = _result.Validate("String").MinLength(10, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }

    [Test]
    public void MaxLength_ShouldReturnSuccess_WhenStringLengthIsLessThanOrEqualToMax()
    {
        var validationResult = _result.Validate("String").MaxLength(10);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void MaxLength_ShouldReturnFailure_WhenStringLengthIsGreaterThanMax()
    {
        var validationResult = _result.Validate("String").MaxLength(3);
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void MaxLength_ShouldReturnFailureWithCustomMessage_WhenStringLengthIsGreaterThanMax()
    {
        var validationResult = _result.Validate("String").MaxLength(3, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }

    [Test]
    public void LengthBetween_ShouldReturnSuccess_WhenStringLengthIsBetweenMinAndMax()
    {
        var validationResult = _result.Validate("String").LengthBetween(3, 10);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void LengthBetween_ShouldReturnFailure_WhenStringLengthIsNotBetweenMinAndMax()
    {
        var validationResult = _result.Validate("String").LengthBetween(10, 20);
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void LengthBetween_ShouldReturnFailureWithCustomMessage_WhenStringLengthIsNotBetweenMinAndMax()
    {
        var validationResult = _result.Validate("String").LengthBetween(10, 20, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }

    [Test]
    public void Matches_ShouldReturnSuccess_WhenStringMatchesPattern()
    {
        var validationResult = _result.Validate("String").MatchesRegex(@"^H.*o$");
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void Matches_ShouldReturnFailure_WhenStringDoesNotMatchPattern()
    {
        var validationResult = _result.Validate("String").MatchesRegex(@"^A.*z$");
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void Matches_ShouldReturnFailureWithCustomMessage_WhenStringDoesNotMatchPattern()
    {
        var validationResult = _result.Validate("String").MatchesRegex(@"^A.*z$", null, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }

    [Test]
    public void Matches_ShouldReturnFailureWithDescription_WhenPatternDescriptionIsProvided()
    {
        var validationResult = _result.Validate("String").MatchesRegex(@"^A.*z$", "pattern description");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldContain("pattern description");
    }
}
