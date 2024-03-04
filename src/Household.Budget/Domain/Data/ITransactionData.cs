using Household.Budget.Domain.Entities;

namespace Household.Budget.Domain.Data
{
    public interface ITransactionData : IData<Transaction>
    {
        Task<PagedListResult<Transaction>> GetAllAsync(int year, int month, int pageSize, int pageNumber,
                                                           string userId, CancellationToken cancellationToken = default);
    }
}