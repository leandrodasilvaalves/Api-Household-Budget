using Household.Budget.Domain.Data;
using Household.Budget.Contracts.Enums;
using Household.Budget.Domain.Entities;
using Household.Budget.Infra.Data.Context;

namespace Household.Budget.Infra.Data
{
    public class MonthlyBudgetData : Data<MonthlyBudget>, IMonthlyBudgetData
    {
        public MonthlyBudgetData(IMongoDbContext<MonthlyBudget> context)
            : base(context) { }

        public async Task<bool> ExistsAsync(string userId, int year, Month month, CancellationToken cancellationToken) =>
            (await GetOneAsync(x => x.UserId == userId && x.Year == year && x.Month == month, cancellationToken)) is { };

        public async Task<MonthlyBudget> GetOneAsync(string userId, int year, Month month, CancellationToken cancellationToken) =>
            await GetOneAsync(x => x.UserId == userId && x.Year == year && x.Month == month, cancellationToken);
    }
}