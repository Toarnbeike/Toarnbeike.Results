using System.ComponentModel;

namespace Toarnbeike.Results.Messaging.Requests;

/// <summary>
/// Generic request handler for any request.
/// </summary>
/// <remarks>
/// This interface is intended for internal infrastructure use only.  
/// Consumers should prefer <see cref="ICommandHandler{TRequest}"/> or <see cref="IQueryHandler{TRequest, TResponse}"/> 
/// when defining application handlers.
/// </remarks>
/// <typeparam name="TRequest">The type of the request to handle.</typeparam>
/// <typeparam name="TResponse">The response that is expected given the request.</typeparam>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IRequestHandler<in TRequest, TResponse> where TRequest : IRequest<TResponse>
    where TResponse : IResult
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}