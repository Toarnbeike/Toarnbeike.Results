namespace Toarnbeike.Results.Tests;

public class ResultTests
{
    private readonly Failure _testFailure = new("test", "Test failure");

    [Test]
    public void Success_ShouldReturn_SuccessResult()
    {
        var result = Result.Success();
        result.IsSuccess.ShouldBeTrue();
        result.IsFailure.ShouldBeFalse();
    }

    [Test]
    public void Failure_ShouldReturn_FailureResult()
    {
        var result = Result.Failure(_testFailure);

        result.IsSuccess.ShouldBeFalse();
        result.IsFailure.ShouldBeTrue();
    }

    [Test]
    public void Failure_ShouldImplictlyCreate_FailureResult()
    {
        Result result = _testFailure;
        result.IsFailure.ShouldBeTrue();
    }

    [Test]
    public void TryGetFailure_ShouldReturn_True_WhenResultIsFailure()
    {
        var result = Result.Failure(_testFailure);

        result.TryGetFailure(out var actualFailure).ShouldBeTrue();
        actualFailure.ShouldNotBeNull();
        actualFailure.Code.ShouldBe(_testFailure.Code);
        actualFailure.Message.ShouldBe(_testFailure.Message);
    }

    [Test]
    public void TryGetFailure_ShouldReturn_False_WhenResultIsSuccess()
    {
        var result = Result.Success();
        result.TryGetFailure(out _).ShouldBeFalse();
    }
}
