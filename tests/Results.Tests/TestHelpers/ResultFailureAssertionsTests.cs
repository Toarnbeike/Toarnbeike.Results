using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Tests.TestHelpers;

/// <summary>
/// Tests for the <see cref="ResultFailureAssertions"/>.
/// </summary>
public class ResultFailureAssertionsTests
{
    [Test]
    public void ShouldBeFailure_ReturnsFailure_WhenResultIsFailure()
    {
        var failure = new Failure("X", "fail");
        var result = Result.Failure(failure);

        var actual = result.ShouldBeFailure();
        actual.ShouldBe(failure);
    }

    [Test]
    public void ShouldBeFailure_Throws_WhenResultIsSuccess()
    {
        var result = Result.Success();

        var ex = Should.Throw<ResultAssertionException>(() => result.ShouldBeFailure());
        ex.Message.ShouldBe("Expected failure result, but got success.");
    }

    [Test]
    public void ShouldBeFailureOfType_Passes_WhenCorrectType()
    {
        var result = Result.Failure(new ValidationFailure("Field", "required"));
        var typed = result.ShouldBeFailureOfType<ValidationFailure>();
        typed.Property.ShouldBe("Field");
    }

    [Test]
    public void ShouldBeFailureOfType_Throws_WhenWrongType()
    {
        var result = Result.Failure(new Failure("X", "msg"));

        var ex = Should.Throw<ResultAssertionException>(() =>
            result.ShouldBeFailureOfType<ValidationFailure>());

        ex.Message.ShouldBe("Expected failure of type 'ValidationFailure', but got 'Failure'.");
    }

    [Test]
    public void ShouldBeFailureOfType_ThrowsWithCustomMessage_WhenWrongType()
    {
        var result = Result.Failure(new Failure("X", "msg"));

        var ex = Should.Throw<ResultAssertionException>(() =>
            result.ShouldBeFailureOfType<ValidationFailure>("custom message"));

        ex.Message.ShouldBe("custom message");
    }
}