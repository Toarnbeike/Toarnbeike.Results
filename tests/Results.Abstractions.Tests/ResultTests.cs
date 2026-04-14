namespace Toarnbeike.Results.Abstractions.Tests;

public class ResultTests
{
    private readonly TestFailure _testFailure = new("test", "Test failure");

    [Fact]
    public void Success_ShouldReturn_SuccessResult()
    {
        var result = Result.Success();
        result.IsSuccess.ShouldBeTrue();
        result.IsFailure.ShouldBeFalse();
    }

    [Fact]
    public void Failure_ShouldReturn_FailureResult()
    {
        var result = Result.Failure(_testFailure);

        result.IsSuccess.ShouldBeFalse();
        result.IsFailure.ShouldBeTrue();
    }

    [Fact]
    public void Failure_ShouldImplicitlyCreate_FailureResult()
    {
        Result result = _testFailure;
        result.IsFailure.ShouldBeTrue();
    }

    [Fact]
    public void TryGetFailure_ShouldReturn_True_WhenResultIsFailure()
    {
        var result = Result.Failure(_testFailure);

        result.TryGetFailure(out var actualFailure).ShouldBeTrue();
        actualFailure.ShouldNotBeNull();
        actualFailure.Message.ShouldBe(_testFailure.Message);
    }

    [Fact]
    public void TryGetFailure_ShouldReturn_False_WhenResultIsSuccess()
    {
        var result = Result.Success();
        result.TryGetFailure(out _).ShouldBeFalse();
    }
}
