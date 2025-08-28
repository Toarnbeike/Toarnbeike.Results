using Toarnbeike.Results.Messaging.Notifications;
using Toarnbeike.Results.Messaging.Pipeline.PerformanceLogging;

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

public class LongRequestLoggingHandler(List<string> log) : INotificationHandler<RequestExceedsExpectedDurationNotification>
{
    public Task HandleAsync(RequestExceedsExpectedDurationNotification notification,
        CancellationToken cancellationToken = default)
    {
        log.Add($"Request {notification.RequestType} => {notification.ResponseType} took {notification.Duration.TotalMilliseconds} ms. to complete.");
        return Task.CompletedTask;
    }
}