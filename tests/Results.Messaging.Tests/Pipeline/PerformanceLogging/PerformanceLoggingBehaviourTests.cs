using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Toarnbeike.Results.Messaging.Notifications.Store;
using Toarnbeike.Results.Messaging.Pipeline.PerformanceLogging;
using Toarnbeike.Results.Messaging.Requests;
using Toarnbeike.Results.Messaging.Tests.Helpers;

namespace Toarnbeike.Results.Messaging.Tests.Pipeline.PerformanceLogging;

public class PerformanceLoggingBehaviourTests
{
    private readonly INotificationStore _notificationStore = Substitute.For<INotificationStore>();
    private readonly TestLogger<PerformanceLoggingBehaviour<DummyRequest, Result>> _logger = new();
    private readonly PerformanceLoggingOptions _options = new() { MaxExpectedDuration = TimeSpan.FromMilliseconds(50) };

    private PerformanceLoggingBehaviour<DummyRequest, Result> CreateBehaviour() =>
        new(Options.Create(_options), _notificationStore, _logger);

    [Fact]
    public async Task PreHandleAsync_ShouldLogAndStartStopwatch()
    {
        // Arrange
        var behaviour = CreateBehaviour();
        var request = new DummyRequest();

        // Act
        var result = await behaviour.PreHandleAsync(request);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        _logger.ShouldHaveMessage(LogLevel.Information, nameof(DummyRequest));
    }

    [Fact]
    public async Task PostHandleAsync_ShouldLogSuccessAndNotStoreNotification_WhenDurationIsWithinLimit()
    {
        var behaviour = CreateBehaviour();
        await behaviour.PreHandleAsync(new DummyRequest());

        var response = Result.Success();
        var result = await behaviour.PostHandleAsync(new DummyRequest(), response);

        result.IsSuccess.ShouldBeTrue();
        _logger.ShouldHaveMessage(LogLevel.Information, "Success");
        await _notificationStore.DidNotReceive().AddAsync(
            Arg.Any<RequestExceedsExpectedDurationNotification>(),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task PostHandleAsync_ShouldStoreNotification_WhenDurationExceedsLimit()
    {
        var behaviour = CreateBehaviour();
        await behaviour.PreHandleAsync(new DummyRequest());

        await Task.Delay(75);

        var response = Result.Success();
        var result = await behaviour.PostHandleAsync(new DummyRequest(), response);

        result.IsSuccess.ShouldBeTrue();
        await _notificationStore.Received(1).AddAsync(
            Arg.Is<RequestExceedsExpectedDurationNotification>(n =>
                n.RequestType.Contains(nameof(DummyRequest)) &&
                n.ResponseType.Contains(nameof(Result)) &&
                n.Duration > _options.MaxExpectedDuration),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task PostHandleAsync_ShouldLogFailure_WhenResponseIsFailure()
    {
        var behaviour = CreateBehaviour();
        await behaviour.PreHandleAsync(new DummyRequest());

        var response = new Failure("X", "Something went wrong");

        var result = await behaviour.PostHandleAsync(new DummyRequest(), response);

        result.IsSuccess.ShouldBeTrue(); // pipeline altijd Success terug
        _logger.ShouldHaveMessage(LogLevel.Information, "Failure");
    }

    private sealed class DummyRequest : IRequest<Result> { }
}