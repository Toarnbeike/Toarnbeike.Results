using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.TestHelpers;

namespace Toarnbeike.Results.Tests.Extensions.Obsolete;

/// <summary>
/// Tests for the <see cref="VerifyWhenExtensions"/> on a <see cref="Result{TValue}"/>.
/// </summary>
[Obsolete("Use BindTap() for binding results without taking their values instead.")]
public class VerifyWhenExtensionsTests
{
    private readonly Result<double> _success = Result.Success(1.3);
    private readonly Result<double> _failure = Result<double>.Failure(new TestFailure("original"));

    private readonly Task<Result<double>> _successTask = Task.FromResult(Result.Success(1.3));
    private readonly Task<Result<double>> _failureTask = Task.FromResult(Result<double>.Failure(new TestFailure("original")));

    private readonly Func<double, bool> _truePredicate = value => value > 1;
    private readonly Func<double, bool> _falsePredicate = value => value < 1;
    private readonly Func<double, bool> _forbiddenPredicate = _ => throw new InvalidOperationException("This function should not be called");

    private readonly Func<double, Result> _successFunc = value => Result.Success((int)value);
    private readonly Func<double, Result> _failureFunc = _ => Result.Failure(new TestFailure("VerifyWhen"));
    private readonly Func<double, Result> _forbiddenFunc = _ => throw new InvalidOperationException("This function should not be called");

    private readonly Func<double, Result<int>> _successOfTFunc = value => Result.Success((int)value);
    private readonly Func<double, Result<int>> _failureOfTFunc = _ => Result<int>.Failure(new TestFailure("VerifyWhen"));
    private readonly Func<double, Result<int>> _forbiddenOfTFunc = _ => throw new InvalidOperationException("This function should not be called");

    private readonly Func<double, Task<Result>> _successTaskFunc = value => Task.FromResult((Result)Result.Success((int)value));
    private readonly Func<double, Task<Result>> _failureTaskFunc = _ => Task.FromResult(Result.Failure(new TestFailure("VerifyWhen")));
    private readonly Func<double, Task<Result>> _forbiddenTaskFunc = _ => throw new InvalidOperationException("This function should not be called");

    private readonly Func<double, Task<Result<int>>> _successOfTTaskFunc = _ => Task.FromResult(Result<int>.Success(42));
    private readonly Func<double, Task<Result<int>>> _failureOfTTaskFunc = _ => Task.FromResult(Result<int>.Failure(new TestFailure("VerifyWhen")));
    private readonly Func<double, Task<Result<int>>> _forbiddenOfTTaskFunc = _ => throw new InvalidOperationException("This function should not be called");

    [Test]
    public void VerifyWhen_Should_ReturnSuccess_WhenResultIsSuccess_AndPredicateIsFalse()
    {
        var result = _success.VerifyWhen(_falsePredicate, _forbiddenFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public void VerifyWhen_Should_ReturnSuccess_WhenResultIsSuccess_PredicateIsTrue_AndFunctionSucceeds()
    {
        var result = _success.VerifyWhen(_truePredicate, _successFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public void VerifyWhen_Should_ReturnFailure_WhenResultIsSuccess_PredicateIsTrue_AndFunctionFails()
    {
        var result = _success.VerifyWhen(_truePredicate, _failureFunc);
        result.ShouldBeFailure().Message.ShouldBe("VerifyWhen");
    }

    [Test]
    public void VerifyWhen_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = _failure.VerifyWhen(_forbiddenPredicate, _forbiddenFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public void VerifyWhen_Should_ReturnSuccess_WhenResultIsSuccess_PredicateIsTrue_AndFunctionOfTSucceeds()
    {
        var result = _success.VerifyWhen(_truePredicate, _successOfTFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public void VerifyWhen_Should_ReturnFailure_WhenResultIsSuccess_PredicateIsTrue_AndFunctionOfTFails()
    {
        var result = _success.VerifyWhen(_truePredicate, _failureOfTFunc);
        result.ShouldBeFailure().Message.ShouldBe("VerifyWhen");
    }

    [Test]
    public void VerifyWhen_Should_ReturnFailure_WhenResultIsFailure_AndCheckIsOfT()
    {
        var result = _failure.VerifyWhen(_forbiddenPredicate, _forbiddenOfTFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public async Task VerifyWhenAsync_Should_ReturnValue_WhenResultIsSuccess_AndPredicateIsFalse()
    {
        var result = await _success.VerifyWhenAsync(_falsePredicate, _forbiddenTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task VerifyWhenAsync_Should_ReturnValue_WhenResultIsSuccess_PredicateIsTrue_AndFunctionSucceeds()
    {
        var result = await _success.VerifyWhenAsync(_truePredicate, _successTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task VerifyWhenAsync_Should_ReturnFailure_WhenResultIsSuccess_PredicateIsTrue_AndFunctionFails()
    {
        var result = await _success.VerifyWhenAsync(_truePredicate, _failureTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("VerifyWhen");
    }

    [Test]
    public async Task VerifyWhenAsync_Should_ReturnFailure_WhenResultIsFailure()
    {
        var result = await _failure.VerifyWhenAsync(_forbiddenPredicate, _forbiddenTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public async Task VerifyWhenAsync_Should_ReturnValue_WhenResultIsSuccess_PredicateIsTrue_AndFunctionOfTSucceeds()
    {
        var result = await _success.VerifyWhenAsync(_truePredicate, _successOfTTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task VerifyWhenAsync_Should_ReturnFailure_WhenResultIsSuccess_PredicateIsTrue_AndFunctionOfTFails()
    {
        var result = await _success.VerifyWhenAsync(_truePredicate, _failureOfTTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("VerifyWhen");
    }

    [Test]
    public async Task VerifyWhenAsync_Should_ReturnFailure_WhenResultIsFailure_AndCheckIsOfT()
    {
        var result = await _failure.VerifyWhenAsync(_forbiddenPredicate, _forbiddenOfTTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public async Task VerifyWhen_Should_ReturnValue_WhenResultTaskIsSuccess_AndPredicateIsFalse()
    {
        var result = await _successTask.VerifyWhen(_falsePredicate, _forbiddenFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task VerifyWhen_Should_ReturnValue_WhenResultTaskIsSuccess_PredicateIsTrue_AndFunctionSucceeds()
    {
        var result = await _successTask.VerifyWhen(_truePredicate, _successFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task VerifyWhen_Should_ReturnFailure_WhenResultTaskIsSuccess_PredicateIsTrue_AndFunctionFails()
    {
        var result = await _successTask.VerifyWhen(_truePredicate, _failureFunc);
        result.ShouldBeFailure().Message.ShouldBe("VerifyWhen");
    }

    [Test]
    public async Task VerifyWhen_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.VerifyWhen(_forbiddenPredicate, _forbiddenFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public async Task VerifyWhen_Should_ReturnValue_WhenResultTaskIsSuccess_PredicateIsTrue_AndFunctionOfTSucceeds()
    {
        var result = await _successTask.VerifyWhen(_truePredicate, _successOfTFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task VerifyWhen_Should_ReturnFailure_WhenResultTaskIsSuccess_PredicateIsTrue_AndFunctionOfTFails()
    {
        var result = await _successTask.VerifyWhen(_truePredicate, _failureOfTFunc);
        result.ShouldBeFailure().Message.ShouldBe("VerifyWhen");
    }

    [Test]
    public async Task VerifyWhen_Should_ReturnFailure_WhenResultTaskIsFailure_AndCheckIsOfT()
    {
        var result = await _failureTask.VerifyWhen(_forbiddenPredicate, _forbiddenOfTFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public async Task VerifyWhenAsync_Should_ReturnValue_WhenResultTaskIsSuccess_PredicateIsFalse()
    {
        var result = await _successTask.VerifyWhenAsync(_falsePredicate, _forbiddenTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task VerifyWhenAsync_Should_ReturnValue_WhenResultTaskIsSuccess_PredicateIsTrue_AndFunctionSucceeds()
    {
        var result = await _successTask.VerifyWhenAsync(_truePredicate, _successTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task VerifyWhenAsync_Should_ReturnFailure_WhenResultTaskIsSuccess_PredicateIsTrue_AndFunctionFails()
    {
        var result = await _successTask.VerifyWhenAsync(_truePredicate, _failureTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("VerifyWhen");
    }

    [Test]
    public async Task VerifyWhenAsync_Should_ReturnFailure_WhenResultTaskIsFailure()
    {
        var result = await _failureTask.VerifyWhenAsync(_forbiddenPredicate, _forbiddenTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }

    [Test]
    public async Task VerifyWhenAsync_Should_ReturnValue_WhenResultTaskIsSuccess_PredicateIsTrue_AndFunctionOfTSucceeds()
    {
        var result = await _successTask.VerifyWhenAsync(_truePredicate, _successOfTTaskFunc);
        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public async Task VerifyWhenAsync_Should_ReturnFailure_WhenResultTaskIsSuccess_PredicateIsTrue_AndFunctionOfTFails()
    {
        var result = await _successTask.VerifyWhenAsync(_truePredicate, _failureOfTTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("VerifyWhen");
    }

    [Test]
    public async Task VerifyWhenAsync_Should_ReturnFailure_WhenResultTaskIsFailure_AndCheckIsOfT()
    {
        var result = await _failureTask.VerifyWhenAsync(_forbiddenPredicate, _forbiddenOfTTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("original");
    }
}
