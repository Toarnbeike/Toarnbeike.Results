using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Tests.Internal;

namespace Toarnbeike.Results.Tests.Extensions.WithValue;

/// <summary>
/// Tests for the <see cref="WithValueExtensions"/> on a <see cref="Result"/>.
/// </summary>
public class WithValueExtensionsTests
{
    private readonly Result _success = Result.Success();
    private readonly Result _failure = Result.Failure(new Failure("original", "Original failure"));

    private readonly Task<Result> _successTask = Task.FromResult(Result.Success());
    private readonly Task<Result> _failureTask = Task.FromResult(Result.Failure(new Failure("original", "Original failure")));

    private readonly string _value = "WithValue";
    private readonly Func<string> _valueFunc = () => "WithValue";
    private readonly Func<string> _forbiddenFunc = () => throw new InvalidOperationException("This function should not be called");
    private readonly Func<Task<string>> _valueFuncAsync = () => Task.FromResult("WithValue");
    private readonly Func<Task<string>> _forbiddenFuncAsync = () => throw new InvalidOperationException("This function should not be called");

    [Fact]
    public void WithValue_Should_ReturnSuccessFromValue_WhenResultIsSuccess()
    {
        var result = _success.WithValue(_value);
        result.ShouldBeSuccessWithValue("WithValue");
    }

    [Fact]
    public void WithValue_Should_ReturnFailureFromValue_WhenResultIsFailure()
    {
        var result = _failure.WithValue(_value);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public void WithValue_Should_ReturnSuccessFromValueFunc_WhenResultIsSuccess()
    {
        var result = _success.WithValue(_valueFunc);
        result.ShouldBeSuccessWithValue("WithValue");
    }

    [Fact]
    public void WithValue_Should_ReturnFailureFromValueFunc_WhenResultIsFailure()
    {
        var result = _failure.WithValue(_forbiddenFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task WithValueAsync_Should_ReturnSuccess_WhenResultIsSuccess()
    {
        var result = await _success.WithValueAsync(_valueFuncAsync);
        result.ShouldBeSuccessWithValue("WithValue");
    }

    [Fact]
    public async Task WithValueAsync_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = await _failure.WithValueAsync(_forbiddenFuncAsync);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task WithValue_Should_ReturnSuccessFromValue_WhenResultTaskIsSuccess()
    {
        var result = await _successTask.WithValue(_value);
        result.ShouldBeSuccessWithValue("WithValue");
    }

    [Fact]
    public async Task WithValue_Should_ReturnFailureFromValue_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.WithValue(_value);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task WithValue_Should_ReturnSuccessFromValueFunc_WhenResultTaskIsSuccess()
    {
        var result = await _successTask.WithValue(_valueFunc);
        result.ShouldBeSuccessWithValue("WithValue");
    }

    [Fact]
    public async Task WithValue_Should_ReturnFailureFromValueFunc_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.WithValue(_forbiddenFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task WithValueAsync_Should_ReturnSuccess_WhenResultTaskIsSuccess()
    {
        var result = await _successTask.WithValueAsync(_valueFuncAsync);
        result.ShouldBeSuccessWithValue("WithValue");
    }

    [Fact]
    public async Task WithValueAsync_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.WithValueAsync(_forbiddenFuncAsync);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }
}
