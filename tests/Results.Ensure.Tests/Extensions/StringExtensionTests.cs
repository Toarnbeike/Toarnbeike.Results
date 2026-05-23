using Toarnbeike.Results.Ensure.Extensions;

namespace Toarnbeike.Results.Ensure.Tests.Extensions;

public class StringExtensionTests
{
    private readonly string _empty = string.Empty;
    private readonly string _value = @"H€//☺";
    private readonly string _invalidUri = "http://[invalid";

    [Test]
    public void NotEmpty_Should_ReturnFormattedFailure()
    {
        var result = _empty.NotEmpty();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _empty,
            expectedMessage: "'_empty' must not be null or empty, but is."
        );
    }

    [Test]
    public void NotWhiteSpace_Should_ReturnFormattedFailure()
    {
        var result = _empty.NotWhiteSpace();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _empty,
            expectedMessage: "'_empty' must not be null or whitespace, but is."
        );
    }

    [Test]
    public void MinLength_Should_ReturnFormattedFailure()
    {
        var result = _value.MinLength(6);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must have at least 6 characters, but is 5 characters long."
        );
    }

    [Test]
    public void MaxLength_Should_ReturnFormattedFailure()
    {
        var result = _value.MaxLength(4);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must have at most 4 characters, but is 5 characters long."
        );
    }

    [Test]
    public void Between_Should_ReturnFormattedFailure()
    {
        var result = _value.LengthBetween(1,4);
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must have a length between 1 and 4 characters, but is 5 characters long."
        );
    }

    [Test]
    public void Match_Should_ReturnFormattedFailure()
    {
        var result = _value.Matches("abc");
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must match the pattern 'abc', but does not."
        );
    }

    [Test]
    public void Alphabetic_Should_ReturnFormattedFailure()
    {
        var result = _value.Alphabetic();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must contain only alphabetic characters, but is H€//☺."
        );
    }

    [Test]
    public void AlphaNumeric_Should_ReturnFormattedFailure()
    {
        var result = _value.AlphaNumeric();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must contain only alphabetic and numeric characters, but is H€//☺."
        );
    }

    [Test]
    public void Numeric_Should_ReturnFormattedFailure()
    {
        var result = _value.Numeric();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must contain only digits, but is H€//☺."
        );
    }

    [Test]
    public void Ascii_Should_ReturnFormattedFailure()
    {
        var result = _value.Ascii();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must contain only valid ASCII characters, but is H€//☺."
        );
    }

    [Test]
    public void EmailAddress_Should_ReturnFormattedFailure()
    {
        var result = _value.EmailAddress();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must be a valid email address, but is H€//☺."
        );
    }

    [Test]
    public void Uri_Should_ReturnFormattedFailure()
    {
        var result = _invalidUri.Uri();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _invalidUri,
            expectedMessage: "'_invalidUri' must be a valid URI, but is http://[invalid."
        );
    }

    [Test]
    public void AbsoluteUri_Should_ReturnFormattedFailure()
    {
        var result = _invalidUri.AbsoluteUri();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _invalidUri,
            expectedMessage: "'_invalidUri' must be a valid absolute URI, but is http://[invalid."
        );
    }

    [Test]
    public void RelativeUri_Should_ReturnFormattedFailure()
    {
        var result = _invalidUri.RelativeUri();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _invalidUri,
            expectedMessage: "'_invalidUri' must be a valid relative URI, but is http://[invalid."
        );
    }

    [Test]
    public void IpAddress_Should_ReturnFormattedFailure()
    {
        var result = _value.IpAddress();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must be a valid IP address, but is H€//☺."
        );
    }

    [Test]
    public void Slug_Should_ReturnFormattedFailure()
    {
        var result = _value.Slug();
        result.ShouldBeGuardFailure(
            expectedAttemptedValue: _value,
            expectedMessage: "'_value' must be a valid slug, but is H€//☺."
        );
    }
}