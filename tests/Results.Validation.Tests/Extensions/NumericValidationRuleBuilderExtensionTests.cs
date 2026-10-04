using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Extensions;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Tests.Extensions;

public class NumericValidationRuleBuilderExtensionTests
{
    private readonly string _customMessage = "Custom message";
    private Func<int, IEnumerable<ValidationFailure>>? _registered = null;
    private readonly ValidationRuleBuilder<int, int> _builder;
    public NumericValidationRuleBuilderExtensionTests()
    {
        _builder = new ValidationRuleBuilder<int, int>(
            r => _registered = r,
            (value, rule) => rule.Predicate(value)
                ? []
                : [new ValidationFailure("value", rule.Message)]);
    }

    [Test]
    public void GreaterThan_ShouldSucceed()
    {
        _builder.GreaterThan(5);
        ShouldSucceed(6);
    }

    [Test]
    public void GreaterThan_ShouldFail()
    {
        _builder.GreaterThan(5);
        ShouldFail(5, "Value must be greater than 5.");
        ShouldFail(4, "Value must be greater than 5.");
    }

    [Test]
    public void GreaterThan_CustomMessage()
    {
        _builder.GreaterThan(5, _customMessage);
        ShouldFail(5, _customMessage);
        ShouldFail(4, _customMessage);
    }

    [Test]
    public void LessThan_ShouldSucceed()
    {
        _builder.LessThan(5);
        ShouldSucceed(4);
    }

    [Test]
    public void LessThan_ShouldFail()
    {
        _builder.LessThan(5);
        ShouldFail(6, "Value must be less than 5.");
    }

    [Test]
    public void LessThan_CustomMessage()
    {
        _builder.LessThan(5, _customMessage);
        ShouldFail(5, _customMessage);
    }

    [Test]
    public void Positive_ShouldSucceed()
    {
        _builder.Positive();
        ShouldSucceed(1);
    }

    [Test]
    public void Positive_ShouldFail()
    {
        _builder.Positive();
        ShouldFail(0, "Value must be positive.");
        ShouldFail(-1, "Value must be positive.");
    }

    [Test]
    public void Positive_CustomMessage()
    {
        _builder.Positive(_customMessage);
        ShouldFail(-1, _customMessage);
    }

    private void ShouldSucceed(int value)
    {
        _registered.ShouldNotBeNull();
        var failures = _registered(value).ToArray();
        failures.ShouldBeEmpty();
    }

    private void ShouldFail(int value, string expectedMessage)
    {
        _registered.ShouldNotBeNull();
        var failures = _registered(value).ToArray();
        failures.Length.ShouldBe(1);
        failures[0].ValidationMessage.ShouldBe(expectedMessage);
    }
}
