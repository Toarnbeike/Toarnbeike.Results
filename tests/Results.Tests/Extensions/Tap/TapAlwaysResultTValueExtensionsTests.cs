using Toarnbeike.Results.Extensions;

namespace Toarnbeike.Results.Tests.Extensions.Tap;

/// <summary>
/// Tests for the <see cref="TapAlwaysExtensions"/> on a <see cref="Result{TValue}"/>.
/// </summary>
public class TapAlwaysResultTValueExtensionsTests
{
    private readonly Result<int> _success = Result.Success(42);
    private readonly Result<int> _failure = Result<int>.Failure(new Failure("original", "Original failure"));

    private readonly Task<Result<int>> _successTask = Task.FromResult(Result.Success(42));
    private readonly Task<Result<int>> _failureTask = Task.FromResult(Result<int>.Failure(new Failure("original", "Original failure")));

    [Fact]
    public void TapAlways_ShouldExecute_WhenResultIsSuccess()
    {
        var message = string.Empty;
        _success.TapAlways(() => message = "tap always will always execute");
        message.ShouldBe("tap always will always execute");
    }

    [Fact]
    public void TapAlways_ShouldExecute_WhenResultIsFailure()
    {
        var message = string.Empty;
        _failure.TapAlways(() => message = "tap always will always execute");
        message.ShouldBe("tap always will always execute");
    }

    [Fact]
    public async Task TapAlwaysAsync_ShouldExecute_WhenResultIsSuccess()
    {
        var message = string.Empty;
        await _success.TapAlwaysAsync(() => Task.Run(() => message = "tap always will always execute"));
        message.ShouldBe("tap always will always execute");
    }

    [Fact]
    public async Task TapAlwaysAsync_ShouldExecute_WhenResultIsFailure()
    {
        var message = string.Empty;
        await _failure.TapAlwaysAsync(() => Task.Run(() => message = "tap always will always execute"));
        message.ShouldBe("tap always will always execute");
    }

    [Fact]
    public async Task TapAlways_ShouldExecute_WhenResultTaskIsSuccess()
    {
        var message = string.Empty;
        await _successTask.TapAlways(() => message = "tap always will always execute");
        message.ShouldBe("tap always will always execute");
    }

    [Fact]
    public async Task TapAlways_ShouldExecute_WhenResultTaskIsFailure()
    {
        var message = string.Empty;
        await _failureTask.TapAlways(() => message = "tap always will always execute");
        message.ShouldBe("tap always will always execute");
    }

    [Fact]
    public async Task TapAlwaysAsync_ShouldExecute_WhenResultTaskIsSuccess()
    {
        var message = string.Empty;
        await _successTask.TapAlwaysAsync(() => Task.Run(() => message = "tap always will always execute"));
        message.ShouldBe("tap always will always execute");
    }

    [Fact]
    public async Task TapAlwaysAsync_ShouldExecute_WhenResultTaskIsFailure()
    {
        var message = string.Empty;
        await _failureTask.TapAlwaysAsync(() => Task.Run(() => message = "tap always will always execute"));
        message.ShouldBe("tap always will always execute");
    }
}