using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.TestExtensions;
using Toarnbeike.Results.Ensure.Extensions;

namespace Toarnbeike.Results.Ensure.Tests.Extensions;

public class StringExtensionTests
{
    [Test]
    public void NotEmpty_Should_ReturnFailure_WhenEmpty()
    {
        string value = string.Empty;
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).NotEmpty();

        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Value must not be empty.");
    }

    [Test]
    public void NotEmpty_Should_ReturnSuccess_WhenNotEmpty()
    {
        string value = "a";
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).NotEmpty();

        result.ShouldBeSuccess();
    }

    [Test]
    public void NotEmpty_Should_UseCustomMessage_WhenProvided()
    {
        string value = string.Empty;
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).NotEmpty("Custom message");
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }

    [Test]
    public void NotWhiteSpace_Should_ReturnFailure_WhenWhiteSpace()
    {
        string value = "   ";
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).NotWhiteSpace();
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Value must not be whitespace.");
    }

    [Test]
    public void NotWhiteSpace_Should_ReturnSuccess_WhenNotWhiteSpace()
    {
        string value = "a";
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).NotWhiteSpace();
        result.ShouldBeSuccess();
    }

    [Test]
    public void NotWhiteSpace_Should_UseCustomMessage_WhenProvided()
    {
        string value = "   ";
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).NotWhiteSpace("Custom message");
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }

    [Test]
    public void MinLength_Should_ReturnFailure_WhenTooShort()
    {
        string value = "ab";
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).MinLength(3);

        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Value must be at least 3 characters long.");
    }

    [Test]
    public void MinLength_Should_ReturnSuccess_WhenLongEnough()
    {
        string value = "abcd";
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).MinLength(3);

        result.ShouldBeSuccess();
    }

    [Test]
    public void MinLength_Should_UseCustomMessage_WhenProvided()
    {
        string value = "ab";
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).MinLength(3, "Custom message");
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }

    [Test]
    public void MaxLength_Should_ReturnFailure_WhenTooLong()
    {
        string value = "abcdef";
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).MaxLength(5);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Value must be at most 5 characters long.");
    }

    [Test]
    public void MaxLength_Should_ReturnSuccess_WhenShortEnough()
    {
        string value = "abcd";
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).MaxLength(5);

        result.ShouldBeSuccess();
    }

    [Test]
    public void MaxLength_Should_UseCustomMessage_WhenProvided()
    {
        string value = "abcdef";
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).MaxLength(5, "Custom message");
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }

    [Test]
    public void LengthBetween_Should_ReturnFailure_WhenOutsideRange()
    {
        string value = "abc";
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).LengthBetween(4, 6);
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Value must be between 4 and 6 characters long.");
    }

    [Test]
    public void LengthBetween_Should_ReturnSuccess_WhenWithinRange()
    {
        string value = "abcde";
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).LengthBetween(4, 6);
        result.ShouldBeSuccess();
    }

    [Test]
    public void LengthBetween_Should_UseCustomMessage_WhenProvided()
    {
        string value = "abc";
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).LengthBetween(4, 6, "Custom message");
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }

    [Test]
    public void MatchesRegex_Should_ReturnFailure_WhenNotMatch()
    {
        string value = "abc";
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).MatchesRegex("^\\d+$");

        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Value must match the pattern '^\\d+$'.");
    }

    [Test]
    public void MatchesRegex_Should_ReturnSuccess_WhenMatch()
    {
        string value = "123";
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).MatchesRegex("^\\d+$");

        result.ShouldBeSuccess();
    }

    [Test]
    public void MatchesRegex_Should_UseCustomMessage_WhenProvided()
    {
        string value = "abc";
        var result =
            Result.Success()
            .WithValue(value)
            .Ensure(x => x).MatchesRegex("^\\d+$", "Custom message");
        var failure = result.ShouldBeFailureOfType<GuardFailure>();
        failure.Message.ShouldBe("Custom message");
    }
}
