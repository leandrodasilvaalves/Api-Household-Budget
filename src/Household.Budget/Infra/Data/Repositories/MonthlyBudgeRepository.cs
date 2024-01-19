using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Models;
using Household.Budget.Infra.Data.Context;

namespace Household.Budget.Infra.Data.Repositories
{
    public class MonthlyBudgeRepository : Repository<MonthlyBudget>, IMonthlyBudgeRepository
    {
        public MonthlyBudgeRepository(IMongoDbContext<MonthlyBudget> context)
            : base(context) { }
    }
}