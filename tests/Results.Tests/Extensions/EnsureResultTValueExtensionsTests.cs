using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Tests.Internal;

namespace Toarnbeike.Results.Tests.Extensions;

/// <summary>
/// Tests for the <see cref="EnsureResultTValueExtensions"/> class.
/// </summary>
public class EnsureResultTValueExtensionsTests
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

    private readonly Func<Failure> _failureFactory = () => new Failure("Ensure", "Ensure failure");

    [Fact]
    public void Ensure_Should_ReturnSuccess_WhenResultIsSuccess_AndPredicateIsTrue()
    {
        var result = _success.Ensure(_truePredicate, _failureFactory);
        result.ShouldBeSuccessWithValue(1.3);
    }

    [Fact]
    public void Ensure_Should_ReturnFailure_WhenResultIsSuccess_AndPredicateIsFalse()
    {
        var result = _success.Ensure(_falsePredicate, _failureFactory);
        result.ShouldBeFailureWithCodeAndMessage("Ensure", "Ensure failure");
    }

    [Fact]
    public void Ensure_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = _failure.Ensure(_forbiddenPredicate, _failureFactory);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task EnsureAsync_Should_ReturnSuccess_WhenResultIsSuccess_AndPredicateIsTrue()
    {
        var result = await _success.EnsureAsync(_trueAsyncPredicate, _failureFactory);
        result.ShouldBeSuccessWithValue(1.3);
    }

    [Fact]
    public async Task EnsureAsync_Should_ReturnFailure_WhenResultIsSuccess_AndPredicateIsFalse()
    {
        var result = await _success.EnsureAsync(_falseAsyncPredicate, _failureFactory);
        result.ShouldBeFailureWithCodeAndMessage("Ensure", "Ensure failure");
    }

    [Fact]
    public async Task EnsureAsync_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = await _failure.EnsureAsync(_forbiddenAsyncPredicate, _failureFactory);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task Ensure_Should_ReturnSuccess_WhenResultTaskIsSuccess_AndPredicateIsTrue()
    {
        var result = await _successTask.Ensure(_truePredicate, _failureFactory);
        result.ShouldBeSuccessWithValue(1.3);
    }

    [Fact]
    public async Task Ensure_Should_ReturnFailure_WhenResultTaskIsSuccess_AndPredicateIsFalse()
    {
        var result = await _successTask.Ensure(_falsePredicate, _failureFactory);
        result.ShouldBeFailureWithCodeAndMessage("Ensure", "Ensure failure");
    }

    [Fact]
    public async Task Ensure_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.Ensure(_forbiddenPredicate, _failureFactory);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task EnsureAsync_Should_ReturnSuccess_WhenResultTaskIsSuccess_AndPredicateIsTrue()
    {
        var result = await _successTask.EnsureAsync(_trueAsyncPredicate, _failureFactory);
        result.ShouldBeSuccessWithValue(1.3);
    }

    [Fact]
    public async Task EnsureAsync_Should_ReturnFailure_WhenResultTaskIsSuccess_AndPredicateIsFalse()
    {
        var result = await _successTask.EnsureAsync(_falseAsyncPredicate, _failureFactory);
        result.ShouldBeFailureWithCodeAndMessage("Ensure", "Ensure failure");
    }

    [Fact]
    public async Task EnsureAsync_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.EnsureAsync(_forbiddenAsyncPredicate, _failureFactory);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }
}
