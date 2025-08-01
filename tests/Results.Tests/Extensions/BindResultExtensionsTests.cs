using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Tests.Internal;

namespace Toarnbeike.Results.Tests.Extensions;

/// <summary>
/// Tests for the <see cref="BindResultExtensions"/> class.
/// </summary>
public class BindResultExtensionsTests
{
    private readonly Result _success = Result.Success();
    private readonly Result _failure = Result.Failure(new Failure("original", "Original failure"));

    private readonly Task<Result> _successTask = Task.FromResult(Result.Success());
    private readonly Task<Result> _failureTask = Task.FromResult(Result.Failure(new Failure("original", "Original failure")));

    private readonly Func<Result<int>> _successFunc = () => 42;
    private readonly Func<Result<int>> _failureFunc = () => new Failure("bind", "Bind failure");
    private readonly Func<Result<int>> _forbiddenFunc = () => throw new InvalidOperationException("This function should not be called");

    private readonly Func<Task<Result<int>>> _successTaskFunc = () => Task.FromResult(Result<int>.Success(42));
    private readonly Func<Task<Result<int>>> _failureTaskFunc = () => Task.FromResult(Result<int>.Failure(new Failure("bind", "Bind failure")));
    private readonly Func<Task<Result<int>>> _forbiddenTaskFunc = () => throw new InvalidOperationException("This function should not be called");

    [Fact]
    public void Bind_Should_ReturnValue_WhenResultIsSuccess_AndFunctionSucceeds()
    {
        var result = _success.Bind(_successFunc);
        result.ShouldBeSuccessWithValue(42);
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
        result.ShouldBeSuccessWithValue(42);
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
        result.ShouldBeSuccessWithValue(42);
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
        result.ShouldBeSuccessWithValue(42);
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
