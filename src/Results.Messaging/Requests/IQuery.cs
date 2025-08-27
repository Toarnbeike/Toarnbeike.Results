namespace Toarnbeike.Results.Messaging.Requests;

/// <summary>
/// Represents a request to retrieve data from the system without modifying state.
/// The handler produces a <see cref="Result{TResponse}"/> containing the queried data.
/// </summary>
/// <typeparam name="TResponse">The type of the data returned by the query.</typeparam>
public interface IQuery<TResponse> : IRequest<Result<TResponse>>;