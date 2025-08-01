namespace Toarnbeike.Results.Tests;

public class ResultTValueTests
{
    private readonly Failure _testFailure = new("test", "Test failure");
    private readonly int _expectedValue = 42;

    [Fact]
    public void Success_ShouldReturn_SuccessResult_WithValue()
    {
        var result = Result<int>.Success(_expectedValue);

        result.IsSuccess.ShouldBeTrue();
        result.IsFailure.ShouldBeFalse();
    }

    [Fact]
    public void Failure_ShouldReturn_FailureResult()
    {
        var result = Result<int>.Failure(_testFailure);

        result.IsSuccess.ShouldBeFalse();
        result.IsFailure.ShouldBeTrue();
    }

    [Fact]
    public void TryGetValue_ShouldReturn_True_WhenResultIsSuccess()
    {
        var result = Result<int>.Success(_expectedValue);

        result.TryGetValue(out var actualValue).ShouldBeTrue();

        actualValue.ShouldBe(_expectedValue);
    }

    [Fact]
    public void TryGetValue_ShouldReturn_False_WhenResultIsFailure()
    {
        var result = Result<int>.Failure(_testFailure);

        result.TryGetValue(out _).ShouldBeFalse();
    }

    [Fact]
    public void TryGetValue_ShouldReturn_Value_WhenResultIsFailure()
    {
        var result = Result<int>.Success(_expectedValue);

        result.TryGetValue(out var actualValue, out _).ShouldBeTrue();

        actualValue.ShouldBe(_expectedValue);
    }

    [Fact]
    public void TryGetValue_ShouldReturn_Failure_WhenResultIsFailure()
    {
        var result = Result<int>.Failure(_testFailure);

        result.TryGetValue(out _, out var failure).ShouldBeFalse();

        failure.ShouldNotBeNull();
        failure.Code.ShouldBe(_testFailure.Code);
        failure.Message.ShouldBe(_testFailure.Message);
    }

    [Fact]
    public void TryGetFailure_ShouldReturn_True_WhenResultIsFailure()
    {
        var result = Result<int>.Failure(_testFailure);

        result.TryGetFailure(out var actualFailure).ShouldBeTrue();
        actualFailure.ShouldNotBeNull();
        actualFailure.Code.ShouldBe(_testFailure.Code);
        actualFailure.Message.ShouldBe(_testFailure.Message);
    }

    [Fact]
    public void TryGetFailure_ShouldReturn_False_WhenResultIsSuccess()
    {
        var result = Result<int>.Success(_expectedValue);
        result.TryGetFailure(out _).ShouldBeFalse();
    }

    [Fact]
    public void Success_ShouldReturn_SuccessResult_WithValue_EvenWithoutTypeInfo()
    {
        var result = Result.Success(_expectedValue);
        
        result.IsSuccess.ShouldBeTrue();
        result.ShouldBeOfType<Result<int>>();
        result.TryGetValue(out var actualValue).ShouldBeTrue();
        actualValue.ShouldBe(_expectedValue);
    }

    [Fact]
    public void ImplicitConversion_ShouldConvert_FailureToResult()
    {
        Result<int> result = _testFailure;

        result.IsFailure.ShouldBeTrue();
    }

    [Fact]
    public void ImplicitConversion_ShouldConvert_TValueToResultTValue()
    {
        Result<int> result = _expectedValue;

        result.IsFailure.ShouldBeFalse();
    }

    [Fact]
    public void ImplicitConversion_ShouldConvert_ResultTValueToResult_SuccessPath()
    {
        Result result = Result<int>.Success(_expectedValue);

        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void ImplicitConversion_ShouldConvert_ResultTValueToResult_FailurePath()
    {
        Result result = Result<int>.Failure(_testFailure);

        result.IsSuccess.ShouldBeFalse();
    }
}