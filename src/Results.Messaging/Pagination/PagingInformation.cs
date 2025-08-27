namespace Toarnbeike.Results.Messaging.Pagination;

/// <summary>
/// Contains page number and page size information for a paginated query.
/// </summary>
public record PagingInformation(int Page, int QuantityPerPage)
{
    /// <summary>
    /// Returns a paging configuration that retrieves all items in a single page.
    /// Use with caution for large collections to avoid performance issues.
    /// </summary>
    public static PagingInformation All => new(1, int.MaxValue);

    /// <summary>
    /// Returns a paging configuration with a default page size of 25 and starting at the first page.
    /// </summary>
    public static PagingInformation Default => new(1, 25);

    /// <summary>
    /// Calculates the index of the first item on this page (1-based).
    /// </summary>
    public int IndexOfFirstEntryOnPage => ((Page - 1) * QuantityPerPage) + 1;

    /// <summary>
    /// Determines if this page exists given the total number of items in the collection.
    /// </summary>
    /// <param name="totalEntries">The total number of items in the collection.</param>
    /// <returns>True if the page exists; otherwise false.</returns>
    public bool PageExist(int totalEntries) => IndexOfFirstEntryOnPage <= totalEntries;
}