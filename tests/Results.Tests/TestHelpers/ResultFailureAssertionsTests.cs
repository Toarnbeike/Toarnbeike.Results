using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestHelpers;

namespace Toarnbeike.Results.Tests.TestHelpers;

/// <summary>
/// Tests for the <see cref="ResultFailureAssertions"/>.
/// </summary>
public class ResultFailureAssertionsTests
{
    [Fact]
    public void ShouldBeFailure_ReturnsFailure_WhenResultIsFailure()
    {
        var failure = new Failure("X", "fail");
        var result = Result.Failure(failure);

        var actual = result.ShouldBeFailure();
        actual.ShouldBe(failure);
    }

    [Fact]
    public void ShouldBeFailure_Throws_WhenResultIsSuccess()
    {
        var result = Result.Success();

        var ex = Should.Throw<ResultAssertionException>(result.ShouldBeFailure);
        ex.Message.ShouldBe("Expected failure result, but got success.");
    }

    [Fact]
    public void ShouldBeFailure_Throws_WhenResultIsNull()
    {
        Result result = null!;

        var ex = Should.Throw<ResultAssertionException>(result.ShouldBeFailure);
        ex.Message.ShouldBe("Expected result to be non-null.");
    }

    [Fact]
    public void ShouldBeFailureWithCode_ReturnsFailure_WhenCodeMatches()
    {
        var failure = new Failure("X", "fail");
        var result = Result.Failure(failure);

        var actual = result.ShouldBeFailureWithCode("X");
        actual.ShouldBe(failure);
    }

    [Fact]
    public void ShouldBeFailureWithCode_Throws_WhenCodeDoesNotMatches()
    {
        var failure = new Failure("X", "fail");
        var result = Result.Failure(failure);

        var ex = Should.Throw<ResultAssertionException>(() => result.ShouldBeFailureWithCode("Y"));
        ex.Message.ShouldBe("Expected failure result with code 'Y', but got 'X'.");
    }

    [Fact]
    public void ShouldBeFailureWithCodeWithCustomMessage_Throws_WhenCodeDoesNotMatches()
    {
        var failure = new Failure("X", "fail");
        var result = Result.Failure(failure);

        var ex = Should.Throw<ResultAssertionException>(() => result.ShouldBeFailureWithCode("Y", "customMessage"));
        ex.Message.ShouldBe("customMessage");
    }

    [Fact]
    public void ShouldBeFailureWithMessage_ReturnsFailure_WhenMessageMatches()
    {
        var failure = new Failure("X", "fail");
        var result = Result.Failure(failure);

        var actual = result.ShouldBeFailureWithMessage("fail");
        actual.ShouldBe(failure);
    }

    [Fact]
    public void ShouldBeFailureWithMessage_Throws_WhenMessageDoesNotMatches()
    {
        var failure = new Failure("X", "fail");
        var result = Result.Failure(failure);

        var ex = Should.Throw<ResultAssertionException>(() => result.ShouldBeFailureWithMessage("failure"));
        ex.Message.ShouldBe("Expected failure result with message 'failure', but got 'fail'.");
    }

    [Fact]
    public void ShouldBeFailureWithMessageWithCustomMessage_Throws_WhenMessageDoesNotMatches()
    {
        var failure = new Failure("X", "fail");
        var result = Result.Failure(failure);

        var ex = Should.Throw<ResultAssertionException>(() => result.ShouldBeFailureWithMessage("failure", "customMessage"));
        ex.Message.ShouldBe("customMessage");
    }

    [Fact]
    public void ShouldBeFailureWithCodeAndMessage_ReturnsFailure_WhenCodeAndMessageMatches()
    {
        var failure = new Failure("X", "fail");
        var result = Result.Failure(failure);

        var actual = result.ShouldBeFailureWithCodeAndMessage("X", "fail");
        actual.ShouldBe(failure);
    }

    [Fact]
    public void ShouldBeFailureWithCodeAndMessage_Throws_WhenCodeDoesNotMatches()
    {
        var failure = new Failure("X", "fail");
        var result = Result.Failure(failure);

        var ex = Should.Throw<ResultAssertionException>(() => result.ShouldBeFailureWithCodeAndMessage("Y", "fail"));
        ex.Message.ShouldBe("Expected failure result with code 'Y' and message 'fail', but got code 'X' and message 'fail'.");
    }

    [Fact]
    public void ShouldBeFailureWithCodeAndMessage_Throws_WhenMessageDoesNotMatches()
    {
        var failure = new Failure("X", "fail");
        var result = Result.Failure(failure);

        var ex = Should.Throw<ResultAssertionException>(() => result.ShouldBeFailureWithCodeAndMessage("X", "failure"));
        ex.Message.ShouldBe("Expected failure result with code 'X' and message 'failure', but got code 'X' and message 'fail'.");
    }

    [Fact]
    public void ShouldBeFailureWithCodeAndMessageWithCustomMessage_Throws_WhenMessageDoesNotMatches()
    {
        var failure = new Failure("X", "fail");
        var result = Result.Failure(failure);

        var ex = Should.Throw<ResultAssertionException>(() => result.ShouldBeFailureWithMessage("failure", "customMessage"));
        ex.Message.ShouldBe("customMessage");
    }

    [Fact]
    public void ShouldBeFailureOfType_Passes_WhenCorrectType()
    {
        var result = Result.Failure(new ValidationFailure("Field", "required"));
        var typed = result.ShouldBeFailureOfType<ValidationFailure>();
        typed.Property.ShouldBe("Field");
    }

    [Fact]
    public void ShouldBeFailureOfType_Throws_WhenWrongType()
    {
        var result = Result.Failure(new Failure("X", "msg"));

        var ex = Should.Throw<ResultAssertionException>(() =>
            result.ShouldBeFailureOfType<ValidationFailure>());

        ex.Message.ShouldBe("Expected failure of type 'ValidationFailure', but got 'Failure'.");
    }

    [Fact]
    public void ShouldBeFailureOfType_ThrowsWithCustomMessage_WhenWrongType()
    {
        var result = Result.Failure(new Failure("X", "msg"));

        var ex = Should.Throw<ResultAssertionException>(() =>
            result.ShouldBeFailureOfType<ValidationFailure>("custom message"));

        ex.Message.ShouldBe("custom message");
    }

    [Fact]
    public void ShouldBeFailureThatSatisfiesPredicate_Passes_WhenMatch()
    {
        var failure = new Failure("X", "Something");
        var result = Result.Failure(failure);

        var actual = result.ShouldBeFailureThatSatisfiesPredicate(f => f.Code == "X");
        actual.ShouldBe(failure);
    }

    [Fact]
    public void ShouldBeFailureThatSatisfiesPredicate_Throws_WhenNotMatch()
    {
        Result result = new Failure("X", "Something");

        var ex = Should.Throw<ResultAssertionException>(() =>
            result.ShouldBeFailureThatSatisfiesPredicate(f => f.Code == "OK"));

        ex.Message.ShouldBe("Expected failure result with a failure that satisfies the predicate, but it did not.");
    }

    [Fact]
    public void ShouldBeFailureThatSatisfiesPredicate_ThrowsWithCustomMessage_WhenNotMatch()
    {
        Result result = new Failure("X", "Something");

        var ex = Should.Throw<ResultAssertionException>(() =>
            result.ShouldBeFailureThatSatisfiesPredicate(f => f.Code == "OK", "custom message"));

        ex.Message.ShouldBe("custom message");
    }

    [Fact]
    public void ShouldBeFailureThatSatisfiesPredicate_Throws_WhenPredicateIsNull()
    {
        Result result = new Failure("X", "Something");

        Should.Throw<ArgumentNullException>(() =>
            result.ShouldBeFailureThatSatisfiesPredicate(null!));
    }
}