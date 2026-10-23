using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Extensions;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Tests.ValidationRuleBuilderExtensions;

public class BoolTests
{
    private Func<bool, IEnumerable<ValidationFailure>> _registered = null!;
    private readonly ValidationRuleBuilder<bool, bool> _builder;

    public BoolTests()
    {
        _builder = new ValidationRuleBuilder<bool, bool>(
            registerRule: rule => _registered = rule,
            executor: (value, rule, message) =>
            {
                var result = rule(value);
                return result.IsValid
                    ? []
                    : [new ValidationFailure(nameof(Boolean), message ?? result.Message)];
            });
    }

    [Test]
    public void IsTrue_ShouldReturnNoFailures_WhenValueIsTrue()
    {
        _builder.IsTrue();
        var failures = _registered(true);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void IsTrue_ShouldReturnFailure_WhenValueIsFalse()
    {
        _builder.IsTrue();
        var failures = _registered(false).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void IsTrue_ShouldUseCustomMessage_WhenProvided()
    {
        _builder.IsTrue("Custom message");
        var failure = _registered(false).Single();
        failure.ValidationMessage.ShouldBe("Custom message");
    }

    [Test]
    public void IsFalse_ShouldReturnNoFailures_WhenValueIsFalse()
    {
        _builder.IsFalse();
        var failures = _registered(false);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void IsFalse_ShouldReturnFailure_WhenValueIsTrue()
    {
        _builder.IsFalse();
        var failures = _registered(true).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void IsFalse_ShouldUseCustomMessage_WhenProvided()
    {
        _builder.IsFalse("Custom message");
        var failure = _registered(true).Single();
        failure.ValidationMessage.ShouldBe("Custom message");
    }
}
