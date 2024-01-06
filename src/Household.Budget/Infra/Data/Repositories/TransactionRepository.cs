using System.Linq.Expressions;

using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Enums;
using Household.Budget.Contracts.Models;

namespace Household.Budget.Infra.Data.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(IRavenDbContext context)
            : base(context) { }

        public Task<PagedListResult<Transaction>> GetAllAsync(int year, int month, int pageSize, int pageNumber,
                                                           string userId, CancellationToken cancellationToken = default)
        {
            Expression<Func<Transaction, bool>> predicate = x =>
                x.Status == ModelStatus.ACTIVE &&
               (x.Owner == ModelOwner.SYSTEM || x.UserId == userId) &&
                x.TransactionDate.Year == year && x.TransactionDate.Month == month;

            return GetAllAsync(pageSize, pageNumber, userId, predicate, cancellationToken);
        }
    }
}