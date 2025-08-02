using Toarnbeike.Results.Extensions;

namespace Toarnbeike.Results.Tests.Extensions.Tap;

/// <summary>
/// Tests for the <see cref="TapExtensions"/> on a <see cref="Result"/>.
/// </summary>
public class TapResultExtensionsTests
{
    private readonly Result _success = Result.Success();
    private readonly Result _failure = Result.Failure(new Failure("original", "Original failure"));

    private readonly Task<Result> _successTask = Task.FromResult(Result.Success());
    private readonly Task<Result> _failureTask = Task.FromResult(Result.Failure(new Failure("original", "Original failure")));

    [Fact]
    public void Tap_ShouldExecute_WhenResultIsSuccess()
    {
        var isExecuted = false;
        _success.Tap(() => isExecuted = true);
        isExecuted.ShouldBeTrue();
    }

    [Fact]
    public void Tap_ShouldNotExecute_WhenResultIsFailure ()
    {
        var isExecuted = false;
        _failure.Tap(() => isExecuted = true);
        isExecuted.ShouldBeFalse();
    }

    [Fact]
    public async Task TapAsync_ShouldExecute_WhenResultIsSuccess()
    {
        var isExecuted = false;
        await _success.TapAsync(async () => await Task.Run(() => isExecuted = true));
        isExecuted.ShouldBeTrue();
    }

    [Fact]
    public async Task TapAsync_ShouldNotExecute_WhenResultIsFailure()
    {
        var isExecuted = false;
        await _failure.TapAsync(async () => await Task.Run(() => isExecuted = true));
        isExecuted.ShouldBeFalse();
    }

    [Fact]
    public async Task Tap_ShouldExecute_WhenResultTaskIsSuccess()
    {
        var isExecuted = false;
        await _successTask.Tap(() => isExecuted = true);
        isExecuted.ShouldBeTrue();
    }

    [Fact]
    public async Task Tap_ShouldNotExecute_WhenResultTaskIsFailure()
    {
        var isExecuted = false;
        await _failureTask.Tap(() => isExecuted = true);
        isExecuted.ShouldBeFalse();
    }

    [Fact]
    public async Task TapAsync_ShouldExecute_WhenResultTaskIsSuccess()
    {
        var isExecuted = false;
        await _successTask.TapAsync(async () => await Task.Run(() => isExecuted = true));
        isExecuted.ShouldBeTrue();
    }

    [Fact]
    public async Task TapAsync_ShouldNotExecute_WhenResultTaskIsFailure()
    {
        var isExecuted = false;
        await _failureTask.TapAsync(async () => await Task.Run(() => isExecuted = true));
        isExecuted.ShouldBeFalse();
    }
}
