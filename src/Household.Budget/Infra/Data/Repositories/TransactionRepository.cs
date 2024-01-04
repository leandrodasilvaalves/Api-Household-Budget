using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Models;

namespace Household.Budget.Infra.Data.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(IRavenDbContext context) 
            : base(context) { }
    }
}