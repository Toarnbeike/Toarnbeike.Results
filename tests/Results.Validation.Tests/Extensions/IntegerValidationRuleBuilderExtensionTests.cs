using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Extensions;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Tests.Extensions;

public class IntegerValidationRuleBuilderExtensionTests
{
    private readonly string _customMessage = "Custom message";
    private Func<int, IEnumerable<ValidationFailure>>? _registered = null;
    private readonly ValidationRuleBuilder<int, int> _builder;
    public IntegerValidationRuleBuilderExtensionTests()
    {
        _builder = new ValidationRuleBuilder<int, int>(
            r => _registered = r,
            (value, rule) => rule.Predicate(value)
                ? []
                : [new ValidationFailure("value", rule.Message)]);
    }

    [Test]
    public void AtLeast_ShouldSucceed()
    {
        _builder.AtLeast(5);
        ShouldSucceed(5);
        ShouldSucceed(10);
    }

    [Test]
    public void AtLeast_ShouldFail()
    {
        _builder.AtLeast(5);
        ShouldFail(4, "Value must be at least 5.");
    }

    [Test]
    public void AtLeast_CustomMessage()
    {
        _builder.AtLeast(5, _customMessage);
        ShouldFail(4, _customMessage);
    }

    [Test]
    public void AtMost_ShouldSucceed()
    {
        _builder.AtMost(5);
        ShouldSucceed(5);
        ShouldSucceed(4);
    }

    [Test]
    public void AtMost_ShouldFail()
    {
        _builder.AtMost(5);
        ShouldFail(6, "Value must be at most 5.");
    }

    [Test]
    public void AtMost_CustomMessage()
    {
        _builder.AtMost(5, _customMessage);
        ShouldFail(6, _customMessage);
    }

    [Test]
    public void Between_ShouldSucceed()
    {
        _builder.Between(5, 10);
        ShouldSucceed(5);
        ShouldSucceed(7);
        ShouldSucceed(10);
    }

    [Test]
    public void Between_ShouldFail()
    {
        _builder.Between(5, 10);
        ShouldFail(4, "Value must be between 5 and 10.");
        ShouldFail(11, "Value must be between 5 and 10.");
    }

    [Test]
    public void Between_CustomMessage()
    {
        _builder.Between(5, 10, _customMessage);
        ShouldFail(4, _customMessage);
        ShouldFail(11, _customMessage);
    }

    [Test]
    public void MultipleOf_ShouldSucceed()
    {
        _builder.MultipleOf(3);
        ShouldSucceed(3);
        ShouldSucceed(6);
        ShouldSucceed(-9);
    }

    [Test]
    public void MultipleOf_ShouldFail()
    {
        _builder.MultipleOf(3);
        ShouldFail(4, "Value must be a multiple of 3.");
    }

    [Test]
    public void MultipleOf_CustomMessage()
    {
        _builder.MultipleOf(3, _customMessage);
        ShouldFail(4, _customMessage);
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
