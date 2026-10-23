using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Rules.Extensions;
using Toarnbeike.Results.Rules.Tests.MessageTemplateConfiguration;

namespace Toarnbeike.Results.Rules.Tests.Extensions;

[ParallelGroup(nameof(TestMessageTemplateGroup))]
public class DateOnlyTests : TestMessageTemplateGroup
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

    [Test]
    public void Future_ShouldReturnSuccess_WhenDateIsInTheFuture()
    {
        Result<DateOnly> futureDate = RuleContext.UtcNow<DateOnly>().AddDays(5);
        var validationResult = futureDate.TestRuleFor("DateOnly").Future();
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void Future_ShouldReturnFailure_WhenDateIsInThePast()
    {
        var validationResult = _result.TestRuleFor("DateOnly").Future();
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void Future_ShouldReturnFailureWithCustomMessage_WhenDateIsInThePast()
    {
        var validationResult = _result.TestRuleFor("DateOnly").Future("Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }

    [Test]
    public void FutureWithin_ShouldReturnSuccess_WhenDateIsWithinFutureDays()
    {
        Result<DateOnly> futureDate = RuleContext.UtcNow<DateOnly>().AddDays(5);
        var validationResult = futureDate.TestRuleFor("DateOnly").WithinFuture(10);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void FutureWithin_ShouldReturnFailure_WhenDateIsNotWithinFutureDays()
    {
        Result<DateOnly> futureDate = RuleContext.UtcNow<DateOnly>().AddDays(15);
        var validationResult = futureDate.TestRuleFor("DateOnly").WithinFuture(10);
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void FutureWithin_ShouldReturnFailureWithCustomMessage_WhenDateIsNotWithinFutureDays()
    {
        Result<DateOnly> futureDate = RuleContext.UtcNow<DateOnly>().AddDays(15);
        var validationResult = futureDate.TestRuleFor("DateOnly").WithinFuture(10, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }

    [Test]
    public void Past_ShouldReturnSuccess_WhenDateIsInThePast()
    {
        var validationResult = _result.TestRuleFor("DateOnly").Past();
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void Past_ShouldReturnFailure_WhenDateIsInTheFuture()
    {
        Result<DateOnly> futureDate = RuleContext.UtcNow<DateOnly>().AddDays(5);
        var validationResult = futureDate.TestRuleFor("DateOnly").Past();
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void Past_ShouldReturnFailureWithCustomMessage_WhenDateIsInTheFuture()
    {
        Result<DateOnly> futureDate = RuleContext.UtcNow<DateOnly>().AddDays(5);
        var validationResult = futureDate.TestRuleFor("DateOnly").Past("Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }

    [Test]
    public void PastWithin_ShouldReturnSuccess_WhenDateIsWithinPastDays()
    {
        Result<DateOnly> pastDate = RuleContext.UtcNow<DateOnly>().AddDays(-5);
        var validationResult = pastDate.TestRuleFor("DateOnly").WithinPast(10);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void PastWithin_ShouldReturnFailure_WhenDateIsNotWithinPastDays()
    {
        Result<DateOnly> pastDate = RuleContext.UtcNow<DateOnly>().AddDays(-15);
        var validationResult = pastDate.TestRuleFor("DateOnly").WithinPast(10);
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void PastWithin_ShouldReturnFailureWithCustomMessage_WhenDateIsNotWithinPastDays()
    {
        Result<DateOnly> pastDate = RuleContext.UtcNow<DateOnly>().AddDays(-15);
        var validationResult = pastDate.TestRuleFor("DateOnly").WithinPast(10, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }
}
