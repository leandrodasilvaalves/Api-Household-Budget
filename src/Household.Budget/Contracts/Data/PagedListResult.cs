namespace Household.Budget.Contracts.Data
{
    public class PagedListResult<T>
    {
        public PagedListResult(List<T> items, long totalResult, int pageSize, int currentPage)
        {
            Items = items;
            TotalResult = totalResult;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }

        public long TotalResult { get; }
        public int CurrentPage { get; }
        public int PageSize { get; }
        public int TotalPages => (int)Math.Ceiling(TotalResult / (double)PageSize);
        public bool HasMorePages => CurrentPage < TotalPages;
        public List<T> Items { get; }
    }
}