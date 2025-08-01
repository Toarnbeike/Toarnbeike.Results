using Toarnbeike.Results.Extensions;

namespace Toarnbeike.Results.Tests.Extensions;

/// <summary>
/// Tests for the <see cref="TapFailureResultExtensions"/> class.
/// </summary>
public class TapFailureResultExtensionsTests
{
    private readonly Result _success = Result.Success();
    private readonly Result _failure = Result.Failure(new Failure("original", "Original failure"));

    private readonly Task<Result> _successTask = Task.FromResult(Result.Success());
    private readonly Task<Result> _failureTask = Task.FromResult(Result.Failure(new Failure("original", "Original failure")));

    [Fact]
    public void TapFailure_ShouldNotExecute_WhenResultIsSuccess()
    {
        var failureMessage = string.Empty;
        _success.TapFailure(failure => failureMessage = failure.Message);
        failureMessage.ShouldBeEmpty();
    }

    [Fact]
    public void TapFailure_ShouldExecute_WhenResultIsFailure()
    {
        var failureMessage = string.Empty;
        _failure.TapFailure(failure => failureMessage = failure.Message);
        failureMessage.ShouldBe("Original failure");
    }

    [Fact]
    public async Task TapFailureAsync_ShouldNotExecute_WhenResultIsSuccess()
    {
        var failureMessage = string.Empty;
        await _success.TapFailureAsync (async failure => await Task.Run(() => failureMessage = failure.Message));
        failureMessage.ShouldBeEmpty();
    }

    [Fact]
    public async Task TapFailureAsync_ShouldExecute_WhenResultIsFailure()
    {
        var failureMessage = string.Empty;
        await _failure.TapFailureAsync(async failure => await Task.Run(() => failureMessage = failure.Message));
        failureMessage.ShouldBe("Original failure");
    }

    [Fact]
    public async Task TapFailure_ShouldNotExecute_WhenResultTaskIsSuccess()
    {
        var failureMessage = string.Empty;
        await _successTask.TapFailure(failure => failureMessage = failure.Message);
        failureMessage.ShouldBeEmpty();
    }

    [Fact]
    public async Task TapFailure_ShouldExecute_WhenResultTaskIsFailure()
    {
        var failureMessage = string.Empty;
        await _failureTask.TapFailure(failure => failureMessage = failure.Message);
        failureMessage.ShouldBe("Original failure");
    }

    [Fact]
    public async Task TapFailureAsync_ShouldNotExecute_WhenResultTaskIsSuccess()
    {
        var failureMessage = string.Empty;
        await _successTask.TapFailureAsync(async failure => await Task.Run(() => failureMessage = failure.Message));
        failureMessage.ShouldBeEmpty();
    }

    [Fact]
    public async Task TapFailureAsync_ShouldExecute_WhenResultTaskIsFailure()
    {
        var failureMessage = string.Empty;
        await _failureTask.TapFailureAsync(async failure => await Task.Run(() => failureMessage = failure.Message));
        failureMessage.ShouldBe("Original failure");
    }
}
