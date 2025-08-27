using Toarnbeike.Results.Messaging.Implementation;
using Toarnbeike.Results.Messaging.Pipeline;
using Toarnbeike.Results.Messaging.Requests;
using Toarnbeike.Results.TestHelpers;

namespace Toarnbeike.Results.Messaging.Tests.Implementation;

public class RequestPipelineExecutorCommandTests
{
    public record TestCommand : ICommand;

    public class TestCommandHandler : ICommandHandler<TestCommand>
    {
        public Task<Result> HandleAsync(TestCommand command, CancellationToken ct) => Result.SuccessTask();
    }

    public record TestQuery : IQuery<string>;

    public class TestQueryHandler : IQueryHandler<TestQuery, string>
    {
        public Task<Result<string>> HandleAsync(TestQuery query, CancellationToken ct) => Result.SuccessTask("Success");
    }

    public class FailingQueryHandler : IQueryHandler<TestQuery, string>
    {
        public Task<Result<string>> HandleAsync(TestQuery query, CancellationToken ct) => Task.FromResult(Result<string>.Failure(new Failure("X", "handler fail")));
    }

    [Fact]
    public async Task ExecuteAsync_Command_NoBehaviours_ShouldInvokeHandler()
    {
        var handler = new TestCommandHandler();
        var executor = new RequestPipelineExecutor<TestCommand, Result>([], handler);

        var result = await executor.ExecuteAsync(new TestCommand());

        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task ExecuteAsync_Command_PreHandleFailure_ShouldShortCircuit()
    {
        var behaviour = Substitute.For<IPipelineBehaviour<TestCommand, Result>>();
        behaviour.PreHandleAsync(Arg.Any<TestCommand>(), Arg.Any<CancellationToken>())
            .Returns(GenerateFailureTask("pre fail"));

        var handler = Substitute.For<IRequestHandler<TestCommand, Result>>();
        var executor = new RequestPipelineExecutor<TestCommand, Result>(
            [behaviour], handler);

        var result = await executor.ExecuteAsync(new TestCommand());

        result.ShouldBeFailureWithCodeAndMessage("X", "pre fail");
        await handler.DidNotReceive().HandleAsync(Arg.Any<TestCommand>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ExecuteAsync_Command_PostHandleFailure_ShouldOverride()
    {
        var behaviour = Substitute.For<IPipelineBehaviour<TestCommand, Result>>();
        behaviour.PreHandleAsync(Arg.Any<TestCommand>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(Result.Success()));
        behaviour.PostHandleAsync(Arg.Any<TestCommand>(), Arg.Any<Result>(), Arg.Any<CancellationToken>())
            .Returns(GenerateFailureTask("post fail"));

        var handler = new TestCommandHandler();
        var executor = new RequestPipelineExecutor<TestCommand, Result>(
            [behaviour], handler);

        var result = await executor.ExecuteAsync(new TestCommand());

        result.ShouldBeFailureWithCodeAndMessage("X", "post fail");
    }

    [Fact]
    public async Task ExecuteAsync_Query_NoBehaviours_ShouldInvokeHandler()
    {
        var handler = new TestQueryHandler();
        var executor = new RequestPipelineExecutor<TestQuery, Result<string>>([], handler);

        var result = await executor.ExecuteAsync(new TestQuery());

        result.ShouldBeSuccessWithValue("Success");
    }

    [Fact]
    public async Task ExecuteAsync_Query_PreHandleFailure_ShouldShortCircuit()
    {
        var behaviour = Substitute.For<IPipelineBehaviour<TestQuery, Result<string>>>();
        behaviour.PreHandleAsync(Arg.Any<TestQuery>(), Arg.Any<CancellationToken>())
            .Returns(GenerateFailureTask("pre fail"));

        var handler = Substitute.For<IRequestHandler<TestQuery, Result<string>>>();
        var executor = new RequestPipelineExecutor<TestQuery, Result<string>>(
            [behaviour], handler);

        var result = await executor.ExecuteAsync(new TestQuery());

        result.ShouldBeFailureWithCodeAndMessage("X", "pre fail");
        await handler.DidNotReceive().HandleAsync(Arg.Any<TestQuery>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ExecuteAsync_Query_HandleFailure_ShouldPropagate()
    {
        var behaviour = Substitute.For<IPipelineBehaviour<TestQuery, Result<string>>>();
        behaviour.PreHandleAsync(Arg.Any<TestQuery>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(Result.Success()));
        behaviour.PostHandleAsync(Arg.Any<TestQuery>(), Arg.Any<Result<string>>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(Result.Success()));

        var handler = new FailingQueryHandler();
        var executor = new RequestPipelineExecutor<TestQuery, Result<string>>([behaviour], handler);

        var result = await executor.ExecuteAsync(new TestQuery());

        result.ShouldBeFailureWithCodeAndMessage("X", "handler fail");
    }

    [Fact]
    public async Task ExecuteAsync_Query_PostHandleFailure_ShouldOverrideSuccess()
    {
        var behaviour = Substitute.For<IPipelineBehaviour<TestQuery, Result<string>>>();
        behaviour.PreHandleAsync(Arg.Any<TestQuery>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(Result.Success()));
        behaviour.PostHandleAsync(Arg.Any<TestQuery>(), Arg.Any<Result<string>>(), Arg.Any<CancellationToken>())
            .Returns(GenerateFailureTask("post fail"));

        var handler = new TestQueryHandler();
        var executor = new RequestPipelineExecutor<TestQuery, Result<string>>(
            [behaviour], handler);

        var result = await executor.ExecuteAsync(new TestQuery());

        result.ShouldBeOfType<Result<string>>();
        result.ShouldBeFailureWithCodeAndMessage("X", "post fail");
    }

    [Fact]
    public async Task ExecuteAsync_Query_PostHandleFailure_ShouldOverrideFailure()
    {
        var behaviour = Substitute.For<IPipelineBehaviour<TestQuery, Result<string>>>();
        behaviour.PreHandleAsync(Arg.Any<TestQuery>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(Result.Success()));
        behaviour.PostHandleAsync(Arg.Any<TestQuery>(), Arg.Any<Result<string>>(), Arg.Any<CancellationToken>())
            .Returns(GenerateFailureTask("post fail"));

        var handler = new FailingQueryHandler();
        var executor = new RequestPipelineExecutor<TestQuery, Result<string>>(
            [behaviour], handler);

        var result = await executor.ExecuteAsync(new TestQuery());

        result.ShouldBeOfType<Result<string>>();
        result.ShouldBeFailureWithCodeAndMessage("X", "post fail");
    }

    [Fact]
    public async Task ExecuteAsync_Command_WithMultipleBehaviours_ShouldInvokeInOrder()
    {
        var behaviour1 = Substitute.For<IPipelineBehaviour<TestCommand, Result>>();
        var behaviour2 = Substitute.For<IPipelineBehaviour<TestCommand, Result>>();

        behaviour1.PreHandleAsync(Arg.Any<TestCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result.SuccessTask());
        behaviour1.PostHandleAsync(Arg.Any<TestCommand>(), Arg.Any<Result>(), Arg.Any<CancellationToken>())
            .Returns(Result.SuccessTask());

        behaviour2.PreHandleAsync(Arg.Any<TestCommand>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(Result.Success()));
        behaviour2.PostHandleAsync(Arg.Any<TestCommand>(), Arg.Any<Result>(), Arg.Any<CancellationToken>())
            .Returns(Result.SuccessTask());

        var handler = new TestCommandHandler();
        var executor = new RequestPipelineExecutor<TestCommand, Result>(
            [behaviour1, behaviour2], handler);

        var result = await executor.ExecuteAsync(new TestCommand());

        result.IsSuccess.ShouldBeTrue();

        Received.InOrder(() =>
        {
            behaviour1.PreHandleAsync(Arg.Any<TestCommand>(), Arg.Any<CancellationToken>());
            behaviour2.PreHandleAsync(Arg.Any<TestCommand>(), Arg.Any<CancellationToken>());
            behaviour2.PostHandleAsync(Arg.Any<TestCommand>(), Arg.Any<Result>(), Arg.Any<CancellationToken>());
            behaviour1.PostHandleAsync(Arg.Any<TestCommand>(), Arg.Any<Result>(), Arg.Any<CancellationToken>());
        });
    }

    [Fact]
    public async Task ExecuteAsync_Query_ShouldPropagateHandlerResult_WhenAllBehavioursPass()
    {
        var behaviour = Substitute.For<IPipelineBehaviour<TestQuery, Result<string>>>();
        behaviour.PreHandleAsync(Arg.Any<TestQuery>(), Arg.Any<CancellationToken>())
            .Returns(Result.SuccessTask());
        behaviour.PostHandleAsync(Arg.Any<TestQuery>(), Arg.Any<Result<string>>(), Arg.Any<CancellationToken>())
            .Returns(Result.SuccessTask());

        var handler = new TestQueryHandler();
        var executor = new RequestPipelineExecutor<TestQuery, Result<string>>(
            [behaviour], handler);

        var result = await executor.ExecuteAsync(new TestQuery());

        result.ShouldBeSuccessWithValue("Success");
    }

    private static Task<Result> GenerateFailureTask(string message) =>
        Task.FromResult(Result.Failure(new Failure("X", message)));
}
