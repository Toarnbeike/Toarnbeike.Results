using Toarnbeike.Results.Failures;
using Toarnbeike.Results.Validation.Extensions;
using Toarnbeike.Results.Validation.Implementation;

namespace Toarnbeike.Results.Validation.Tests.ValidationRuleBuilderExtensions;

public class StringTests
{
    private readonly string _value = "hello";
    private readonly string _customMessage = "Custom message";
    private Func<string, IEnumerable<ValidationFailure>> _registered = null!;
    private readonly ValidationRuleBuilder<string, string> _builder;

    public StringTests()
    {
        _builder = new ValidationRuleBuilder<string, string>(
            registerRule: rule => _registered = rule,
            executor: (value, rule, message) =>
            {
                var result = rule(value);
                return result.IsValid
                    ? []
                    : [new ValidationFailure(nameof(String), message ?? result.Message)];
            });
    }

    [Test]
    public void NotEmpty_ShouldReturnNoFailures_WhenStringIsNotEmpty()
    {
        _builder.NotEmpty();
        var failures = _registered(_value);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void NotEmpty_ShouldReturnFailure_WhenStringIsEmpty()
    {
        _builder.NotEmpty();
        var failures = _registered(string.Empty).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void NotEmpty_ShouldUseCustomMessage_WhenProvided()
    {
        _builder.NotEmpty(_customMessage);
        var failure = _registered(string.Empty).Single();
        failure.ValidationMessage.ShouldBe(_customMessage);
    }

    [Test]
    public void NotWhiteSpace_ShouldReturnNoFailures_WhenStringIsNotWhitespace()
    {
        _builder.NotWhiteSpace();
        var failures = _registered(_value);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void NotWhiteSpace_ShouldReturnFailure_WhenStringIsWhitespace()
    {
        _builder.NotWhiteSpace();
        var failures = _registered("   ").ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void NotWhiteSpace_ShouldUseCustomMessage_WhenProvided()
    {
        _builder.NotWhiteSpace(_customMessage);
        var failure = _registered("   ").Single();
        failure.ValidationMessage.ShouldBe(_customMessage);
    }

    [Test]
    public void MinLength_ShouldReturnNoFailures_WhenStringMeetsMinLength()
    {
        _builder.MinLength(3);
        var failures = _registered(_value);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void MinLength_ShouldReturnFailure_WhenStringIsShorterThanMinLength()
    {
        _builder.MinLength(10);
        var failures = _registered(_value).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void MinLength_UseCustomMessage()
    {
        _builder.MinLength(10, _customMessage);
        var failure = _registered(_value).Single();
        failure.ValidationMessage.ShouldBe(_customMessage);
    }

    [Test]
    public void MaxLength_ShouldReturnNoFailures_WhenStringIsShorterThanMaxLength()
    {
        _builder.MaxLength(10);
        var failures = _registered(_value);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void MaxLength_ShouldReturnFailure_WhenStringExceedsMaxLength()
    {
        _builder.MaxLength(3);
        var failures = _registered(_value).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void MaxLength_UseCustomMessage()
    {
        _builder.MaxLength(3, _customMessage);
        var failure = _registered(_value).Single();
        failure.ValidationMessage.ShouldBe(_customMessage);
    }

    [Test]
    public void LengthBetween_ShouldReturnNoFailures_WhenStringLengthIsWithinRange()
    {
        _builder.LengthBetween(3, 10);
        var failures = _registered(_value);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void LengthBetween_ShouldReturnFailure_WhenStringLengthIsOutsideRange()
    {
        _builder.LengthBetween(6, 10);
        var failures = _registered(_value).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void LengthBetween_UseCustomMessage()
    {
        _builder.LengthBetween(6, 10, _customMessage);
        var failure = _registered(_value).Single();
        failure.ValidationMessage.ShouldBe(_customMessage);
    }

    [Test]
    public void Matches_ShouldReturnNoFailures_WhenStringMatchesPattern()
    {
        _builder.Matches(@"^\w+$");
        var failures = _registered(_value);
        failures.Count().ShouldBe(0);
    }

    [Test]
    public void Matches_ShouldReturnFailure_WhenStringDoesNotMatchPattern()
    {
        _builder.Matches(@"^\d+$");
        var failures = _registered(_value).ToArray();
        failures.Length.ShouldBe(1);
    }

    [Test]
    public void Matches_UseCustomMessage()
    {
        _builder.Matches(@"^\d+$", message: _customMessage);
        var failure = _registered(_value).Single();
        failure.ValidationMessage.ShouldBe(_customMessage);
    }
}
