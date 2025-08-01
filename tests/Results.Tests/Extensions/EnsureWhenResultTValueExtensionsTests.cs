using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.Tests.Internal;

namespace Toarnbeike.Results.Tests.Extensions;

/// <summary>
/// Tests for the <see cref="EnsureWhenResultTValueExtensions"/> class.
/// </summary>
public class EnsureWhenResultTValueExtensionsTests
{
    private readonly Result<double> _success = Result.Success(1.3);
    private readonly Result<double> _failure = Result<double>.Failure(new Failure("original", "Original failure"));

    private readonly Task<Result<double>> _successTask = Task.FromResult(Result.Success(1.3));
    private readonly Task<Result<double>> _failureTask = Task.FromResult(Result<double>.Failure(new Failure("original", "Original failure")));

    private readonly Func<double, bool> _truePredicate = value => value > 1;
    private readonly Func<double, bool> _falsePredicate = value => value < 1;
    private readonly Func<double, bool> _forbiddenPredicate = _ => throw new InvalidOperationException("This function should not be called");

    private readonly Func<double, Result> _successFunc = value => Result.Success((int)value);
    private readonly Func<double, Result> _failureFunc = _ => Result.Failure(new Failure("ensurewhen", "EnsureWhen failure"));
    private readonly Func<double, Result> _forbiddenFunc = _ => throw new InvalidOperationException("This function should not be called");

    private readonly Func<double, Result<int>> _successOfTFunc = value => Result.Success((int)value);
    private readonly Func<double, Result<int>> _failureOfTFunc = _ => Result<int>.Failure(new Failure("ensurewhen", "EnsureWhen failure"));
    private readonly Func<double, Result<int>> _forbiddenOfTFunc = _ => throw new InvalidOperationException("This function should not be called");

    private readonly Func<double, Task<Result>> _successTaskFunc = value => Task.FromResult((Result)Result.Success((int)value));
    private readonly Func<double, Task<Result>> _failureTaskFunc = _ => Task.FromResult(Result.Failure(new Failure("ensurewhen", "EnsureWhen failure")));
    private readonly Func<double, Task<Result>> _forbiddenTaskFunc = _ => throw new InvalidOperationException("This function should not be called");

    private readonly Func<double, Task<Result<int>>> _successOfTTaskFunc = _ => Task.FromResult(Result<int>.Success(42));
    private readonly Func<double, Task<Result<int>>> _failureOfTTaskFunc = _ => Task.FromResult(Result<int>.Failure(new Failure("ensurewhen", "EnsureWhen failure")));
    private readonly Func<double, Task<Result<int>>> _forbiddenOfTTaskFunc = _ => throw new InvalidOperationException("This function should not be called");

    [Fact]
    public void EnsureWhen_Should_ReturnSuccess_WhenResultIsSuccess_AndPredicateIsFalse()
    {
        var result = _success.EnsureWhen(_falsePredicate, _forbiddenFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void EnsureWhen_Should_ReturnSuccess_WhenResultIsSuccess_PredicateIsTrue_AndFunctionSucceeds()
    {
        var result = _success.EnsureWhen(_truePredicate, _successFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void EnsureWhen_Should_ReturnFailure_WhenResultIsSuccess_PredicateIsTrue_AndFunctionFails()
    {
        var result = _success.EnsureWhen(_truePredicate, _failureFunc);
        result.ShouldBeFailureWithCodeAndMessage("ensurewhen", "EnsureWhen failure");
    }

    [Fact]
    public void EnsureWhen_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = _failure.EnsureWhen(_forbiddenPredicate, _forbiddenFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public void EnsureWhen_Should_ReturnSuccess_WhenResultIsSuccess_PredicateIsTrue_AndFunctionOfTSucceeds()
    {
        var result = _success.EnsureWhen(_truePredicate, _successOfTFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void EnsureWhen_Should_ReturnFailure_WhenResultIsSuccess_PredicateIsTrue_AndFunctionOfTFails()
    {
        var result = _success.EnsureWhen(_truePredicate, _failureOfTFunc);
        result.ShouldBeFailureWithCodeAndMessage("ensurewhen", "EnsureWhen failure");
    }

    [Fact]
    public void EnsureWhen_Should_ReturnFailure_WhenResultIsFailure_AndCheckIsOfT()
    {
        var result = _failure.EnsureWhen(_forbiddenPredicate, _forbiddenOfTFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task EnsureWhenAsync_Should_ReturnValue_WhenResultIsSuccess_AndPredicateIsFalse()
    {
        var result = await _success.EnsureWhenAsync(_falsePredicate, _forbiddenTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task EnsureWhenAsync_Should_ReturnValue_WhenResultIsSuccess_PredicateIsTrue_AndFunctionSucceeds()
    {
        var result = await _success.EnsureWhenAsync(_truePredicate, _successTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task EnsureWhenAsync_Should_ReturnFailure_WhenResultIsSuccess_PredicateIsTrue_AndFunctionFails()
    {
        var result = await _success.EnsureWhenAsync(_truePredicate, _failureTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("ensurewhen", "EnsureWhen failure");
    }

    [Fact]
    public async Task EnsureWhenAsync_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = await _failure.EnsureWhenAsync(_forbiddenPredicate, _forbiddenTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task EnsureWhenAsync_Should_ReturnValue_WhenResultIsSuccess_PredicateIsTrue_AndFunctionOfTSucceeds()
    {
        var result = await _success.EnsureWhenAsync(_truePredicate, _successOfTTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task EnsureWhenAsync_Should_ReturnFailure_WhenResultIsSuccess_PredicateIsTrue_AndFunctionOfTFails()
    {
        var result = await _success.EnsureWhenAsync(_truePredicate, _failureOfTTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("ensurewhen", "EnsureWhen failure");
    }

    [Fact]
    public async Task EnsureWhenAsync_Should_ReturnFailure_WhenResultIsFailure_AndCheckIsOfT()
    {
        var result = await _failure.EnsureWhenAsync(_forbiddenPredicate, _forbiddenOfTTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task EnsureWhen_Should_ReturnValue_WhenResultTaskIsSuccess_AndPredicateIsFalse()
    {
        var result = await _successTask.EnsureWhen(_falsePredicate, _forbiddenFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task EnsureWhen_Should_ReturnValue_WhenResultTaskIsSuccess_PredicateIsTrue_AndFunctionSucceeds()
    {
        var result = await _successTask.EnsureWhen(_truePredicate, _successFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task EnsureWhen_Should_ReturnFailure_WhenResultTaskIsSuccess_PredicateIsTrue_AndFunctionFails()
    {
        var result = await _successTask.EnsureWhen(_truePredicate, _failureFunc);
        result.ShouldBeFailureWithCodeAndMessage("ensurewhen", "EnsureWhen failure");
    }

    [Fact]
    public async Task EnsureWhen_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.EnsureWhen(_forbiddenPredicate, _forbiddenFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task EnsureWhen_Should_ReturnValue_WhenResultTaskIsSuccess_PredicateIsTrue_AndFunctionOfTSucceeds()
    {
        var result = await _successTask.EnsureWhen(_truePredicate, _successOfTFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task EnsureWhen_Should_ReturnFailure_WhenResultTaskIsSuccess_PredicateIsTrue_AndFunctionOfTFails()
    {
        var result = await _successTask.EnsureWhen(_truePredicate, _failureOfTFunc);
        result.ShouldBeFailureWithCodeAndMessage("ensurewhen", "EnsureWhen failure");
    }

    [Fact]
    public async Task EnsureWhen_Should_ReturnFailure_WhenResultTaskIsFailure_AndCheckIsOfT()
    {
        var result = await _failureTask.EnsureWhen(_forbiddenPredicate, _forbiddenOfTFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task EnsureWhenAsync_Should_ReturnValue_WhenResultTaskIsSuccess_PredicateIsFalse()
    {
        var result = await _successTask.EnsureWhenAsync(_falsePredicate, _forbiddenTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task EnsureWhenAsync_Should_ReturnValue_WhenResultTaskIsSuccess_PredicateIsTrue_AndFunctionSucceeds()
    {
        var result = await _successTask.EnsureWhenAsync(_truePredicate, _successTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task EnsureWhenAsync_Should_ReturnFailure_WhenResultTaskIsSuccess_PredicateIsTrue_AndFunctionFails()
    {
        var result = await _successTask.EnsureWhenAsync(_truePredicate, _failureTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("ensurewhen", "EnsureWhen failure");
    }

    [Fact]
    public async Task EnsureWhenAsync_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.EnsureWhenAsync(_forbiddenPredicate, _forbiddenTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }

    [Fact]
    public async Task EnsureWhenAsync_Should_ReturnValue_WhenResultTaskIsSuccess_PredicateIsTrue_AndFunctionOfTSucceeds()
    {
        var result = await _successTask.EnsureWhenAsync(_truePredicate, _successOfTTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task EnsureWhenAsync_Should_ReturnFailure_WhenResultTaskIsSuccess_PredicateIsTrue_AndFunctionOfTFails()
    {
        var result = await _successTask.EnsureWhenAsync(_truePredicate, _failureOfTTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("ensurewhen", "EnsureWhen failure");
    }

    [Fact]
    public async Task EnsureWhenAsync_Should_ReturnFailure_WhenResultTaskIsFailure_AndCheckIsOfT()
    {
        var result = await _failureTask.EnsureWhenAsync(_forbiddenPredicate, _forbiddenOfTTaskFunc);
        result.ShouldBeFailureWithCodeAndMessage("original", "Original failure");
    }
}
