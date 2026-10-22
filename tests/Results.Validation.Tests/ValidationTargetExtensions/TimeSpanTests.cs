using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Rules.Extensions;

namespace Toarnbeike.Results.Validation.Tests.ValidationTargetExtensions;

public class TimeSpanTests
{
    private readonly Result<TimeSpan> _result = TimeSpan.FromHours(1);
    private readonly TimeSpan _smaller = TimeSpan.FromMinutes(30);
    private readonly TimeSpan _greater = TimeSpan.FromHours(2);

    private readonly TimeSpan _tolerance = TimeSpan.FromMinutes(30);


    [Test]
    public void AtLeast_ShouldReturnSuccess_WhenTimeSpanIsGreaterThanOrEqualToMin()
    {
        var validationResult = _result.Validate("TimeSpan").AtLeast(_smaller);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void AtLeast_ShouldReturnFailure_WhenTimeSpanIsLessThanMin()
    {
        var validationResult = _result.Validate("TimeSpan").AtLeast(_greater);
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void AtLeast_ShouldReturnFailureWithCustomMessage_WhenTimeSpanIsLessThanMin()
    {
        var validationResult = _result.Validate("TimeSpan").AtLeast(_greater, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }

    [Test]
    public void AtMost_ShouldReturnSuccess_WhenTimeSpanIsLessThanOrEqualToMax()
    {
        var validationResult = _result.Validate("TimeSpan").AtMost(_greater);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void AtMost_ShouldReturnFailure_WhenTimeSpanIsGreaterThanMax()
    {
        var validationResult = _result.Validate("TimeSpan").AtMost(_smaller);
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void AtMost_ShouldReturnFailureWithCustomMessage_WhenTimeSpanIsGreaterThanMax()
    {
        var validationResult = _result.Validate("TimeSpan").AtMost(_smaller, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }

    [Test]
    public void Between_ShouldReturnSuccess_WhenTimeSpanIsBetweenMinAndMax()
    {
        var validationResult = _result.Validate("TimeSpan").Between(_smaller, _greater);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void Between_ShouldReturnFailure_WhenTimeSpanIsNotBetweenMinAndMax()
    {
        var validationResult = _result.Validate("TimeSpan").Between(_greater, _greater.Add(TimeSpan.FromMinutes(30)));
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void Between_ShouldReturnFailureWithCustomMessage_WhenTimeSpanIsNotBetweenMinAndMax()
    {
        var validationResult = _result.Validate("TimeSpan").Between(_greater, _greater.Add(TimeSpan.FromMinutes(30)), "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }

    [Test]
    public void Around_ShouldReturnSuccess_WhenTimeSpanIsWithinTolerance()
    {
        var validationResult = _result.Validate("TimeSpan").Around(_smaller, _tolerance);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void Around_ShouldReturnFailure_WhenTimeSpanIsOutsideTolererance()
    {
        var validationResult = _result.Validate("TimeSpan").Around(_greater, _tolerance);
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void Around_ShouldReturnFailureWithCustomMessage_WhenTimeSpanIsOutsideTolererance()
    {
        var validationResult = _result.Validate("TimeSpan").Around(_greater, _tolerance, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }
}
