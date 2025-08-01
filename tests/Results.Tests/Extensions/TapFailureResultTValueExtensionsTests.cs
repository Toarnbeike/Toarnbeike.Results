using Toarnbeike.Results.Extensions;

namespace Toarnbeike.Results.Tests.Extensions;

/// <summary>
/// Tests for the <see cref="TapFailureResultTValueExtensions"/> class.
/// </summary>
public class TapFailureResultTValueExtensionsTests
{
    private readonly Result<int> _success = Result.Success(42);
    private readonly Result<int> _failure = Result<int>.Failure(new Failure("original", "Original failure"));

    private readonly Task<Result<int>> _successTask = Task.FromResult(Result.Success(42));
    private readonly Task<Result<int>> _failureTask = Task.FromResult(Result<int>.Failure(new Failure("original", "Original failure")));

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
        await _success.TapFailureAsync(async failure => await Task.Run(() => failureMessage = failure.Message));
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