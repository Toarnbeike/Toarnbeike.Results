using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Extensions;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Tests.Extensions;

public class DateOnlyValidationRuleBuilderExtensionTests
{
    private readonly DateOnly _date = new(2023, 1, 1);
    private readonly DateOnly _later = new(2023, 1, 2);
    private readonly DateOnly _earlier = new(2022, 12, 31);
    private readonly string _customMessage = "Custom message";
    private Func<DateOnly, IEnumerable<ValidationFailure>>? _registered = null;
    private readonly ValidationRuleBuilder<DateOnly, DateOnly> _builder;

    public DateOnlyValidationRuleBuilderExtensionTests()
    {
        _builder = new ValidationRuleBuilder<DateOnly, DateOnly>(
            r => _registered = r,
            (value, rule) => rule.Predicate(value)
                ? []
                : [new ValidationFailure("value", rule.Message)]);
    }

    [Test]
    public void OnOrAfter_ShouldSucceed()
    {
        _builder.OnOrAfter(_date);
        ShouldSucceed(_later);
    }

    [Test]
    public void OnOrAfter_ShouldFail()
    {
        _builder.OnOrAfter(_date);
        ShouldFail(_earlier, $"Date must be on or after {_date}.");
    }

    [Test]
    public void OnOrAfter_CustomMessage()
    {
        _builder.OnOrAfter(_date, _customMessage);
        ShouldFail(_earlier, _customMessage);
    }

    [Test]
    public void OnOrBefore_ShouldSucceed()
    {
        _builder.OnOrBefore(_date);
        ShouldSucceed(_earlier);
    }

    [Test]
    public void OnOrBefore_ShouldFail()
    {
        _builder.OnOrBefore(_date);
        ShouldFail(_later, $"Date must be on or before {_date}.");
    }

    [Test]
    public void OnOrBefore_CustomMessage()
    {
        _builder.OnOrBefore(_date, _customMessage);
        ShouldFail(_later, _customMessage);
    }

    [Test]
    public void MustBeBetween_ShouldSucceed()
    {
        _builder.MustBeBetween(_earlier, _later);
        ShouldSucceed(_date);
    }

    [Test]
    public void MustBeBetween_ShouldFail()
    {
        _builder.MustBeBetween(_earlier, _date);
        ShouldFail(_later, $"Date must be between {_earlier} and {_date}.");
    }

    [Test]
    public void MustBeBetween_CustomMessage()
    {
        _builder.MustBeBetween(_earlier, _date, _customMessage);
        ShouldFail(_later, _customMessage);
    }

    private void ShouldSucceed(DateOnly value)
    {
        _registered.ShouldNotBeNull();
        var failures = _registered(value).ToArray();
        failures.ShouldBeEmpty();
    }

    private void ShouldFail(DateOnly value, string expectedMessage)
    {
        _registered.ShouldNotBeNull();
        var failures = _registered(value).ToArray();
        failures.Length.ShouldBe(1);
        failures[0].ValidationMessage.ShouldBe(expectedMessage);
    }
}
