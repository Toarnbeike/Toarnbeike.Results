using Toarnbeike.Results.Extensions.Unsafe;

namespace Toarnbeike.Results.Tests.Extensions.Obsolete;

/// <summary>
/// Tests for the <see cref="GetFailureOrThrowExtensionsTests"/> class.
/// </summary>
[Obsolete("This extension will be removed. For testing purposes, use ShouldBeFailure() from the Toarnbeike.Results.TestExtensions namespace. " +
          "For production code, consider using TryGetFailure(out var failure) or Match() methods instead, which are more explicit and less error-prone.")]
public class GetFailureOrThrowExtensionsTests
{
    private readonly Result _success = Result.Success();
    private readonly Result _failure = Result.Failure(new TestFailure("original"));

    private readonly Task<Result> _successTask = Task.FromResult(Result.Success());
    private readonly Task<Result> _failureTask = Task.FromResult(Result.Failure(new TestFailure("original")));


    private readonly Task<Result<int>> _successTaskOfT = Task.FromResult(Result.Success(42));
    private readonly Task<Result<int>> _failureTaskOfT = Task.FromResult(Result<int>.Failure(new TestFailure("original")));

    [Test]
    public void GetFailureOrThrow_ShouldThrow_WhenResultIsSuccess()
    {
        var ex = Should.Throw<InvalidOperationException>(() => _success.GetFailureOrThrow());
        ex.Message.ShouldBe("Trying to get the failure of a success result. No failure available.");
    }

    [Test]
    public void GetFailureOrThrow_ShouldReturnFailure_WhenResultIsFailure()
    {
        var failure = _failure.GetFailureOrThrow();
        failure.Message.ShouldBe("original");
    }

    [Test]
    public async Task GetFailureOrThrow_ShouldThrow_WhenResultTaskIsSuccess()
    {
        var ex = await Should.ThrowAsync<InvalidOperationException>(_successTask.GetFailureOrThrow);
        ex.Message.ShouldBe("Trying to get the failure of a success result. No failure available.");
    }

    [Test]
    public async Task GetFailureOrThrow_ShouldReturnFailure_WhenResultTaskIsFailure()
    {
        var failure = await _failureTask.GetFailureOrThrow();
        failure.Message.ShouldBe("original");
    }

    [Test]
    public async Task GetFailureOrThrow_ShouldThrow_WhenResultTaskOfTIsSuccess()
    {
        var ex = await Should.ThrowAsync<InvalidOperationException>(_successTaskOfT.GetFailureOrThrow);
        ex.Message.ShouldBe("Trying to get the failure of a success result. No failure available.");
    }

    [Test]
    public async Task GetFailureOrThrow_ShouldReturnFailure_WhenResultTaskOfTIsFailure()
    {
        var failure = await _failureTaskOfT.GetFailureOrThrow();
        failure.Message.ShouldBe("original");
    }
}