namespace Toarnbeike.Results.Messaging.Requests;

/// <summary>
/// Defines a handler for an <see cref="IQuery{TResponse}"/>.
/// </summary>
/// <typeparam name="TRequest">The type of query request being handled.</typeparam>
/// <typeparam name="TResponse">The type of the data returned by the query.</typeparam>
public interface IQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse>>
    where TRequest : IQuery<TResponse>;