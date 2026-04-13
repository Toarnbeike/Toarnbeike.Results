using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Tests.TestExtensions;

/// <summary>
/// Tests for the <see cref="ResultFailureAssertions"/>.
/// </summary>
public class ResultFailureAssertionTests
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

    [Test]
    public async Task ShouldBeFailureAsync_ReturnsFailure_WhenResultIsFailure()
    {
        var failure = new Failure("X", "fail");
        var result = Task.FromResult(Result.Failure(failure));

        var actual = await result.ShouldBeFailureAsync();
        actual.ShouldBe(failure);
    }

    [Test]
    public async Task ShouldBeFailureAsync_Throws_WhenResultIsSuccess()
    {
        var result = Task.FromResult(Result.Success());

        var ex = await Should.ThrowAsync<ResultAssertionException>(() => result.ShouldBeFailureAsync());
        ex.Message.ShouldBe("Expected failure result, but got success.");
    }

    [Test]
    public async Task ShouldBeFailureAsync_ReturnsFailure_WhenResultIsFailure_ResultT()
    {
        var failure = new Failure("X", "fail");
        var result = Task.FromResult(Result<int>.Failure(failure));

        var actual = await result.ShouldBeFailureAsync();
        actual.ShouldBe(failure);
    }

    [Test]
    public async Task ShouldBeFailureAsync_Throws_WhenResultIsSuccess_ResultT()
    {
        var result = Task.FromResult(Result.Success(1));

        var ex = await Should.ThrowAsync<ResultAssertionException>(() => result.ShouldBeFailureAsync());
        ex.Message.ShouldBe("Expected failure result, but got success.");
    }

    [Test]
    public async Task ShouldBeFailureOfTypeAsync_Passes_WhenCorrectType()
    {
        var failure = new ValidationFailure("Field", "required");
        var result = Task.FromResult(Result.Failure(failure));

        var typed = await result.ShouldBeFailureOfTypeAsync<ValidationFailure>();
        typed.Property.ShouldBe("Field");
    }

    [Test]
    public async Task ShouldBeFailureOfTypeAsync_Throws_WhenWrongType()
    {
        var failure = new Failure("X", "msg");
        var result = Task.FromResult(Result.Failure(failure));

        var ex = await Should.ThrowAsync<ResultAssertionException>(() =>
            result.ShouldBeFailureOfTypeAsync<ValidationFailure>());

        ex.Message.ShouldBe("Expected failure of type 'ValidationFailure', but got 'Failure'.");
    }

    [Test]
    public async Task ShouldBeFailureOfTypeAsync_ThrowsWithCustomMessage_WhenWrongType()
    {
        var failure = new Failure("X", "msg");
        var result = Task.FromResult(Result.Failure(failure));

        var ex = await Should.ThrowAsync<ResultAssertionException>(() =>
            result.ShouldBeFailureOfTypeAsync<ValidationFailure>("custom message"));

        ex.Message.ShouldBe("custom message");
    }

    [Test]
    public async Task ShouldBeFailureOfTypeAsync_Passes_WhenCorrectType_ResultT()
    {
        var failure = new ValidationFailure("Field", "required");
        var result = Task.FromResult(Result<int>.Failure(failure));

        var typed = await result.ShouldBeFailureOfTypeAsync<ValidationFailure, int>();
        typed.Property.ShouldBe("Field");
    }

    [Test]
    public async Task ShouldBeFailureOfTypeAsync_Throws_WhenWrongType_ResultT()
    {
        var failure = new Failure("X", "msg");
        var result = Task.FromResult(Result<int>.Failure(failure));

        var ex = await Should.ThrowAsync<ResultAssertionException>(() =>
            result.ShouldBeFailureOfTypeAsync<ValidationFailure, int>());

        ex.Message.ShouldBe("Expected failure of type 'ValidationFailure', but got 'Failure'.");
    }

    [Test]
    public async Task ShouldBeFailureOfTypeAsync_ThrowsWithCustomMessage_WhenWrongType_ResultT()
    {
        var failure = new Failure("X", "msg");
        var result = Task.FromResult(Result<int>.Failure(failure));

        var ex = await Should.ThrowAsync<ResultAssertionException>(() =>
            result.ShouldBeFailureOfTypeAsync<ValidationFailure, int>("custom message"));

        ex.Message.ShouldBe("custom message");
    }
}