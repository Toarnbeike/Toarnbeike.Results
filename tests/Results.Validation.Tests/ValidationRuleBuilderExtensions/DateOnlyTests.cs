using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Rules;
using Toarnbeike.Results.Validation.Extensions;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Tests.ValidationRuleBuilderExtensions;

public class DateOnlyTests
{
    private readonly DateOnly _date = new(2023, 1, 1);
    private readonly DateOnly _earlier = new(2022, 12, 31);
    private readonly DateOnly _later = new(2023, 1, 2);
    private readonly string _customMessage = "Custom message";
    private Func<DateOnly, IEnumerable<ValidationFailure>> _registered = null!;
    private readonly ValidationRuleBuilder<DateOnly, DateOnly> _builder;

    public DateOnlyTests()
    {
        _builder = new ValidationRuleBuilder<DateOnly, DateOnly>(
            registerRule: rule => _registered = rule,
            executor: (value, rule, message) =>
            {
                var result = rule(value);
                return result.IsValid
                    ? []
                    : [new ValidationFailure(nameof(DateOnly), message ?? result.Message)];
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
        var futureDate = RuleContext.UtcNow<DateOnly>().AddDays(1);
        _builder.Future();
        var failures = _registered(futureDate);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void Future_ShouldReturnFailure_WhenDateIsInPast()
    {
        var pastDate = RuleContext.UtcNow<DateOnly>().AddDays(-1);
        _builder.Future();
        var failures = _registered(pastDate).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void Future_ShouldUseCustomMessage_WhenProvided()
    {
        var pastDate = RuleContext.UtcNow<DateOnly>().AddDays(-1);
        _builder.Future(_customMessage);
        var failure = _registered(pastDate).Single();
        failure.ValidationMessage.ShouldBe(_customMessage);
    }

    [Test]
    public void FutureWithin_ShouldReturnNoFailures_WhenDateIsWithinFutureRange()
    {
        var futureDate = RuleContext.UtcNow<DateOnly>().AddDays(1);
        _builder.WithinFuture(2);
        var failures = _registered(futureDate);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void FutureWithin_ShouldReturnFailure_WhenDateIsOutsideFutureRange()
    {
        var futureDate = RuleContext.UtcNow<DateOnly>().AddDays(3);
        _builder.WithinFuture(2);
        var failures = _registered(futureDate).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void FutureWithin_ShouldUseCustomMessage_WhenProvided()
    {
        var futureDate = RuleContext.UtcNow<DateOnly>().AddDays(3);
        _builder.WithinFuture(2, _customMessage);
        var failure = _registered(futureDate).Single();
        failure.ValidationMessage.ShouldBe(_customMessage);
    }

    [Test]
    public void Past_ShouldReturnNoFailures_WhenDateIsInPast()
    {
        var pastDate = RuleContext.UtcNow<DateOnly>().AddDays(-1);
        _builder.Past();
        var failures = _registered(pastDate);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void Past_ShouldReturnFailure_WhenDateIsInFuture()
    {
        var futureDate = RuleContext.UtcNow<DateOnly>().AddDays(1);
        _builder.Past();
        var failures = _registered(futureDate).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void Past_ShouldUseCustomMessage_WhenProvided()
    {
        var futureDate = RuleContext.UtcNow<DateOnly>().AddDays(1);
        _builder.Past(_customMessage);
        var failure = _registered(futureDate).Single();
        failure.ValidationMessage.ShouldBe(_customMessage);
    }

    [Test]
    public void PastWithin_ShouldReturnNoFailures_WhenDateIsWithinPastRange()
    {
        var pastDate = RuleContext.UtcNow<DateOnly>().AddDays(-1);
        _builder.WithinPast(2);
        var failures = _registered(pastDate);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void PastWithin_ShouldReturnFailure_WhenDateIsOutsidePastRange()
    {
        var pastDate = RuleContext.UtcNow<DateOnly>().AddDays(-3);
        _builder.WithinPast(2);
        var failures = _registered(pastDate).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void PastWithin_ShouldUseCustomMessage_WhenProvided()
    {
        var pastDate = RuleContext.UtcNow<DateOnly>().AddDays(-3);
        _builder.WithinPast(2, _customMessage);
        var failure = _registered(pastDate).Single();
        failure.ValidationMessage.ShouldBe(_customMessage);
    }
}
