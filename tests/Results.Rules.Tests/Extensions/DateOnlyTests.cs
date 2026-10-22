using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Rules.Extensions;

namespace Toarnbeike.Results.Rules.Tests.Extensions;

public class DateOnlyTests
{
    private readonly Result<DateOnly> _result = new DateOnly(2023, 1, 1);
    private readonly DateOnly _earlier = new(2022, 12, 31);
    private readonly DateOnly _later = new(2023, 1, 2);
    private readonly DateOnly _evenLater = new(2023, 1, 3);

    [Test]
    public void OnOrAfter_ShouldReturnSuccess_WhenDateIsOnOrAfterMin()
    {
        var validationResult = _result.TestRuleFor("DateOnly").OnOrAfter(_earlier);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void OnOrAfter_ShouldReturnFailure_WhenDateIsBeforeMin()
    {
        var validationResult = _result.TestRuleFor("DateOnly").OnOrAfter(_later);
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void OnOrAfter_ShouldReturnFailureWithCustomMessage_WhenDateIsBeforeMin()
    {
        var validationResult = _result.TestRuleFor("DateOnly").OnOrAfter(_later, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }

    [Test]
    public void OnOrBefore_ShouldReturnSuccess_WhenDateIsOnOrBeforeMax()
    {
        var validationResult = _result.TestRuleFor("DateOnly").OnOrBefore(_later);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void OnOrBefore_ShouldReturnFailure_WhenDateIsAfterMax()
    {
        var validationResult = _result.TestRuleFor("DateOnly").OnOrBefore(_earlier);
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void OnOrBefore_ShouldReturnFailureWithCustomMessage_WhenDateIsAfterMax()
    {
        var validationResult = _result.TestRuleFor("DateOnly").OnOrBefore(_earlier, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }

    [Test]
    public void Between_ShouldReturnSuccess_WhenDateIsBetweenMinAndMax()
    {
        var validationResult = _result.TestRuleFor("DateOnly").Between(_earlier, _later);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void Between_ShouldReturnFailure_WhenDateIsBeforeMin()
    {
        var validationResult = _result.TestRuleFor("DateOnly").Between(_later, _evenLater);
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void Between_ShouldReturnFailureWithCustomMessage_WhenDateIsBeforeMin()
    {
        var validationResult = _result.TestRuleFor("DateOnly").Between(_later, _evenLater, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }
}
