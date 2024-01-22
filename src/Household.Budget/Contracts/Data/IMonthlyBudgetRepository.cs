using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Models;

namespace Household.Budget.Contracts.Data
{
    public interface IMonthlyBudgetRepository : IRepository<MonthlyBudget>
    {
        Task<bool> ExistsAsync(string userId, int year, Month month, CancellationToken cancellationToken);
        Task<MonthlyBudget> GetOneAsync(string userId, int year, Month month, CancellationToken cancellationToken);
    }
}