namespace Shared
{
    public class PaginatedData<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public IEnumerable<T> Data { get; set; } = [];

        public PaginatedData(int pageIndex, int pageSize, int totalCount, int totalPages, IEnumerable<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = totalPages;
            Data = data;
        }
    }
}
