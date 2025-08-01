using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Tests.Internal;

namespace Toarnbeike.Results.Tests.Extensions;

/// <summary>
/// Tests for the <see cref="BindResultExtensions"/> class.
/// </summary>
public class BindResultTValueExtensionsTests
{
    private readonly Result<double> _success = Result<double>.Success(1.3);
    private readonly Result<double> _failure = Result<double>.Failure(new Failure("original", "Original failure"));

    private readonly Task<Result<double>> _successTask = Task.FromResult(Result.Success(1.3));
    private readonly Task<Result<double>> _failureTask = Task.FromResult(Result<double>.Failure(new Failure("original", "Original failure")));

    private readonly Func<double, Result<int>> _successFunc = value => (int)Math.Round(value, 0);
    private readonly Func<double, Result<int>> _failureFunc = _ => new Failure("bind", "Bind failure");
    private readonly Func<double, Result<int>> _forbiddenFunc = _ => throw new InvalidOperationException("This function should not be called");

    private readonly Func<double, Task<Result<int>>> _successTaskFunc = value => Task.FromResult(Result<int>.Success((int)Math.Round(value, 0)));
    private readonly Func<double, Task<Result<int>>> _failureTaskFunc = _ => Task.FromResult(Result<int>.Failure(new Failure("bind", "Bind failure")));
    private readonly Func<double, Task<Result<int>>> _forbiddenTaskFunc = _ => throw new InvalidOperationException("This function should not be called");

    [Fact]
    public void Bind_Should_ReturnValue_WhenResultIsSuccess_AndFunctionSucceeds()
    {
        var result = _success.Bind(_successFunc);
        result.ShouldBeSuccessWithValue(1);
    }

    [Fact]
    public void Bind_Should_ReturnFailure_WhenResultIsSuccess_AndFunctionFails()
    {
        var result = _success.Bind(_failureFunc);
        result.ShouldBeFailureWithCodeAndMessage("bind", "Bind failure");
    }

    [Fact]
    public void Bind_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = _failure.Bind(_forbiddenFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task BindAsync_Should_ReturnValue_WhenResultIsSuccess_AndFunctionSucceeds()
    {
        var result = await _success.BindAsync(_successTaskFunc);
        result.ShouldBeSuccessWithValue(1);
    }

    [Fact]
    public async Task BindAsync_Should_ReturnFailure_WhenResultIsSuccess_AndFunctionFails()
    {
        var result = await _success.BindAsync(_failureTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("bind", "Bind failure");
    }

    [Fact]
    public async Task BindAsync_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = await _failure.BindAsync(_forbiddenTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task Bind_Should_ReturnValue_WhenResultTaskIsSuccess_AndFunctionSucceeds()
    {
        var result = await _successTask.Bind(_successFunc);
        result.ShouldBeSuccessWithValue(1);
    }

    [Fact]
    public async Task Bind_Should_ReturnFailure_WhenResultTaskIsSuccess_AndFunctionFails()
    {
        var result = await _successTask.Bind(_failureFunc);
        result.ShouldBeFailureWithCodeAndMessage("bind", "Bind failure");
    }

    [Fact]
    public async Task Bind_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.Bind(_forbiddenFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task BindAsync_Should_ReturnValue_WhenResultTaskIsSuccess_AndFunctionSucceeds()
    {
        var result = await _successTask.BindAsync(_successTaskFunc);
        result.ShouldBeSuccessWithValue(1);
    }

    [Fact]
    public async Task BindAsync_Should_ReturnFailure_WhenResultTaskIsSuccess_AndFunctionFails()
    {
        var result = await _successTask.BindAsync(_failureTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("bind", "Bind failure");
    }

    [Fact]
    public async Task BindAsync_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.BindAsync(_forbiddenTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }
}
