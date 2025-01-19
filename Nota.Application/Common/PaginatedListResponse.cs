namespace Nota.Application.Common
{
    public class PaginatedListResponse<T>(IEnumerable<T> data, int totalCount, int pageNumber, int pageSize)
    {
        public IEnumerable<T> Data { get; set; } = data;
        public int TotalCount { get; set; } = totalCount;
        public int PageNumber { get; set; } = pageNumber;
        public int PageSize { get; set; } = pageSize;
        public int TotalPages => (int)Math.Ceiling(totalCount / (double)pageSize);
    }
}