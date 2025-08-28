using Toarnbeike.Results.Messaging.Requests;

namespace Toarnbeike.Results.Messaging.Pagination;

/// <summary>
/// Defines a handler for an <see cref="IPaginatedQuery{TResponse}"/>.
/// </summary>
/// <typeparam name="TRequest">The type of query being handled.</typeparam>
/// <typeparam name="TResponse">The type of items returned in the paginated collection.</typeparam>
public interface IPaginatedQueryHandler<in TRequest, TResponse>
    : IRequestHandler<TRequest, Result<PaginatedCollection<TResponse>>>
    where TRequest : IPaginatedQuery<TResponse>;