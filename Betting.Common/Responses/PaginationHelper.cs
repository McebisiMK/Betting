namespace Betting.Common.Responses
{
    public static class PaginationHelper
    {
        public static PaginatedResponse<TData> AddPagination<TData>(this PaginatedResponse<TData> response, int totalItems)
        {
            var totalPages = Convert.ToInt32(Math.Ceiling(((double)totalItems / (double)response.PageSize)));
            response.Total = totalItems;
            response.TotalPages = totalPages;

            return response;
        }
    }
}
