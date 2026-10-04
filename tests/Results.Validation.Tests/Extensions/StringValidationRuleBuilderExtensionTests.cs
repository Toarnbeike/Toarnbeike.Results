using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Extensions;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Tests.Extensions;

public class StringValidationRuleBuilderExtensionTests
{
    private readonly string _customMessage = "Custom message";
    private Func<string, IEnumerable<ValidationFailure>>? _registered = null;
    private readonly ValidationRuleBuilder<string, string> _builder;

    public StringValidationRuleBuilderExtensionTests()
    {
        _builder = new ValidationRuleBuilder<string, string>(
            r => _registered = r,
            (value, rule) => rule.Predicate(value)
                ? []
                : [new ValidationFailure("value", rule.Message)]);
    }

    [Test]
    public void NotEmpty_ShouldSucceed()
    {
        _builder.NotEmpty();
        ShouldSucceed("abc");
    }

    [Test]
    public void NotEmpty_ShouldFail()
    {
        _builder.NotEmpty();
        ShouldFail("", "Value must not be empty.");
    }

    [Test]
    public void NotEmpty_CustomMessage()
    {
        _builder.NotEmpty(_customMessage);
        ShouldFail("", _customMessage);
    }

    [Test]
    public void NotWhiteSpace_ShouldSucceed()
    {
        _builder.NotWhiteSpace();
        ShouldSucceed("abc");
    }

    [Test]
    public void NotWhiteSpace_ShouldFail()
    {
        _builder.NotWhiteSpace();
        ShouldFail("   ", "Value must not be whitespace.");
    }

    [Test]
    public void NotWhiteSpace_CustomMessage()
    {
        _builder.NotWhiteSpace(_customMessage);
        ShouldFail("   ", _customMessage);
    }

    [Test]
    public void MinLength_ShouldSucceed()
    {
        _builder.MinLength(3);
        ShouldSucceed("abc");
    }

    [Test]
    public void MinLength_ShouldFail()
    {
        _builder.MinLength(3);
        ShouldFail("ab", "Value must be at least 3 characters long.");
    }

    [Test]
    public void MinLength_CustomMessage()
    {
        _builder.MinLength(3, _customMessage);
        ShouldFail("ab", _customMessage);
    }


    [Test]
    public void MaxLength_ShouldSucceed()
    {
        _builder.MaxLength(3);
        ShouldSucceed("abc");
    }

    [Test]
    public void MaxLength_ShouldFail()
    {
        _builder.MaxLength(3);
        ShouldFail("abcd", "Value must be at most 3 characters long.");
    }

    [Test]
    public void MaxLength_CustomMessage()
    {
        _builder.MaxLength(3, _customMessage);
        ShouldFail("abcd", _customMessage);
    }

    [Test]
    public void LengthBetween_ShouldSucceed()
    {
        _builder.LengthBetween(2, 4);
        ShouldSucceed("abc");
    }

    [Test]
    public void LengthBetween_ShouldFail()
    {
        _builder.LengthBetween(2, 4);
        ShouldFail("a", "Value must be between 2 and 4 characters long.");
        ShouldFail("abcde", "Value must be between 2 and 4 characters long.");
    }

    [Test]
    public void LengthBetween_CustomMessage()
    {
        _builder.LengthBetween(2, 4, _customMessage);
        ShouldFail("a", _customMessage);
        ShouldFail("abcde", _customMessage);
    }

    [Test]
    public void MatchesRegex_ShouldSucceed()
    {
        _builder.MatchesRegex(@"^\d+$");
        ShouldSucceed("12345");
    }

    [Test]
    public void MatchesRegex_ShouldFail()
    {
        _builder.MatchesRegex(@"^\d+$");
        ShouldFail("abc", @"Value must match the pattern '^\d+$'.");
    }

    [Test]
    public void MatchesRegex_CustomMessage()
    {
        _builder.MatchesRegex(@"^\d+$", _customMessage);
        ShouldFail("abc", _customMessage);
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
