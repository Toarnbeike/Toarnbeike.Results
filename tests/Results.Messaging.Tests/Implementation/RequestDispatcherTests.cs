using Microsoft.Extensions.DependencyInjection;
using Toarnbeike.Results.Messaging.Implementation;
using Toarnbeike.Results.Messaging.Pipeline;
using Toarnbeike.Results.Messaging.Requests;
using Toarnbeike.Results.Messaging.Tests.TestData.Behaviours;
using Toarnbeike.Results.Messaging.Tests.TestData.Requests;
using Toarnbeike.Results.TestHelpers;

namespace Toarnbeike.Results.Messaging.Tests.Implementation;
public class RequestDispatcherTests
{
    private readonly IServiceProvider _provider;

    public RequestDispatcherTests()
    {
        var services = new ServiceCollection();

        services.AddTransient<IRequestHandler<TestQuery, Result<string>>, TestQueryHandler>();

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
        var dispatcher = new RequestDispatcher(_provider);
        var request = new TestQuery();

        var result = await dispatcher.DispatchAsync(request);

        result.ShouldBeSuccessWithValue("Success");
    }

    [Fact]
    public async Task DispatchAsync_ShouldExecutePipelineBehaviour()
    {
        var services = new ServiceCollection();
        var log = new List<string>();
        services.AddSingleton(log);
        services.AddTransient<IRequestHandler<TestQuery, Result<string>>, TestQueryHandler>();
        services.AddTransient(typeof(IPipelineBehaviour<TestQuery, Result<string>>), sp => new LoggingBehaviour<TestQuery, Result<string>>(sp.GetRequiredService<List<string>>()));

        var provider = services.BuildServiceProvider();
        var dispatcher = new RequestDispatcher(provider);
        var request = new TestQuery();

        var result = await dispatcher.DispatchAsync(request);

        result.ShouldBeSuccessWithValue("Success");
        log.Count.ShouldBe(1);
        log.ShouldContain("LoggingBehaviour");
    }

    [Fact]
    public async Task DispatchAsync_ShouldCacheDelegatePerRequestType()
    {
        var dispatcher = new RequestDispatcher(_provider);
        var request = new TestQuery();

        var result1 = await dispatcher.DispatchAsync(request);
        var result2 = await dispatcher.DispatchAsync(request);

        result1.ShouldBeSuccessWithValue("Success");
        result2.ShouldBeSuccessWithValue("Success");
    }
}
