using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Tests.Internal;

namespace Toarnbeike.Results.Tests.Extensions;

/// <summary>
/// Tests for the <see cref="MapResultTValueExtensions"/> class.
/// </summary>
public class MapResultTValueExtensionsTests
{
    private readonly Result<double> _success = Result.Success(1.3);
    private readonly Result<double> _failure = Result<double>.Failure(new Failure("original", "Original failure"));

    private readonly Task<Result<double>> _successTask = Task.FromResult(Result.Success(1.3));
    private readonly Task<Result<double>> _failureTask = Task.FromResult(Result<double>.Failure(new Failure("original", "Original failure")));

    private readonly Func<double, int> _mapFunc = value => (int)(value * 2);
    private readonly Func<double, int> _forbiddenFunc = _ => throw new InvalidOperationException("This function should not be called");

    private readonly Func<double, Task<int>> _mapTaskFunc = value => Task.FromResult((int)(value * 2));
    private readonly Func<double, Task<int>> _forbiddenTaskFunc = _ => throw new InvalidOperationException("This function should not be called");

    [Fact]
    public void Map_Should_ReturnValue_WhenResultIsSuccess()
    {
        var result = _success.Map(_mapFunc);
        result.ShouldBeSuccessWithValue(2);
    }

    [Fact]
    public void Map_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = _failure.Map(_forbiddenFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task MapAsync_Should_ReturnValue_WhenResultIsSuccess()
    {
        var result = await _success.MapAsync(_mapTaskFunc);
        result.ShouldBeSuccessWithValue(2);
    }

    [Fact]
    public async Task MapAsync_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = await _failure.MapAsync(_forbiddenTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task Map_Should_ReturnValue_WhenResultTaskIsSuccess()
    {
        var result = await _successTask.Map(_mapFunc);
        result.ShouldBeSuccessWithValue(2);
    }

    [Fact]
    public async Task Map_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.Map(_forbiddenFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task MapAsync_Should_ReturnValue_WhenResultTaskIsSuccess()
    {
        var result = await _successTask.MapAsync(_mapTaskFunc);
        result.ShouldBeSuccessWithValue(2);
    }

    [Fact]
    public async Task MapAsync_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.MapAsync(_forbiddenTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }
}
