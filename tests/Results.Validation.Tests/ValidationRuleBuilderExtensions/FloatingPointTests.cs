using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Extensions;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Tests.ValidationRuleBuilderExtensions;

public class FloatingPointTests
{
    private Func<double, IEnumerable<ValidationFailure>> _registered = null!;
    private readonly ValidationRuleBuilder<double, double> _builder;

    public FloatingPointTests()
    {
        _builder = new ValidationRuleBuilder<double, double>(
            registerRule: rule => _registered = rule,
            executor: (value, rule, message) =>
            {
                var result = rule(value);
                return result.IsValid
                    ? []
                    : [new ValidationFailure(nameof(Double), message ?? result.Message)];
            });
    }

    [Test]
    public void AtLeast_ShouldReturnNoFailures_WhenValueIsAtLeastMin()
    {
        _builder.AtLeast(5.0);
        var failures = _registered(5.0);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void AtLeast_ShouldReturnFailure_WhenValueIsLessThanMin()
    {
        _builder.AtLeast(5.0);
        var failures = _registered(4.9).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void AtLeast_ShouldReturnNoFailures_WhenValueIsWithinTolerance()
    {
        _builder.AtLeast(5.0, 0.1);
        var failures = _registered(4.91);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void AtLeast_ShouldUseCustomMessage_WhenProvided()
    {
        var customMessage = "Value must be at least 5.0";
        _builder.AtLeast(5.0, message: customMessage);
        var failure = _registered(4.9).Single();
        failure.ValidationMessage.ShouldBe(customMessage);
    }

    [Test]
    public void AtMost_ShouldReturnNoFailures_WhenValueIsAtMostMax()
    {
        _builder.AtMost(5.0);
        var failures = _registered(5.0);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void AtMost_ShouldReturnFailure_WhenValueIsGreaterThanMax()
    {
        _builder.AtMost(5.0);
        var failures = _registered(5.1).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void AtMost_ShouldReturnNoFailures_WhenValueIsWithinTolerance()
    {
        _builder.AtMost(5.0, 0.1);
        var failures = _registered(5.09);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void AtMost_ShouldUseCustomMessage_WhenProvided()
    {
        var customMessage = "Value must be at most 5.0";
        _builder.AtMost(5.0, message: customMessage);
        var failure = _registered(5.1).Single();
        failure.ValidationMessage.ShouldBe(customMessage);
    }

    [Test]
    public void Between_ShouldReturnNoFailures_WhenValueIsWithinRange()
    {
        _builder.Between(5.0, 10.0);
        var failures = _registered(7.5);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void Between_ShouldReturnFailure_WhenValueIsOutsideRange()
    {
        _builder.Between(5.0, 10.0);
        var failuresBelow = _registered(4.9).ToArray();
        var failuresAbove = _registered(10.1).ToArray();
        failuresBelow.Length.ShouldBe(1);
        failuresAbove.Length.ShouldBe(1);
    }

    [Test]
    public void Between_ShouldReturnNoFailures_WhenValueIsWithinTolerance()
    {
        _builder.Between(5.0, 10.0, 0.1);
        var failuresBelow = _registered(4.91);
        var failuresAbove = _registered(10.09);
        failuresBelow.Count().ShouldBe(0);
        failuresAbove.Count().ShouldBe(0);
    }

    [Test]
    public void Between_ShouldUseCustomMessage_WhenProvided()
    {
        var customMessage = "Value must be between 5.0 and 10.0";
        _builder.Between(5.0, 10.0, message: customMessage);
        var failureBelow = _registered(4.9).Single();
        var failureAbove = _registered(10.1).Single();
        failureBelow.ValidationMessage.ShouldBe(customMessage);
        failureAbove.ValidationMessage.ShouldBe(customMessage);
    }

    [Test]
    public void MultipleOf_ShouldReturnNoFailures_WhenValueIsMultipleOfFactor()
    {
        _builder.MultipleOf(2.0);
        var failures = _registered(4.0);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void MultipleOf_ShouldReturnFailure_WhenValueIsNotMultipleOfFactor()
    {
        _builder.MultipleOf(2.0);
        var failures = _registered(3.0).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void MultipleOf_ShouldReturnNoFailures_WhenValueIsWithinTolerance()
    {
        _builder.MultipleOf(2.0, 0.1);
        var failures = _registered(4.09);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void MultipleOf_ShouldUseCustomMessage_WhenProvided()
    {
        var customMessage = "Value must be a multiple of 2.0";
        _builder.MultipleOf(2.0, message: customMessage);
        var failure = _registered(3.0).Single();
        failure.ValidationMessage.ShouldBe(customMessage);
    }

    [Test]
    public void Finite_ShouldReturnNoFailures_WhenValueIsFinite()
    {
        _builder.Finite();
        var failures = _registered(5.0);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void Finite_ShouldReturnFailure_WhenValueIsInfinite()
    {
        _builder.Finite();
        var failures = _registered(double.PositiveInfinity).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void Finite_ShouldReturnFailure_WhenValueIsNaN()
    {
        _builder.Finite();
        var failures = _registered(double.NaN).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void Finite_ShouldUseCustomMessage_WhenProvided()
    {
        var customMessage = "Value must be finite";
        _builder.Finite(message: customMessage);
        var failure = _registered(double.PositiveInfinity).Single();
        failure.ValidationMessage.ShouldBe(customMessage);
    }
}
