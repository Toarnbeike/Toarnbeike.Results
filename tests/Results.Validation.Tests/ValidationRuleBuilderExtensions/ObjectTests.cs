using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Extensions;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Tests.ValidationRuleBuilderExtensions;

public class ObjectTests
{
    private Func<int, IEnumerable<ValidationFailure>> _registered = null!;
    private readonly ValidationRuleBuilder<int, int> _builder;

    public ObjectTests()
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
    public void EqualTo_ShouldReturnNoFailures_WhenValuesAreEqual()
    {
        _builder.EqualTo(5, "Value must be 5");
        var failures = _registered(5);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void EqualTo_ShouldReturnFailure_WhenValuesAreNotEqual()
    {
        _builder.EqualTo(5, "Value must be 5");
        var failures = _registered(10).ToArray();
        failures.Length.ShouldBe(1);
        failures[0].ValidationMessage.ShouldBe("Value must be 5");
    }

    [Test]
    public void EqualTo_ShouldUseCustomMessage_WhenProvided()
    {
        _builder.EqualTo(5, "Custom message");
        var failures = _registered(10).ToArray();
        failures.Length.ShouldBe(1);
        failures[0].ValidationMessage.ShouldBe("Custom message");
    }

    [Test]
    public void NotEqualTo_ShouldReturnNoFailures_WhenValuesAreNotEqual()
    {
        _builder.NotEqualTo(5, "Value must not be 5");
        var failures = _registered(10);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void NotEqualTo_ShouldReturnFailure_WhenValuesAreEqual()
    {
        _builder.NotEqualTo(5, "Value must not be 5");
        var failures = _registered(5).ToArray();
        failures.Length.ShouldBe(1);
        failures[0].ValidationMessage.ShouldBe("Value must not be 5");
    }

    [Test]
    public void NotEqualTo_ShouldUseCustomMessage_WhenProvided()
    {
        _builder.NotEqualTo(5, "Custom message");
        var failures = _registered(5).ToArray();
        failures.Length.ShouldBe(1);
        failures[0].ValidationMessage.ShouldBe("Custom message");
    }

    [Test]
    public void Must_ShouldReturnNoFailures_WhenPredicateIsSatisfied()
    {
        _builder.Must(x => x > 0, "Value must be positive");
        var failures = _registered(5);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void Must_ShouldReturnFailure_WhenPredicateIsNotSatisfied()
    {
        _builder.Must(x => x > 0, "Value must be positive");
        var failures = _registered(-1).ToArray();
        failures.Length.ShouldBe(1);
        failures[0].ValidationMessage.ShouldBe("Value must be positive");
    }

    [Test]
    public void MustNot_ShouldReturnNoFailures_WhenPredicateIsNotSatisfied()
    {
        _builder.MustNot(x => x < 0, "Value must not be negative");
        var failures = _registered(5);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void MustNot_ShouldReturnFailure_WhenPredicateIsSatisfied()
    {
        _builder.MustNot(x => x < 0, "Value must not be negative");
        var failures = _registered(-1).ToArray();
        failures.Length.ShouldBe(1);
        failures[0].ValidationMessage.ShouldBe("Value must not be negative");
    }
}
