using Toarnbeike.Results.Messaging.Pipeline;
using Toarnbeike.Results.Messaging.Requests;

namespace Toarnbeike.Results.Messaging.Tests.TestData.Behaviours;

public sealed class LoggingBehaviour<TRequest, TResponse>(List<string> log) : IPipelineBehaviour<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse> where TResponse : IResult
{
    public Task<Result> PreHandleAsync(TRequest query, CancellationToken cancellationToken = default) =>
        Result.SuccessTask();

    public Task<Result> PostHandleAsync(TRequest query, TResponse response, CancellationToken cancellationToken = default)
    {
        log.Add("LoggingBehaviour");
        return Result.SuccessTask();
    }
}