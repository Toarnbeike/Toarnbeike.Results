using Toarnbeike.Results.Extensions;

namespace Toarnbeike.Results.Tests.Extensions.Match;

/// <summary>
/// Tests for the <see cref="MatchExtensions"/> on a <see cref="Result"/>.
/// </summary>
public class MatchResultExtensionsTests
{
    private readonly Result _success = Result.Success();
    private readonly Result _failure = Result.Failure(new Failure("original", "Original failure"));

    private readonly Task<Result> _successTask = Task.FromResult(Result.Success());
    private readonly Task<Result> _failureTask = Task.FromResult(Result.Failure(new Failure("original", "Original failure")));

    private readonly Func<bool> _onSuccess = () => true;
    private readonly Func<Failure, bool> _onFailure = _ => false;

    private readonly Func<Task<bool>> _onSuccessAsync = () => Task.FromResult(true);
    private readonly Func<Failure, Task<bool>> _onFailureAsync = _ => Task.FromResult(false);

    [Test]
    public void Match_Should_ReturnTrue_WhenResultIsSuccess()
    {
        var actual = _success.Match(_onSuccess, _onFailure);
        actual.ShouldBeTrue();
    }

    [Test]
    public void Match_Should_ReturnFalse_WhenResultIsFailure()
    {
        var actual = _failure.Match(_onSuccess, _onFailure);
        actual.ShouldBeFalse();
    }

    [Test]
    public async Task MatchAsync_Should_ReturnTrue_WhenResultIsSuccess()
    {
        var actual = await _success.MatchAsync(_onSuccessAsync, _onFailureAsync);
        actual.ShouldBeTrue();
    }

    [Test]
    public async Task MatchAsync_Should_ReturnFalse_WhenResultIsFailure()
    {
        var actual = await _failure.MatchAsync(_onSuccessAsync, _onFailureAsync);
        actual.ShouldBeFalse();
    }

    [Test]
    public async Task Match_Should_ReturnTrue_WhenResultTaskIsSuccess()
    {
        var actual = await _successTask.Match(_onSuccess, _onFailure);
        actual.ShouldBeTrue();
    }

    [Test]
    public async Task Match_Should_ReturnFalse_WhenResultTaskIsFailure()
    {
        var actual = await _failureTask.Match(_onSuccess, _onFailure);
        actual.ShouldBeFalse();
    }

    [Test]
    public async Task MatchAsync_Should_ReturnTrue_WhenResultTaskIsSuccess()
    {
        var actual = await _successTask.MatchAsync(_onSuccessAsync, _onFailureAsync);
        actual.ShouldBeTrue();
    }

    [Test]
    public async Task MatchAsync_Should_ReturnFalse_WhenResultTaskIsFailure()
    {
        var actual = await _failureTask.MatchAsync(_onSuccessAsync, _onFailureAsync);
        actual.ShouldBeFalse();
    }
}