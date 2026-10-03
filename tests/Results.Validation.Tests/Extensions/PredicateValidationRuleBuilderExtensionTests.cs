using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Extensions;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Tests.Extensions;

public class PredicateValidationRuleBuilderExtensionTests
{
    private Func<string, IEnumerable<ValidationFailure>>? _registered = null;
    private readonly ValidationRuleBuilder<string, string> _builder;

    public PredicateValidationRuleBuilderExtensionTests()
    {
        _builder = new ValidationRuleBuilder<string, string>(
            r => _registered = r,
            (value, rule) => rule.Predicate(value)
                ? []
                : [new ValidationFailure("value", rule.Message)]);
    }

    [Test]
    public void Must_ShouldSucceed()
    {
        _builder.Must(s => s.Length > 3, "Value must be longer than 3 characters.");
        ShouldSucceed("abcd");
    }

    [Test]
    public void Must_ShouldFail()
    {
        _builder.Must(s => s.Length > 3, "Value must be longer than 3 characters.");
        ShouldFail("abc", "Value must be longer than 3 characters.");
    }

    [Test]
    public void MustNot_ShouldSucceed()
    {
        _builder.MustNot(s => s.Length > 3, "Value must not be longer than 3 characters.");
        ShouldSucceed("abc");
    }

    [Test]
    public void MustNot_ShouldFail()
    {
        _builder.MustNot(s => s.Length > 3, "Value must not be longer than 3 characters.");
        ShouldFail("abcd", "Value must not be longer than 3 characters.");
    }

    private void ShouldSucceed(string value)
    {
        _registered.ShouldNotBeNull();
        var failures = _registered(value).ToArray();
        failures.ShouldBeEmpty();
    }

    private void ShouldFail(string value, string expectedMessage)
    {
        _registered.ShouldNotBeNull();
        var failures = _registered(value).ToArray();
        failures.Length.ShouldBe(1);
        failures[0].ValidationMessage.ShouldBe(expectedMessage);
    }
}
