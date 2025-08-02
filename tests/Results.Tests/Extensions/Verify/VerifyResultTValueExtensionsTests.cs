using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Tests.Internal;

namespace Toarnbeike.Results.Tests.Extensions.Verify;

/// <summary>
/// Tests for the <see cref="VerifyExtensions"/> on a <see cref="Result{TValue}"/>.
/// </summary>
public class VerifyResultTValueExtensionsTests
{
    private readonly Result<double> _success = Result.Success(1.3);
    private readonly Result<double> _failure = Result<double>.Failure(new Failure("original", "Original failure"));

    private readonly Task<Result<double>> _successTask = Task.FromResult(Result.Success(1.3));
    private readonly Task<Result<double>> _failureTask = Task.FromResult(Result<double>.Failure(new Failure("original", "Original failure")));

    private readonly Func<double, Result> _successFunc = value => Result.Success((int)value);
    private readonly Func<double, Result> _failureFunc = _ => Result.Failure(new Failure("Verify", "Verify failure"));
    private readonly Func<double, Result> _forbiddenFunc = _ => throw new InvalidOperationException("This function should not be called");

    private readonly Func<double, Result<int>> _successOfTFunc = value => Result.Success((int)value);
    private readonly Func<double, Result<int>> _failureOfTFunc = _ => Result<int>.Failure(new Failure("Verify", "Verify failure"));
    private readonly Func<double, Result<int>> _forbiddenOfTFunc = _ => throw new InvalidOperationException("This function should not be called");

    private readonly Func<double, Task<Result>> _successTaskFunc = value => Task.FromResult((Result)Result.Success((int)value));
    private readonly Func<double, Task<Result>> _failureTaskFunc = _ => Task.FromResult(Result.Failure(new Failure("Verify", "Verify failure")));
    private readonly Func<double, Task<Result>> _forbiddenTaskFunc = _ => throw new InvalidOperationException("This function should not be called");

    private readonly Func<double, Task<Result<int>>> _successOfTTaskFunc = _ => Task.FromResult(Result<int>.Success(42));
    private readonly Func<double, Task<Result<int>>> _failureOfTTaskFunc = _ => Task.FromResult(Result<int>.Failure(new Failure("Verify", "Verify failure")));
    private readonly Func<double, Task<Result<int>>> _forbiddenOfTTaskFunc = _ => throw new InvalidOperationException("This function should not be called");

    [Fact]
    public void Verify_Should_ReturnSuccess_WhenResultIsSuccess_AndFunctionSucceeds()
    {
        var result = _success.Verify(_successFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Verify_Should_ReturnFailure_WhenResultIsSuccess_AndFunctionFails()
    {
        var result = _success.Verify(_failureFunc);
        result.ShouldBeFailureWithCodeAndMessage("Verify", "Verify failure");
    }

    [Fact]
    public void Verify_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = _failure.Verify(_forbiddenFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public void Verify_Should_ReturnSuccess_WhenResultIsSuccess_AndFunctionOfTSucceeds()
    {
        var result = _success.Verify(_successOfTFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Verify_Should_ReturnFailure_WhenResultIsSuccess_AndFunctionOfTFails()
    {
        var result = _success.Verify(_failureOfTFunc);
        result.ShouldBeFailureWithCodeAndMessage("Verify", "Verify failure");
    }

    [Fact]
    public void Verify_Should_ReturnFailure_WhenResultIsFailure_AndCheckIsOfT()
    {
        var result = _failure.Verify(_forbiddenOfTFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task VerifyAsync_Should_ReturnValue_WhenResultIsSuccess_AndFunctionSucceeds()
    {
        var result = await _success.VerifyAsync(_successTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task VerifyAsync_Should_ReturnFailure_WhenResultIsSuccess_AndFunctionFails()
    {
        var result = await _success.VerifyAsync(_failureTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("Verify", "Verify failure");
    }

    [Fact]
    public async Task VerifyAsync_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = await _failure.VerifyAsync(_forbiddenTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task VerifyAsync_Should_ReturnValue_WhenResultIsSuccess_AndFunctionOfTSucceeds()
    {
        var result = await _success.VerifyAsync(_successOfTTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task VerifyAsync_Should_ReturnFailure_WhenResultIsSuccess_AndFunctionOfTFails()
    {
        var result = await _success.VerifyAsync(_failureOfTTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("Verify", "Verify failure");
    }

    [Fact]
    public async Task VerifyAsync_Should_ReturnFailure_WhenResultIsFailure_AndCheckIsOfT()
    {
        var result = await _failure.VerifyAsync(_forbiddenOfTTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task Verify_Should_ReturnValue_WhenResultTaskIsSuccess_AndFunctionSucceeds()
    {
        var result = await _successTask.Verify(_successFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task Verify_Should_ReturnFailure_WhenResultTaskIsSuccess_AndFunctionFails()
    {
        var result = await _successTask.Verify(_failureFunc);
        result.ShouldBeFailureWithCodeAndMessage("Verify", "Verify failure");
    }

    [Fact]
    public async Task Verify_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.Verify(_forbiddenFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task Verify_Should_ReturnValue_WhenResultTaskIsSuccess_AndFunctionOfTSucceeds()
    {
        var result = await _successTask.Verify(_successOfTFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task Verify_Should_ReturnFailure_WhenResultTaskIsSuccess_AndFunctionOfTFails()
    {
        var result = await _successTask.Verify(_failureOfTFunc);
        result.ShouldBeFailureWithCodeAndMessage("Verify", "Verify failure");
    }

    [Fact]
    public async Task Verify_Should_ReturnFailure_WhenResultTaskIsFailure_AndCheckIsOfT()
    {
        var result = await _failureTask.Verify(_forbiddenOfTFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task VerifyAsync_Should_ReturnValue_WhenResultTaskIsSuccess_AndFunctionSucceeds()
    {
        var result = await _successTask.VerifyAsync(_successTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task VerifyAsync_Should_ReturnFailure_WhenResultTaskIsSuccess_AndFunctionFails()
    {
        var result = await _successTask.VerifyAsync(_failureTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("Verify", "Verify failure");
    }

    [Fact]
    public async Task VerifyAsync_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.VerifyAsync(_forbiddenTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task VerifyAsync_Should_ReturnValue_WhenResultTaskIsSuccess_AndFunctionOfTSucceeds()
    {
        var result = await _successTask.VerifyAsync(_successOfTTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task VerifyAsync_Should_ReturnFailure_WhenResultTaskIsSuccess_AndFunctionOfTFails()
    {
        var result = await _successTask.VerifyAsync(_failureOfTTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("Verify", "Verify failure");
    }

    [Fact]
    public async Task VerifyAsync_Should_ReturnFailure_WhenResultTaskIsFailure_AndCheckIsOfT()
    {
        var result = await _failureTask.VerifyAsync(_forbiddenOfTTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }
}
