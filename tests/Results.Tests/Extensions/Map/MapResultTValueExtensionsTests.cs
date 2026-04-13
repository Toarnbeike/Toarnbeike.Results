using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Tests.Extensions.Map;

/// <summary>
/// Tests for the <see cref="MapExtensions"/> on a <see cref="Result{TValue}"/>.
/// </summary>
public class MapResultTValueExtensionsTests
{
    private readonly Result<double> _success = Result.Success(1.3);
    private readonly Result<double> _failure = Result<double>.Failure(new TestFailure("original"));

    private readonly Task<Result<double>> _successTask = Task.FromResult(Result.Success(1.3));
    private readonly Task<Result<double>> _failureTask = Task.FromResult(Result<double>.Failure(new TestFailure("original")));

    private readonly Func<double, int> _mapFunc = value => (int)(value * 2);
    private readonly Func<double, int> _forbiddenFunc = _ => throw new InvalidOperationException("This function should not be called");

    private readonly Func<double, Task<int>> _mapTaskFunc = value => Task.FromResult((int)(value * 2));
    private readonly Func<double, Task<int>> _forbiddenTaskFunc = _ => throw new InvalidOperationException("This function should not be called");

    [Test]
    public void Map_Should_ReturnValue_WhenResultIsSuccess()
    {
        var result = _success.Map(_mapFunc);
        result.ShouldBeSuccess().ShouldBe(2);
    }

    [Test]
    public void Map_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = _failure.Map(_forbiddenFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public async Task MapAsync_Should_ReturnValue_WhenResultIsSuccess()
    {
        var result = await _success.MapAsync(_mapTaskFunc);
        result.ShouldBeSuccess().ShouldBe(2);
    }

    [Test]
    public async Task MapAsync_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = await _failure.MapAsync(_forbiddenTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public async Task Map_Should_ReturnValue_WhenResultTaskIsSuccess()
    {
        var result = await _successTask.Map(_mapFunc);
        result.ShouldBeSuccess().ShouldBe(2);
    }

    [Test]
    public async Task Map_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.Map(_forbiddenFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public async Task MapAsync_Should_ReturnValue_WhenResultTaskIsSuccess()
    {
        var result = await _successTask.MapAsync(_mapTaskFunc);
        result.ShouldBeSuccess().ShouldBe(2);
    }

    [Test]
    public async Task MapAsync_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.MapAsync(_forbiddenTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }
}
