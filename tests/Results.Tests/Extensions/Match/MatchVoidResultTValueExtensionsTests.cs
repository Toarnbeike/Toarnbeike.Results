using Toarnbeike.Results.Extensions;

namespace Toarnbeike.Results.Tests.Extensions.Match;

/// <summary>
/// Tests for the <see cref="MatchExtensions"/> on a <see cref="Result{TValue}"/>.
/// </summary>
public class MatchVoidResultTValueExtensionsTests
{
    private readonly Result<string> _success = Result.Success("Success");
    private readonly Result<string> _failure = Result<string>.Failure(new TestFailure("original"));

    private readonly Task<Result<string>> _successTask = Task.FromResult(Result.Success("Success"));
    private readonly Task<Result<string>> _failureTask = Task.FromResult(Result<string>.Failure(new TestFailure("original")));

    [Test]
    public void Match_Should_ReturnTrue_WhenResultIsSuccess()
    {
        var isSuccess = false;
        _success.Match(_=> isSuccess = true, _ => { });
        isSuccess.ShouldBeTrue();
    }

    [Test]
    public void Match_Should_ReturnFalse_WhenResultIsFailure()
    {
        var isFailure = false;
        _failure.Match(_ => { }, _ => isFailure = true);
        isFailure.ShouldBeTrue();
    }

    [Test]
    public async Task MatchAsync_Should_ReturnTrue_WhenResultIsSuccess()
    {
        var isSuccess = false;
        await _success.MatchAsync(async _ => await Task.FromResult(isSuccess = true), async _ => await Task.CompletedTask);
        isSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task MatchAsync_Should_ReturnFalse_WhenResultIsFailure()
    {
        var isFailure = false;
        await _failure.MatchAsync(async _ => await Task.CompletedTask, async _ => await Task.FromResult(isFailure = true));
        isFailure.ShouldBeTrue();
    }

    [Test]
    public async Task Match_Should_ReturnTrue_WhenResultTaskIsSuccess()
    {
        var isSuccess = false;
        await _successTask.Match(_ => isSuccess = true, _ => { });
        isSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task Match_Should_ReturnFalse_WhenResultTaskIsFailure()
    {
        var isFailure = false;
        await _failureTask.Match(_ => { }, _ => isFailure = true);
        isFailure.ShouldBeTrue();
    }

    [Test]
    public async Task MatchAsync_Should_ReturnTrue_WhenResultTaskIsSuccess()
    {
        var isSuccess = false;
        await _successTask.MatchAsync(async _ => await Task.FromResult(isSuccess = true), async _ => await Task.CompletedTask);
        isSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task MatchAsync_Should_ReturnFalse_WhenResultTaskIsFailure()
    {
        var isFailure = false;
        await _failureTask.MatchAsync(async _ => await Task.CompletedTask, async _ => await Task.FromResult(isFailure = true));
        isFailure.ShouldBeTrue();
    }
}