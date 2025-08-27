using Toarnbeike.Results.Messaging.Requests;

namespace Toarnbeike.Results.Messaging.Pipeline;
/// <summary>
/// Represents a behaviour in a request pipeline that can execute logic
/// before and after the handler. 
/// PreHandle can short-circuit the pipeline by returning a Failure.
/// PostHandle can access any instance fields of the behaviour for metadata.
/// </summary>
/// <typeparam name="TRequest">The request type being handled.</typeparam>
/// <typeparam name="TResponse">The response type returned by the handler, must implement IResult.</typeparam>
public interface IPipelineBehaviour<in TRequest, in TResponse>
    where TRequest : class, IRequest<TResponse>
    where TResponse : IResult
{
    /// <summary>
    /// Executed before the next behaviour or handler.
    /// Return a Failure to short-circuit the pipeline. Return Success to continue.
    /// </summary>
    Task<Result> PreHandleAsync(TRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executed after the next behaviour or handler has completed successfully.
    /// Can optionally return a Failure to override the response.
    /// </summary>
    Task<Result> PostHandleAsync(TRequest request, TResponse response, CancellationToken cancellationToken = default);
}
