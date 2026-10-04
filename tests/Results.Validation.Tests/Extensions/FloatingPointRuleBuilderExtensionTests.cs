using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Extensions;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Tests.Extensions;

public class FloatingPointRuleBuilderExtensionTests
{
    private readonly string _customMessage = "Custom message";
    private Func<double, IEnumerable<ValidationFailure>>? _registered = null;
    private readonly ValidationRuleBuilder<double, double> _builder;

    public FloatingPointRuleBuilderExtensionTests()
    {
        _builder = new ValidationRuleBuilder<double, double>(
            r => _registered = r,
            (value, rule) => rule.Predicate(value)
                ? []
                : [new ValidationFailure("value", rule.Message)]);
    }

    [Test]
    public void AtLeast_ShouldSucceed()
    {
        _builder.AtLeast(5.0);
        ShouldSucceed(5.0);
    }

    [Test]
    public void AtLeast_ShouldFail()
    {
        _builder.AtLeast(5.0);
        ShouldFail(4.9, "Value must be at least 5.");
    }

    [Test]
    public void AtLeast_CustomMessage()
    {
        _builder.AtLeast(5.0, null, _customMessage);
        ShouldFail(4.9, _customMessage);
    }

    [Test]
    public void AtMost_ShouldSucceed()
    {
        _builder.AtMost(5.0);
        ShouldSucceed(5.0);
    }

    [Test]
    public void AtMost_ShouldFail()
    {
        _builder.AtMost(5.0);
        ShouldFail(5.1, "Value must be at most 5.");
    }

    [Test]
    public void AtMost_CustomMessage()
    {
        _builder.AtMost(5.0, null, _customMessage);
        ShouldFail(5.1, _customMessage);
    }

    [Test]
    public void Between_ShouldSucceed()
    {
        _builder.Between(5.0, 10.0);
        ShouldSucceed(7.5);
    }

    [Test]
    public void Between_ShouldFail()
    {
        _builder.Between(5.0, 10.0);
        ShouldFail(4.9, "Value must be between 5 and 10.");
    }

    [Test]
    public void Between_CustomMessage()
    {
        _builder.Between(5.0, 10.0, null, _customMessage);
        ShouldFail(4.9, _customMessage);
    }

    [Test]
    public void MultipleOf_ShouldSucceed()
    {
        _builder.MultipleOf(2.0);
        ShouldSucceed(4.0);
    }

    [Test]
    public void MultipleOf_ShouldFail()
    {
        _builder.MultipleOf(2.0);
        ShouldFail(3.0, "Value must be a multiple of 2.");
    }

    [Test]
    public void MultipleOf_CustomMessage()
    {
        _builder.MultipleOf(2.0, null, _customMessage);
        ShouldFail(3.0, _customMessage);
    }

    [Test]
    public void Finite_ShouldSucceed()
    {
        _builder.Finite();
        ShouldSucceed(5.0);
    }

    [Test]
    public void Finite_ShouldFail()
    {
        _builder.Finite();
        ShouldFail(double.PositiveInfinity, "Value must be a finite Double.");
    }

    [Test]
    public void Finite_CustomMessage()
    {
        _builder.Finite(_customMessage);
        ShouldFail(double.PositiveInfinity, _customMessage);
    }

    [Test]
    public void NotNaN_ShouldSucceed()
    {
        _builder.NotNaN();
        ShouldSucceed(5.0);
    }

    [Test]
    public void NotNaN_ShouldFail()
    {
        _builder.NotNaN();
        ShouldFail(double.NaN, "Value must not be NaN.");
    }

    [Test]
    public void NotNaN_CustomMessage()
    {
        _builder.NotNaN(_customMessage);
        ShouldFail(double.NaN, _customMessage);
    }

    private void ShouldSucceed(double value)
    {
        _registered.ShouldNotBeNull();
        var failures = _registered(value).ToArray();
        failures.ShouldBeEmpty();
    }

    private void ShouldFail(double value, string expectedMessage)
    {
        _registered.ShouldNotBeNull();
        var failures = _registered(value).ToArray();
        failures.Length.ShouldBe(1);
        failures[0].ValidationMessage.ShouldBe(expectedMessage);
    }
}
