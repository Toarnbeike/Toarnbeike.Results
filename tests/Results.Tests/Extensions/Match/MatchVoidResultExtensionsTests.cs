using Toarnbeike.Results.Extensions;

namespace Toarnbeike.Results.Tests.Extensions.Match;

/// <summary>
/// Tests for the <see cref="MatchExtensions"/> on a <see cref="Result{TValue}"/>.
/// </summary>
public class MatchVoidResultExtensionsTests
{
    private readonly Result _success = Result.Success();
    private readonly Result _failure = Result.Failure(new Failure("original", "Original failure"));

    private readonly Task<Result> _successTask = Task.FromResult(Result.Success());
    private readonly Task<Result> _failureTask = Task.FromResult(Result.Failure(new Failure("original", "Original failure")));

    [Test]
    public void Match_Should_ReturnTrue_WhenResultIsSuccess()
    {
        var isSuccess = false;
        _success.Match(() => isSuccess = true, _ => { });
        isSuccess.ShouldBeTrue();
    }

    [Test]
    public void Match_Should_ReturnFalse_WhenResultIsFailure()
    {
        var isFailure = false;
        _failure.Match(() => { }, _ => isFailure = true);
        isFailure.ShouldBeTrue();
    }

    [Test]
    public async Task MatchAsync_Should_ReturnTrue_WhenResultIsSuccess()
    {
        var isSuccess = false;
        await _success.MatchAsync(async () => await Task.FromResult(isSuccess = true), async _ => await Task.CompletedTask);
        isSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task MatchAsync_Should_ReturnFalse_WhenResultIsFailure()
    {
        var isFailure = false;
        await _failure.MatchAsync(async () => await Task.CompletedTask, async _ => await Task.FromResult(isFailure = true));
        isFailure.ShouldBeTrue();
    }

    [Test]
    public async Task Match_Should_ReturnTrue_WhenResultTaskIsSuccess()
    {
        var isSuccess = false;
        await _successTask.Match(() => isSuccess = true, _ => { });
        isSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task Match_Should_ReturnFalse_WhenResultTaskIsFailure()
    {
        var isFailure = false;
        await _failureTask.Match(() => { }, _ => isFailure = true);
        isFailure.ShouldBeTrue();
    }

    [Test]
    public async Task MatchAsync_Should_ReturnTrue_WhenResultTaskIsSuccess()
    {
        var isSuccess = false;
        await _successTask.MatchAsync(async () => await Task.FromResult(isSuccess = true), async _ => await Task.CompletedTask);
        isSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task MatchAsync_Should_ReturnFalse_WhenResultTaskIsFailure()
    {
        var isFailure = false;
        await _failureTask.MatchAsync(async () => await Task.CompletedTask, async _ => await Task.FromResult(isFailure = true));
        isFailure.ShouldBeTrue();
    }
}