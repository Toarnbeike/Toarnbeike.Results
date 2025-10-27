using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestHelpers;

namespace Toarnbeike.Results.Tests;

public class ResultTryTests
{
    [Fact]
    public void Try_Action_Success()
    {
        var result = Result.Try(() => { /* no-op */ });

        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void Try_Action_Exception_ReturnsFailure()
    {
        var result = Result.Try(() => throw new InvalidOperationException("Something went wrong"));

        var failure = result.ShouldBeFailureOfType<ExceptionFailure>();

        failure.Exception.ShouldBeOfType<InvalidOperationException>();
        failure.Code.ShouldBe("exception:InvalidOperationException");
        failure.Message.ShouldBe("Something went wrong");
        failure.ExceptionType.ShouldBe("InvalidOperationException");
    }

    [Fact]
    public void Try_Function_Success()
    {
        var result = Result.Try(() => 42);

        result.ShouldBeSuccessWithValue(42);
    }

    [Fact]
    public void Try_Function_Exception_ReturnsFailure()
    {
        var result = Result.Try<int>(() => throw new ArgumentException("Invalid input"));

        var failure = result.ShouldBeFailureOfType<ExceptionFailure>();

        failure.Exception.ShouldBeOfType<ArgumentException>();
        failure.Code.ShouldBe("exception:ArgumentException");
        failure.Message.ShouldBe("Invalid input");
        failure.ExceptionType.ShouldBe("ArgumentException");
    }

    [Fact]
    public async Task TryAsync_Task_Success()
    {
        var result = await Result.TryAsync(async () => await Task.Yield());

        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task TryAsync_Task_Exception_ReturnsFailure()
    {
        var result = await Result.TryAsync(async () =>
        {
            await Task.Yield();
            throw new NotSupportedException("Not supported!");
        });

        var failure = result.ShouldBeFailureOfType<ExceptionFailure>();

        failure.Exception.ShouldBeOfType<NotSupportedException>();
        failure.Code.ShouldBe("exception:NotSupportedException");
        failure.Message.ShouldBe("Not supported!");
        failure.ExceptionType.ShouldBe("NotSupportedException");
    }

    [Fact]
    public async Task TryValueTaskAsync_Task_Success()
    {
        var result = await Result.TryValueAsync(async () => await ValueTask.CompletedTask);

        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task TryValueTaskAsync_Task_Exception_ReturnsFailure()
    {
        var result = await Result.TryAsync(async () =>
        {
            await ValueTask.CompletedTask;
            throw new NotSupportedException("Not supported!");
        });

        var failure = result.ShouldBeFailureOfType<ExceptionFailure>();

        failure.Exception.ShouldBeOfType<NotSupportedException>();
        failure.Code.ShouldBe("exception:NotSupportedException");
        failure.Message.ShouldBe("Not supported!");
        failure.ExceptionType.ShouldBe("NotSupportedException");
    }


    [Fact]
    public async Task TryAsync_Function_Success()
    {
        var result = await Result.TryAsync(async () =>
        {
            await Task.Yield();
            return "async value";
        });

        result.ShouldBeSuccessWithValue("async value");
    }

    [Fact]
    public async Task TryAsync_Function_Exception_ReturnsFailure()
    {
        var result = await Result.TryAsync<string>(async () =>
        {
            await Task.Yield();
            throw new NullReferenceException("Something is null");
        });

        var failure = result.ShouldBeFailureOfType<ExceptionFailure>();

        failure.Exception.ShouldBeOfType<NullReferenceException>();
        failure.Code.ShouldBe("exception:NullReferenceException");
        failure.Message.ShouldBe("Something is null");
        failure.ExceptionType.ShouldBe("NullReferenceException");
    }

    [Fact]
    public async Task TryValueAsync_Function_Success()
    {
        var result = await Result.TryValueAsync(async () =>
        {
            await ValueTask.CompletedTask;
            return "async value";
        });

        result.ShouldBeSuccessWithValue("async value");
    }

    [Fact]
    public async Task TryValueAsync_Function_Exception_ReturnsFailure()
    {
        var result = await Result.TryAsync<string>(async () =>
        {
            await ValueTask.CompletedTask;
            throw new NullReferenceException("Something is null");
        });

        var failure = result.ShouldBeFailureOfType<ExceptionFailure>();

        failure.Exception.ShouldBeOfType<NullReferenceException>();
        failure.Code.ShouldBe("exception:NullReferenceException");
        failure.Message.ShouldBe("Something is null");
        failure.ExceptionType.ShouldBe("NullReferenceException");
    }
}
