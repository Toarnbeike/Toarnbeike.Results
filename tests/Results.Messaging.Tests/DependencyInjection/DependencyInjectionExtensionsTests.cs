using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Configuration;
using Toarnbeike.Results.Messaging.DependencyInjection;
using Toarnbeike.Results.Messaging.Implementation;
using Toarnbeike.Results.Messaging.Notifications;
using Toarnbeike.Results.Messaging.Notifications.Publisher;
using Toarnbeike.Results.Messaging.Notifications.Store;
using Toarnbeike.Results.Messaging.Pipeline;
using Toarnbeike.Results.Messaging.Requests;
using Toarnbeike.Results.Messaging.Tests.TestData.Behaviours;
using Toarnbeike.Results.Messaging.Tests.TestData.Notifications;
using Toarnbeike.Results.Messaging.Tests.TestData.Requests;
using Toarnbeike.Results.TestHelpers;

namespace Toarnbeike.Results.Messaging.Tests.DependencyInjection;

public class DependencyInjectionExtensionsTests
{
    [Fact]
    public void AddNotificationMessaging_Default_ShouldRegisterPublisherAndStore()
    {
        var services = new ServiceCollection();

        services.AddNotificationMessaging();

        var serviceProvider = services.BuildServiceProvider();

        var publisher = serviceProvider.GetRequiredService<INotificationPublisher>();
        publisher.ShouldNotBeNull();

        var store = serviceProvider.GetRequiredService<INotificationStore>();
        store.ShouldBeOfType<InMemoryNotificationStore>();

        var logger = serviceProvider.GetRequiredService<ILogger<NotificationPublisher>>();
        logger.ShouldNotBeNull();
        logger.ShouldBeOfType<NullLogger<NotificationPublisher>>();
    }

    [Fact]
    public void AddNotificationMessaging_CustomStore_ShouldRegisterCustomStore()
    {
        var services = new ServiceCollection();

        services.AddNotificationMessaging(options => { options.UseCustomNotificationStore<FakeNotificationStore>(); });

        var serviceProvider = services.BuildServiceProvider();

        var store = serviceProvider.GetRequiredService<INotificationStore>();
        store.ShouldBeOfType<FakeNotificationStore>();
    }

    [Fact]
    public void AddNotificationMessaging_HandlerAssembly_ShouldRegisterHandlers_FromAssembly()
    {
        var services = new ServiceCollection();
        services.AddSingleton(new List<string>()); // log for the SampleNotificationHandler
        services.AddNotificationMessaging(options =>
        {
            options.FromAssembly(typeof(SampleNotificationHandler).Assembly);
        });

        var sp = services.BuildServiceProvider();

        var handlers = sp.GetServices<INotificationHandler<SampleNotification>>().ToList();
        handlers.ShouldNotBeEmpty();
        handlers.First().ShouldBeOfType<SampleNotificationHandler>();
    }

    [Fact]
    public void AddNotificationMessaging_HandlerAssembly_ShouldRegisterHandlers_FromAssemblies()
    {
        var services = new ServiceCollection();
        services.AddSingleton(new List<string>()); // log for the SampleNotificationHandler
        services.AddNotificationMessaging(options =>
        {
            options.FromAssemblies(typeof(SampleNotificationHandler).Assembly);
        });

        var sp = services.BuildServiceProvider();

        var handlers = sp.GetServices<INotificationHandler<SampleNotification>>().ToList();
        handlers.ShouldNotBeEmpty();
        handlers.First().ShouldBeOfType<SampleNotificationHandler>();
    }

    [Fact]
    public void AddNotificationMessaging_HandlerAssembly_ShouldRegisterHandlers_FromAssemblyContaining()
    {
        var services = new ServiceCollection();
        services.AddSingleton(new List<string>()); // log for the SampleNotificationHandler
        services.AddNotificationMessaging(options => { options.FromAssemblyContaining<SampleNotificationHandler>(); });

        var sp = services.BuildServiceProvider();

        var handlers = sp.GetServices<INotificationHandler<SampleNotification>>().ToList();
        handlers.ShouldNotBeEmpty();
        handlers.First().ShouldBeOfType<SampleNotificationHandler>();
    }

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

    [Fact]
    public async Task AddRequestMessaging_ShouldIncludePerformanceLogging_WhenConfigured()
    {
        var services = new ServiceCollection();
        var log = new List<string>();
        services.AddSingleton(log);
        var configuration = new ConfigurationBuilder().Build();
        services.AddSingleton<IConfiguration>(configuration);
        services.AddRequestMessaging(o =>
        {
            o.FromAssemblyContaining<TestQueryHandler>();
            o.AddPerformanceLoggingBehavior();
        });

        services.AddNotificationMessaging(options => options.FromAssemblyContaining<LongRequestLoggingHandler>());
        var provider = services.BuildServiceProvider();
        
        var dispatcher = provider.GetRequiredService<IRequestDispatcher>();

        var result = await dispatcher.DispatchAsync(new TestQuery());

        var notificationPublisher = provider.GetRequiredService<INotificationPublisher>();
        await notificationPublisher.PublishAsync();
        
        result.ShouldBeSuccessWithValue("Success");
        log.Count.ShouldBe(0);
    }

    [Fact]
    public async Task AddRequestMessaging_ShouldIncludePerformanceLoggingMessage_WhenConfiguredToAlwaysRun()
    {
        var services = new ServiceCollection();
        var log = new List<string>();
        services.AddSingleton(log);
        var configuration = new ConfigurationBuilder().Build();
        services.AddSingleton<IConfiguration>(configuration);
        services.AddRequestMessaging(o =>
        {
            o.FromAssemblyContaining<TestQueryHandler>();
            o.AddPerformanceLoggingBehavior(configure =>
            {
                configure.MaxExpectedDuration = TimeSpan.FromMicroseconds(1);
            });
        });

        services.AddNotificationMessaging(options => options.FromAssemblyContaining<LongRequestLoggingHandler>());
        var provider = services.BuildServiceProvider();

        var dispatcher = provider.GetRequiredService<IRequestDispatcher>();

        var result = await dispatcher.DispatchAsync(new TestQuery());

        result.ShouldBeSuccessWithValue("Success");

        var notificationPublisher = provider.GetRequiredService<INotificationPublisher>();
        await notificationPublisher.PublishAsync();
        
        log.Count.ShouldBe(1);
        log.ShouldContain(message => message.Contains("TestQuery => Result<String> took"));
    }

    // Dummy implementations for testing
    private sealed class FakeNotificationStore : INotificationStore
    {
        public Task AddAsync(INotification notification, CancellationToken cancellationToken = default) =>
            Task.CompletedTask;

        public Task UpdateAsync(INotification notification, CancellationToken cancellationToken = default) =>
            Task.CompletedTask;

        public Task<INotification> GetAsync(NotificationId notificationId,
            CancellationToken cancellationToken = default) =>
            Task.FromResult<INotification>(new SampleNotification("Payload"));

        public Task<IReadOnlyList<INotification>> GetUnprocessedAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult<IReadOnlyList<INotification>>(new List<INotification>());

        public Task MarkAsHandledAsync(NotificationId notificationId, string processor, Result processingResult,
            CancellationToken cancellationToken = default) => Task.CompletedTask;
    }
}
