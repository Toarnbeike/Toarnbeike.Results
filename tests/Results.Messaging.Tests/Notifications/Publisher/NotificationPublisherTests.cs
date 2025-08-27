using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Toarnbeike.Results.Messaging.Notifications;
using Toarnbeike.Results.Messaging.Notifications.Publisher;
using Toarnbeike.Results.Messaging.Notifications.Store;
using Toarnbeike.Results.Messaging.Tests.TestData.Notifications;

namespace Toarnbeike.Results.Messaging.Tests.Notifications.Publisher;
public class NotificationPublisherTests
{
    private readonly SampleNotification _notification = new("Payload");
    private readonly InMemoryNotificationStore _store = new();
    private readonly ILogger<NotificationPublisher> _logger = NullLogger<NotificationPublisher>.Instance;

    [Fact]
    public async Task PublishAsync_NoHandlersRegistered_ShouldMarkAsFailure()
    {
        await _store.AddAsync(_notification);

        var serviceProvider = BuildServiceProvider();
        var publisher = new NotificationPublisher(serviceProvider.GetRequiredService<IServiceScopeFactory>(), _logger);

        await publisher.PublishAsync();

        var stored = await _store.GetAsync(_notification.Id);
        stored.ProcessingState.IsProcessed.ShouldBeTrue();
        stored.ProcessingState.IsSuccess.ShouldBeFalse();
    }

    [Fact]
    public async Task PublishAsync_OneHandlerRegistered_ShouldInvokeHandler()
    {
        await _store.AddAsync(_notification);

        var handler = Substitute.For<INotificationHandler<SampleNotification>>();

        var serviceProvider = BuildServiceProvider(handler);

        var publisher = new NotificationPublisher(serviceProvider.GetRequiredService<IServiceScopeFactory>(), _logger);

        await publisher.PublishAsync();

        await handler.Received(1).HandleAsync(_notification, Arg.Any<CancellationToken>());
        var stored = await _store.GetAsync(_notification.Id);
        stored.ProcessingState.IsProcessed.ShouldBeTrue();
        stored.ProcessingState.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task PublishAsync_MultipleHandlersRegistered_ShouldInvokeAllHandlers()
    {
        await _store.AddAsync(_notification);

        var handler1 = Substitute.For<INotificationHandler<SampleNotification>>();
        var handler2 = Substitute.For<INotificationHandler<SampleNotification>>();

        var serviceProvider = BuildServiceProvider(handler1, handler2);
        var publisher = new NotificationPublisher(serviceProvider.GetRequiredService<IServiceScopeFactory>(), _logger);

        await publisher.PublishAsync();

        await handler1.Received(1).HandleAsync(_notification, Arg.Any<CancellationToken>());
        await handler2.Received(1).HandleAsync(_notification, Arg.Any<CancellationToken>());

        var stored = await _store.GetAsync(_notification.Id);
        stored.ProcessingState.IsProcessed.ShouldBeTrue();
        stored.ProcessingState.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task PublishAsync_HandlerThrowsException_ShouldMarkAsFailure()
    {
        await _store.AddAsync(_notification);

        var handler = Substitute.For<INotificationHandler<SampleNotification>>();
        handler.HandleAsync(_notification, Arg.Any<CancellationToken>())
               .Returns(_ => throw new InvalidOperationException("Handler failed"));

        var serviceProvider = BuildServiceProvider(handler);

        var publisher = new NotificationPublisher(serviceProvider.GetRequiredService<IServiceScopeFactory>(), _logger);

        await publisher.PublishAsync();

        await handler.Received(1).HandleAsync(_notification, Arg.Any<CancellationToken>());
        var stored = await _store.GetAsync(_notification.Id);
        stored.ProcessingState.IsProcessed.ShouldBeTrue();
        stored.ProcessingState.IsSuccess.ShouldBeFalse();
    }

    private ServiceProvider BuildServiceProvider(params IEnumerable<INotificationHandler<SampleNotification>> handlers)
    {
        var services = new ServiceCollection();
        services.AddSingleton(_logger);
        services.AddSingleton<INotificationStore>(_store);
        foreach (var handler in handlers)
        {
            services.AddSingleton(handler);
        }
        var sp = services.BuildServiceProvider();
        return sp;
    }
}