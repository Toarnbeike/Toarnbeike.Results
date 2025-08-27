using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Toarnbeike.Results.Messaging.DependencyInjection;
using Toarnbeike.Results.Messaging.Implementation;
using Toarnbeike.Results.Messaging.Pipeline;
using Toarnbeike.Results.Messaging.Requests;
using Toarnbeike.Results.Messaging.Tests.TestData.Behaviours;
using Toarnbeike.Results.Messaging.Tests.TestData.Requests;
using Toarnbeike.Results.TestHelpers;

namespace Toarnbeike.Results.Messaging.Tests.DependencyInjection;

public class DependencyInjectionExtensionsTests
{
    [Fact]
    public void AddRequestMessaging_ShouldRegisterDispatcher()
    {
        var services = new ServiceCollection();

        services.AddRequestMessaging();
        var provider = services.BuildServiceProvider();

        var dispatcher = provider.GetService<IRequestDispatcher>();
        dispatcher.ShouldNotBeNull();
        dispatcher.ShouldBeOfType<RequestDispatcher>();
    }

    [Fact]
    public void AddRequestMessaging_ShouldRegisterNullLogger_WhenNoLoggerProvided()
    {
        var services = new ServiceCollection();

        services.AddRequestMessaging();
        var provider = services.BuildServiceProvider();

        var logger = provider.GetRequiredService<ILogger<RequestDispatcher>>();
        logger.ShouldBeOfType<NullLogger<RequestDispatcher>>();
    }

    [Fact]
    public void AddRequestMessaging_ShouldRegisterHandler_FromAssembly()
    {
        var services = new ServiceCollection();

        services.AddRequestMessaging(o => o.FromAssembly(typeof(TestQueryHandler).Assembly));
        var provider = services.BuildServiceProvider();

        var handler = provider.GetService<IRequestHandler<TestQuery, Result<string>>>();
        handler.ShouldNotBeNull();
        handler.ShouldBeOfType<TestQueryHandler>();
    }

    [Fact]
    public void AddRequestMessaging_ShouldRegisterHandler_FromAssemblies()
    {
        var services = new ServiceCollection();

        services.AddRequestMessaging(o => o.FromAssemblies(typeof(TestQueryHandler).Assembly));
        var provider = services.BuildServiceProvider();

        var handler = provider.GetService<IRequestHandler<TestQuery, Result<string>>>();
        handler.ShouldNotBeNull();
        handler.ShouldBeOfType<TestQueryHandler>();
    }
    
    [Fact]
    public void AddRequestMessaging_ShouldRegisterHandler_FromAssemblyContaining()
    {
        var services = new ServiceCollection();

        services.AddRequestMessaging(o => o.FromAssemblyContaining<TestQueryHandler>());
        var provider = services.BuildServiceProvider();

        var handler = provider.GetService<IRequestHandler<TestQuery, Result<string>>>();
        handler.ShouldNotBeNull();
        handler.ShouldBeOfType<TestQueryHandler>();
    }

    [Fact]
    public void AddRequestMessaging_ShouldRegisterPipelineBehaviour()
    {
        var services = new ServiceCollection();
        var log = new List<string>();
        services.AddSingleton(log);
        services.AddRequestMessaging(o => o.AddPipelineBehaviour(typeof(LoggingBehaviour<,>)));
        var provider = services.BuildServiceProvider();

        var behaviour = provider.GetService<IPipelineBehaviour<TestQuery, Result<string>>>();
        behaviour.ShouldNotBeNull();
        behaviour.ShouldBeOfType<LoggingBehaviour<TestQuery, Result<string>>>();
    }

    [Fact]
    public async Task AddRequestMessaging_ShouldResolveDispatcherWithHandlerAndPipeline()
    {
        var services = new ServiceCollection();
        var log = new List<string>();
        services.AddSingleton(log);
        services.AddRequestMessaging(o =>
        {
            o.FromAssemblyContaining<TestQueryHandler>();
            o.AddPipelineBehaviour(typeof(LoggingBehaviour<,>));
        });
        var provider = services.BuildServiceProvider();

        var dispatcher = provider.GetRequiredService<IRequestDispatcher>();

        var result = await dispatcher.DispatchAsync(new TestQuery());

        result.ShouldBeSuccessWithValue("Success");
        log.Count.ShouldBe(1);
        log.ShouldContain("LoggingBehaviour");
    }
}
