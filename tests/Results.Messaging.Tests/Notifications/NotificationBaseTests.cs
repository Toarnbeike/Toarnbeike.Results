using Toarnbeike.Results.Messaging.Notifications;
using Toarnbeike.Results.Messaging.Tests.TestData.Notifications;

namespace Toarnbeike.Results.Messaging.Tests.Notifications;

public class NotificationBaseTests
{
    private readonly string _processedBy = "TestProcessor";
    
    [Fact]
    public void Constructor_Should_CreateNotificationWithDefaultValues()
    {
        var notification = new SampleNotification("Payload");
        notification.Id.Value.ShouldNotBe(Guid.Empty);
        notification.Id.Value.Version.ShouldBe(7);
        notification.CreatedAt.ShouldBe(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        notification.Sender.ShouldBe("TestSender");
        notification.ProcessingState.ShouldBe(ProcessingState.NotProcessed());
    }

    [Fact]
    public void MarkProcessed_Should_UpdateNotificationState_Success()
    {
        var notification = new SampleNotification("Payload");
        var updated = notification.MarkProcessed(_processedBy, Result.Success());

        updated.ProcessingState.IsProcessed.ShouldBeTrue();
        updated.ProcessingState.IsSuccess.ShouldBeTrue();
        
        var state = updated.ProcessingState.ShouldBeOfType<ProcessingState.ProcessedSuccessfullyState>();
        state.ShouldNotBeNull();
        state.ProcessedAt.ShouldBe(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        state.ProcessedBy.ShouldBe(_processedBy);
    }

    [Fact]
    public void MarkProcessed_Should_UpdateNotificationState_Failure()
    {
        var notification = new SampleNotification("Payload");
        var failure = new Failure("TEST_ERROR", "Something went wrong");
        var updated = notification.MarkProcessed(_processedBy, Result.Failure(failure));

        updated.ProcessingState.IsProcessed.ShouldBeTrue();
        updated.ProcessingState.IsSuccess.ShouldBeFalse();

        var state = updated.ProcessingState.ShouldBeOfType<ProcessingState.ProcessedWithFailureState>();
        state.ShouldNotBeNull();
        state.ProcessedAt.ShouldBe(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        state.ProcessedBy.ShouldBe(_processedBy);
        state.Failure.ShouldBe(failure);
    }
}