using Toarnbeike.Results.Messaging.Requests;

namespace Toarnbeike.Results.Messaging.Pagination;

/// <summary>
/// Represents a request for a collection of data that should be returned in pages.
/// Handlers produce a <see cref="PaginatedCollection{TResponse}"/> containing the requested page.
/// </summary>
/// <typeparam name="TResponse">The type of items contained in the paginated collection.</typeparam>
public interface IPaginatedQuery<TResponse> : IRequest<Result<PaginatedCollection<TResponse>>>
{
    /// <summary>
    /// Provides information about which page and page size to return.
    /// </summary>
    PagingInformation Paging { get; }
}