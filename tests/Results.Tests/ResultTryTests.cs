using Toarnbeike.Results.Failures;
using Toarnbeike.Results.TestExtensions;

namespace Toarnbeike.Results.Tests;

public class ResultTryTests
{
    [Test]
    public void Try_Action_Success()
    {
        var result = Result.Try(() => { /* no-op */ });

        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public void Try_Action_Exception_ReturnsFailure()
    {
        var result = Result.Try(() => throw new InvalidOperationException("Something went wrong"));

        var failure = result.ShouldBeFailureOfType<ExceptionFailure>();

        failure.Exception.ShouldBeOfType<InvalidOperationException>();
        failure.Code.ShouldBe("exception:InvalidOperationException");
        failure.Message.ShouldBe("Something went wrong");
        failure.ExceptionType.ShouldBe("InvalidOperationException");
    }

    [Test]
    public void Try_Function_Success()
    {
        var result = Result.Try(() => 42);

        result.ShouldBeSuccess().ShouldBe(42);
    }

    [Test]
    public void Try_Function_Exception_ReturnsFailure()
    {
        var result = Result.Try<int>(() => throw new ArgumentException("Invalid input"));

        var failure = result.ShouldBeFailureOfType<ExceptionFailure>();

        failure.Exception.ShouldBeOfType<ArgumentException>();
        failure.Code.ShouldBe("exception:ArgumentException");
        failure.Message.ShouldBe("Invalid input");
        failure.ExceptionType.ShouldBe("ArgumentException");
    }

    [Test]
    public async Task TryAsync_Task_Success()
    {
        var result = await Result.TryAsync(async () => await Task.Yield());

        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
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

    [Test]
    public async Task TryValueTaskAsync_Task_Success()
    {
        var result = await Result.TryValueAsync(async () => await ValueTask.CompletedTask);

        result.IsSuccess.ShouldBeTrue();
    }

    [Test]
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


    [Test]
    public async Task TryAsync_Function_Success()
    {
        var result = await Result.TryAsync(async () =>
        {
            await Task.Yield();
            return "async value";
        });

        result.ShouldBeSuccess().ShouldBe("async value");
    }

    [Test]
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

    [Test]
    public async Task TryValueAsync_Function_Success()
    {
        var result = await Result.TryValueAsync(async () =>
        {
            await ValueTask.CompletedTask;
            return "async value";
        });

        result.ShouldBeSuccess().ShouldBe("async value");
    }

    [Test]
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
