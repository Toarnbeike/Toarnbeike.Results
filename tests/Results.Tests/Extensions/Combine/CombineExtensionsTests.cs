using Toarnbeike.Results.Extensions;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Tests.Extensions.Combine;

public class CombineExtensionsTests
{
    private readonly Result<int> _firstSuccess = Result.Success(1);
    private readonly Result<int> _firstFailure = Result<int>.Failure(new Failure("first", "First failure"));

    private readonly Task<Result<int>> _firstSuccessTask = Task.FromResult(Result.Success(1));
    private readonly Task<Result<int>> _firstFailureTask = Task.FromResult(Result<int>.Failure(new Failure("first", "First failure")));

    private readonly Result<string> _secondSuccess = Result.Success("success"); 
    private readonly Result<string> _secondFailure = Result<string>.Failure(new Failure("second", "Second failure"));

    private readonly Task<Result<string>> _secondSuccessTask = Task.FromResult(Result.Success("success"));
    private readonly Task<Result<string>> _secondFailureTask = Task.FromResult(Result<string>.Failure(new Failure("second", "Second failure")));

    private readonly Func<int, string, string> _mapFunc = (first, second) => $"{first} {second}";
    private readonly Func<int, string, string> _forbiddenFunc = (_,_) => throw new InvalidOperationException("This function should not be called");

    private readonly Func<int, string, Task<string>> _mapTaskFunc = (first, second) => Task.FromResult($"{first} {second}");
    private readonly Func<int, string, Task<string>> _forbiddenTaskFunc = (_,_) => throw new InvalidOperationException("This function should not be called");

    [Test]
    public void Combine_ShouldReturnSuccess_WhenBothResultsAreSuccess()
    {
        var result = _firstSuccess.Combine(_secondSuccess, _mapFunc);
        result.ShouldBeSuccess().ShouldBe("1 success");
    }

    [Test]
    public void Combine_ShouldReturnFailure_WhenFirstResultIsFailure()
    {
        var result = _firstFailure.Combine(_secondSuccess, _forbiddenFunc);
        result.ShouldBeFailure().Code.ShouldBe("first");
    }

    [Test]
    public void Combine_ShouldReturnFailure_WhenSecondResultIsFailure()
    {
        var result = _firstSuccess.Combine(_secondFailure, _forbiddenFunc);
        result.ShouldBeFailure().Code.ShouldBe("second");
    }

    [Test]
    public async Task CombineAsync_ShouldReturnSuccess_WhenBothResultsAreSuccess()
    {
        var result = await _firstSuccess.CombineAsync(_secondSuccess, _mapTaskFunc);
        result.ShouldBeSuccess().ShouldBe("1 success");
    }

    [Test]
    public async Task CombineAsync_ShouldReturnFailure_WhenFirstResultIsFailure()
    {
        var result = await _firstFailure.CombineAsync(_secondSuccess, _forbiddenTaskFunc);
        result.ShouldBeFailure().Code.ShouldBe("first");
    }

    [Test]
    public async Task CombineAsync_ShouldReturnFailure_WhenSecondResultIsFailure()
    {
        var result = await _firstSuccess.CombineAsync(_secondFailure, _forbiddenTaskFunc);
        result.ShouldBeFailure().Code.ShouldBe("second");
    }

    [Test]
    public async Task Combine_ShouldReturnSuccess_WhenBothResultTasksAreSuccess()
    {
        var result = await _firstSuccessTask.Combine(_secondSuccessTask, _mapFunc);
        result.ShouldBeSuccess().ShouldBe("1 success");
    }

    [Test]
    public async Task Combine_ShouldReturnFailure_WhenFirstResultTaskIsFailure()
    {
        var result = await _firstFailureTask.Combine(_secondSuccessTask, _forbiddenFunc);
        result.ShouldBeFailure().Code.ShouldBe("first");
    }

    [Test]
    public async Task Combine_ShouldReturnFailure_WhenSecondResultTaskIsFailure()
    {
        var result = await _firstSuccessTask.Combine(_secondFailureTask, _forbiddenFunc);
        result.ShouldBeFailure().Code.ShouldBe("second");
    }

    [Test]
    public async Task CombineAsync_ShouldReturnSuccess_WhenBothResultTasksAreSuccess()
    {
        var result = await _firstSuccessTask.CombineAsync(_secondSuccessTask, _mapTaskFunc);
        result.ShouldBeSuccess().ShouldBe("1 success");
    }

    [Test]
    public async Task CombineAsync_ShouldReturnFailure_WhenFirstResultTaskIsFailure()
    {
        var result = await _firstFailureTask.CombineAsync(_secondSuccessTask, _forbiddenTaskFunc);
        result.ShouldBeFailure().Code.ShouldBe("first");
    }

    [Test]
    public async Task CombineAsync_ShouldReturnFailure_WhenSecondResultTaskIsFailure()
    {
        var result = await _firstSuccessTask.CombineAsync(_secondFailureTask, _forbiddenTaskFunc);
        result.ShouldBeFailure().Code.ShouldBe("second");
    }
}
