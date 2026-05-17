using System.Text.RegularExpressions;
using Toarnbeike.Results.Ensure.Implementations_Obsolete;

#pragma warning disable SYSLIB1045 // inline regexes for testing purposes is fine

namespace Toarnbeike.Results.Ensure.Tests.Implementations;

public class StringGuardsTests
{
    private readonly string _value = "Hello";

    private readonly Regex _matchRegex = new("Hello");
    private readonly Regex _doesNotMatchRegex = new("^(?!Hello).*$");

    [Test]
    public void NotEmpty_Should_ReturnFailure_WhenEmpty()
    {
        var result = StringGuards.NotEmpty("");
        result.IsValid.ShouldBeFalse();
        result.Constraint.ShouldBeNull();
    }

    [Test]
    public void NotEmpty_Should_ReturnSuccess_WhenNotEmpty()
    {
        var result = StringGuards.NotEmpty(_value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void NotWhiteSpace_Should_ReturnFailure_WhenEmpty()
    {
        var result = StringGuards.NotWhiteSpace("");
        result.IsValid.ShouldBeFalse();
        result.Constraint.ShouldBeNull();
    }

    [Test]
    public void NotWhiteSpace_Should_ReturnFailure_WhenWhiteSpace()
    {
        var result = StringGuards.NotWhiteSpace(" ");
        result.IsValid.ShouldBeFalse();
        result.Constraint.ShouldBeNull();
    }

    [Test]
    public void NotWhiteSpace_Should_ReturnSuccess_WhenNotWhiteSpace()
    {
        var result = StringGuards.NotWhiteSpace(_value);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void MinLength_Should_ReturnFailure_WhenShorter()
    {
        var result = StringGuards.MinLength(_value, 10);
        result.IsValid.ShouldBeFalse();
        result.Constraint.ShouldBe(10);
    }

    [Test]
    public void MinLength_Should_ReturnSuccess_WhenEqualLength()
    {
        var result = StringGuards.MinLength(_value, 5);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void MaxLength_Should_ReturnFailure_WhenLonger()
    {
        var result = StringGuards.MaxLength(_value, 4);
        result.IsValid.ShouldBeFalse();
        result.Constraint.ShouldBe(4);
    }

    [Test]
    public void MaxLength_Should_ReturnSuccess_WhenEqualLength()
    {
        var result = StringGuards.MaxLength(_value, 5);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void Matches_Should_ReturnFailure_WhenRegexDoesNotMatch()
    {
        var result = StringGuards.Matches(_value, _doesNotMatchRegex);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void Matches_Should_ReturnSuccess_WhenRegexDoesMatch()
    {
        var result = StringGuards.Matches(_value, _matchRegex);
        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public void ValidEmail_Should_ReturnFailure_WhenInputIsInvalid()
    {
        // no need to test all different types of invalid mail addresses, rely on MailAddress.TryCreate.
        var result = StringGuards.ValidEmail(_value);
        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void ValidEmail_Should_ReturnSuccess_WhenInputIsValid()
    {
        // no need to test all different types of valid mail addresses, rely on MailAddress.TryCreate.
        var result = StringGuards.ValidEmail("test@domain.com");
        result.IsValid.ShouldBeTrue();
    }
}