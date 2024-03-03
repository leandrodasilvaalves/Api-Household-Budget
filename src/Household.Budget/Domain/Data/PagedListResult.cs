namespace Household.Budget.Domain.Data
{
    public class PagedListResult<T>
    {
        public PagedListResult(List<T> items, long totalResult, int pageSize, int currentPage)
        {
            Items = items;
            TotalResult = totalResult;
            CurrentPage = currentPage;
            PageSize = Items?.Count ?? 0;
            TotalPages = CalculateTotalPages(pageSize);
        }

        public long TotalResult { get; }
        public int CurrentPage { get; }
        public int PageSize { get; }
        public int TotalPages { get; }
        public bool HasMorePages => CurrentPage < TotalPages;
        public List<T>? Items { get; }

        private int CalculateTotalPages(int pageSize) =>
            (int)Math.Ceiling(TotalResult / (double)pageSize);
    }
}