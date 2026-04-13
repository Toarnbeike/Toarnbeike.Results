using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Tests.TestExtensions;

/// <summary>
/// Tests for the <see cref="ResultSuccessAssertions"/>.
/// </summary>
public class ResultSuccessAssertionTests
{
    [Test]
    public void ShouldBeSuccess_Passes_WhenResultIsSuccess()
    {
        var result = Result.Success();
        var act = () => result.ShouldBeSuccess();
        act.ShouldNotThrow();
    }

    [Test]
    public void ShouldBeSuccess_Throws_WhenResultIsNull()
    {
        Result result = null!;

        var ex = Should.Throw<ResultAssertionException>(() => result.ShouldBeSuccess());
        ex.Message.ShouldBe("Expected result to be non-null.");
    }

    [Test]
    public void ShouldBeSuccess_Throws_WhenResultIsFailure()
    {
        Result result = new Failure("fail", "Failed");

        var ex = Should.Throw<ResultAssertionException>(() => result.ShouldBeSuccess());
        ex.Message.ShouldBe("Expected success result, but got failure: 'Failed'.");
    }

    [Test]
    public void ShouldBeSuccess_Passes_WhenResultIsSuccessOfTValue()
    {
        var result = Result.Success(42);
        var actual = Should.NotThrow(() => result.ShouldBeSuccess());

        actual.ShouldBe(42);
    }

    [Test]
    public void ShouldBeSuccess_Throws_WhenResultOfTValueIsNull()
    {
        Result<int> result = default!;

        var ex = Should.Throw<ResultAssertionException>(() => result.ShouldBeSuccess());
        ex.Message.ShouldBe("Expected result to be non-null.");
    }

    [Test]
    public void ShouldBeSuccess_Throws_WhenResultIsFailureOfT()
    {
        Result<int> result = new Failure("fail", "Failed");

        var ex = Should.Throw<ResultAssertionException>(() => result.ShouldBeSuccess());
        ex.Message.ShouldBe("Expected success result, but got failure: 'Failed'.");
    }

    [Test]
    public void ShouldBeSuccessAsync_Passes_WhenResultIsSuccess()
    {
        var result = Task.FromResult(Result.Success());
        var act = async () => await result.ShouldBeSuccessAsync();
        act.ShouldNotThrow();
    }

    [Test]
    public async Task ShouldBeSuccessAsync_Throws_WhenResultIsFailure()
    {
        var result = Task.FromResult(Result.Failure(new Failure("fail", "Failed")));

        var ex = await Should.ThrowAsync<ResultAssertionException>(() => result.ShouldBeSuccessAsync());
        ex.Message.ShouldBe("Expected success result, but got failure: 'Failed'.");
    }

    [Test]
    public async Task ShouldBeSuccessAsync_Passes_WhenResultIsSuccessOfTValue()
    {
        var result = Task.FromResult(Result.Success(42));
        var actual = await result.ShouldBeSuccessAsync();

        actual.ShouldBe(42);
    }

    [Test]
    public async Task ShouldBeSuccessAsync_Throws_WhenResultIsFailureOfT()
    {
        var result = Task.FromResult(Result<int>.Failure(new Failure("fail", "Failed")));

        var ex = await Should.ThrowAsync<ResultAssertionException>(() => result.ShouldBeSuccessAsync());
        ex.Message.ShouldBe("Expected success result, but got failure: 'Failed'.");
    }
}