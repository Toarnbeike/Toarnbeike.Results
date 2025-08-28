using Microsoft.Extensions.DependencyInjection;
using Toarnbeike.Results.Integration.Tests.Examples;
using Toarnbeike.Results.Messaging.DependencyInjection;
using Toarnbeike.Results.Messaging.Notifications.Publisher;
using Toarnbeike.Results.Messaging.Notifications.Store;

namespace Toarnbeike.Results.Integration.Tests.Messaging;

public class NotificationHandlingTest
{
    private static ServiceProvider BuilderServiceCollectionWithNotificationHandling()
    {
        var services = new ServiceCollection();

        services.AddNotificationMessaging(options =>
            options.FromAssemblyContaining<CreatedCustomerLoggerHandler>());
        services.AddSingleton(new List<string>());
        return services.BuildServiceProvider();
    }

    [Fact]
    public async Task CreatedCustomerLoggerHandler_WriteLogMessage_UponNotifying()
    {
        var serviceProvider = BuilderServiceCollectionWithNotificationHandling();
        var notification = new CustomerCreatedNotification(2);
        
        var notificationStore = serviceProvider.GetRequiredService<INotificationStore>();
        await notificationStore.AddAsync(notification);
        var unprocessedMessages = await notificationStore.GetUnprocessedAsync();
        unprocessedMessages.Count.ShouldBe(1);
        
        var notificationPublisher = serviceProvider.GetRequiredService<INotificationPublisher>();
        await notificationPublisher.PublishAsync();
        
        var log = serviceProvider.GetRequiredService<List<string>>();
        log.Count.ShouldBe(1);
        log.ShouldContain("Customer with Id 2 created.");

        unprocessedMessages = await notificationStore.GetUnprocessedAsync();
        unprocessedMessages.ShouldBeEmpty();
    }
}