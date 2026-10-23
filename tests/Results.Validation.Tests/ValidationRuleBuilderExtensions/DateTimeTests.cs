using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Rules;
using Toarnbeike.Results.Validation.Extensions;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Tests.ValidationRuleBuilderExtensions;

public class DateTimeTests
{
    private readonly DateTime _date = new(2023, 1, 1, 12, 0, 0, DateTimeKind.Local);
    private readonly DateTime _earlier = new(2022, 1, 1, 10, 0, 0, DateTimeKind.Local);
    private readonly DateTime _later = new(2023, 1, 1, 14, 0, 0, DateTimeKind.Local);
    private readonly string _customMessage = "Custom message";
    private Func<DateTime, IEnumerable<ValidationFailure>> _registered = null!;
    private readonly ValidationRuleBuilder<DateTime, DateTime> _builder;

    public DateTimeTests()
    {
        _builder = new ValidationRuleBuilder<DateTime, DateTime>(
            registerRule: rule => _registered = rule,
            executor: (value, rule, message) =>
            {
                var result = rule(value);
                return result.IsValid
                    ? []
                    : [new ValidationFailure(nameof(DateTime), message ?? result.Message)];
            });
    }

    [Test]
    public void OnOrAfter_ShouldReturnNoFailures_WhenDateIsOnOrAfterMin()
    {
        _builder.OnOrAfter(_earlier);
        var failures = _registered(_date);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void OnOrAfter_ShouldReturnFailure_WhenDateIsBeforeMin()
    {
        _builder.OnOrAfter(_later);
        var failures = _registered(_date).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void OnOrAfter_ShouldUseCustomMessage_WhenProvided()
    {
        _builder.OnOrAfter(_later, _customMessage);
        var failure = _registered(_date).Single();
        failure.ValidationMessage.ShouldBe(_customMessage);
    }

    [Test]
    public void OnOrBefore_ShouldReturnNoFailures_WhenDateIsOnOrBeforeMax()
    {
        _builder.OnOrBefore(_later);
        var failures = _registered(_date);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void OnOrBefore_ShouldReturnFailure_WhenDateIsAfterMax()
    {
        _builder.OnOrBefore(_earlier);
        var failures = _registered(_date).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void OnOrBefore_ShouldUseCustomMessage_WhenProvided()
    {
        _builder.OnOrBefore(_earlier, _customMessage);
        var failure = _registered(_date).Single();
        failure.ValidationMessage.ShouldBe(_customMessage);
    }

    [Test]
    public void Between_ShouldReturnNoFailures_WhenDateIsWithinRange()
    {
        _builder.Between(_earlier, _later);
        var failures = _registered(_date);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void Between_ShouldReturnFailure_WhenDateIsOutsideRange()
    {
        _builder.Between(_earlier, _later);
        var failuresBefore = _registered(_earlier.AddDays(-1)).ToArray();
        var failuresAfter = _registered(_later.AddDays(1)).ToArray();
        failuresBefore.Length.ShouldBe(1);
        failuresAfter.Length.ShouldBe(1);
    }

    [Test]
    public void Between_ShouldUseCustomMessage_WhenProvided()
    {
        _builder.Between(_earlier, _later, _customMessage);
        var failure = _registered(_earlier.AddDays(-1)).Single();
        failure.ValidationMessage.ShouldBe(_customMessage);
    }

    [Test]
    public void Future_ShouldReturnNoFailures_WhenDateIsInFuture()
    {
        var futureDate = RuleContext.UtcNow<DateTime>().AddDays(1);
        _builder.Future();
        var failures = _registered(futureDate);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void Future_ShouldReturnFailure_WhenDateIsInPast()
    {
        var pastDate = RuleContext.UtcNow<DateTime>().AddDays(-1);
        _builder.Future();
        var failures = _registered(pastDate).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void Future_ShouldUseCustomMessage_WhenProvided()
    {
        var pastDate = RuleContext.UtcNow<DateTime>().AddDays(-1);
        _builder.Future(_customMessage);
        var failure = _registered(pastDate).Single();
        failure.ValidationMessage.ShouldBe(_customMessage);
    }

    [Test]
    public void Past_ShouldReturnNoFailures_WhenDateIsInPast()
    {
        var pastDate = RuleContext.UtcNow<DateTime>().AddDays(-1);
        _builder.Past();
        var failures = _registered(pastDate);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void Past_ShouldReturnFailure_WhenDateIsInFuture()
    {
        var futureDate = RuleContext.UtcNow<DateTime>().AddDays(1);
        _builder.Past();
        var failures = _registered(futureDate).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void Past_ShouldUseCustomMessage_WhenProvided()
    {
        var futureDate = RuleContext.UtcNow<DateTime>().AddDays(1);
        _builder.Past(_customMessage);
        var failure = _registered(futureDate).Single();
        failure.ValidationMessage.ShouldBe(_customMessage);
    }
}
