using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Rules.Extensions;

namespace Toarnbeike.Results.Validation.Tests.ValidationTargetExtensions;

public class DateTimeOffsetTests
{
    private readonly Result<DateTimeOffset> _result = new DateTimeOffset(2023, 1, 1, 12, 0, 0, TimeSpan.Zero);
    private readonly DateTimeOffset _earlier = new(2022, 1, 1, 10, 0, 0, TimeSpan.Zero);
    private readonly DateTimeOffset _later = new(2023, 1, 1, 14, 0, 0, TimeSpan.Zero);
    private readonly DateTimeOffset _evenLater = new(2023, 1, 1, 16, 0, 0, TimeSpan.Zero);

    [Test]
    public void OnOrAfter_ShouldReturnSuccess_WhenDateIsOnOrAfterMin()
    {
        var validationResult = _result.Validate("DateTimeOffset").OnOrAfter(_earlier);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void OnOrAfter_ShouldReturnFailure_WhenDateIsBeforeMin()
    {
        var validationResult = _result.Validate("DateTimeOffset").OnOrAfter(_later);
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void OnOrAfter_ShouldReturnFailureWithCustomMessage_WhenDateIsBeforeMin()
    {
        var validationResult = _result.Validate("DateTimeOffset").OnOrAfter(_later, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }

    [Test]
    public void OnOrBefore_ShouldReturnSuccess_WhenDateIsOnOrBeforeMax()
    {
        var validationResult = _result.Validate("DateTimeOffset").OnOrBefore(_later);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void OnOrBefore_ShouldReturnFailure_WhenDateIsAfterMax()
    {
        var validationResult = _result.Validate("DateTimeOffset").OnOrBefore(_earlier);
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void OnOrBefore_ShouldReturnFailureWithCustomMessage_WhenDateIsAfterMax()
    {
        var validationResult = _result.Validate("DateTimeOffset").OnOrBefore(_earlier, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }

    [Test]
    public void Between_ShouldReturnSuccess_WhenDateIsBetweenMinAndMax()
    {
        var validationResult = _result.Validate("DateTimeOffset").Between(_earlier, _later);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void Between_ShouldReturnFailure_WhenDateIsBeforeMin()
    {
        var validationResult = _result.Validate("DateTimeOffset").Between(_later, _evenLater);
        _ = validationResult.ShouldBeFailureOfType<ValidationFailure>();
    }

    [Test]
    public void Between_ShouldReturnFailureWithCustomMessage_WhenDateIsBeforeMin()
    {
        var validationResult = _result.Validate("DateTimeOffset").Between(_later, _evenLater, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<ValidationFailure>();
        failure.ValidationMessage.ShouldBe("Custom failure message");
    }
}
