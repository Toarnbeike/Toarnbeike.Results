//using System.Text.RegularExpressions;
//using Toarnbeike.Results.Ensure.Extensions;
//using Toarnbeike.Results.TestExtensions;
//// ReSharper disable JoinDeclarationAndInitializer - Here used explicitly for nullable strings.

//#pragma warning disable SYSLIB1045 // inline regexes for testing purposes is fine

//namespace Toarnbeike.Results.Ensure.Tests.Extensions;

//public class StringExtensionTests
//{
//    private readonly string _value = "Hello";
//    private readonly string _customMessage = "CustomFailureMessage";

//    [Test]
//    public void NotNullOrEmpty_Should_ReturnResultOfNonNullableString()
//    {
//        string? value; // explicitly declare as `string?`
//        value = "Hello";
//        var result = value.NotNullOrEmpty();
//        result.ShouldBeSuccess().ShouldBeOfType<string>();
//    }

//    [Test]
//    public void NotNullOrEmpty_Should_ReturnFormattedFailure_ForNull()
//    {
//        string? value = null;
//        var result = value.NotNullOrEmpty();
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("NotNullOrEmpty");
//        failure.Message.ShouldBe("'value' must not be null or whitespace, but is.");
//    }

//    [Test]
//    public void NotNullOrEmpty_Should_ReturnFormattedFailure_ForEmpty()
//    {
//        var value = "";
//        var result = value.NotNullOrEmpty();
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("NotNullOrEmpty");
//        failure.Message.ShouldBe("'value' must not be null or whitespace, but is.");
//    }

//    [Test]
//    public void NotNullOrEmpty_Should_ReturnFormattedFailure_ForWhitespace()
//    {
//        var value = " ";
//        var result = value.NotNullOrEmpty();
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("NotNullOrEmpty");
//        failure.Message.ShouldBe("'value' must not be null or whitespace, but is.");
//    }

//    [Test]
//    public void NotNullOrEmpty_Should_ReturnFormattedFailure_ForEmpty_WhenWhiteSpaceIsAllowed()
//    {
//        var value = "";
//        var result = value.NotNullOrEmpty(true);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("NotNullOrEmpty");
//        failure.Message.ShouldBe("'value' must not be null or empty, but is.");
//    }

//    [Test]
//    public void NotNullOrEmpty_Should_ReturnSuccess_ForWhiteSpace_WhenWhiteSpaceIsAllowed()
//    {
//        var value = " ";
//        value.NotNullOrEmpty(true).ShouldBeSuccess();
//    }

//    [Test]
//    public void NotNullOrEmpty_Should_ReturnCustomMessageFailure()
//    {
//        var value = "";
//        var result = value.NotNullOrEmpty(message: _customMessage);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.Message.ShouldBe(_customMessage);
//    }

//    [Test]
//    public void MinLength_Should_ReturnFormattedFailure()
//    {
//        var result = _value.MinLength(6);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("MinLength");
//        failure.Message.ShouldBe("'_value' must be at least 6 characters, but is 'Hello' (5).");
//    }

//    [Test]
//    public void MinLength_Should_ReturnCustomMessageFailure()
//    {
//        var result = _value.MinLength(6, _customMessage);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.Message.ShouldBe(_customMessage);
//    }

//    [Test]
//    public void MaxLength_Should_ReturnFormattedFailure()
//    {
//        var result = _value.MaxLength(4);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("MaxLength");
//        failure.Message.ShouldBe("'_value' must be at most 4 characters, but is 'Hello' (5).");
//    }

//    [Test]
//    public void MaxLength_Should_ReturnCustomMessageFailure()
//    {
//        var result = _value.MaxLength(4, _customMessage);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.Message.ShouldBe(_customMessage);
//    }

//    [Test]
//    public void LengthBetween_Should_ReturnFormattedFailure_WhenTooLong()
//    {
//        var result = _value.LengthBetween(2, 4);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("MaxLength");
//        failure.Message.ShouldBe("'_value' must be at most 4 characters, but is 'Hello' (5).");
//    }

//    [Test]
//    public void LengthBetween_Should_ReturnFormattedFailure_WhenTooShort()
//    {
//        var result = _value.LengthBetween(6, 10);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("MinLength");
//        failure.Message.ShouldBe("'_value' must be at least 6 characters, but is 'Hello' (5).");
//    }

//    [Test]
//    public void LengthBetween_Should_ReturnCustomMessageFailure()
//    {
//        var result = _value.LengthBetween(6, 10, _customMessage);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.Message.ShouldBe(_customMessage);
//    }

//    [Test]
//    public void Matches_Should_ReturnFormattedFailure()
//    {
//        var result = _value.Matches("abc");
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("Matches");
//        failure.Message.ShouldBe("'_value' must be in the correct format, but is 'Hello'.");
//    }

//    [Test]
//    public void Matches_Should_UseRegexOptions()
//    {
//        // Normally this would fail since the regex is in lowercase.
//        _value.Matches("hello", RegexOptions.IgnoreCase).ShouldBeSuccess();
//    }

//    [Test]
//    public void Matches_Should_ReturnCustomMessageFailure()
//    {
//        var result = _value.Matches("abc", message: _customMessage);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.Message.ShouldBe(_customMessage);
//    }

//    [Test]
//    public void ValidEmail_Should_ReturnFormattedFailure()
//    {
//        var result = _value.ValidEmail();
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.GuardName.ShouldBe("ValidEmail");
//        failure.Message.ShouldBe("'_value' must be a valid email address, but is 'Hello'.");
//    }

//    [Test]
//    public void ValidEmail_Should_ReturnCustomMessageFailure()
//    {
//        var result = _value.ValidEmail(_customMessage);
//        var failure = result.ShouldBeFailureOfType<GuardFailure>();
//        failure.Message.ShouldBe(_customMessage);
//    }
//}