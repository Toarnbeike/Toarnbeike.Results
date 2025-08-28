using Toarnbeike.Results.Messaging.Notifications;

namespace Toarnbeike.Results.Messaging.Tests.Notifications;

public class ProcessingStateTests
{
    private readonly DateTime _processedAt = DateTime.UtcNow;
    private readonly string _processedBy = "TestProcessor";
    
    [Fact]
    public void NotProcessed_Should_Have_CorrectProperties()
    {
        var state = ProcessingState.NotProcessed();

        state.ShouldBeOfType<ProcessingState.NotProcessedState>();
        state.IsProcessed.ShouldBeFalse();
        state.IsSuccess.ShouldBeFalse();
    }

    [Fact]
    public void ProcessedSuccessfully_Should_Have_CorrectProperties()
    {
        var state = ProcessingState.ProcessedSuccessfully(_processedAt, _processedBy);

        state.ShouldBeOfType<ProcessingState.ProcessedSuccessfullyState>();
        state.IsProcessed.ShouldBeTrue();
        state.IsSuccess.ShouldBeTrue();

        state.ProcessedAt.ShouldBe(_processedAt);
        state.ProcessedBy.ShouldBe(_processedBy);
    }

    [Fact]
    public void ProcessedWithFailure_Should_Have_CorrectProperties()
    {
        var failure = new Failure("TEST_ERROR", "Something went wrong");

        var state = ProcessingState.ProcessedWithFailure(_processedAt, _processedBy, failure);

        state.ShouldBeOfType<ProcessingState.ProcessedWithFailureState>();
        state.IsProcessed.ShouldBeTrue();
        state.IsSuccess.ShouldBeFalse();

        state.ProcessedAt.ShouldBe(_processedAt);
        state.ProcessedBy.ShouldBe(_processedBy);
        state.Failure.ShouldBe(failure);
    }

    [Fact]
    public void FromResult_Should_Return_ProcessedSuccessfully_When_Success()
    {
        var result = Result.Success();

        var state = ProcessingState.FromResult(_processedAt, _processedBy, result);

        state.ShouldBeOfType<ProcessingState.ProcessedSuccessfullyState>();
        state.IsProcessed.ShouldBeTrue();
        state.IsSuccess.ShouldBeTrue();

        var successState = (ProcessingState.ProcessedSuccessfullyState)state;
        successState.ProcessedAt.ShouldBe(_processedAt);
        successState.ProcessedBy.ShouldBe(_processedBy);
    }

    [Fact]
    public void FromResult_Should_Return_ProcessedWithFailure_When_Failure()
    {
        var failure = new Failure("FAIL", "Something bad");
        var result = Result.Failure(failure);

        var state = ProcessingState.FromResult(_processedAt, _processedBy, result);

        state.ShouldBeOfType<ProcessingState.ProcessedWithFailureState>();
        state.IsProcessed.ShouldBeTrue();
        state.IsSuccess.ShouldBeFalse();

        var failureState = (ProcessingState.ProcessedWithFailureState)state;
        failureState.ProcessedAt.ShouldBe(_processedAt);
        failureState.ProcessedBy.ShouldBe(_processedBy);
        failureState.Failure.ShouldBe(failure);
    }
}