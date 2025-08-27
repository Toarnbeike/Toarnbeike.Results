namespace Toarnbeike.Results.Messaging.Pagination;

/// <summary>
/// Represents a collection of items that has been paginated.
/// </summary>
/// <typeparam name="TInner">The type of items in the collection.</typeparam>
public class PaginatedCollection<TInner>(IEnumerable<TInner> items, int totalCount, PagingInformation pagingInfo)
{
    /// <summary>
    /// The items contained in the current page.
    /// </summary>
    public IEnumerable<TInner> Items { get; } = items;

    /// <summary>
    /// The total number of items in the collection across all pages.
    /// </summary>
    public int TotalCount { get; } = totalCount;

    /// <summary>
    /// The current page number (1-based).
    /// </summary>
    public int Page { get; } = pagingInfo.Page;

    /// <summary>
    /// The number of items per page.
    /// </summary>
    public int PageSize { get; } = pagingInfo.QuantityPerPage;

    /// <summary>
    /// Indicates whether there is a next page after the current one.
    /// </summary>
    public bool HasNextPage => Page * PageSize < TotalCount;

    /// <summary>
    /// Indicates whether there is a previous page before the current one.
    /// </summary>
    public bool HasPreviousPage => Page > 1;

    /// <summary>
    /// The total number of pages available based on <see cref="TotalCount"/> and <see cref="PageSize"/>.
    /// </summary>
    public int PageCount => (int)Math.Ceiling((double)TotalCount / PageSize);
}