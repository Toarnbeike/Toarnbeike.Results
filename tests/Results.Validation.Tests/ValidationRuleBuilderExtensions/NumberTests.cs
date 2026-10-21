using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Extensions;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Tests.ValidationRuleBuilderExtensions;

public class NumberTests
{
    private Func<int, IEnumerable<ValidationFailure>> _registered = null!;
    private readonly ValidationRuleBuilder<int, int> _builder;

    public NumberTests()
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
    public void GreaterThan_ShouldReturnNoFailures_WhenValueIsGreaterThanMin()
    {
        _builder.GreaterThan(5);
        var failures = _registered(6);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void GreaterThan_ShouldReturnFailure_WhenValueIsNotGreaterThanMin()
    {
        _builder.GreaterThan(5);
        var failures = _registered(5).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void GreaterThan_ShouldUseCustomMessage_WhenProvided()
    {
        var customMessage = "Custom message";
        _builder.GreaterThan(5, customMessage);
        var failure = _registered(5).Single();
        failure.ValidationMessage.ShouldBe(customMessage);
    }

    [Test]
    public void LessThan_ShouldReturnNoFailures_WhenValueIsLessThanMax()
    {
        _builder.LessThan(5);
        var failures = _registered(4);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void LessThan_ShouldReturnFailure_WhenValueIsNotLessThanMax()
    {
        _builder.LessThan(5);
        var failures = _registered(5).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void LessThan_ShouldUseCustomMessage_WhenProvided()
    {
        var customMessage = "Custom message";
        _builder.LessThan(5, customMessage);
        var failure = _registered(5).Single();
        failure.ValidationMessage.ShouldBe(customMessage);
    }

    [Test]
    public void Positive_ShouldReturnNoFailures_WhenValueIsPositive()
    {
        _builder.Positive();
        var failures = _registered(1);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void Positive_ShouldReturnFailure_WhenValueIsNotPositive()
    {
        _builder.Positive();
        var failures = _registered(0).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void Positive_ShouldUseCustomMessage_WhenProvided()
    {
        var customMessage = "Custom message";
        _builder.Positive(customMessage);
        var failure = _registered(0).Single();
        failure.ValidationMessage.ShouldBe(customMessage);
    }
}
