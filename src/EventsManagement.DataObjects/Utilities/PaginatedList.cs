using EventsManagement.DataObjects.Utilities.Interfaces;

namespace EventsManagement.DataObjects.Utilities
{
    public class PaginatedList<T> : IPaginatedList<T>
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalCount { get; private set; }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public List<T> Items { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (float)pageSize);
            TotalCount = count;
            Items = items;
        }

        public static async Task<PaginatedList<T>> CreateAsync(IEnumerable<T> sourceList, int pageIndex, int pageSize)
        {
            var count = await Task.Run(() => sourceList.Count());
            var items = await Task.Run(() => sourceList.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
