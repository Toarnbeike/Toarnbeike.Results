using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Extensions;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Tests.ValidationRuleBuilderExtensions;

public class EnumTests
{
    private Func<DayOfWeek, IEnumerable<ValidationFailure>> _registered = null!;
    private readonly ValidationRuleBuilder<DayOfWeek, DayOfWeek> _builder;

    public EnumTests()
    {
        _builder = new ValidationRuleBuilder<DayOfWeek, DayOfWeek>(
            registerRule: rule => _registered = rule,
            executor: (value, rule, message) =>
            {
                var result = rule(value);
                return result.IsValid
                    ? []
                    : [new ValidationFailure(nameof(DayOfWeek), message ?? result.Message)];
            });
    }

    [Test]
    public void IsDefined_ShouldReturnNoFailures_WhenValueIsDefined()
    {
        _builder.IsDefined();
        var failures = _registered(DayOfWeek.Monday);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void IsDefined_ShouldReturnFailure_WhenValueIsNotDefined()
    {
        _builder.IsDefined();
        var failures = _registered((DayOfWeek)100).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void IsDefined_ShouldUseCustomMessage_WhenProvided()
    {
        _builder.IsDefined("Custom message");
        var failure = _registered((DayOfWeek)100).Single();
        failure.ValidationMessage.ShouldBe("Custom message");
    }

    [Test]
    public void OneOf_ShouldReturnNoFailures_WhenValueIsInAcceptedValues()
    {
        _builder.OneOf([DayOfWeek.Monday, DayOfWeek.Tuesday]);
        var failures = _registered(DayOfWeek.Monday);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void OneOf_ShouldReturnFailure_WhenValueIsNotInAcceptedValues()
    {
        _builder.OneOf([DayOfWeek.Monday, DayOfWeek.Tuesday]);
        var failures = _registered(DayOfWeek.Wednesday).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void OneOf_ShouldUseCustomMessage_WhenProvided()
    {
        _builder.OneOf([DayOfWeek.Monday, DayOfWeek.Tuesday], "Custom message");
        var failure = _registered(DayOfWeek.Wednesday).Single();
        failure.ValidationMessage.ShouldBe("Custom message");
    }

    [Test]
    public void NotOneOf_ShouldReturnNoFailures_WhenValueIsNotInRejectedValues()
    {
        _builder.NotOneOf([DayOfWeek.Monday, DayOfWeek.Tuesday]);
        var failures = _registered(DayOfWeek.Wednesday);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void NotOneOf_ShouldReturnFailure_WhenValueIsInRejectedValues()
    {
        _builder.NotOneOf([DayOfWeek.Monday, DayOfWeek.Tuesday]);
        var failures = _registered(DayOfWeek.Monday).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void NotOneOf_ShouldUseCustomMessage_WhenProvided()
    {
        _builder.NotOneOf([DayOfWeek.Monday, DayOfWeek.Tuesday], "Custom message");
        var failure = _registered(DayOfWeek.Monday).Single();
        failure.ValidationMessage.ShouldBe("Custom message");
    }
}
