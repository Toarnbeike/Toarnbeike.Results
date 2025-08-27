using Microsoft.Extensions.DependencyInjection;
using Toarnbeike.Results.Messaging.Implementation;
using Toarnbeike.Results.Messaging.Pipeline;
using Toarnbeike.Results.Messaging.Requests;
using Toarnbeike.Results.TestHelpers;

namespace Toarnbeike.Results.Messaging.Tests.Implementation;
public class RequestDispatcherTests
{
    private readonly IServiceProvider _provider;

    public RequestDispatcherTests()
    {
        var services = new ServiceCollection();

        services.AddTransient<IRequestHandler<TestRequest, Result<string>>, TestRequestHandler>();

        _provider = services.BuildServiceProvider();
    }

    [Fact]
    public async Task DispatchAsync_ShouldThrow_WhenRequestIsNull()
    {
        var dispatcher = new RequestDispatcher(_provider);
        var act = async () => await dispatcher.DispatchAsync<Result<string>>(null!);
        await act.ShouldThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task DispatchAsync_ShouldReturnResultFromHandler()
    {
        // Arrange
        var dispatcher = new RequestDispatcher(_provider);
        var request = new TestRequest("ping");

        var result = await dispatcher.DispatchAsync(request);

        result.ShouldBeSuccessWithValue("pong");
    }

    [Fact]
    public async Task DispatchAsync_ShouldExecutePipelineBehaviour()
    {
        // Arrange
        var services = new ServiceCollection();
        var log = new List<string>();
        services.AddSingleton(log);
        services.AddTransient<IRequestHandler<TestRequest, Result<string>>, TestRequestHandler>();
        services.AddTransient<IPipelineBehaviour<TestRequest, Result<string>>, LoggingBehaviour>();

        var provider = services.BuildServiceProvider();
        var dispatcher = new RequestDispatcher(provider);
        var request = new TestRequest("ping");

        var result = await dispatcher.DispatchAsync(request);

        result.ShouldBeSuccessWithValue("pong");
        log.Count.ShouldBe(1);
        log.ShouldContain("LoggingBehaviour");
    }

    [Fact]
    public async Task DispatchAsync_ShouldCacheDelegatePerRequestType()
    {
        var dispatcher = new RequestDispatcher(_provider);
        var request = new TestRequest("ping");

        var result1 = await dispatcher.DispatchAsync(request);
        var result2 = await dispatcher.DispatchAsync(request);

        result1.ShouldBeSuccessWithValue("pong");
        result2.ShouldBeSuccessWithValue("pong");
    }

    private sealed record TestRequest(string Input) : IRequest<Result<string>>;

    private sealed class TestRequestHandler : IRequestHandler<TestRequest, Result<string>>
    {
        public Task<Result<string>> HandleAsync(TestRequest request, CancellationToken cancellationToken)
            => Task.FromResult<Result<string>>("pong");
    }

    private sealed class LoggingBehaviour(List<string> log) : IPipelineBehaviour<TestRequest, Result<string>>
    {
        public Task<Result> PreHandleAsync(TestRequest request, CancellationToken cancellationToken = default) =>
            Result.SuccessTask();

        public Task<Result> PostHandleAsync(TestRequest request, Result<string> response, CancellationToken cancellationToken = default)
        {
            log.Add(nameof(LoggingBehaviour));
            return Result.SuccessTask();
        }
    }
}
