using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Extensions;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Tests.ValidationRuleBuilderExtensions;

public class IntegerTests
{
    private Func<int, IEnumerable<ValidationFailure>> _registered = null!;
    private readonly ValidationRuleBuilder<int, int> _builder;

    public IntegerTests()
    {
        _builder = new ValidationRuleBuilder<int, int>(
            registerRule: rule => _registered = rule,
            executor: (value, rule, message) =>
            {
                var result = rule(value);
                return result.IsValid
                    ? []
                    : [new ValidationFailure(nameof(Int32), message ?? result.Message)];
            });
    }

    [Test]
    public void AtLeast_ShouldReturnNoFailures_WhenValueIsAtLeastMin()
    {
        _builder.AtLeast(5);
        var failures = _registered(5);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void AtLeast_ShouldReturnFailure_WhenValueIsLessThanMin()
    {
        _builder.AtLeast(5);
        var failures = _registered(4).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void AtLeast_ShouldUseCustomMessage_WhenProvided()
    {
        var customMessage = "Custom message";
        _builder.AtLeast(5, customMessage);
        var failure = _registered(4).Single();
        failure.ValidationMessage.ShouldBe(customMessage);
    }

    [Test]
    public void AtMost_ShouldReturnNoFailures_WhenValueIsAtMostMax()
    {
        _builder.AtMost(5);
        var failures = _registered(5);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void AtMost_ShouldReturnFailure_WhenValueIsGreaterThanMax()
    {
        _builder.AtMost(5);
        var failures = _registered(6).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void AtMost_ShouldUseCustomMessage_WhenProvided()
    {
        var customMessage = "Custom message";
        _builder.AtMost(5, customMessage);
        var failure = _registered(6).Single();
        failure.ValidationMessage.ShouldBe(customMessage);
    }

    [Test]
    public void Between_ShouldReturnNoFailures_WhenValueIsWithinRange()
    {
        _builder.Between(5, 10);
        var failures = _registered(7);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void Between_ShouldReturnFailure_WhenValueIsOutsideRange()
    {
        _builder.Between(5, 10);
        var failuresBelow = _registered(4).ToArray();
        var failuresAbove = _registered(11).ToArray();
        failuresBelow.Length.ShouldBe(1);
        failuresAbove.Length.ShouldBe(1);
    }

    [Test]
    public void Between_ShouldUseCustomMessage_WhenProvided()
    {
        var customMessage = "Custom message";
        _builder.Between(5, 10, customMessage);
        var failure = _registered(4).Single();
        failure.ValidationMessage.ShouldBe(customMessage);
    }

    [Test]
    public void MultipleOf_ShouldReturnNoFailures_WhenValueIsMultipleOfFactor()
    {
        _builder.MultipleOf(3);
        var failures = _registered(9);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void MultipleOf_ShouldReturnFailure_WhenValueIsNotMultipleOfFactor()
    {
        _builder.MultipleOf(3);
        var failures = _registered(4).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void MultipleOf_ShouldUseCustomMessage_WhenProvided()
    {
        var customMessage = "Custom message";
        _builder.MultipleOf(3, customMessage);
        var failure = _registered(4).Single();
        failure.ValidationMessage.ShouldBe(customMessage);
    }
}
