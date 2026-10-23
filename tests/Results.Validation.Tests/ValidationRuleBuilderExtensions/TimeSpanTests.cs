using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Extensions;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Tests.ValidationRuleBuilderExtensions;

public class TimeSpanTests
{
    private readonly TimeSpan _time = TimeSpan.FromHours(1);
    private readonly TimeSpan _longer = TimeSpan.FromHours(2);
    private readonly TimeSpan _shorter = TimeSpan.FromMinutes(30);

    private readonly string _customMessage = "Custom message";

    private Func<TimeSpan, IEnumerable<ValidationFailure>> _registered = null!;
    private readonly ValidationRuleBuilder<TimeSpan, TimeSpan> _builder;

    public TimeSpanTests()
    {
        _builder = new ValidationRuleBuilder<TimeSpan, TimeSpan>(
            registerRule: rule => _registered = rule,
            executor: (value, rule, message) =>
            {
                var result = rule(value);
                return result.IsValid
                    ? []
                    : [new ValidationFailure(nameof(TimeSpan), message ?? result.Message)];
            });
    }

    [Test]
    public void AtLeast_ShouldReturnNoFailures_WhenTimeIsAtLeastMin()
    {
        _builder.AtLeast(_shorter);
        var failures = _registered(_time);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void AtLeast_ShouldReturnFailure_WhenTimeIsLessThanMin()
    {
        _builder.AtLeast(_longer);
        var failures = _registered(_time).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void AtLeast_ShouldUseCustomMessage_WhenProvided()
    {
        _builder.AtLeast(_longer, _customMessage);
        var failure = _registered(_time).Single();
        failure.ValidationMessage.ShouldBe(_customMessage);
    }

    [Test]
    public void AtMost_ShouldReturnNoFailures_WhenTimeIsAtMostMax()
    {
        _builder.AtMost(_longer);
        var failures = _registered(_time);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void AtMost_ShouldReturnFailure_WhenTimeIsGreaterThanMax()
    {
        _builder.AtMost(_shorter);
        var failures = _registered(_time).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void AtMost_ShouldUseCustomMessage_WhenProvided()
    {
        _builder.AtMost(_shorter, _customMessage);
        var failure = _registered(_time).Single();
        failure.ValidationMessage.ShouldBe(_customMessage);
    }

    [Test]
    public void Between_ShouldReturnNoFailures_WhenTimeIsWithinRange()
    {
        _builder.Between(_shorter, _longer);
        var failures = _registered(_time);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void Between_ShouldReturnFailure_WhenTimeIsOutsideRange()
    {
        _builder.Between(_shorter, _longer);
        var failuresBefore = _registered(_shorter.Subtract(TimeSpan.FromMinutes(1))).ToArray();
        var failuresAfter = _registered(_longer.Add(TimeSpan.FromMinutes(1))).ToArray();
        failuresBefore.Length.ShouldBe(1);
        failuresAfter.Length.ShouldBe(1);
    }

    [Test]
    public void Between_ShouldUseCustomMessage_WhenProvided()
    {
        _builder.Between(_shorter, _longer, _customMessage);
        var failure = _registered(_shorter.Subtract(TimeSpan.FromMinutes(1))).Single();
        failure.ValidationMessage.ShouldBe(_customMessage);
    }

    [Test]
    public void Around_ShouldReturnNoFailures_WhenTimeIsWithinTolerance()
    {
        var expected = TimeSpan.FromHours(1);
        var tolerance = TimeSpan.FromMinutes(30);
        _builder.Around(expected, tolerance);
        var failures = _registered(_time);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void Around_ShouldReturnFailure_WhenTimeIsOutsideTolerance()
    {
        var tolerance = TimeSpan.FromMinutes(30);
        _builder.Around(_time, tolerance);
        var failuresBefore = _registered(_time.Subtract(tolerance).Subtract(TimeSpan.FromMinutes(1))).ToArray();
        var failuresAfter = _registered(_time.Add(tolerance).Add(TimeSpan.FromMinutes(1))).ToArray();
        failuresBefore.Length.ShouldBe(1);
        failuresAfter.Length.ShouldBe(1);
    }

    [Test]
    public void Around_ShouldUseCustomMessage_WhenProvided()
    {
        var tolerance = TimeSpan.FromMinutes(30);
        _builder.Around(_time, tolerance, _customMessage);
        var failure = _registered(_time.Subtract(tolerance).Subtract(TimeSpan.FromMinutes(1))).Single();
        failure.ValidationMessage.ShouldBe(_customMessage);
    }

    [Test]
    public void AtLeastZero_ShouldReturnNoFailures_WhenTimeIsZeroOrGreater()
    {
        _builder.AtLeastZero();
        var failures = _registered(_time);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void AtLeastZero_ShouldReturnFailure_WhenTimeIsNegative()
    {
        _builder.AtLeastZero();
        var failures = _registered(TimeSpan.FromMinutes(-1)).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void AtLeastZero_ShouldUseCustomMessage_WhenProvided()
    {
        _builder.AtLeastZero(_customMessage);
        var failure = _registered(TimeSpan.FromMinutes(-1)).Single();
        failure.ValidationMessage.ShouldBe(_customMessage);
    }
}
