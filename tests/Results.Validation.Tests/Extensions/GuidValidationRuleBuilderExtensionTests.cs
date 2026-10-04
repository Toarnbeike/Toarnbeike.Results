using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Extensions;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Tests.Extensions;

public class GuidValidationRuleBuilderExtensionTests
{
    private readonly string _customMessage = "Custom message";
    private Func<Guid, IEnumerable<ValidationFailure>>? _registered = null;
    private readonly ValidationRuleBuilder<Guid, Guid> _builder;

    public GuidValidationRuleBuilderExtensionTests()
    {
        _builder = new ValidationRuleBuilder<Guid, Guid>(
            r => _registered = r,
            (value, rule) => rule.Predicate(value)
                ? []
                : [new ValidationFailure("value", rule.Message)]);
    }

    [Test]
    public void NotEmpty_ShouldSucceed()
    {
        _builder.NotEmpty();
        ShouldSucceed(Guid.NewGuid());
    }

    [Test]
    public void NotEmpty_ShouldFail()
    {
        _builder.NotEmpty();
        ShouldFail(Guid.Empty, "Guid must not be empty.");
    }

    [Test]
    public void NotEmpty_CustomMessage()
    {
        _builder.NotEmpty(_customMessage);
        ShouldFail(Guid.Empty, _customMessage);
    }

    [Test]
    public void Version4_ShouldSucceed()
    {
        _builder.Version4();
        ShouldSucceed(Guid.NewGuid());
    }

    [Test]
    public void Version4_ShouldFail()
    {
        _builder.Version4();
        ShouldFail(Guid.CreateVersion7(), "Guid must be version 4.");
    }

    [Test]
    public void Version4_CustomMessage()
    {
        _builder.Version4(_customMessage);
        ShouldFail(Guid.CreateVersion7(), _customMessage);
    }

    [Test]
    public void Version7_ShouldSucceed()
    {
        _builder.Version7();
        ShouldSucceed(Guid.CreateVersion7());
    }

    [Test]
    public void Version7_ShouldFail()
    {
        _builder.Version7();
        ShouldFail(Guid.NewGuid(), "Guid must be version 7.");
    }


    [Test]
    public void Version7_CustomMessage()
    {
        _builder.Version7(_customMessage);
        ShouldFail(Guid.NewGuid(), _customMessage);
    }

    private void ShouldSucceed(Guid value)
    {
        _registered.ShouldNotBeNull();
        var failures = _registered(value).ToArray();
        failures.ShouldBeEmpty();
    }

    private void ShouldFail(Guid value, string expectedMessage)
    {
        _registered.ShouldNotBeNull();
        var failures = _registered(value).ToArray();
        failures.Length.ShouldBe(1);
        failures[0].ValidationMessage.ShouldBe(expectedMessage);
    }
}
