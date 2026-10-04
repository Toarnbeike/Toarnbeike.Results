using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Extensions;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Tests.Extensions;

public class EnumRuleBuilderExtensionTests
{
    private readonly string _customMessage = "Custom message";
    private Func<DayOfWeek, IEnumerable<ValidationFailure>>? _registered = null;
    private readonly ValidationRuleBuilder<DayOfWeek, DayOfWeek> _builder;
    public EnumRuleBuilderExtensionTests()
    {
        _builder = new ValidationRuleBuilder<DayOfWeek, DayOfWeek>(
            r => _registered = r,
            (value, rule) => rule.Predicate(value)
                ? []
                : [new ValidationFailure("value", rule.Message)]);
    }

    [Test]
    public void IsDefined_ShouldSucceed()
    {
        _builder.IsDefined();
        ShouldSucceed(DayOfWeek.Monday);
    }

    [Test]
    public void IsDefined_ShouldFail()
    {
        _builder.IsDefined();
        ShouldFail((DayOfWeek)100, "Value must be a defined value of the DayOfWeek enum.");
    }

    [Test]
    public void IsDefined_CustomMessage()
    {
        _builder.IsDefined(_customMessage);
        ShouldFail((DayOfWeek)100, _customMessage);
    }

    [Test]
    public void OneOf_ShouldSucceed()
    {
        _builder.OneOf([DayOfWeek.Monday, DayOfWeek.Tuesday]);
        ShouldSucceed(DayOfWeek.Monday);
    }

    [Test]
    public void OneOf_ShouldFail()
    {
        _builder.OneOf([DayOfWeek.Monday, DayOfWeek.Tuesday]);
        ShouldFail(DayOfWeek.Wednesday, "Value must be one of [Monday, Tuesday].");
    }

    [Test]
    public void OneOf_CustomMessage()
    {
        _builder.OneOf([DayOfWeek.Monday, DayOfWeek.Tuesday], _customMessage);
        ShouldFail(DayOfWeek.Wednesday, _customMessage);
    }

    [Test]
    public void NotOneOf_ShouldSucceed()
    {
        _builder.NotOneOf([DayOfWeek.Monday, DayOfWeek.Tuesday]);
        ShouldSucceed(DayOfWeek.Wednesday);
    }

    [Test]
    public void NotOneOf_ShouldFail()
    {
        _builder.NotOneOf([DayOfWeek.Monday, DayOfWeek.Tuesday]);
        ShouldFail(DayOfWeek.Monday, "Value must not be one of [Monday, Tuesday].");
    }

    [Test]
    public void NotOneOf_CustomMessage()
    {
        _builder.NotOneOf([DayOfWeek.Monday, DayOfWeek.Tuesday], _customMessage);
        ShouldFail(DayOfWeek.Monday, _customMessage);
    }

    private void ShouldSucceed(DayOfWeek value)
    {
        _registered.ShouldNotBeNull();
        var failures = _registered(value).ToArray();
        failures.ShouldBeEmpty();
    }

    private void ShouldFail(DayOfWeek value, string expectedMessage)
    {
        _registered.ShouldNotBeNull();
        var failures = _registered(value).ToArray();
        failures.Length.ShouldBe(1);
        failures[0].ValidationMessage.ShouldBe(expectedMessage);
    }
}
