using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Rules.Extensions;

namespace Toarnbeike.Results.Rules.Tests.Extensions;

public class DateTimeTests
{
    private readonly Result<DateTime> _result = new DateTime(2023, 1, 1, 12, 0, 0, DateTimeKind.Utc);
    private readonly DateTime _earlier = new(2022, 1, 1, 10, 0, 0, DateTimeKind.Utc);
    private readonly DateTime _later = new(2023, 1, 1, 14, 0, 0, DateTimeKind.Utc);
    private readonly DateTime _evenLater = new(2023, 1, 1, 16, 0, 0, DateTimeKind.Utc);

    [Test]
    public void OnOrAfter_ShouldReturnSuccess_WhenDateIsOnOrAfterMin()
    {
        var validationResult = _result.TestRuleFor("DateTime").OnOrAfter(_earlier);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void OnOrAfter_ShouldReturnFailure_WhenDateIsBeforeMin()
    {
        var validationResult = _result.TestRuleFor("DateTime").OnOrAfter(_later);
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void OnOrAfter_ShouldReturnFailureWithCustomMessage_WhenDateIsBeforeMin()
    {
        var validationResult = _result.TestRuleFor("DateTime").OnOrAfter(_later, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }

    [Test]
    public void OnOrBefore_ShouldReturnSuccess_WhenDateIsOnOrBeforeMax()
    {
        var validationResult = _result.TestRuleFor("DateTime").OnOrBefore(_later);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void OnOrBefore_ShouldReturnFailure_WhenDateIsAfterMax()
    {
        var validationResult = _result.TestRuleFor("DateTime").OnOrBefore(_earlier);
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void OnOrBefore_ShouldReturnFailureWithCustomMessage_WhenDateIsAfterMax()
    {
        var validationResult = _result.TestRuleFor("DateTime").OnOrBefore(_earlier, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }

    [Test]
    public void Between_ShouldReturnSuccess_WhenDateIsBetweenMinAndMax()
    {
        var validationResult = _result.TestRuleFor("DateTime").Between(_earlier, _later);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void Between_ShouldReturnFailure_WhenDateIsBeforeMin()
    {
        var validationResult = _result.TestRuleFor("DateTime").Between(_later, _evenLater);
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void Between_ShouldReturnFailureWithCustomMessage_WhenDateIsBeforeMin()
    {
        var validationResult = _result.TestRuleFor("DateTime").Between(_later, _evenLater, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }

    [Test]
    public void Future_ShouldReturnSuccess_WhenDateIsInTheFuture()
    {
        Result<DateTime> futureResult = DateTime.UtcNow.AddDays(1);
        var validationResult = futureResult.TestRuleFor("DateTime").Future();
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void Future_ShouldReturnFailure_WhenDateIsInThePast()
    {
        Result<DateTime> pastResult = DateTime.UtcNow.AddDays(-1);
        var validationResult = pastResult.TestRuleFor("DateTime").Future();
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void Future_ShouldReturnFailureWithCustomMessage_WhenDateIsInThePast()
    {
        Result<DateTime> pastResult = DateTime.UtcNow.AddDays(-1);
        var validationResult = pastResult.TestRuleFor("DateTime").Future("Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }

    [Test]
    public void Past_ShouldReturnSuccess_WhenDateIsInThePast()
    {
        Result<DateTime> pastResult = DateTime.UtcNow.AddDays(-1);
        var validationResult = pastResult.TestRuleFor("DateTime").Past();
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void Past_ShouldReturnFailure_WhenDateIsInTheFuture()
    {
        Result<DateTime> futureResult = DateTime.UtcNow.AddDays(1);
        var validationResult = futureResult.TestRuleFor("DateTime").Past();
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void Past_ShouldReturnFailureWithCustomMessage_WhenDateIsInTheFuture()
    {
        Result<DateTime> futureResult = DateTime.UtcNow.AddDays(1);
        var validationResult = futureResult.TestRuleFor("DateTime").Past("Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }
}
