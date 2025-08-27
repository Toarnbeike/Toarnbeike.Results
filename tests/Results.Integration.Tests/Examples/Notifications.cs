using Toarnbeike.Results.Messaging.Notifications;

namespace Toarnbeike.Results.Integration.Tests.Examples;

public record CustomerCreatedNotification(int CustomerId) : NotificationBase("TestSender");

public sealed class CreatedCustomerLoggerHandler(List<string> log) : INotificationHandler<CustomerCreatedNotification>
{
    public Task HandleAsync(CustomerCreatedNotification notification, CancellationToken cancellationToken = default)
    {
        log.Add($"Customer with Id {notification.CustomerId} created.");
        return Task.CompletedTask;
    }
}
