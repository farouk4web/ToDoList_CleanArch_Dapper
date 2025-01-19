namespace Nota.Application.Common
{
    public class PaginatedRequest(int pageNumber = 0, int pageSize = 0, string? searchTerm = null)
    {
        public int PageNumber { get; set; } = pageNumber > 0 ? pageNumber : 1;
        public int PageSize { get; set; } = pageSize > 0 ? pageSize : 10;
        public string? SearchTerm { get; set; } = searchTerm ?? string.Empty;
    }
}