using System.Text.RegularExpressions;
using Toarnbeike.Results.Guards.Rules;

namespace Toarnbeike.Results.Guards.Tests.Rules;

public class StringRuleTests
{
    private readonly string _empty = string.Empty;
    private readonly string _value = @"H€//☺";
    private readonly string _invalidUri = "http://[invalid";

    [Test]
    public void NotEmpty_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_empty).NotEmpty();
        result.AssertFailure(
            expectedAttemptedValue: _empty,
            expectedArgumentName: "_empty",
            expectedGuardName: "String.NotEmpty"
        );
    }

    [Test]
    public void NotWhiteSpace_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_empty).NotWhiteSpace();
        result.AssertFailure(
            expectedAttemptedValue: _empty,
            expectedArgumentName: "_empty",
            expectedGuardName: "String.NotWhiteSpace"
        );
    }

    [Test]
    public void MinLength_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var min = 6;
        var result = Result.Ensure().That(_value).MinLength(min);
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "String.MinLength",
            ("MinLength", min),
            ("ActualLength", _value.Length)
        );
    }

    [Test]
    public void MaxLength_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var max = 4;
        var result = Result.Ensure().That(_value).MaxLength(max);
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "String.MaxLength",
            ("MaxLength", max),
            ("ActualLength", _value.Length)
        );
    }

    [Test]
    public void LengthBetween_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var min = 6;
        var max = 10;
        var result = Result.Ensure().That(_value).LengthBetween(min, max);
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "String.LengthBetween",
            ("MinLength", min),
            ("MaxLength", max),
            ("ActualLength", _value.Length)
        );
    }

    [Test]
    public void Matches_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var pattern = "abc";
        var result = Result.Ensure().That(_value).Matches(pattern);
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "String.Matches",
            ("Pattern", pattern),
            ("Options", default(RegexOptions))
        );
    }

    [Test]
    public void Alphabetic_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_value).Alphabetic();
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "String.Alphabetic"
        );
    }

    [Test]
    public void AlphaNumeric_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_value).AlphaNumeric();
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "String.AlphaNumeric"
        );
    }

    [Test]
    public void Numeric_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_value).Numeric();
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "String.Numeric"
        );
    }

    [Test]
    public void EmailAddress_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_value).EmailAddress();
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "String.EmailAddress"
        );
    }

    [Test]
    public void Uri_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_invalidUri).Uri();
        result.AssertFailure(
            expectedAttemptedValue: _invalidUri,
            expectedArgumentName: "_invalidUri",
            expectedGuardName: "String.Uri"
        );
    }

    [Test]
    public void AbsoluteUri_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_invalidUri).AbsoluteUri();
        result.AssertFailure(
            expectedAttemptedValue: _invalidUri,
            expectedArgumentName: "_invalidUri",
            expectedGuardName: "String.AbsoluteUri"
        );
    }

    [Test]
    public void RelativeUri_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_invalidUri).RelativeUri();
        result.AssertFailure(
            expectedAttemptedValue: _invalidUri,
            expectedArgumentName: "_invalidUri",
            expectedGuardName: "String.RelativeUri"
        );
    }

    [Test]
    public void IpAddress_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_value).IpAddress();
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "String.IpAddress"
        );
    }

    [Test]
    public void Slug_Should_ReturnFailingRuleResult_WhenFailure()
    {
        var result = Result.Ensure().That(_value).Slug();
        result.AssertFailure(
            expectedAttemptedValue: _value,
            expectedArgumentName: "_value",
            expectedGuardName: "String.Slug"
        );
    }
}