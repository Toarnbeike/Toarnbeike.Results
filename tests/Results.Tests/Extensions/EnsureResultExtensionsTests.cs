using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Tests.Internal;

namespace Toarnbeike.Results.Tests.Extensions;

/// <summary>
/// Tests for the <see cref="EnsureResultExtensions"/> class.
/// </summary>
public class EnsureResultExtensionsTests
{
    private readonly Result _success = Result.Success();
    private readonly Result _failure = Result.Failure(new Failure("original", "Original failure"));

    private readonly Task<Result> _successTask = Task.FromResult(Result.Success());
    private readonly Task<Result> _failureTask = Task.FromResult(Result.Failure(new Failure("original", "Original failure")));

    private readonly Func<Result> _successFunc = Result.Success;
    private readonly Func<Result> _failureFunc = () => Result.Failure(new Failure("ensure", "Ensure failure"));
    private readonly Func<Result> _forbiddenFunc = () => throw new InvalidOperationException("This function should not be called");

    private readonly Func<Result<int>> _successOfTFunc = () => Result.Success(42);
    private readonly Func<Result<int>> _failureOfTFunc = () => Result<int>.Failure(new Failure("ensure", "Ensure failure"));
    private readonly Func<Result<int>> _forbiddenOfTFunc = () => throw new InvalidOperationException("This function should not be called");

    private readonly Func<Task<Result>> _successTaskFunc = () => Task.FromResult(Result.Success());
    private readonly Func<Task<Result>> _failureTaskFunc = () => Task.FromResult(Result.Failure(new Failure("ensure", "Ensure failure")));
    private readonly Func<Task<Result>> _forbiddenTaskFunc = () => throw new InvalidOperationException("This function should not be called");

    private readonly Func<Task<Result<int>>> _successOfTTaskFunc = () => Task.FromResult(Result<int>.Success(42));
    private readonly Func<Task<Result<int>>> _failureOfTTaskFunc = () => Task.FromResult(Result<int>.Failure(new Failure("ensure", "Ensure failure")));
    private readonly Func<Task<Result<int>>> _forbiddenOfTTaskFunc = () => throw new InvalidOperationException("This function should not be called");

    [Fact]
    public void Ensure_Should_ReturnSuccess_WhenResultIsSuccess_AndFunctionSucceeds()
    {
        var result = _success.Ensure(_successFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Ensure_Should_ReturnFailure_WhenResultIsSuccess_AndFunctionFails()
    {
        var result = _success.Ensure(_failureFunc);
        result.ShouldBeFailureWithCodeAndMessage("ensure", "Ensure failure");
    }

    [Fact]
    public void Ensure_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = _failure.Ensure(_forbiddenFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public void Ensure_Should_ReturnSuccess_WhenResultIsSuccess_AndFunctionOfTSucceeds()
    {
        var result = _success.Ensure(_successOfTFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Ensure_Should_ReturnFailure_WhenResultIsSuccess_AndFunctionOfTFails()
    {
        var result = _success.Ensure(_failureOfTFunc);
        result.ShouldBeFailureWithCodeAndMessage("ensure", "Ensure failure");
    }

    [Fact]
    public void Ensure_Should_ReturnFailure_WhenResultIsFailure_AndCheckIsOfT()
    {
        var result = _failure.Ensure(_forbiddenOfTFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task EnsureAsync_Should_ReturnValue_WhenResultIsSuccess_AndFunctionSucceeds()
    {
        var result = await _success.EnsureAsync(_successTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task EnsureAsync_Should_ReturnFailure_WhenResultIsSuccess_AndFunctionFails()
    {
        var result = await _success.EnsureAsync(_failureTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("ensure", "Ensure failure");
    }

    [Fact]
    public async Task EnsureAsync_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = await _failure.EnsureAsync(_forbiddenTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task EnsureAsync_Should_ReturnValue_WhenResultIsSuccess_AndFunctionOfTSucceeds()
    {
        var result = await _success.EnsureAsync(_successOfTTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task EnsureAsync_Should_ReturnFailure_WhenResultIsSuccess_AndFunctionOfTFails()
    {
        var result = await _success.EnsureAsync(_failureOfTTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("ensure", "Ensure failure");
    }

    [Fact]
    public async Task EnsureAsync_Should_ReturnFailure_WhenResultIsFailure_AndCheckIsOfT()
    {
        var result = await _failure.EnsureAsync(_forbiddenOfTTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task Ensure_Should_ReturnValue_WhenResultTaskIsSuccess_AndFunctionSucceeds()
    {
        var result = await _successTask.Ensure(_successFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task Ensure_Should_ReturnFailure_WhenResultTaskIsSuccess_AndFunctionFails()
    {
        var result = await _successTask.Ensure(_failureFunc);
        result.ShouldBeFailureWithCodeAndMessage("ensure", "Ensure failure");
    }

    [Fact]
    public async Task Ensure_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.Ensure(_forbiddenFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task Ensure_Should_ReturnValue_WhenResultTaskIsSuccess_AndFunctionOfTSucceeds()
    {
        var result = await _successTask.Ensure(_successOfTFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task Ensure_Should_ReturnFailure_WhenResultTaskIsSuccess_AndFunctionOfTFails()
    {
        var result = await _successTask.Ensure(_failureOfTFunc);
        result.ShouldBeFailureWithCodeAndMessage("ensure", "Ensure failure");
    }

    [Fact]
    public async Task Ensure_Should_ReturnFailure_WhenResultTaskIsFailure_AndCheckIsOfT()
    {
        var result = await _failureTask.Ensure(_forbiddenOfTFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task EnsureAsync_Should_ReturnValue_WhenResultTaskIsSuccess_AndFunctionSucceeds()
    {
        var result = await _successTask.EnsureAsync(_successTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task EnsureAsync_Should_ReturnFailure_WhenResultTaskIsSuccess_AndFunctionFails()
    {
        var result = await _successTask.EnsureAsync(_failureTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("ensure", "Ensure failure");
    }

    [Fact]
    public async Task EnsureAsync_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.EnsureAsync(_forbiddenTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task EnsureAsync_Should_ReturnValue_WhenResultTaskIsSuccess_AndFunctionOfTSucceeds()
    {
        var result = await _successTask.EnsureAsync(_successOfTTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task EnsureAsync_Should_ReturnFailure_WhenResultTaskIsSuccess_AndFunctionOfTFails()
    {
        var result = await _successTask.EnsureAsync(_failureOfTTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("ensure", "Ensure failure");
    }

    [Fact]
    public async Task EnsureAsync_Should_ReturnFailure_WhenResultTaskIsFailure_AndCheckIsOfT()
    {
        var result = await _failureTask.EnsureAsync(_forbiddenOfTTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }
}
