using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Extensions;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Tests.ValidationRuleBuilderExtensions;

public class GuidTests
{
    private Func<Guid, IEnumerable<ValidationFailure>> _registered = null!;
    private readonly ValidationRuleBuilder<Guid, Guid> _builder;

    public GuidTests()
    {
        _builder = new ValidationRuleBuilder<Guid, Guid>(
            registerRule: rule => _registered = rule,
            executor: (value, rule, message) =>
            {
                var result = rule(value);
                return result.IsValid
                    ? []
                    : [new ValidationFailure(nameof(Guid), message ?? result.Message)];
            });
    }

    [Test]
    public void NotEmpty_ShouldReturnNoFailures_WhenGuidIsNotEmpty()
    {
        _builder.NotEmpty();
        var failures = _registered(Guid.NewGuid());
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void NotEmpty_ShouldReturnFailure_WhenGuidIsEmpty()
    {
        _builder.NotEmpty();
        var failures = _registered(Guid.Empty).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void NotEmpty_ShouldUseCustomMessage_WhenProvided()
    {
        var customMessage = "Custom message";
        _builder.NotEmpty(customMessage);
        var failure = _registered(Guid.Empty).Single();
        failure.ValidationMessage.ShouldBe(customMessage);
    }

    [Test]
    public void Version4_ShouldReturnNoFailures_WhenGuidIsVersion4()
    {
        var guid = Guid.NewGuid();
        _builder.Version4();
        var failures = _registered(guid);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void Version4_ShouldReturnFailure_WhenGuidIsNotVersion4()
    {
        var guid = Guid.CreateVersion7();
        _builder.Version4();
        var failures = _registered(guid).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void Version4_ShouldUseCustomMessage_WhenProvided()
    {
        var customMessage = "Custom message";
        var guid = Guid.CreateVersion7();
        _builder.Version4(customMessage);
        var failure = _registered(guid).Single();
        failure.ValidationMessage.ShouldBe(customMessage);
    }

    [Test]
    public void Version7_ShouldReturnNoFailures_WhenGuidIsVersion7()
    {
        var guid = Guid.CreateVersion7();
        _builder.Version7();
        var failures = _registered(guid).ToArray();
        failures.Length.ShouldBe(0);
    }

    [Test]
    public void Version7_ShouldReturnFailure_WhenGuidIsNotVersion7()
    {
        var guid = Guid.NewGuid();
        _builder.Version7();
        var failures = _registered(guid).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void Version7_ShouldUseCustomMessage_WhenProvided()
    {
        var customMessage = "Custom message";
        var guid = Guid.NewGuid();
        _builder.Version7(customMessage);
        var failure = _registered(guid).Single();
        failure.ValidationMessage.ShouldBe(customMessage);
    }
}
