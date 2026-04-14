using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Tests.Extensions.Combine;

public class CombineBindExtensionsTests
{
    private readonly Result<int> _firstSuccess = Result.Success(1);
    private readonly Result<int> _firstFailure = Result<int>.Failure(new TestFailure("first"));

    private readonly Task<Result<int>> _firstSuccessTask = Task.FromResult(Result.Success(1));
    private readonly Task<Result<int>> _firstFailureTask = Task.FromResult(Result<int>.Failure(new TestFailure("first")));

    private readonly Result<string> _secondSuccess = Result.Success("success");
    private readonly Result<string> _secondFailure = Result<string>.Failure(new TestFailure("second"));

    private readonly Task<Result<string>> _secondSuccessTask = Task.FromResult(Result.Success("success"));
    private readonly Task<Result<string>> _secondFailureTask = Task.FromResult(Result<string>.Failure(new TestFailure("second")));

    private readonly Func<int, string, Result<string>> _mapFunc = (first, second) => $"{first} {second}";
    private readonly Func<int, string, Result<string>> _forbiddenFunc = (_, _) => throw new InvalidOperationException("This function should not be called");

    private readonly Func<int, string, Task<Result<string>>> _mapTaskFunc = (first, second) => Result.SuccessTask($"{first} {second}");
    private readonly Func<int, string, Task<Result<string>>> _forbiddenTaskFunc = (_, _) => throw new InvalidOperationException("This function should not be called");

    [Test]
    public void CombineBind_ShouldReturnSuccess_WhenBothResultsAreSuccess()
    {
        var result = _firstSuccess.CombineBind(_secondSuccess, _mapFunc);
        result.ShouldBeSuccess().ShouldBe("1 success");
    }

    [Test]
    public void CombineBind_ShouldReturnFailure_WhenFirstResultIsFailure()
    {
        var result = _firstFailure.CombineBind(_secondSuccess, _forbiddenFunc);
        result.ShouldBeFailure().Message.ShouldBe("first");
    }

    [Test]
    public void CombineBind_ShouldReturnFailure_WhenSecondResultIsFailure()
    {
        var result = _firstSuccess.CombineBind(_secondFailure, _forbiddenFunc);
        result.ShouldBeFailure().Message.ShouldBe("second");
    }

    [Test]
    public async Task CombineBindAsync_ShouldReturnSuccess_WhenBothResultsAreSuccess()
    {
        var result = await _firstSuccess.CombineBindAsync(_secondSuccess, _mapTaskFunc);
        result.ShouldBeSuccess().ShouldBe("1 success");
    }

    [Test]
    public async Task CombineBindAsync_ShouldReturnFailure_WhenFirstResultIsFailure()
    {
        var result = await _firstFailure.CombineBindAsync(_secondSuccess, _forbiddenTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("first");
    }

    [Test]
    public async Task CombineBindAsync_ShouldReturnFailure_WhenSecondResultIsFailure()
    {
        var result = await _firstSuccess.CombineBindAsync(_secondFailure, _forbiddenTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("second");
    }

    [Test]
    public async Task CombineBind_ShouldReturnSuccess_WhenBothResultTasksAreSuccess()
    {
        var result = await _firstSuccessTask.CombineBind(_secondSuccessTask, _mapFunc);
        result.ShouldBeSuccess().ShouldBe("1 success");
    }

    [Test]
    public async Task CombineBind_ShouldReturnFailure_WhenFirstResultTaskIsFailure()
    {
        var result = await _firstFailureTask.CombineBind(_secondSuccessTask, _forbiddenFunc);
        result.ShouldBeFailure().Message.ShouldBe("first");
    }

    [Test]
    public async Task CombineBind_ShouldReturnFailure_WhenSecondResultTaskIsFailure()
    {
        var result = await _firstSuccessTask.CombineBind(_secondFailureTask, _forbiddenFunc);
        result.ShouldBeFailure().Message.ShouldBe("second");
    }

    [Test]
    public async Task CombineBindAsync_ShouldReturnSuccess_WhenBothResultTasksAreSuccess()
    {
        var result = await _firstSuccessTask.CombineBindAsync(_secondSuccessTask, _mapTaskFunc);
        result.ShouldBeSuccess().ShouldBe("1 success");
    }

    [Test]
    public async Task CombineBindAsync_ShouldReturnFailure_WhenFirstResultTaskIsFailure()
    {
        var result = await _firstFailureTask.CombineBindAsync(_secondSuccessTask, _forbiddenTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("first");
    }

    [Test]
    public async Task CombineBindAsync_ShouldReturnFailure_WhenSecondResultTaskIsFailure()
    {
        var result = await _firstSuccessTask.CombineBindAsync(_secondFailureTask, _forbiddenTaskFunc);
        result.ShouldBeFailure().Message.ShouldBe("second");
    }
}