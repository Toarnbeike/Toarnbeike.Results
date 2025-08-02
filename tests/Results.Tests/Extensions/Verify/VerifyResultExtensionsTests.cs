using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Tests.Internal;

namespace Toarnbeike.Results.Tests.Extensions.Verify;

/// <summary>
/// Tests for the <see cref="VerifyExtensions"/> on a <see cref="Result"/>.
/// </summary>
public class VerifyResultExtensionsTests
{
    private readonly Result _success = Result.Success();
    private readonly Result _failure = Result.Failure(new Failure("original", "Original failure"));

    private readonly Task<Result> _successTask = Task.FromResult(Result.Success());
    private readonly Task<Result> _failureTask = Task.FromResult(Result.Failure(new Failure("original", "Original failure")));

    private readonly Func<Result> _successFunc = Result.Success;
    private readonly Func<Result> _failureFunc = () => Result.Failure(new Failure("Verify", "Verify failure"));
    private readonly Func<Result> _forbiddenFunc = () => throw new InvalidOperationException("This function should not be called");

    private readonly Func<Result<int>> _successOfTFunc = () => Result.Success(42);
    private readonly Func<Result<int>> _failureOfTFunc = () => Result<int>.Failure(new Failure("Verify", "Verify failure"));
    private readonly Func<Result<int>> _forbiddenOfTFunc = () => throw new InvalidOperationException("This function should not be called");

    private readonly Func<Task<Result>> _successTaskFunc = () => Task.FromResult(Result.Success());
    private readonly Func<Task<Result>> _failureTaskFunc = () => Task.FromResult(Result.Failure(new Failure("Verify", "Verify failure")));
    private readonly Func<Task<Result>> _forbiddenTaskFunc = () => throw new InvalidOperationException("This function should not be called");

    private readonly Func<Task<Result<int>>> _successOfTTaskFunc = () => Task.FromResult(Result<int>.Success(42));
    private readonly Func<Task<Result<int>>> _failureOfTTaskFunc = () => Task.FromResult(Result<int>.Failure(new Failure("Verify", "Verify failure")));
    private readonly Func<Task<Result<int>>> _forbiddenOfTTaskFunc = () => throw new InvalidOperationException("This function should not be called");

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
