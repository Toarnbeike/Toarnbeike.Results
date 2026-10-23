using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Rules.Extensions;
using Toarnbeike.Results.Rules.Tests.MessageTemplateConfiguration;

namespace Toarnbeike.Results.Rules.Tests.Extensions;

[ParallelGroup(nameof(TestMessageTemplateGroup))]
public class DateTimeOffsetTests : TestMessageTemplateGroup
{
    private readonly Result<DateTimeOffset> _result = new DateTimeOffset(2023, 1, 1, 12, 0, 0, TimeSpan.Zero);
    private readonly DateTimeOffset _earlier = new(2022, 1, 1, 10, 0, 0, TimeSpan.Zero);
    private readonly DateTimeOffset _later = new(2023, 1, 1, 14, 0, 0, TimeSpan.Zero);
    private readonly DateTimeOffset _evenLater = new(2023, 1, 1, 16, 0, 0, TimeSpan.Zero);

    [Test]
    public void OnOrAfter_ShouldReturnSuccess_WhenDateIsOnOrAfterMin()
    {
        var validationResult = _result.TestRuleFor("DateTimeOffset").OnOrAfter(_earlier);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void OnOrAfter_ShouldReturnFailure_WhenDateIsBeforeMin()
    {
        var validationResult = _result.TestRuleFor("DateTimeOffset").OnOrAfter(_later);
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void OnOrAfter_ShouldReturnFailureWithCustomMessage_WhenDateIsBeforeMin()
    {
        var validationResult = _result.TestRuleFor("DateTimeOffset").OnOrAfter(_later, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }

    [Test]
    public void OnOrBefore_ShouldReturnSuccess_WhenDateIsOnOrBeforeMax()
    {
        var validationResult = _result.TestRuleFor("DateTimeOffset").OnOrBefore(_later);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void OnOrBefore_ShouldReturnFailure_WhenDateIsAfterMax()
    {
        var validationResult = _result.TestRuleFor("DateTimeOffset").OnOrBefore(_earlier);
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void OnOrBefore_ShouldReturnFailureWithCustomMessage_WhenDateIsAfterMax()
    {
        var validationResult = _result.TestRuleFor("DateTimeOffset").OnOrBefore(_earlier, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }

    [Test]
    public void Between_ShouldReturnSuccess_WhenDateIsBetweenMinAndMax()
    {
        var validationResult = _result.TestRuleFor("DateTimeOffset").Between(_earlier, _later);
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void Between_ShouldReturnFailure_WhenDateIsBeforeMin()
    {
        var validationResult = _result.TestRuleFor("DateTimeOffset").Between(_later, _evenLater);
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void Between_ShouldReturnFailureWithCustomMessage_WhenDateIsBeforeMin()
    {
        var validationResult = _result.TestRuleFor("DateTimeOffset").Between(_later, _evenLater, "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }

    [Test]
    public void Future_ShouldReturnSuccess_WhenDateIsInTheFuture()
    {
        Result<DateTimeOffset> futureDate = RuleContext.UtcNow<DateTimeOffset>().AddDays(1);
        var validationResult = futureDate.TestRuleFor("DateTimeOffset").Future();
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void Future_ShouldReturnFailure_WhenDateIsInThePast()
    {
        Result<DateTimeOffset> pastDate = RuleContext.UtcNow<DateTimeOffset>().AddDays(-1);
        var validationResult = pastDate.TestRuleFor("DateTimeOffset").Future();
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void Future_ShouldReturnFailureWithCustomMessage_WhenDateIsInThePast()
    {
        Result<DateTimeOffset> pastDate = RuleContext.UtcNow<DateTimeOffset>().AddDays(-1);
        var validationResult = pastDate.TestRuleFor("DateTimeOffset").Future("Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }

    [Test]
    public void FutureWithin_ShouldReturnSuccess_WhenDateIsWithinFutureTimeSpan()
    {
        Result<DateTimeOffset> futureDate = RuleContext.UtcNow<DateTimeOffset>().AddDays(1);
        var validationResult = futureDate.TestRuleFor("DateTimeOffset").WithinFuture(TimeSpan.FromDays(2));
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void FutureWithin_ShouldReturnFailure_WhenDateIsOutsideFutureTimeSpan()
    {
        Result<DateTimeOffset> futureDate = RuleContext.UtcNow<DateTimeOffset>().AddDays(3);
        var validationResult = futureDate.TestRuleFor("DateTimeOffset").WithinFuture(TimeSpan.FromDays(2));
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void FutureWithin_ShouldReturnFailureWithCustomMessage_WhenDateIsOutsideFutureTimeSpan()
    {
        Result<DateTimeOffset> futureDate = RuleContext.UtcNow<DateTimeOffset>().AddDays(3);
        var validationResult = futureDate.TestRuleFor("DateTimeOffset").WithinFuture(TimeSpan.FromDays(2), "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }

    [Test]
    public void Past_ShouldReturnSuccess_WhenDateIsInThePast()
    {
        Result<DateTimeOffset> pastDate = RuleContext.UtcNow<DateTimeOffset>().AddDays(-1);
        var validationResult = pastDate.TestRuleFor("DateTimeOffset").Past();
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void Past_ShouldReturnFailure_WhenDateIsInTheFuture()
    {
        Result<DateTimeOffset> futureDate = RuleContext.UtcNow<DateTimeOffset>().AddDays(1);
        var validationResult = futureDate.TestRuleFor("DateTimeOffset").Past();
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void Past_ShouldReturnFailureWithCustomMessage_WhenDateIsInTheFuture()
    {
        Result<DateTimeOffset> futureDate = RuleContext.UtcNow<DateTimeOffset>().AddDays(1);
        var validationResult = futureDate.TestRuleFor("DateTimeOffset").Past("Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }

    [Test]
    public void PastWithin_ShouldReturnSuccess_WhenDateIsWithinPastTimeSpan()
    {
        Result<DateTimeOffset> pastDate = RuleContext.UtcNow<DateTimeOffset>().AddDays(-1);
        var validationResult = pastDate.TestRuleFor("DateTimeOffset").WithinPast(TimeSpan.FromDays(2));
        validationResult.ShouldBeSuccess();
    }

    [Test]
    public void PastWithin_ShouldReturnFailure_WhenDateIsOutsidePastTimeSpan()
    {
        Result<DateTimeOffset> pastDate = RuleContext.UtcNow<DateTimeOffset>().AddDays(-3);
        var validationResult = pastDate.TestRuleFor("DateTimeOffset").WithinPast(TimeSpan.FromDays(2));
        _ = validationResult.ShouldBeFailureOfType<TestFailure>();
    }

    [Test]
    public void PastWithin_ShouldReturnFailureWithCustomMessage_WhenDateIsOutsidePastTimeSpan()
    {
        Result<DateTimeOffset> pastDate = RuleContext.UtcNow<DateTimeOffset>().AddDays(-3);
        var validationResult = pastDate.TestRuleFor("DateTimeOffset").WithinPast(TimeSpan.FromDays(2), "Custom failure message");
        var failure = validationResult.ShouldBeFailureOfType<TestFailure>();
        failure.Identifier.ShouldBe("Custom failure message");
    }
}
