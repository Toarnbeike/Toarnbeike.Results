using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Tests.Extensions.BindTap;

/// <summary>
/// Tests for the <see cref="BindTapExtensions"/> on a <see cref="Result{TValue}"/>.
/// </summary>
public class BindTapResultTValueExtensionsTests
{
    private readonly Result<double> _success = Result.Success(1.3);
    private readonly Result<double> _failure = Result<double>.Failure(new Failure("original", "Original failure"));

    private readonly Task<Result<double>> _successTask = Task.FromResult(Result.Success(1.3));
    private readonly Task<Result<double>> _failureTask = Task.FromResult(Result<double>.Failure(new Failure("original", "Original failure")));

    private readonly Func<double, Result> _successFunc = value => Result.Success((int)value);
    private readonly Func<double, Result> _failureFunc = _ => Result.Failure(new Failure("BindTap", "BindTap failure"));
    private readonly Func<double, Result> _forbiddenFunc = _ => throw new InvalidOperationException("This function should not be called");

    private readonly Func<double, Result<int>> _successOfTFunc = value => Result.Success((int)value);
    private readonly Func<double, Result<int>> _failureOfTFunc = _ => Result<int>.Failure(new Failure("BindTap", "BindTap failure"));
    private readonly Func<double, Result<int>> _forbiddenOfTFunc = _ => throw new InvalidOperationException("This function should not be called");

    private readonly Func<double, Task<Result>> _successTaskFunc = value => Task.FromResult((Result)Result.Success((int)value));
    private readonly Func<double, Task<Result>> _failureTaskFunc = _ => Task.FromResult(Result.Failure(new Failure("BindTap", "BindTap failure")));
    private readonly Func<double, Task<Result>> _forbiddenTaskFunc = _ => throw new InvalidOperationException("This function should not be called");

    private readonly Func<double, Task<Result<int>>> _successOfTTaskFunc = _ => Task.FromResult(Result<int>.Success(42));
    private readonly Func<double, Task<Result<int>>> _failureOfTTaskFunc = _ => Task.FromResult(Result<int>.Failure(new Failure("BindTap", "BindTap failure")));
    private readonly Func<double, Task<Result<int>>> _forbiddenOfTTaskFunc = _ => throw new InvalidOperationException("This function should not be called");

    [Test]
    public void BindTap_Should_ReturnSuccess_WhenResultIsSuccess_AndFunctionSucceeds()
    {
        var result = _success.BindTap(_successFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public void BindTap_Should_ReturnFailure_WhenResultIsSuccess_AndFunctionFails()
    {
        var result = _success.BindTap(_failureFunc);
        result.ShouldBeFailureWithCodeAndMessage("BindTap", "BindTap failure");
    }

    [Test]
    public void BindTap_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = _failure.BindTap(_forbiddenFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Test]
    public void BindTap_Should_ReturnSuccess_WhenResultIsSuccess_AndFunctionOfTSucceeds()
    {
        var result = _success.BindTap(_successOfTFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public void BindTap_Should_ReturnFailure_WhenResultIsSuccess_AndFunctionOfTFails()
    {
        var result = _success.BindTap(_failureOfTFunc);
        result.ShouldBeFailureWithCodeAndMessage("BindTap", "BindTap failure");
    }

    [Test]
    public void BindTap_Should_ReturnFailure_WhenResultIsFailure_AndCheckIsOfT()
    {
        var result = _failure.BindTap(_forbiddenOfTFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnValue_WhenResultIsSuccess_AndFunctionSucceeds()
    {
        var result = await _success.BindTapAsync(_successTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnFailure_WhenResultIsSuccess_AndFunctionFails()
    {
        var result = await _success.BindTapAsync(_failureTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("BindTap", "BindTap failure");
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = await _failure.BindTapAsync(_forbiddenTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnValue_WhenResultIsSuccess_AndFunctionOfTSucceeds()
    {
        var result = await _success.BindTapAsync(_successOfTTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnFailure_WhenResultIsSuccess_AndFunctionOfTFails()
    {
        var result = await _success.BindTapAsync(_failureOfTTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("BindTap", "BindTap failure");
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnFailure_WhenResultIsFailure_AndCheckIsOfT()
    {
        var result = await _failure.BindTapAsync(_forbiddenOfTTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Test]
    public async Task BindTap_Should_ReturnValue_WhenResultTaskIsSuccess_AndFunctionSucceeds()
    {
        var result = await _successTask.BindTap(_successFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task BindTap_Should_ReturnFailure_WhenResultTaskIsSuccess_AndFunctionFails()
    {
        var result = await _successTask.BindTap(_failureFunc);
        result.ShouldBeFailureWithCodeAndMessage("BindTap", "BindTap failure");
    }

    [Test]
    public async Task BindTap_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.BindTap(_forbiddenFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Test]
    public async Task BindTap_Should_ReturnValue_WhenResultTaskIsSuccess_AndFunctionOfTSucceeds()
    {
        var result = await _successTask.BindTap(_successOfTFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task BindTap_Should_ReturnFailure_WhenResultTaskIsSuccess_AndFunctionOfTFails()
    {
        var result = await _successTask.BindTap(_failureOfTFunc);
        result.ShouldBeFailureWithCodeAndMessage("BindTap", "BindTap failure");
    }

    [Test]
    public async Task BindTap_Should_ReturnFailure_WhenResultTaskIsFailure_AndCheckIsOfT()
    {
        var result = await _failureTask.BindTap(_forbiddenOfTFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnValue_WhenResultTaskIsSuccess_AndFunctionSucceeds()
    {
        var result = await _successTask.BindTapAsync(_successTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnFailure_WhenResultTaskIsSuccess_AndFunctionFails()
    {
        var result = await _successTask.BindTapAsync(_failureTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("BindTap", "BindTap failure");
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.BindTapAsync(_forbiddenTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnValue_WhenResultTaskIsSuccess_AndFunctionOfTSucceeds()
    {
        var result = await _successTask.BindTapAsync(_successOfTTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnFailure_WhenResultTaskIsSuccess_AndFunctionOfTFails()
    {
        var result = await _successTask.BindTapAsync(_failureOfTTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("BindTap", "BindTap failure");
    }

    [Test]
    public async Task BindTapAsync_Should_ReturnFailure_WhenResultTaskIsFailure_AndCheckIsOfT()
    {
        var result = await _failureTask.BindTapAsync(_forbiddenOfTTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }
}
