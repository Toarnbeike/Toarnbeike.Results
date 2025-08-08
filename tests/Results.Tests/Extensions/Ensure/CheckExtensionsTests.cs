using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.TestHelpers;

namespace Toarnbeike.Results.Tests.Extensions.Ensure;

/// <summary>
/// Tests for the <see cref="CheckExtensions"/> on a <see cref="Result{TValue}"/>.
/// </summary>
public class CheckExtensionsTests
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
        var result = _success.Check(_truePredicate, _failureFactory);
        result.ShouldBeSuccessWithValue(1.3);
    }

    [Fact]
    public void Ensure_Should_ReturnFailure_WhenResultIsSuccess_AndPredicateIsFalse()
    {
        var result = _success.Check(_falsePredicate, _failureFactory);
        result.ShouldBeFailureWithCodeAndMessage("Ensure", "Ensure failure");
    }

    [Fact]
    public void Ensure_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = _failure.Check(_forbiddenPredicate, _failureFactory);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task EnsureAsync_Should_ReturnSuccess_WhenResultIsSuccess_AndPredicateIsTrue()
    {
        var result = await _success.CheckAsync(_trueAsyncPredicate, _failureFactory);
        result.ShouldBeSuccessWithValue(1.3);
    }

    [Fact]
    public async Task EnsureAsync_Should_ReturnFailure_WhenResultIsSuccess_AndPredicateIsFalse()
    {
        var result = await _success.CheckAsync(_falseAsyncPredicate, _failureFactory);
        result.ShouldBeFailureWithCodeAndMessage("Ensure", "Ensure failure");
    }

    [Fact]
    public async Task EnsureAsync_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = await _failure.CheckAsync(_forbiddenAsyncPredicate, _failureFactory);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task Ensure_Should_ReturnSuccess_WhenResultTaskIsSuccess_AndPredicateIsTrue()
    {
        var result = await _successTask.Check(_truePredicate, _failureFactory);
        result.ShouldBeSuccessWithValue(1.3);
    }

    [Fact]
    public async Task Ensure_Should_ReturnFailure_WhenResultTaskIsSuccess_AndPredicateIsFalse()
    {
        var result = await _successTask.Check(_falsePredicate, _failureFactory);
        result.ShouldBeFailureWithCodeAndMessage("Ensure", "Ensure failure");
    }

    [Fact]
    public async Task Ensure_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.Check(_forbiddenPredicate, _failureFactory);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task EnsureAsync_Should_ReturnSuccess_WhenResultTaskIsSuccess_AndPredicateIsTrue()
    {
        var result = await _successTask.CheckAsync(_trueAsyncPredicate, _failureFactory);
        result.ShouldBeSuccessWithValue(1.3);
    }

    [Fact]
    public async Task EnsureAsync_Should_ReturnFailure_WhenResultTaskIsSuccess_AndPredicateIsFalse()
    {
        var result = await _successTask.CheckAsync(_falseAsyncPredicate, _failureFactory);
        result.ShouldBeFailureWithCodeAndMessage("Ensure", "Ensure failure");
    }

    [Fact]
    public async Task EnsureAsync_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.CheckAsync(_forbiddenAsyncPredicate, _failureFactory);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }
}
