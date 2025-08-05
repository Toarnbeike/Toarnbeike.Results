using Toarnbeike.Optional;
using Toarnbeike.Optional.TestExtensions;

namespace Toarnbeike.Results.Optional.Tests;

/// <summary>
/// Tests for the <see cref="ToOptionExtensions"/> class.
/// </summary>
public class ToOptionExtensionTests
{
    private readonly Result<int> _success = Result.Success(3);
    private readonly Result<int> _failure = Result<int>.Failure(new Failure("original", "Original failure"));

    private readonly Task<Result<int>> _successTask = Task.FromResult(Result.Success(3));
    private readonly Task<Result<int>> _failureTask = Task.FromResult(Result<int>.Failure(new Failure("original", "Original failure")));

    [Fact]
    public void ToOption_ShouldReturnSome_WhenResultIsSuccess()
    {
        var option = _success.ToOption();

        option.ShouldBeSomeWithValue(3);
    }

    [Fact]
    public void ToOption_ShouldReturnNone_WhenResultIsFailure()
    {
        var option = _failure.ToOption();

        option.ShouldBeOfType<Option<int>>();
        option.ShouldBeNone();
    }

    [Fact]
    public async Task ToOption_ShouldReturnSome_WhenResultTaskIsSuccess()
    {
        var option = await _successTask.ToOption();
        option.ShouldBeSomeWithValue(3);
    }

    [Fact]
    public async Task ToOption_ShouldReturnNone_WhenResultTaskIsFailure()
    {
        var option = await _failureTask.ToOption();
        option.ShouldBeNone();
    }
}