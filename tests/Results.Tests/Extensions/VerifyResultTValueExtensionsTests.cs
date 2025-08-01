using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Tests.Internal;

namespace Toarnbeike.Results.Tests.Extensions;

/// <summary>
/// Tests for the <see cref="VerifyResultTValueExtensions"/> class.
/// </summary>
public class VerifyResultTValueExtensionsTests
{
    private readonly Result<double> _success = Result.Success(1.3);
    private readonly Result<double> _failure = Result<double>.Failure(new Failure("original", "Original failure"));

    private readonly Task<Result<double>> _successTask = Task.FromResult(Result.Success(1.3));
    private readonly Task<Result<double>> _failureTask = Task.FromResult(Result<double>.Failure(new Failure("original", "Original failure")));

    private readonly Func<double, bool> _truePredicate = value => value > 1;
    private readonly Func<double, bool> _falsePredicate = value => value < 1;
    private readonly Func<double, bool> _forbiddenPredicate = _ => throw new InvalidOperationException("This function should not be called");

    private readonly Func<double, Task<bool>> _trueAsyncPredicate = value => Task.FromResult(value > 1);
    private readonly Func<double, Task<bool>> _falseAsyncPredicate = value => Task.FromResult(value < 1);
    private readonly Func<double, Task<bool>> _forbiddenAsyncPredicate = _ => throw new InvalidOperationException("This function should not be called");

    private readonly Func<Failure> _failureFactory = () => new Failure("verify", "Verify failure");

    [Fact]
    public void Verify_Should_ReturnSuccess_WhenResultIsSuccess_AndPredicateIsTrue()
    {
        var result = _success.Verify(_truePredicate, _failureFactory);
        result.ShouldBeSuccessWithValue(1.3);
    }

    [Fact]
    public void Verify_Should_ReturnFailure_WhenResultIsSuccess_AndPredicateIsFalse()
    {
        var result = _success.Verify(_falsePredicate, _failureFactory);
        result.ShouldBeFailureWithCodeAndMessage("verify", "Verify failure");
    }

    [Fact]
    public void Verify_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = _failure.Verify(_forbiddenPredicate, _failureFactory);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task VerifyAsync_Should_ReturnSuccess_WhenResultIsSuccess_AndPredicateIsTrue()
    {
        var result = await _success.VerifyAsync(_trueAsyncPredicate, _failureFactory);
        result.ShouldBeSuccessWithValue(1.3);
    }

    [Fact]
    public async Task VerifyAsync_Should_ReturnFailure_WhenResultIsSuccess_AndPredicateIsFalse()
    {
        var result = await _success.VerifyAsync(_falseAsyncPredicate, _failureFactory);
        result.ShouldBeFailureWithCodeAndMessage("verify", "Verify failure");
    }

    [Fact]
    public async Task VerifyAsync_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = await _failure.VerifyAsync(_forbiddenAsyncPredicate, _failureFactory);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task Verify_Should_ReturnSuccess_WhenResultTaskIsSuccess_AndPredicateIsTrue()
    {
        var result = await _successTask.Verify(_truePredicate, _failureFactory);
        result.ShouldBeSuccessWithValue(1.3);
    }

    [Fact]
    public async Task Verify_Should_ReturnFailure_WhenResultTaskIsSuccess_AndPredicateIsFalse()
    {
        var result = await _successTask.Verify(_falsePredicate, _failureFactory);
        result.ShouldBeFailureWithCodeAndMessage("verify", "Verify failure");
    }

    [Fact]
    public async Task Verify_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.Verify(_forbiddenPredicate, _failureFactory);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task VerifyAsync_Should_ReturnSuccess_WhenResultTaskIsSuccess_AndPredicateIsTrue()
    {
        var result = await _successTask.VerifyAsync(_trueAsyncPredicate, _failureFactory);
        result.ShouldBeSuccessWithValue(1.3);
    }

    [Fact]
    public async Task VerifyAsync_Should_ReturnFailure_WhenResultTaskIsSuccess_AndPredicateIsFalse()
    {
        var result = await _successTask.VerifyAsync(_falseAsyncPredicate, _failureFactory);
        result.ShouldBeFailureWithCodeAndMessage("verify", "Verify failure");
    }

    [Fact]
    public async Task VerifyAsync_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.VerifyAsync(_forbiddenAsyncPredicate, _failureFactory);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }
}
