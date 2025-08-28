using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Toarnbeike.Results.Messaging.Extensions;
using Toarnbeike.Results.Messaging.Notifications.Store;
using Toarnbeike.Results.Messaging.Requests;

namespace Toarnbeike.Results.Messaging.Pipeline.PerformanceLogging;

/// <summary>
/// Logging behaviour that logs the performance of handling a request.
/// It will raise a <see cref="RequestExceedsExpectedDurationNotification"/> notification is the request takes longer than expected.
/// </summary>
/// <typeparam name="TRequest">The type of the request</typeparam>
/// <typeparam name="TResponse">The type of the response that matches the request.</typeparam>
/// <param name="options">Configuration for the performance logging notification part.</param>
/// <param name="notificationStore">Store to save the notification when created.</param>
/// <param name="logger">The logger to write the log messages to.</param>
public sealed class PerformanceLoggingBehaviour<TRequest, TResponse>(
    IOptions<PerformanceLoggingOptions> options,
    INotificationStore notificationStore,
    ILogger<PerformanceLoggingBehaviour<TRequest, TResponse>> logger)
    : IPipelineBehaviour<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse> where TResponse : IResult
{
    private string _requestName = null!;
    private string _responseName = null!;
    private Stopwatch? _stopwatch;
    
    public Task<Result> PreHandleAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        _requestName = typeof(TRequest).PrettyName();
        _responseName = typeof(TResponse).PrettyName();
        _stopwatch = Stopwatch.StartNew();

        logger.LogInformation("Handling {RequestType} => {ResponseType}", _requestName, _responseName);
        return Result.SuccessTask();
    }

    public async Task<Result> PostHandleAsync(TRequest request, TResponse response, CancellationToken cancellationToken = default)
    {
        var duration = _stopwatch?.Elapsed;
        _stopwatch = null;

        logger.LogInformation("Handled {RequestType} => {ResponseType} in {Duration}ms. Result was {Result}",
            _requestName, _responseName,
            duration?.TotalMilliseconds.ToString("F0") ?? "unknown",
            response.IsSuccess ? "Success" : "Failure");

        if ((duration ?? TimeSpan.Zero) > options.Value.MaxExpectedDuration)
        {
            logger.LogWarning("Handling {RequestType} => {ResponseType} took longer than expected: {Duration}ms",
                _requestName, _responseName, duration!.Value.TotalMilliseconds.ToString("F0"));

            var notification = new RequestExceedsExpectedDurationNotification(
                RequestType: _requestName,
                ResponseType: _responseName,
                Duration: duration.Value,
                Sender: typeof(PerformanceLoggingBehaviour<TRequest, TResponse>).PrettyName()
            );

            await notificationStore.AddAsync(notification, cancellationToken);
        }
        
        return Result.Success();
    }
}