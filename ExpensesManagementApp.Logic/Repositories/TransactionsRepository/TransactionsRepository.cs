using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System.Data;
using ExpensesManagementApp.Database;
using ExpensesManagementApp.Database.DbModels;
using Core.Repositories;
using ExpensesManagementApp.Database.Filters;

namespace ExpensesManagementApp.Logic.Repositories.TransactionsRepository
{
    public class TransactionsRepository : Repository<ApplicationDbContext, Transaction, Models.Transaction.Transaction, Guid>, ITransactionsRepository
    {
        private readonly TransactionGroupRepository.ITransactionGroupRepository transactionGroupRepository;

        public TransactionsRepository(ApplicationDbContext dbContext, ILogger<Transaction> logger, TransactionGroupRepository.ITransactionGroupRepository transactionGroupRepository) : base(dbContext, logger) 
        {
            this.transactionGroupRepository = transactionGroupRepository;
        }

        public async Task<Models.Transaction.Transaction?> GetTransactionAsync(Guid id) => await GetEntityDTOAsync<Transaction, Models.Transaction.Transaction, Guid>(id);

        public async Task<IEnumerable<Models.Transaction.Transaction?>> GetAllTransactionsAsync() => await GetEntitiesDTOAsync<Transaction, Models.Transaction.Transaction, Guid>(t => t);

        public async Task<IEnumerable<Models.Transaction.Transaction?>> GetSpecificTransactionsAsync(DbTransactionFilter filter) => await GetFilteredEntiteiesDTOAsync<Transaction, Models.Transaction.Transaction, Guid, DbTransactionFilter>(filter, t => t);

        public async Task AddTransactionsAsync(IEnumerable<Models.Transaction.Transaction> transactions, IDbContextTransaction? parentDbContextTransaction = null)
        {
            IDbContextTransaction dbContextTransaction = parentDbContextTransaction == null ? _dbContext.Database.BeginTransaction() : parentDbContextTransaction;
            try
            {
                transactions = await ValidateTransactionsAsync(transactions);

                foreach (var transaction in transactions)
                {
                    await AddEntityDTOAsync<Transaction, Models.Transaction.Transaction, Guid>(transaction);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("[{0D}] An error occured while adding transactions to database: {1M}", DateTime.Now, ex.Message);
                await dbContextTransaction.GetDbTransaction().RollbackAsync();
            }
        }

        public async Task<IEnumerable<Models.Transaction.Transaction>> ValidateTransactionsAsync(IEnumerable<Models.Transaction.Transaction> transactions)
        {
            DateOnly? earliestDate = transactions.MinBy(e => e.OperationDate)?.OperationDate;
            DateOnly? latestDate = transactions.MaxBy(e => e.OperationDate)?.OperationDate;

            var areTransactionsFromGivenPeriodPresent = _dbContext.Transactions.Any(expense => expense.OperationDate >= earliestDate && expense.OperationDate <= latestDate);

            if (!areTransactionsFromGivenPeriodPresent)
            {
                return transactions;
            }
            else
            {
                var transactionsFromGivenPeriod = await GetEntitiesDTOAsync<Transaction, Models.Transaction.Transaction, Guid>(transaction => transaction.Where(t => t.OperationDate >= earliestDate && t.OperationDate <= latestDate));

                //todo
                return transactions.Where(e => transactionsFromGivenPeriod.All(ep => ep != null && !ep.CompareTransactionsWithTheSameDate(e)));
            }
        }

        public async Task<bool> DeleteTransactionsAsync(Guid id) => await DeleteEntityAsync<Transaction, Models.Transaction.Transaction, Guid>(id);

        public async Task<Models.Transaction.TransactionGroup> AssignTransactionsToGroup(Models.Transaction.TransactionGroup transactionGroup)
        {
            using var _dbTransaction = _dbContext.Database.BeginTransaction();
            try
            {
                var ids = transactionGroup.Transactions.Select(t => t.Id);

                var transactionsToUpdate = await GetEntitiesDTOAsync<Transaction, Models.Transaction.Transaction, Guid>(transaction => transaction.Where(t => ids.Contains(t.Id)));

                transactionGroup.Round();
                transactionGroup.Count();

                var latestTransactionInGroup = transactionGroup.Transactions.MaxBy(t => t.OperationDate);

                transactionGroup.Representant = new Models.Transaction.TransactionRepresentant
                {
                    Id = Guid.NewGuid(),
                    AccountingDate = latestTransactionInGroup?.AccountingDate ??
                        throw new ArgumentNullException(nameof(transactionGroup), "AccountingDate cannot be null."),
                    OperationDate = latestTransactionInGroup?.OperationDate ?? 
                        throw new ArgumentNullException(nameof(transactionGroup), "OperationDate cannot be null."),
                    Amount = transactionGroup.TransactionGroupSum,
                    OperationTitle = transactionGroup.TransactionGroupName,
                    TransactionGroupId = transactionGroup.TransactionGroupId,
                };

                transactionGroup.Transactions = transactionsToUpdate;

                var addedTransactionGroup = await transactionGroupRepository.AddAsync(transactionGroup);

                await SaveChangesAsync();
                await _dbTransaction.CommitAsync();

                return addedTransactionGroup;
            }
            catch (Exception ex)
            {
                _logger.LogError("[{0D}] Error while attempt to assign transactions to group: {1M}", DateTime.Now, ex.Message);
                await _dbTransaction.RollbackAsync();
                throw;
            }
        }
    }
}
