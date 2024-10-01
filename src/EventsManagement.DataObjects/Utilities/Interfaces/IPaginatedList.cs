namespace EventsManagement.DataObjects.Utilities.Interfaces
{
    public interface IPaginatedList<T>
    {
        List<T> Items { get; }

        int PageIndex { get; }

        int TotalPages { get; }

        int PageSize { get; }

        int TotalCount { get; }

        bool HasPreviousPage { get; }

        bool HasNextPage { get; }
    }
}
