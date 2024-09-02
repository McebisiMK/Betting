namespace Betting.Common.Responses
{
    public class PaginatedResponse<TData>(TData? data, int pageNumber, int pageSize)
    {
        public TData Data { get; set; } = data;
        public int Page { get; set; } = pageNumber < 1 ? 1 : pageNumber;
        public int PageSize { get; set; } = pageSize > 10 ? 10 : pageSize;
        public int Total { get; set; }
        public int TotalPages { get; set; }
    }
}
