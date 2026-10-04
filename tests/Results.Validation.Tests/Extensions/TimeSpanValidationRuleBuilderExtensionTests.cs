using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Extensions;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Tests.Extensions;

public class TimeSpanValidationRuleBuilderExtensionTests
{
    private readonly string _customMessage = "Custom message";

    private Func<TimeSpan, IEnumerable<ValidationFailure>>? _registered = null;
    private readonly ValidationRuleBuilder<TimeSpan, TimeSpan> _builder;

    public TimeSpanValidationRuleBuilderExtensionTests()
    {
        _builder = new ValidationRuleBuilder<TimeSpan, TimeSpan>(
            r => _registered = r,
            (value, rule) => rule.Predicate(value)
                ? []
                : [new ValidationFailure("value", rule.Message)]);
    }

    [Test]
    public void AtLeast_ShouldSucceed()
    {
        _builder.AtLeast(TimeSpan.FromSeconds(5));
        ShouldSucceed(TimeSpan.FromSeconds(6));
    }

    [Test]
    public void AtLeast_ShouldFail()
    {
        _builder.AtLeast(TimeSpan.FromSeconds(5));
        ShouldFail(TimeSpan.FromSeconds(4), "TimeSpan must be at least 00:00:05.");
    }

    [Test]
    public void AtLeast_CustomMessage()
    {
        _builder.AtLeast(TimeSpan.FromSeconds(5), _customMessage);
        ShouldFail(TimeSpan.FromSeconds(4), _customMessage);
    }

    [Test]
    public void AtMost_ShouldSucceed()
    {
        _builder.AtMost(TimeSpan.FromSeconds(5));
        ShouldSucceed(TimeSpan.FromSeconds(4));
    }

    [Test]
    public void AtMost_ShouldFail()
    {
        _builder.AtMost(TimeSpan.FromSeconds(5));
        ShouldFail(TimeSpan.FromSeconds(6), "TimeSpan must be at most 00:00:05.");
    }

    [Test]
    public void AtMost_CustomMessage()
    {
        _builder.AtMost(TimeSpan.FromSeconds(5), _customMessage);
        ShouldFail(TimeSpan.FromSeconds(6), _customMessage);
    }

    [Test]
    public void Between_ShouldSucceed()
    {
        _builder.Between(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10));
        ShouldSucceed(TimeSpan.FromSeconds(7));
    }

    [Test]
    public void Between_ShouldFail()
    {
        _builder.Between(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10));
        ShouldFail(TimeSpan.FromSeconds(4), "TimeSpan must be between 00:00:05 and 00:00:10.");
        ShouldFail(TimeSpan.FromSeconds(11), "TimeSpan must be between 00:00:05 and 00:00:10.");
    }

    [Test]
    public void Between_CustomMessage()
    {
        _builder.Between(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10), _customMessage);
        ShouldFail(TimeSpan.FromSeconds(4), _customMessage);
        ShouldFail(TimeSpan.FromSeconds(11), _customMessage);
    }

    [Test]
    public void Around_ShouldSucceed()
    {
        _builder.Around(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(2));
        ShouldSucceed(TimeSpan.FromSeconds(6));
    }

    [Test]
    public void Around_ShouldFail()
    {
        _builder.Around(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(2));
        ShouldFail(TimeSpan.FromSeconds(2), "TimeSpan must be around 00:00:05 with a tolerance of 00:00:02.");
        ShouldFail(TimeSpan.FromSeconds(8), "TimeSpan must be around 00:00:05 with a tolerance of 00:00:02.");
    }

    [Test]
    public void Around_CustomMessage()
    {
        _builder.Around(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(2), _customMessage);
        ShouldFail(TimeSpan.FromSeconds(2), _customMessage);
        ShouldFail(TimeSpan.FromSeconds(8), _customMessage);
    }

    private void ShouldSucceed(TimeSpan value)
    {
        _registered.ShouldNotBeNull();
        var failures = _registered(value).ToArray();
        failures.ShouldBeEmpty();
    }

    private void ShouldFail(TimeSpan value, string expectedMessage)
    {
        _registered.ShouldNotBeNull();
        var failures = _registered(value).ToArray();
        failures.Length.ShouldBe(1);
        failures[0].ValidationMessage.ShouldBe(expectedMessage);
    }
}
