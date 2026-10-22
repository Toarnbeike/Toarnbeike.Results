using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Rules.Extensions;

namespace Toarnbeike.Results.Validation.Tests.ValidationTargetExtensions;

public class IntegerTests
{
    private readonly Result<int> _result = 5;
    private readonly int _min = 3;
    private readonly int _max = 10;
    private readonly int _evenBigger = 15;

    [Test]
    public void AtLeast_ShouldReturnSuccess_WhenValueIsGreaterThanOrEqualToMin()
    {
        var validationResult = _result.Validate("Integer").AtLeast(_min);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void AtLeast_ShouldReturnFailure_WhenValueIsLessThanMin()
    {
        var validationResult = _result.Validate("Integer").AtLeast(_max);
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void AtLeast_ShouldReturnFailureWithCustomMessage_WhenValueIsLessThanMin()
    {
        var validationResult = _result.Validate("Integer").AtLeast(_max, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }

    [Test]
    public void AtMost_ShouldReturnSuccess_WhenValueIsLessThanOrEqualToMax()
    {
        var validationResult = _result.Validate("Integer").AtMost(_max);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void AtMost_ShouldReturnFailure_WhenValueIsGreaterThanMax()
    {
        var validationResult = _result.Validate("Integer").AtMost(_min);
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void AtMost_ShouldReturnFailureWithCustomMessage_WhenValueIsGreaterThanMax()
    {
        var validationResult = _result.Validate("Integer").AtMost(_min, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }

    [Test]
    public void Between_ShouldReturnSuccess_WhenValueIsBetweenMinAndMax()
    {
        var validationResult = _result.Validate("Integer").Between(_min, _max);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void Between_ShouldReturnFailure_WhenValueIsLessThanMin()
    {
        var validationResult = _result.Validate("Integer").Between(_max, _evenBigger);
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void Between_ShouldReturnFailureWithCustomMessage_WhenValueIsLessThanMin()
    {
        var validationResult = _result.Validate("Integer").Between(_max, _evenBigger, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }

    [Test]
    public void MultipleOf_ShouldReturnSuccess_WhenValueIsMultipleOfFactor()
    {
        var validationResult = _result.Validate("Integer").MultipleOf(5);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void MultipleOf_ShouldReturnFailure_WhenValueIsNotMultipleOfFactor()
    {
        var validationResult = _result.Validate("Integer").MultipleOf(3);
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void MultipleOf_ShouldReturnFailureWithCustomMessage_WhenValueIsNotMultipleOfFactor()
    {
        var validationResult = _result.Validate("Integer").MultipleOf(3, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }
}
