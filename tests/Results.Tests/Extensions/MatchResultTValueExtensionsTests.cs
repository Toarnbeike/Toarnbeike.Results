using Toarnbeike.Results.Extensions;

namespace Toarnbeike.Results.Tests.Extensions;

/// <summary>
/// Tests for the <see cref="MatchResultTValueExtensions"/> class.
/// </summary>
public class MatchResultTValueExtensionsTests
{
    private readonly Result<string> _success = Result.Success("Success");
    private readonly Result<string> _failure = Result<string>.Failure(new Failure("original", "Original failure"));

    private readonly Task<Result<string>> _successTask = Task.FromResult(Result.Success("Success"));
    private readonly Task<Result<string>> _failureTask = Task.FromResult(Result<string>.Failure(new Failure("original", "Original failure")));

    private readonly Func<string, string> _onSuccess = value => value;
    private readonly Func<Failure, string> _onFailure = failure => failure.Code;

    private readonly Func<string, Task<string>> _onSuccessAsync = value => Task.FromResult(value);
    private readonly Func<Failure, Task<string>> _onFailureAsync = failure => Task.FromResult(failure.Code);

    [Fact]
    public void Match_Should_ReturnTrue_WhenResultIsSuccess()
    {
        var actual = _success.Match(_onSuccess, _onFailure);
        actual.ShouldBe("Success");
    }

    [Fact]
    public void Match_Should_ReturnFalse_WhenResultIsFailure()
    {
        var actual = _failure.Match(_onSuccess, _onFailure);
        actual.ShouldBe("original");
    }

    [Fact]
    public async Task MatchAsync_Should_ReturnTrue_WhenResultIsSuccess()
    {
        var actual = await _success.MatchAsync(_onSuccessAsync, _onFailureAsync);
        actual.ShouldBe("Success");
    }

    [Fact]
    public async Task MatchAsync_Should_ReturnFalse_WhenResultIsFailure()
    {
        var actual = await _failure.MatchAsync(_onSuccessAsync, _onFailureAsync);
        actual.ShouldBe("original");
    }

    [Fact]
    public async Task Match_Should_ReturnTrue_WhenResultTaskIsSuccess()
    {
        var actual = await _successTask.Match(_onSuccess, _onFailure);
        actual.ShouldBe("Success");
    }

    [Fact]
    public async Task Match_Should_ReturnFalse_WhenResultTaskIsFailure()
    {
        var actual = await _failureTask.Match(_onSuccess, _onFailure);
        actual.ShouldBe("original");
    }

    [Fact]
    public async Task MatchAsync_Should_ReturnTrue_WhenResultTaskIsSuccess()
    {
        var actual = await _successTask.MatchAsync(_onSuccessAsync, _onFailureAsync);
        actual.ShouldBe("Success");
    }

    [Fact]
    public async Task MatchAsync_Should_ReturnFalse_WhenResultTaskIsFailure()
    {
        var actual = await _failureTask.MatchAsync(_onSuccessAsync, _onFailureAsync);
        actual.ShouldBe("original");
    }
}