using Toarnbeike.Results.Messaging.Notifications;

namespace Toarnbeike.Results.Messaging.Pipeline.PerformanceLogging;

/// <summary>
/// Notification that is raised when the handling of a request exceeds the expected duration.
/// </summary>
/// <param name="RequestType">The name of the request.</param>
/// <param name="ResponseType">The name of the response.</param>
/// <param name="Duration">The actual duration of the handling of the request.</param>
public record RequestExceedsExpectedDurationNotification(
    string RequestType, string ResponseType, TimeSpan Duration, string Sender) : NotificationBase(Sender);