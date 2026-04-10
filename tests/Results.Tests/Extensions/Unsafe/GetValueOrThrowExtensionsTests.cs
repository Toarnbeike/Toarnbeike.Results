using Toarnbeike.Results.Extensions.Unsafe;

namespace Toarnbeike.Results.Tests.Extensions.Unsafe;

/// <summary>
/// Tests for the <see cref="GetValueOrThrowExtensions"/> class.
/// </summary>
public class GetValueOrThrowExtensionsTests
{
    private readonly Result<int> _success = Result.Success(42);
    private readonly Result<int> _failure = Result<int>.Failure(new Failure("original", "Original failure"));

    private readonly Task<Result<int>> _successTask = Task.FromResult(Result.Success(42));
    private readonly Task<Result<int>> _failureTask = Task.FromResult(Result<int>.Failure(new Failure("original", "Original failure")));

    [Test]
    public void GetValueOrThrow_ShouldReturnValue_WhenResultIsSuccess()
    {
        var value = _success.GetValueOrThrow();
        value.ShouldBe(42);
    }

    [Test]
    public void GetValueOrThrow_ShouldThrow_WhenResultIsFailure()
    {
        var ex = Should.Throw<InvalidOperationException>(() => _failure.GetValueOrThrow());
        ex.Message.ShouldBe("Trying to get the value of a failure result. Failure: 'Original failure'.");
    }

    [Test]
    public async Task GetValueOrThrow_ShouldReturnValue_WhenResultTaskIsSuccess()
    {
        var value = await _successTask.GetValueOrThrow();
        value.ShouldBe(42);
    }

    [Test]
    public async Task GetValueOrThrow_ShouldThrow_WhenResultTaskIsFailure()
    {
        var ex = await Should.ThrowAsync<InvalidOperationException>(
            async () => await _failureTask.GetValueOrThrow());
        ex.Message.ShouldBe("Trying to get the value of a failure result. Failure: 'Original failure'.");
    }
}