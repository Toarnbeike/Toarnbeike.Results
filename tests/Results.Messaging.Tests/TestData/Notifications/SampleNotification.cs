using Toarnbeike.Results.Messaging.Notifications;

namespace Toarnbeike.Results.Messaging.Tests.TestData.Notifications;

public record SampleNotification(string Payload) : NotificationBase("TestSender");

public class SampleNotificationHandler(List<string> log) : INotificationHandler<SampleNotification>
{
    public Task HandleAsync(SampleNotification notification, CancellationToken cancellationToken = default)
    {
        log.Add($"SampleNotification has handled notification with Id {notification.Id}");
        return Task.CompletedTask;
    }
}