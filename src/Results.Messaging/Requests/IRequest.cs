using System.ComponentModel;

namespace Toarnbeike.Results.Messaging.Requests;

/// <summary>
/// Marker interface for every request that can be handled by the <see cref="IRequestDispatcher"/>
/// </summary>
/// <remarks>
/// This interface is intended for internal infrastructure use only.  
/// Consumers should prefer <see cref="ICommand"/> or <see cref="IQuery{TResponse}"/> 
/// when defining application requests.
/// </remarks>
/// <typeparam name="TResponse">The response that is returned after handling the request.</typeparam>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IRequest<TResponse> where TResponse : IResult;