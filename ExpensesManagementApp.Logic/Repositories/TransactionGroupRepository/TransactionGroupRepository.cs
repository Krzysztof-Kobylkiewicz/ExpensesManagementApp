using Core.Repositories;
using ExpensesManagementApp.Database;
using ExpensesManagementApp.Database.DbModels;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace ExpensesManagementApp.Logic.Repositories.TransactionGroupRepository
{
    public class TransactionGroupRepository : Repository<ApplicationDbContext, TransactionGroup, Models.Transaction.TransactionGroup, Guid> ,ITransactionGroupRepository
    {
        public TransactionGroupRepository(ApplicationDbContext dbContext, ILogger<TransactionGroup> logger) : base(dbContext, logger) { }

        public async Task<IEnumerable<Models.Transaction.TransactionGroup?>> GetTransactionGroupsAsync(IEnumerable<Guid?> ids, IDbContextTransaction? parentDbContextTransaction = null)
        {
            using var _dbTransaction = parentDbContextTransaction == null ? _dbContext.Database.BeginTransaction() : parentDbContextTransaction;
            try
            {
                return await GetEntitiesDTOAsync<TransactionGroup, Models.Transaction.TransactionGroup, Guid>(transaction => transaction.Where(t => ids.Contains(t.Id)));
            }
            catch (Exception ex)
            {
                _logger.LogError("[{0D}] Error while attempt to get transaction groups: {1M}", DateTime.Now, ex.Message);
                await _dbTransaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Models.Transaction.TransactionGroup> AddAsync(Models.Transaction.TransactionGroup transactionGroup) => await AddEntityDTOAsync<TransactionGroup, Models.Transaction.TransactionGroup, Guid>(transactionGroup);

        public async Task<bool> DeleteTransactionGroupAsync(Guid id) => await DeleteEntityAsync<TransactionGroup, Models.Transaction.TransactionGroup, Guid>(id);
    }
}
