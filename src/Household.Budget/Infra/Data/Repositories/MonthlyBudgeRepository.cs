using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Models;
using Household.Budget.Infra.Data.Context;

namespace Household.Budget.Infra.Data.Repositories
{
    public class MonthlyBudgeRepository : Repository<MonthlyBudget>, IMonthlyBudgeRepository
    {
        public MonthlyBudgeRepository(IMongoDbContext<MonthlyBudget> context)
            : base(context) { }

        public async Task<bool> ExistsAsync(string userId, int year, Month month, CancellationToken cancellationToken) =>
            (await GetOneAsync(x => x.UserId == userId && x.Year == year && x.Month == month, cancellationToken)) is { };

    }
}