using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Rules.Extensions;

namespace Toarnbeike.Results.Validation.Tests.ValidationTargetExtensions;

public class FloatingPointTests
{
    private readonly Result<double> _result = 10d;
    private readonly double _min = 5d;
    private readonly double _max = 15d;
    private readonly double _evenBigger = 20d;
    private readonly double _tolerance = 6d;

    [Test]
    public void AtLeast_ShouldReturnSuccess_WhenValueIsAtLeastMin()
    {
        var validationResult = _result.Validate("Double").AtLeast(_min);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void AtLeast_ShouldReturnFailure_WhenValueIsLessThanMin()
    {
        var validationResult = _result.Validate("Double").AtLeast(_evenBigger);
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void AtLeast_ShouldReturnFailureWithCustomMessage_WhenValueIsLessThanMin()
    {
        var validationResult = _result.Validate("Double").AtLeast(_evenBigger, message: "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }

    [Test]
    public void AtLeast_ShouldReturnSuccess_WhenValueIsAtLeastMinWithTolerance()
    {
        var validationResult = _result.Validate("Double").AtLeast(_min, _tolerance);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void AtMost_ShouldReturnSuccess_WhenValueIsAtMostMax()
    {
        var validationResult = _result.Validate("Double").AtMost(_max);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void AtMost_ShouldReturnFailure_WhenValueIsGreaterThanMax()
    {
        var validationResult = _result.Validate("Double").AtMost(_min);
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void AtMost_ShouldReturnFailureWithCustomMessage_WhenValueIsGreaterThanMax()
    {
        var validationResult = _result.Validate("Double").AtMost(_min, message: "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }

    [Test]
    public void AtMost_ShouldReturnSuccess_WhenValueIsAtMostMaxWithTolerance()
    {
        var validationResult = _result.Validate("Double").AtMost(_max, _tolerance);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void Between_ShouldReturnSuccess_WhenValueIsBetweenMinAndMax()
    {
        var validationResult = _result.Validate("Double").Between(_min, _max);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void Between_ShouldReturnFailure_WhenValueIsLessThanMin()
    {
        var validationResult = _result.Validate("Double").Between(_max, _evenBigger);
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void Between_ShouldReturnFailureWithCustomMessage_WhenValueIsLessThanMin()
    {
        var validationResult = _result.Validate("Double").Between(_max, _evenBigger, message: "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }

    [Test]
    public void Between_ShouldReturnSuccess_WhenValueIsWithinTolerance()
    {
        var validationResult = _result.Validate("Double").Between(_max, _evenBigger, _tolerance);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void MultipleOf_ShouldReturnSuccess_WhenValueIsMultipleOfFactor()
    {
        var validationResult = _result.Validate("Double").MultipleOf(2d);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void MultipleOf_ShouldReturnFailure_WhenValueIsNotMultipleOfFactor()
    {
        var validationResult = _result.Validate("Double").MultipleOf(3d);
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void MultipleOf_ShouldReturnFailureWithCustomMessage_WhenValueIsNotMultipleOfFactor()
    {
        var validationResult = _result.Validate("Double").MultipleOf(3d, message: "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }

    [Test]
    public void Finite_ShouldReturnSuccess_WhenValueIsFinite()
    {
        var validationResult = _result.Validate("Double").Finite();
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void Finite_ShouldReturnFailure_WhenValueIsNotFinite()
    {
        Result<double> result = double.PositiveInfinity;
        var validationResult = result.Validate("Double").Finite();
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void Finite_ShouldReturnFailureWithCustomMessage_WhenValueIsNotFinite()
    {
        Result<double> result = double.PositiveInfinity;
        var validationResult = result.Validate("Double").Finite(message: "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }
}
