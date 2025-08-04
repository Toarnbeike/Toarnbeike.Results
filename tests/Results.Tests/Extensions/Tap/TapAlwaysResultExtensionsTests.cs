using System.CodeDom.Compiler;
using Toarnbeike.Results.Extensions;

namespace Toarnbeike.Results.Tests.Extensions.Tap;

/// <summary>
/// Tests for the <see cref="TapAlwaysExtensions"/> on a <see cref="Result"/>.
/// </summary>
public class TapAlwaysResultExtensionsTests
{
    private readonly Result _success = Result.Success();
    private readonly Result _failure = Result.Failure(new Failure("original", "Original failure"));

    private readonly Task<Result> _successTask = Task.FromResult(Result.Success());
    private readonly Task<Result> _failureTask = Task.FromResult(Result.Failure(new Failure("original", "Original failure")));

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
