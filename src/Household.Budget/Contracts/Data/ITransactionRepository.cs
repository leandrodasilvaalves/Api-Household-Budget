using Household.Budget.Contracts.Models;

namespace Household.Budget.Contracts.Data
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<PagedListResult<Transaction>> GetAllAsync(int year, int month, int pageSize, int pageNumber,
                                                           string userId, CancellationToken cancellationToken = default);
    }
}